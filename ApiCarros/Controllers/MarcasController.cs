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
    public class MarcasController : ControllerBase
    {

        public IMarcasRepository _marcasRepository { get; }
        public IMapper _mapper { get; }

        public MarcasController(IMarcasRepository marcasRepository, IMapper mapper)
        {
            _marcasRepository = marcasRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Marca>> GET()
        {
            var marcas = await _marcasRepository.GetMarcas();

            var marcasDTO = _mapper.Map<List<MarcaDTO>>(marcas);

            if (marcasDTO == null)
            {
                return BadRequest();
            }

            return Ok(marcasDTO);
        }

        [HttpGet("{id}", Name ="GetMarca")]
        public async Task<ActionResult<Marca>> GETBYID(int id)
        {
            var marca = await _marcasRepository.GetMarcaByID(id);

            if (marca == null)
            {
                return NotFound();
            }

            var marcaDTO = _mapper.Map<MarcaDTO>(marca);

            return Ok(marcaDTO);
        }
        
        [HttpPost]
        public async Task<ActionResult> POST([FromBody] Marca marca)
        {
            var nuevaMarca = await _marcasRepository.PostMarca(marca);

            if (!ModelState.IsValid || nuevaMarca == null)
            {
                return BadRequest();
            }

            return new RedirectToRouteResult("GetMarca", new {ID = marca.id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PUT(int id, [FromBody] Marca marca)
        {
            if (id != marca.id)
            {
                return NotFound();
            }

            var editMarca = await _marcasRepository.PutMarca(marca);

            if (!ModelState.IsValid || editMarca == null)
            {
                return BadRequest();
            }

            return Ok(marca);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PATCH(int id, [FromBody] JsonPatchDocument<Marca> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }

            var marcaPatch = await _marcasRepository.PatchMarca(id);

            if (marcaPatch == null)
            {
                return NotFound();
            }

            jsonPatchDocument.ApplyTo(marcaPatch, ModelState);

            var modelIsValid = TryValidateModel(marcaPatch);

            if (!modelIsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> PUT(int id)
        {
            var marca = await _marcasRepository.DelMarca(id);

            if (marca == false)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
