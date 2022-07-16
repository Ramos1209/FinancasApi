using System;

namespace ControleFinanceiro.Api
{
    public static class Settings
    {
        public static string ChaveSecreta = Guid.NewGuid().ToString();
    }
}
