namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IGraficoRepository
    {
        object PegarGanhosAnuaisPeloUsuarioId(string usuarioId, int ano);

        object PegarDespesasAnuaisPeloUsuarioId(string usuarioId, int ano);
    }
}
