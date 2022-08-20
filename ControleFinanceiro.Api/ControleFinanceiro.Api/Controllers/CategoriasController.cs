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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRpository _categoriaRpository;

        public CategoriasController(ICategoriaRpository categoriaRpository)
        {
            _categoriaRpository = categoriaRpository;

        }

      
        [HttpGet]
        //[Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
          var categoria=  await _categoriaRpository.GetAll().ToListAsync();
          return categoria;

        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _categoriaRpository.GetById(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _categoriaRpository.Update(categoria);

                return Ok(new { mensagem = $"Categoria {categoria.Nome} atualizada com sucesso" });
            }

            return BadRequest(ModelState);

        }

      
        [HttpPost]
        //[Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaRpository.Insert(categoria);
                return Ok(new { mensagem = $"Categoria {categoria.Nome} cadastrada com sucesso" });
            }
            return BadRequest(ModelState);
           
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = await _categoriaRpository.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            await _categoriaRpository.Excluir(id);

            return Ok(new { mensagem = $"Categoria {categoria.Nome} excluida com sucesso" });
        }

        [HttpGet("FiltrarCategoria/{nome}")]
        //[Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoria(string nome)
        {
            return await _categoriaRpository.FiltrarCategorias(nome).ToListAsync();
        }

        [HttpGet("FiltrarCategoriaDespesa")]
        public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoriaDespesa()
        {
            return await _categoriaRpository.PegarCategoriaPeloTipo("Despesa").ToListAsync();
        }
       
        [HttpGet("FiltrarCategoriasGanhos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> FiltrarCategoriasGanhos()
        {
            return await _categoriaRpository.PegarCategoriaPeloTipo("Ganho").ToListAsync();
        }
    }
}
