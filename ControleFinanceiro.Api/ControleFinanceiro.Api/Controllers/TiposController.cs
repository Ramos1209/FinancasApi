using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.Api.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrador")]
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
