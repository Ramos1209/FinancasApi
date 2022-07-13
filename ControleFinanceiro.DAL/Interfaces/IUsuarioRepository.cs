using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.Bll.Models;
using Microsoft.AspNetCore.Identity;

namespace ControleFinanceiro.DAL.Interfaces
{
   public interface IUsuarioRepository: IRepositoryGenerics<Usuario>
   {
       Task<int> TotalUsuariosRegistrado();
       Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);
       Task IncluirUsuarioEmFuncao(Usuario usuario, string funcao);
       Task LogarUsuario(Usuario usuario, bool lembrar);
   }
}
