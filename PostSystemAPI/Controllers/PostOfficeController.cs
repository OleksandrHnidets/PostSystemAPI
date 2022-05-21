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
    [Route("api/postoffice")]
    [ApiController]
    public class PostOfficeController: ControllerBase
    {
        private readonly IPostOfficeService _postOfficeService;
        private readonly IMapper _mapper;

        public PostOfficeController(IPostOfficeService cityService, IMapper mapper)
        {
            _postOfficeService = cityService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetPostOfficeById")]
        public async Task<ActionResult<PostOfficeView>> GetPostOfficeById(string id)
        {
            var postOffice = await _postOfficeService.GetPostOfficeById(id);
            var postOfficeView = _mapper.Map<PostOfficeView>(postOffice);
            return Ok(postOfficeView);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostOfficeView>>> GetAllPostOffices()
        {
            var postOffices = await _postOfficeService.GetAllPostOffices();
            var postOfficeView = _mapper.Map<IEnumerable<PostOfficeView>>(postOffices);
            return Ok(postOfficeView);
        }

        [HttpPost]
        public async Task<ActionResult<PostOfficeView>> CreatePostOffice(PostOfficeDTO newpostOffice)
        {
            var postOffice = _mapper.Map<PostOffice>(newpostOffice);
            await _postOfficeService.CreatePostOfficeAsync(postOffice);
            var postOfficeView = _mapper.Map<PostOfficeView>(postOffice);
            return CreatedAtRoute(nameof(GetPostOfficeById), new { Id = postOfficeView.Id }, postOfficeView);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostOffice(string id)
        {
            await _postOfficeService.DeletePostOfficeAsync(id);
            return Ok();
        }



    }
}
