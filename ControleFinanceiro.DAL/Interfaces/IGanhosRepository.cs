using ControleFinanceiro.Bll.Models;
using System.Linq;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IGanhosRepository: IRepositoryGenerics<Ganho>
   {
       IQueryable<Ganho> PegarGanhosUsuarioId(string usuarioId);
   }
}
