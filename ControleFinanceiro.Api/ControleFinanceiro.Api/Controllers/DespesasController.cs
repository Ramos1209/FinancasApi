using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesasRepository _despesasRepository;
        public DespesasController(IDespesasRepository despesasRepository)
        {
            _despesasRepository = despesasRepository;
        }
       
        [HttpGet("PegarDespesasPeloUsuarioId/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Despesa>>> PegarDespesasPeloUsuarioId(string usuarioId)
        {
            return await _despesasRepository.PegarDespesasPorUsuarioId(usuarioId).ToListAsync();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>>GetDespesa(int id)
        {
            Despesa despesa = await _despesasRepository.GetById(id);
            if (despesa == null)
                return NotFound();
            

            return despesa;
        }

        // POST api/<DespesasController>
        [HttpPost]
        public async Task<ActionResult<Despesa>>  PostDespesa(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                await _despesasRepository.Insert(despesa);

                return Ok(new
                {
                    mensagem =$"Despesa {despesa.Valor} adicionada com sucesso!"
                });
            }

            return BadRequest(despesa);
        }

        // PUT api/<DespesasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Despesa>> PutDespesa(int id, Despesa despesa)
        {
            if (id != despesa.DespesaId)
            {
                return BadRequest(despesa);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(despesa);
            }

            await _despesasRepository.Update(despesa);
            return Ok(new
            {
                mensagem = $"Despesa no valor {despesa.Valor} atualizada com sucesso!!"
            });
        }

   
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDespesa(int id)
        {
            Despesa despesa = await _despesasRepository.GetById(id);
            if (despesa == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(despesa);
            }

            await _despesasRepository.Excluir(despesa);
            return Ok(new
            {
                mensagem = $"Despesa {despesa.Valor} excluida com sucesso"
            });
        }

        [HttpGet("FiltrarDespesas/{nomeCategoria}")]
        public async Task<IEnumerable<Despesa>> FiltrarDespesas(string nomeCategoria)
        {
            return await _despesasRepository.FiltrarDespesas(nomeCategoria).ToListAsync();
        }
    }
}
