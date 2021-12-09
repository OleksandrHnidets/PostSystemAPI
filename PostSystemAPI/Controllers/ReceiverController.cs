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
    [Route("api/receiver")]
    [ApiController]
    public class ReceiverController: ControllerBase
    {
        private readonly IReceiverService _receiverService;
        private readonly IMapper _mapper;

        public ReceiverController(IReceiverService receiverService, IMapper mapper)
        {
            _receiverService = receiverService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetReceiverById")]
        public async Task<ActionResult<ReceiverView>> GetReceiverById(int id)
        {
            var receiver = await _receiverService.GetReceiverByIdAsync(id);
            var receiverView = _mapper.Map<ReceiverView>(receiver);
            return Ok(receiverView);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiverView>>> GetAllReceivers()
        {
            var receivers = await _receiverService.GetAllReceiversAsync();
            var receiverView = _mapper.Map<IEnumerable<ReceiverView>>(receivers);
            return Ok(receiverView);
        }

        [HttpPost]
        public async Task<ActionResult<ReceiverView>> CreateReceiver(ReceiverDTO newReceiver)
        {
            var receiver = _mapper.Map<Receiver>(newReceiver);
            await _receiverService.CreateReceiverAsync(receiver);
            var receiverView = _mapper.Map<ReceiverView>(receiver);
            return CreatedAtRoute(nameof(GetReceiverById), new { Id = receiverView.Id }, receiverView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReceiverView>> UpdateReceiver(int id, ReceiverDTO updatedReceiver)
        {
            var receiver = _mapper.Map<Receiver>(updatedReceiver);
            await _receiverService.UpdateReceiverAsync(id, receiver);
            var receiverView = _mapper.Map<ReceiverView>(receiver);
            return Ok(receiverView);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReceiver(int id)
        {
            await _receiverService.DeleteReceiverAsync(id);
            return Ok();
        }
    }
}
