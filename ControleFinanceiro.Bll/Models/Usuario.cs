using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ControleFinanceiro.Bll.Models
{
    public class Usuario:IdentityUser<string>
    {
        public string Cpf { get; set; }
        public string Email { get; set; }

        public string Profissao { get; set; }

        public byte[] Foto { get; set; }


        public virtual ICollection<Cartao> Cartoes { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }

        public virtual ICollection<Ganho> Ganhos { get; set; }
    }
}