namespace ControleFinanceiro.Api.ViewModels
{
    public class AtualizarUsuarioViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

        public string Profissao { get; set; }
        public byte[] Foto { get; set; }
    }
}
