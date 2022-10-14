using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Api.ViewModels;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ICartaoRepository _cartaoRepositorio;
        private readonly IGanhosRepository _ganhosRepositorio;
        private readonly IDespesasRepository _despesaRepositorio;
        private readonly IMesRepository _mesRepositorio;
        private readonly IGraficoRepository _graficoRepositorio;

        public DashboardController(ICartaoRepository cartaoRepositorio, IGanhosRepository ganhosRepositorio, IDespesasRepository despesaRepositorio, IMesRepository mesRepositorio, IGraficoRepository graficoRepositorio)
        {
            _cartaoRepositorio = cartaoRepositorio;
            _ganhosRepositorio = ganhosRepositorio;
            _despesaRepositorio = despesaRepositorio;
            _mesRepositorio = mesRepositorio;
            _graficoRepositorio = graficoRepositorio;
        }

        [HttpGet("PegarDadosCardsDashboard/{usuarioId}")]
        public async Task<ActionResult<DadosCardsDashboardViewModel>> PegarDadosCardsDashboard(string usuarioId)
        {
            int qtdCartoes = await _cartaoRepositorio.PegarQuantidadeCartoesPeloUsuarioId(usuarioId);
            double ganhoTotal = Math.Round(await _ganhosRepositorio.PegarGanhoTotalPeloUsuarioId(usuarioId), 2);
            double despesaTotal = Math.Round(await _despesaRepositorio.PegarDespesaTotalPorUsuarioId(usuarioId), 2);
            double saldo = Math.Round(ganhoTotal - despesaTotal, 2);

            DadosCardsDashboardViewModel model = new DadosCardsDashboardViewModel
            {
                QtdCartoes = qtdCartoes,
                GanhoTotal = ganhoTotal,
                DespesaTotal = despesaTotal,
                Saldo = saldo
            };

            return model;
        }

        [HttpGet("PegarDadosAnuaisPeloUsuarioId/{usuarioId}/{ano}")]
        public object PegarDadosAnuaisPeloUsuarioId(string usuarioId, int ano)
        {
            return new
            {
                ganhos = _graficoRepositorio.PegarGanhosAnuaisPeloUsuarioId(usuarioId, ano),
                despesas = _graficoRepositorio.PegarDespesasAnuaisPeloUsuarioId(usuarioId, ano),
                meses = _mesRepositorio.GetAll()
            };
        }

    }
}
