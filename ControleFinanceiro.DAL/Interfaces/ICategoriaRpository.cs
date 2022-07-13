using ControleFinanceiro.Bll.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public  interface ICategoriaRpository: IRepositoryGenerics<Categoria>
  {
      new IQueryable<Categoria> GetAll();
      new Task<Categoria> GetById(int id);

      IQueryable<Categoria> FiltrarCategorias(string nome);

  }
}
