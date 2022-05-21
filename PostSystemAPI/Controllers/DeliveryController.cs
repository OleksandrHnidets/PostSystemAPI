using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.DTO.ReadDTO;
using PostSystemAPI.Domain.Services.Interfaces;
using PostSystemAPI.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostSystemAPI.WebApi.Controllers
{
    [Route("api/delivery")]
    [ApiController]
    public class DeliveryController: ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IMapper _mapper;

        public DeliveryController(IDeliveryService deliveryService, IMapper mapper)
        {
            _deliveryService = deliveryService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name ="GetDeliveryById")]
        public async Task<ActionResult<DeliveryView>> GetDeliveryById(string id)
        {
            var delivery = await _deliveryService.GetDeliveryByIdAsync(id);
            var deliveryView = _mapper.Map<DeliveryView>(delivery);
            return Ok(deliveryView);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryView>>> GetAllDeliveries()
        {
            var deliveries = await _deliveryService.GetAllDeliveries();
            var deliveryView = _mapper.Map<IEnumerable<DeliveryView>>(deliveries);
            return Ok(deliveryView);
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryView>> CreateDelivery(DeliveryDTO newDelivery)
        {
            var delivery = _mapper.Map<Delivery>(newDelivery);
            await _deliveryService.CreateDeliveryAsync(delivery);
            var deliveryView = _mapper.Map<DeliveryView>(delivery);
            return CreatedAtRoute(nameof(GetDeliveryById), new { Id = deliveryView.Id }, deliveryView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryView>> UpdateDelivery(string id, DeliveryDTO updatedDelivery)
        {
            var delivery = _mapper.Map<Delivery>(updatedDelivery);
            await _deliveryService.UpdateDeliveryAsync(id, delivery);
            var deliveryView = _mapper.Map<DeliveryView>(delivery);
            return Ok(deliveryView);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDelivery(string id)
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            return Ok();
        }
    }
}
