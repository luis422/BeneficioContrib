namespace BeneficioContrib.Helpers
{
    public class Validador
    {

        public static Dictionary<char, char> Acentos = new Dictionary<char, char>()
        {
            #region Minúsculas
            { 'á', 'a' },
            { 'é', 'e' },
            { 'í', 'i' },
            { 'ó', 'o' },
            { 'ú', 'u' },
            { 'à', 'a' },
            { 'è', 'e' },
            { 'ì', 'i' },
            { 'ò', 'o' },
            { 'ù', 'u' },
            { 'ã', 'a' },
            { 'õ', 'o' },
            { 'ñ', 'n' },
            { 'â', 'a' },
            { 'ê', 'e' },
            { 'î', 'i' },
            { 'ô', 'o' },
            { 'û', 'u' },
            { 'ä', 'a' },
            { 'ë', 'e' },
            { 'ï', 'i' },
            { 'ö', 'o' },
            { 'ü', 'u' },
            { 'ç', 'c' },
            #endregion
            #region Maiúsculas
            { 'Á', 'A' },
            { 'É', 'E' },
            { 'Í', 'I' },
            { 'Ó', 'O' },
            { 'Ú', 'U' },
            { 'À', 'A' },
            { 'È', 'E' },
            { 'Ì', 'I' },
            { 'Ò', 'O' },
            { 'Ù', 'U' },
            { 'Ã', 'A' },
            { 'Õ', 'O' },
            { 'Ñ', 'N' },
            { 'Â', 'A' },
            { 'Ê', 'E' },
            { 'Î', 'I' },
            { 'Ô', 'O' },
            { 'Û', 'U' },
            { 'Ä', 'A' },
            { 'Ë', 'E' },
            { 'Ï', 'I' },
            { 'Ö', 'O' },
            { 'Ü', 'U' },
            { 'Ç', 'C' },
            #endregion
        };
        public static List<char> CaracteresEspeciais = new List<char>() {
            '/','\\',':','&','$','@','#','!','"','¨','*','(',')','=','-',
            '%','+','¹','²','³','£','¢','¬','ª','º','°','<','>','\'','´',
            '`','~','^','[',']','{','}','?','|','\"',
        };
        public static List<int> Numeros = new List<int>()
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9
        };

        public static bool Senha(string texto, out string msgErro)
        {
            msgErro = "";
            bool valido = true;

            if (texto == null || texto == "")
            {
                msgErro = "Senha não pode ser vazia";
                return false;
            }


            for (int i = 0; i < CaracteresEspeciais.Count && valido; i++)
            {
                var item = CaracteresEspeciais[i];
                if (texto.Contains(item) && texto.IndexOf(item) != texto.LastIndexOf(item))
                {
                    msgErro = "Senha deve conter ao menos 2 caracteres especiais";
                    valido = false;
                }
            }

            for (int i = 0; i < Numeros.Count; i++)
            {
                var item = Numeros[i];
                if (texto.Contains(item.ToString()) && texto.IndexOf(item.ToString()) != texto.LastIndexOf(item.ToString()))
                {
                    msgErro = "Senha deve conter ao menos 2 números";
                    valido = false;
                }
            }



            return valido;
        }

    }
}
