using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartaoController : ControllerBase
    {

        private readonly ICartaoRepository _cartaoRepository;
        private readonly IDespesasRepository _despesasRepository;

        public CartaoController(ICartaoRepository cartaoRepository, IDespesasRepository despesasRepository)
        {
            _cartaoRepository = cartaoRepository;
            _despesasRepository = despesasRepository;
        }

        [HttpGet("PegarCartoesPeloUsuarioId/{usuarioId}")]
        public async Task<IEnumerable<Cartao>> PegarCartoesPeloUsuarioId(string usuarioId)
        {
            usuarioId =  usuarioId.Replace("'", " ");
            return await _cartaoRepository.PegarCartoesPeloUsuarioId(usuarioId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cartao>> PegarCartao(int id)
        {
            Cartao cartao = await _cartaoRepository.GetById(id);
            if (cartao == null)
                return NotFound();

            return cartao;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCartao(int id, Cartao cartao)
        {
            if (id != cartao.CartaoId)
            {
                return BadRequest("Cartoes diferentes. Não foi possivel atualizar!!");
            }

            if (ModelState.IsValid)
            {
                await _cartaoRepository.Update(cartao);
                return Ok(new
                {
                    mensagem = $"Cartao numero  {cartao.Numero} atualizado com sucesso!!!"
                });
            }

            return BadRequest(cartao);
        }

        [HttpPost]
        public async Task<ActionResult> PostCartao(Cartao cartao)
        {
            if (ModelState.IsValid)
            {
                await _cartaoRepository.Insert(cartao);
                return Ok(new
                {
                    mensagem = $"Cartao numero  {cartao.Numero}  criado com sucesso!!!"
                });
            }

            return BadRequest(cartao);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCartao(int id)
        {
            Cartao cartao = await _cartaoRepository.GetById(id);

            if (cartao == null)
                return NotFound();

            IEnumerable<Despesa> despesas = await _despesasRepository.PegarDespesasPeloCartaoId(cartao.CartaoId);
            _despesasRepository.ExcluirDespesas(despesas);

            await _cartaoRepository.Excluir(cartao);


            return Ok(new
            {
                mensagem = $"Cartão número {cartao.Numero} excluído com sucesso"
            });
        }

        [HttpGet("FiltrarCartoes/{numeroCartao}")]
        public async Task<IEnumerable<Cartao>> FiltrarCartoes(string numeroCartao)
        {
            return await _cartaoRepository.FiltrarCartoes(numeroCartao).ToListAsync();
        }
    }
}
