using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ControleFinanceiro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController( IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
          
        }

      

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost("SalvarFoto")]
        public async Task<IActionResult> SalvarFoto()
        {
            var foto = Request.Form.Files[0];
            byte[] b;

            using (var openReadString = foto.OpenReadStream())
            {
                using (var memorystrean = new MemoryStream())
                {
                    await openReadString.CopyToAsync(memorystrean);
                    b = memorystrean.ToArray();
                }
            }
            return Ok(new
            {
                foto = b
            });

        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(RegistroViewModels models)
        {
            if (ModelState.IsValid)
            {
                IdentityResult criaUsuario;
                string funcaoUsuario;

                Usuario usuario = new Usuario
                {
                    UserName = models.NomeUsuario,
                    Email = models.Email,
                    Profissao = models.Profissao,
                    PasswordHash = models.Senha,
                    Cpf = models.CPF,
                    Foto = models.Foto

                };

                if (await _usuarioRepository.TotalUsuariosRegistrado() > 0)
                {
                    funcaoUsuario = "Usuario";
                }
                else
                {
                    funcaoUsuario = "Administrador";
                }

                criaUsuario = await _usuarioRepository.CriarUsuario(usuario, models.Senha);

                if (criaUsuario.Succeeded)
                {
                    await _usuarioRepository.IncluirUsuarioEmFuncao(usuario, funcaoUsuario);
                    await _usuarioRepository.LogarUsuario(usuario, false);

                    return Ok(new
                    {
                        EmailUsuario = usuario.Email,
                        usuarioId = usuario.Id
                    });
                }
                else
                {
                    return BadRequest(models);
                }
            }
            return BadRequest(models);
        }

    }
}
