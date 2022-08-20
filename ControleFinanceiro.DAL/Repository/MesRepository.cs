using System;
using System.Collections.Generic;
using System.Text;
using ControleFinanceiro.Bll.Models;
using ControleFinanceiro.DAL.Interfaces;

namespace ControleFinanceiro.DAL.Repository
{
  public  class MesRepository: RepositoryGenerics<Mes>, IMesRepository
    {
        public MesRepository(FinanceiroContext context) : base(context)
        {
        }
    }
}
