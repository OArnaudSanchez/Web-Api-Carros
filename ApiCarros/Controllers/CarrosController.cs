using ApiCarros.DTos;
using AutoMapper;
using CarrosData.Models;
using CarrosServices.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarrosController : ControllerBase
    {
        public ICarrosRepository _carrosRepository { get; }
        public IMapper _mapper { get; }

        public CarrosController(ICarrosRepository carrosRepository, IMapper mapper)
        {
            _carrosRepository = carrosRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CarroDTO>> GET()
        {
            //Obtenemos los carros
            var cars = await _carrosRepository.GetCars();

            if (cars == null)
            {
                return BadRequest();
            }

            var carrosDTO = _mapper.Map<List<CarroDTO>>(cars);

            return Ok(carrosDTO);
        }

        [HttpGet("{id}", Name = "GetCarro")]
        public async Task<ActionResult<CarroDTO>> GETBYID(int id)
        {
            var car = await _carrosRepository.GetCarByID(id);

            if (car == null)
            {
                return NotFound();
            }


            var carroDTO = _mapper.Map<CarroDTO>(car);

            return Ok(carroDTO);
        }

        [HttpPost]
        public async Task<ActionResult> POST([FromBody] Carro carro)
        {
            var carroNew = await _carrosRepository.PostCarro(carro);

            if (carroNew == null || !ModelState.IsValid)
            {
                return BadRequest();
            }


            return new RedirectToRouteResult("GetCarro", new { ID = carro.id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PUT(int id, [FromBody] Carro carro)
        {
            if (id != carro.id)
            {
                return NotFound();
            }

            var modificacionCarro = await _carrosRepository.PutCarro(carro);

            if (modificacionCarro == null)
            {
                return BadRequest();
            }

            return Ok(carro);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PATCH(int id,[FromBody] JsonPatchDocument<Carro> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }

            var carroPatch = await _carrosRepository.PatchCarro(id);

            if (carroPatch == null)
            {
                return NotFound();
            }

            jsonPatchDocument.ApplyTo(carroPatch, ModelState);

            var modelIsValid = TryValidateModel(carroPatch);

            if (!modelIsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DELETE(int id)
        {
            var carro = await _carrosRepository.DelCarro(id);

            if (!carro)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok();

        }
        
    }
}
