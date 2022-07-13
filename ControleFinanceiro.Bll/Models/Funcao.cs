using Microsoft.AspNetCore.Identity;

namespace ControleFinanceiro.Bll.Models
{
    public class Funcao :IdentityRole<string>
    {
        public string Descricao { get; set; }
    }
}
