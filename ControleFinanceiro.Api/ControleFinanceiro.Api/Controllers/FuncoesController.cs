using ControleFinanceiro.Api.ViewModels;
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
    public class FuncoesController : ControllerBase
    {
        private readonly IFuncaoRepository _funcaoRepository;

        public FuncoesController( IFuncaoRepository funcaoRepository)
        {
            _funcaoRepository = funcaoRepository;
           
        }

        // GET: api/Funcoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcao>>> GetFuncoes()
        {
            return await _funcaoRepository.GetAll().ToListAsync();
        }

        // GET: api/Funcoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcao>> GetFuncao(string id)
        {
            var funcao = await _funcaoRepository.GetById(id);

            if (funcao == null)
            {
                return NotFound();
            }

            return funcao;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncao(string id, FuncaoViewModel funcao)
        {
            if (id != funcao.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Funcao f = new Funcao
                {
                    Id = funcao.Id,
                    Name = funcao.Name,
                    Descricao = funcao.Descricao
                };

                await _funcaoRepository.AtualizarFuncao(f);

                return Ok(new
                {
                    mensagem = $"função {funcao.Name} atualizada com sucesso ."
                });
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<ActionResult<Funcao>> PostFuncao(FuncaoViewModel funcao)
        {
            if (ModelState.IsValid)
            {
                Funcao f = new Funcao
                {
                  
                    Name = funcao.Name,
                    Descricao = funcao.Descricao
                };

                await _funcaoRepository.AdicionarFuncao(f);

                return Ok(new
                {
                    mensagem = $"função {funcao.Name} adicionada  com sucesso ."
                });
            }

            return BadRequest(ModelState);
        }

      
        [HttpDelete("{id}")]
        public async Task<ActionResult<Funcao>> DeleteFuncao(string id)
        {
            var funcao = await _funcaoRepository.GetById(id);
            if (funcao == null)
            {
                return NotFound();
            }

            await _funcaoRepository.Excluir(funcao);
            return Ok(new
            {
                mensagem = $"função {funcao.Name} excluida  com sucesso ."
            });

        }

        [HttpGet("FiltrarFuncao/{nome}")]
        public async Task<ActionResult<IEnumerable<Funcao>>> FiltrarFuncao(string nome)
        {
            return await _funcaoRepository.FiltrarCategorias(nome).ToListAsync();
        }


    }
}
