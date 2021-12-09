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
    [Route("api/sender")]
    [ApiController]
    public class SenderController: ControllerBase
    {
        private readonly ISenderService _senderService;
        private readonly IMapper _mapper;

        public SenderController(ISenderService senderService, IMapper mapper)
        {
            _senderService = senderService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetSenderById")]
        public async Task<ActionResult<SenderView>> GetSenderById(int id)
        {
            var sender = await _senderService.GetSenderByIdAsync(id);
            var senderView = _mapper.Map<SenderView>(sender);
            return Ok(senderView);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SenderView>>> GetAllSenders()
        {
            var senders = await _senderService.GetAllSendersAsync();
            var senderView = _mapper.Map<IEnumerable<SenderView>>(senders);
            return Ok(senderView);
        }

        [HttpPost]
        public async Task<ActionResult<SenderView>> CreateSender(SenderDTO newSender)
        {
            var sender = _mapper.Map<Sender>(newSender);
            await _senderService.CreateSenderAsync(sender);
            var senderView = _mapper.Map<SenderView>(sender);
            return CreatedAtRoute(nameof(GetSenderById), new { Id = senderView.Id }, senderView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SenderView>> UpdateSender(int id, SenderDTO updatedSender)
        {
            var sender = _mapper.Map<Sender>(updatedSender);
            await _senderService.UpdateSenderAsync(id, sender);
            var senderView = _mapper.Map<SenderView>(sender);
            return Ok(senderView);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSender(int id)
        {
            await _senderService.DeleteSenderAsync(id);
            return Ok();
        }
    }
}
