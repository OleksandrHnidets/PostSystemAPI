using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.DAL.Repository;
using PostSystemAPI.Domain.DTO.ReadDTO;
using PostSystemAPI.Domain.Services.Interfaces;
using PostSystemAPI.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostSystemAPI.WebApi.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController: ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }


        [HttpGet("{id}",Name="GetCityById")]
        public async Task<ActionResult<CityView>> GetCityById(int id)
        {
            var city = await _cityService.GetCityById(id);
            var cityView = _mapper.Map<CityView>(city);
            return Ok(cityView);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityView>>> GetAllCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            var citiesView = _mapper.Map<IEnumerable<CityView>>(cities);
            return Ok(citiesView);
        }

        [HttpPost]
        public async Task<ActionResult<CityView>> CreateCity(CityDTO newCity)
        {
            var city = _mapper.Map<City>(newCity);
            await _cityService.CreateCityAsync(city);
            var cityView = _mapper.Map<CityView>(city);
            return CreatedAtRoute(nameof(GetCityById), new { Id = cityView.Id }, cityView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CityView>> UpdateCity(int id, CityDTO updatedCity)
        {
            var city = _mapper.Map<City>(updatedCity);
            await _cityService.UpdateCityAsync(id, city);
            var cityView = _mapper.Map<CityView>(city);
            return Ok(cityView);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            await _cityService.DeleteCityAsync(id);
            return Ok();
        }
    }
}
