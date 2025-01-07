namespace BeneficioContrib.Helpers
{
    public class Formatacao
    {
        #region Máscaras

        public static string? RemoverMascara(string? texto)
        {
            if (texto == null)
                return null;

            return texto.Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace(".", "")
                .Replace("/", "")
                .Replace("+", "");
        }
        public static string? CpfCnpj(string? cpfCnpj)
        {
            if (cpfCnpj == null)
                return null;

            string texto = "";

            if (cpfCnpj.Length == 11)
            {
                texto += cpfCnpj.Substring(0, 3) + ".";
                texto += cpfCnpj.Substring(3, 3) + ".";
                texto += cpfCnpj.Substring(6, 3) + "-";
                texto += cpfCnpj.Substring(9, 2);
            }
            else if (cpfCnpj.Length == 14)
            {
                texto += cpfCnpj.Substring(0, 2) + ".";
                texto += cpfCnpj.Substring(2, 3) + ".";
                texto += cpfCnpj.Substring(5, 3) + "/";
                texto += cpfCnpj.Substring(8, 4) + "-";
                texto += cpfCnpj.Substring(12, 2);
            }
            else
            {
                texto = cpfCnpj;
            }

            return texto;
        }

        #endregion

    }
}
