using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.DAL.Repository;
using PostSystemAPI.Domain.DTO.ReadDTO;
using PostSystemAPI.Domain.Services.Interfaces;
using PostSystemAPI.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using MediatR;
using PostSystemAPI.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.Domain.Commands;
using PostSystemAPI.Domain.Queries;

namespace PostSystemAPI.WebApi.Controllers
{
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController: ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IMapper _mapper;
        private readonly IDeliveryRepo _repo;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public DeliveryController(IDeliveryService deliveryService, IMapper mapper, IDeliveryRepo repo, UserManager<User> userManager, ITransactionHistoryRepository transactionHistoryRepository, IPostOfficeRepo postOfficeRepo, IMediator mediator)
        {
            _deliveryService = deliveryService;
            _mapper = mapper;
            _repo = repo;
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet("{id}", Name ="GetDeliveryById")]
        public async Task<ActionResult<DeliveryView>> GetDeliveryById(string id)
        {
            var delivery = await _deliveryService.GetDeliveryByIdAsync(id);
            var deliveryView = _mapper.Map<DeliveryView>(delivery);
            return Ok(deliveryView);
        }

        [HttpGet("get-sended")]
        public async Task<ActionResult<IEnumerable<ReadDeliveryView>>> GetSendedDeliveries(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser == null)
                return BadRequest("User is not found");

            var deliveries = _repo.Entities
                .Where(d => d.SentUserId == id || d.ReceivedUserId == id)
                .Include(i => i.SentUser)
                .ToList();
            foreach(var delivery in deliveries)
            {
                if (delivery.DeliveryDate == DateTime.Now)
                    delivery.DeliveryStatus = DeliveryStatus.WaitingToAccept;
            }
            List<ReadDeliveryView> deliveryViews = new List<ReadDeliveryView>();
            foreach (var delivery in deliveries)
            {
                deliveryViews.Add(_mapper.Map<ReadDeliveryView>(delivery));
            }

            return Ok(deliveryViews);
        }

        [HttpGet("get-received")]
        public async Task<ActionResult<IEnumerable<ReadDeliveryView>>> GetReceivedDeliveries(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser == null)
                return BadRequest("User is not found");

            var deliveries = _repo.Entities
                .Where(d => d.SentUserId == id || d.ReceivedUserId == id)
                .Where(d => d.DeliveryStatus == DeliveryStatus.Received)
                .Include(i => i.SentUser)
                .ToList();
            List<ReadDeliveryView> deliveryViews = new List<ReadDeliveryView>();
            foreach (var delivery in deliveries)
            {
                deliveryViews.Add(_mapper.Map<ReadDeliveryView>(delivery));
            }

            return Ok(deliveryViews);
        }

        [HttpGet("get-declined")]
        public async Task<ActionResult<IEnumerable<ReadDeliveryView>>> GetDeclinedDeliveries(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser == null)
                return BadRequest("User is not found");

            var deliveries = _repo.Entities
                .Where(d => d.SentUserId == id || d.ReceivedUserId == id)
                .Where(d => d.DeliveryStatus == DeliveryStatus.Declined)
                .Include(i => i.SentUser)
                .ToList();
            List<ReadDeliveryView> deliveryViews = new List<ReadDeliveryView>();
            foreach (var delivery in deliveries)
            {
                deliveryViews.Add(_mapper.Map<ReadDeliveryView>(delivery));
            }

            return Ok(deliveryViews);
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateDelivery(DeliveryView newDeliveryView)
        {
            var newDelivery = _mapper.Map<Delivery>(newDeliveryView);
            newDelivery.Id = Guid.NewGuid();
            newDelivery.DeliveryStatus = DeliveryStatus.Delivering;
            newDelivery.DeliveryDate = DateTime.Now.AddSeconds(15);
            if(newDelivery == null)
            {
                return BadRequest("Could not create delivery");
            }

            await _repo.AddAsync(newDelivery);
            await _repo.SaveChangesAsync();
            return Ok("Delivery succesfuly created");
        }

        [HttpGet("is-user-receiver")]
        public async Task<ActionResult<bool>> IsUserReceiver(string userId, string deliveryId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var delivery = _repo.Entities.FirstOrDefault(d => d.Id == Guid.Parse(deliveryId));
            if (user == null)
                return BadRequest("User is not found");

            if (delivery.ReceivedUserId == userId)
            {
                return Ok(true);
            }
            else
                return Ok(false);

        }

        [HttpPost("decline-delivery")]
        public async Task<ActionResult<string>> DeclineDelivery(string deliveryId, string userId)
        {
            try
            {
                var delivery = _repo.Entities.FirstOrDefault(r => r.Id == Guid.Parse(deliveryId));
                await _userManager.FindByIdAsync(userId);

                if (delivery == null)
                    return NotFound();

                delivery.DeliveryStatus = DeliveryStatus.Declined;

                await _repo.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
            return Ok("Delivery declined succesfully");
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryView>> UpdateDelivery(string id, DeliveryDTO updatedDelivery)
        {
            var delivery = _mapper.Map<Delivery>(updatedDelivery);
            await _deliveryService.UpdateDeliveryAsync(id, delivery);
            var deliveryView = _mapper.Map<DeliveryView>(delivery);
            return Ok(deliveryView);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteDelivery(string id)
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            return Ok();
        }


        [HttpGet("deliveries")]
        public async Task<ActionResult> GetDeliveries()
        {
            var result = await _mediator.Send(new GetDeliveriesQuery());
            return Ok(result);
        }

        [HttpPost("create-delivery")]
        public async Task<ActionResult<bool>> AddDelivery([FromBody] CreateDeliveryViemModel createDelivery)
        {
            var result = await _mediator.Send(new CreateDeliveryCommand(createDelivery));
            return new ActionResult<bool>(result);
        }

        [HttpGet("delivery-edit/{id}")]
        public async Task<ActionResult> GetDeliveryToUpdate(string id)
        {
            var result = await _mediator.Send(new GetDeliveryToUpdate(id));
            return Ok(result);
        }

        [HttpPost("delivery-update")]
        public async Task<ActionResult<bool>> UpdateDelivery([FromBody] UpdateDeliveryViewModel deliveryModel)
        {
            var result = await _mediator.Send(new UpdateDeliveryCommand(deliveryModel));
            return new ActionResult<bool>(result);
        }

        [HttpGet("available-driver-deliveries/{driverId}")]
        public async Task<ActionResult> GetAvaliableDeliveries(string driverId)
        {
            var result = await _mediator.Send(new GetAvaliableDeliveriesForDriver(driverId));
            return Ok(result);
        }
    }
}
