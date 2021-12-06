﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.DAL.Repository;
using PostSystemAPI.Domain.DTO.ReadDTO;
using System.Collections.Generic;

namespace PostSystemAPI.WebApi.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICityRepo _repository;

        public CityController(ICityRepo repository, IMapper mapper)
        {
            _mapper = mapper; 
            _repository = repository;
        }


        [HttpGet("{id}")]
        public ActionResult<CityDTO> GetCityById(int id)
        {
            var query = _repository.GetByIdAsync(id);
            return Ok(_mapper.Map<CityDTO>(query));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDTO>> GetAllCities()
        {
            var query = _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CityDTO>>(query));
        }
    }
}
