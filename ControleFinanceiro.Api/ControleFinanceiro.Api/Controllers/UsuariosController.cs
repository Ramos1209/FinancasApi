using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Api.Services;
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
        public async Task<ActionResult> SalvarFoto()
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

        [HttpPost("RegistrarUsuario")]
        public async Task<ActionResult> RegistrarUsuario(RegistroViewModels models)
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
                    var token = TokenService.GerarToken(usuario, funcaoUsuario);
                    await _usuarioRepository.LogarUsuario(usuario, false);

                    return Ok(new
                    {
                        EmailUsuarioLogado = usuario.Email,
                        usuarioId = usuario.Id,
                        tokenUsuarioLogado = token
                    });
                }
                else
                {
                    return BadRequest(models);
                }
            }
            return BadRequest(models);
        }

        [HttpPost("LogarUsuario")]

        public async Task<ActionResult> LogarUsuario(LoginViewModel model)
        {
            if (model == null)
            {
                return NotFound("Usuario e / ou senhas invalidos");
            }

            Usuario usuario = await _usuarioRepository.BuscarUsuarioPorEmail(model.Email);

            if (usuario != null)
            {
                PasswordHasher<Usuario> pass = new PasswordHasher<Usuario>();
                if (pass.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) !=
                    PasswordVerificationResult.Failed)
                {
                    var funcaoUsuario = await _usuarioRepository.PegarFuncoesUsuarios(usuario);
                    var token = TokenService.GerarToken(usuario, funcaoUsuario.First());
                    await _usuarioRepository.LogarUsuario(usuario, false);

                    return Ok(new
                    {
                        EmailUsuarioLogado = usuario.Email,
                        usuarioId = usuario.Id,
                        tokenUsuarioLogado = token
                    });
                }
                return NotFound("Usuario e / ou senhas invalidos");
            }
            return NotFound("Usuario e / ou senhas invalidos");
        }

    }
}
