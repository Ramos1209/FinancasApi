using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanhosController : ControllerBase
    {
        private readonly IGanhosRepository _ganhosRepository;

        public GanhosController(IGanhosRepository ganhosRepository)
        {
            _ganhosRepository = ganhosRepository;
        }

        [HttpGet("PegarGanhosUsuarioId/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Ganho>>> PegarGanhosUsuarioId(string usuarioId)
        {
            return await _ganhosRepository.PegarGanhosUsuarioId(usuarioId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ganho>> GetGanho(int id)
        {
            Ganho ganho = await _ganhosRepository.GetById(id);
            if (ganho == null)
            {
                return NotFound();
            }

            return ganho;
        }

        [HttpPut("{ganhoId}")]
        public async Task<ActionResult<Ganho>> PutGanho(int ganhoId, Ganho ganho)
        {
            if (ganhoId != ganho.GanhoId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _ganhosRepository.Update(ganho);
                return Ok(new
                {
                    mensagem = $"Ganho {ganho.Mes} atualizado com sucesso"
                });
            }

            return BadRequest(ganho);
        }

        [HttpPost]
        public async Task<ActionResult<Ganho>> PostGanho(Ganho ganho)
        {
            if (!ModelState.IsValid)
                return BadRequest(ganho);

            await _ganhosRepository.Insert(ganho);
            return Ok(new
            {
                mensagem = $"Ganho no valor {ganho.Valor} adicionado com sucesso."
            });
        }

        [HttpDelete("{ganhoId}")]
        public async Task<ActionResult<Ganho>> Delete(int ganhoId)
        {
            Ganho ganho = await _ganhosRepository.GetById(ganhoId);
            if (ganho == null)
                return NotFound();


            await _ganhosRepository.Excluir(ganho.GanhoId);
            return Ok(new
            {
                mensagem = $"Ganho no valor de {ganho.Valor}  excluido com sucesso"
            });

                
        }
    }
}
