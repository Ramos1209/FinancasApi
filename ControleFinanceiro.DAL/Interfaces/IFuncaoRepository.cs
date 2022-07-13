using System.Linq;
using ControleFinanceiro.Bll.Models;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IFuncaoRepository: IRepositoryGenerics<Funcao>
   {
       Task AdicionarFuncao(Funcao funcao);
       Task AtualizarFuncao(Funcao funcao);

       IQueryable<Funcao> FiltrarCategorias(string nome);
    }
}
