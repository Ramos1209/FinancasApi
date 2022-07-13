using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly ITipoRepository _tipoRepository;

        public TiposController(ITipoRepository tipoRepository)
        {
            _tipoRepository = tipoRepository;
          
        }

        [HttpGet]
        public async Task<IEnumerable<Tipo>> GetCategoria()
        {
            return await _tipoRepository.GetAll().ToListAsync();
        }

    }
}
