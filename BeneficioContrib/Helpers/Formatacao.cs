namespace BeneficioContrib.Helpers
{
    public class Formatacao
    {
        #region Substituição de caracteres

        public static string? SubstituirCaractereEspecial(string? texto, string newChar = "_")
        {
            if (texto == null)
                return null;

            if (newChar == null || texto.Length == 0)
                return texto;

            foreach (var ch in Validador.CaracteresEspeciais)
            {
                texto.Replace(ch.ToString(), newChar);
            }

            return texto;
        }
        public static string? SubstituirAcentos(string? texto)
        {
            if (texto == null)
                return null;

            if (texto.Length == 0)
                return "";

            foreach (var a in Validador.Acentos)
            {
                texto = texto.Replace(a.Key, a.Value);
            }

            return texto;
        }

        #endregion

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
        public static string? Cep(string? cep)
        {
            if (cep == null)
                return null;

            string texto = "";

            if (cep.Length == 8)
            {
                texto += cep.Substring(0, 5) + "-";
                texto += cep.Substring(5, 3);
            }
            else
            {
                texto = cep;
            }

            return texto;
        }
        public static string? TelefoneComDDD(string? telefone)
        {
            if (telefone == null)
                return null;

            string texto = "";

            if (telefone.Length == 10)
            {
                texto += "(" + telefone.Substring(0, 2) + ") ";
                texto += telefone.Substring(2, 4) + "-";
                texto += telefone.Substring(6, 4);
            }
            else if (telefone.Length == 11)
            {
                texto += "(" + telefone.Substring(0, 2) + ") ";
                texto += telefone.Substring(2, 5) + "-";
                texto += telefone.Substring(7, 4);
            }
            else
            {
                texto = telefone;
            }

            return texto;
        }

        #endregion

    }
}
