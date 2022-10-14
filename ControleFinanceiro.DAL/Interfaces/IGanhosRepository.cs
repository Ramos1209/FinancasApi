using ControleFinanceiro.Bll.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IGanhosRepository: IRepositoryGenerics<Ganho>
   {
       IQueryable<Ganho> PegarGanhosUsuarioId(string usuarioId);

       Task<double> PegarGanhoTotalPeloUsuarioId(string usuarioId);
    }
}
