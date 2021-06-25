using API.CRUD.Data;
using API.CRUD.Domain.Models;
using API.CRUD.Models.InputModels;
using API.CRUD.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private AppDbContext Context = null;
        private IMapper Mapper = null;

        public FornecedoresController(AppDbContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Get()
        {
            var fornecedores = await this.Context.Fornecedores.ToListAsync();
            if (fornecedores.Count == 0)
                return NotFound("Nenhum registro encontrado!");

            var fornecedoresViewModel = fornecedores.Select(x => this.Mapper.Map<FornecedorViewModel>(x));
            return Ok(fornecedores);
        }

        [HttpGet("/{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var fornecedor = await this.Context.Fornecedores.Where(x => x.Id == id && x.Ativo == true).SingleOrDefaultAsync();
            if (fornecedor == null)
                return NotFound("Nenhum registro encontrado!");

            var fornecedorViewModel = this.Mapper.Map<FornecedorViewModel>(fornecedor);
            return Ok(fornecedor);
        }

        [HttpPost("/")]
        public async Task<IActionResult> Post([FromBody] CreateFornecedorInputModel fornecedorInputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Oops, algo deu errado! verifique as informações e tente novamente!");

                var fornecedor = this.Mapper.Map<Fornecedor>(fornecedorInputModel);
                await this.Context.AddAsync(fornecedor);
                this.Context.SaveChanges();

                return Created($"api/fornecedor/{fornecedor.Id}", fornecedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("/{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateFornecedorInputModel fornecedorInputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Oops, algo deu errado! verifique as informações e tente novamente!");

                var fornecedorUpdate = this.Mapper.Map<Fornecedor>(fornecedorInputModel);
                var fornecedor = await this.Context.Fornecedores.Where(x => x.Id == id).SingleOrDefaultAsync() ;
                if (fornecedor == null)
                    return NotFound("Nenhum registro encontrado!");

                fornecedor.Nome = fornecedorUpdate.Nome;
                fornecedor.Documento = fornecedorUpdate.Documento;
                fornecedor.Ativo = fornecedorUpdate.Ativo;
                fornecedor.TipoFornecedor = fornecedorUpdate.TipoFornecedor;

                this.Context.Update(fornecedor);
                this.Context.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var fornecedor = await this.Context.Fornecedores.Where(x => x.Id == id).SingleOrDefaultAsync();
                if (fornecedor == null)
                    return NotFound("Nenhum registro encontrado!");

                this.Context.Remove(fornecedor);
                this.Context.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
