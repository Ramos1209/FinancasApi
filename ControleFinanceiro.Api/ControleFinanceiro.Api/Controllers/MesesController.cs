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
    public class MesesController : ControllerBase
    {
        private readonly IMesRepository _mesRepository;
        public MesesController(IMesRepository mesRepository)
        {
            _mesRepository = mesRepository;
        }

        public async Task<ActionResult<IEnumerable<Mes>>> PegarTodos()
        {
            return await _mesRepository.GetAll().ToListAsync();
        }
    }
}
