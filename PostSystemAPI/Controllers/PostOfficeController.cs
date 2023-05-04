using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.DAL.Repository;
using PostSystemAPI.Domain.DTO.ReadDTO;
using PostSystemAPI.Domain.Services.Interfaces;
using PostSystemAPI.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostSystemAPI.WebApi.Controllers
{
    [Route("api/post-office")]
    [ApiController]
    public class PostOfficeController: ControllerBase
    {
        private readonly IPostOfficeService _postOfficeService;
        private readonly IMapper _mapper;
        private readonly IPostOfficeRepo _repo;
        private readonly ICityRepo _cityRepo;
        private readonly PostSystemContext _context;

        public PostOfficeController(IPostOfficeService cityService, IMapper mapper, IPostOfficeRepo repo, PostSystemContext context, ICityRepo cityRepo)
        {
            _postOfficeService = cityService;
            _mapper = mapper;
            _repo = repo;
            _context = context;
            _cityRepo = cityRepo;
        }

        [HttpGet("{id}", Name = "GetPostOfficeById")]
        public async Task<ActionResult<PostOfficeView>> GetPostOfficeById(string id)
        {
            var postOffice = await _postOfficeService.GetPostOfficeById(id);
            var postOfficeView = _mapper.Map<PostOfficeView>(postOffice);
            return Ok(postOfficeView);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostOfficeView>>> GetAllPostOffices()
        {
            var postOffices = _repo.Entities.ToList();
            List<PostOfficeView> postOfficeView = new List<PostOfficeView>();
            foreach (var postOffice in postOffices)
            {
                postOfficeView.Add(_mapper.Map<PostOfficeView>(postOffice));
            }
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

        [HttpGet("all-admin")]
        public async Task<ActionResult<PostOfficeReadView>> GetAllAdminPostOffices()
        {
            var postOffices = _repo.Entities.Include(p=>p.City).Include(P=>P.SentDeliveries).ToList();
            List<PostOfficeReadView> postOfficesView = new List<PostOfficeReadView>();
            foreach (var postOffice in postOffices)
            {
                postOfficesView.Add(_mapper.Map<PostOfficeReadView>(postOffice));
                postOfficesView.Last().CountOfdeliveries = postOffice.SentDeliveries.Count;
            }
            return Ok(postOfficesView);
        }



    }
}
