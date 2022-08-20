using ControleFinanceiro.Api.Services;
using ControleFinanceiro.Api.ViewModels;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<AtualizarUsuarioViewModel>> GetUsuario(string id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            AtualizarUsuarioViewModel model = new AtualizarUsuarioViewModel
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Email = usuario.Email,
                CPF = usuario.Cpf,
                Profissao = usuario.Profissao,
                Foto = usuario.Foto
            };

            return model;
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


        [HttpGet("RetornaFotoUsuario/{usuarioId")]
        public async Task<dynamic> RetornaFotoUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepository.GetById(usuarioId);

            return new { imagem = usuario.Foto };
        }

        [HttpPut("AtualizarUsuario")]
        public async Task<ActionResult> AtualizarUsuario(AtualizarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepository.GetById(model.Id);
                usuario.UserName = model.UserName;
                usuario.Email = model.Email;
                usuario.Cpf = model.CPF;
                usuario.Profissao = model.Profissao;
                usuario.Foto = model.Foto;

                await _usuarioRepository.AtualizaUsuario(usuario);
                return Ok(new
                {
                    mensagem = $"Usuario {usuario.Email} atualizado com sucesso"
                });
            }

            return BadRequest(model);
        }
    }
}
