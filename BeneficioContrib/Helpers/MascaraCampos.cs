namespace BeneficioContrib.Helpers
{
    public class MascaraCampos
    {
        public enum Mascara
        {
            cnpj,
            cpf,
            rg,
            pis_pasep,
            telefone,
            telefone_ddd,
            celular,
            celular_ddd,
            data,
            data_hora,
            email,
            decimal2,
        }

        private static readonly string placeholderChar = "";
        private static readonly string placeholderNumero = "0";

        private static readonly Dictionary<Mascara, string> Mascaras = new Dictionary<Mascara, string>()
        {
            { Mascara.cnpj, $"'mask':'99.999.999.9999-99','placeholder':'{placeholderChar}'" },
            { Mascara.cpf, $"'mask':'999.999.999-99','placeholder':'{placeholderChar}'" },
            { Mascara.rg, $"'mask':'99.999.999-*','placeholder':'{placeholderChar}'" },
            { Mascara.pis_pasep, $"'mask':'999.99999.99.9','placeholder':'{placeholderChar}'" },
            { Mascara.telefone, $"'mask':'9999-9999','placeholder':'{placeholderChar}'" },
            { Mascara.telefone_ddd, $"'mask':'99 9999-9999','placeholder':'{placeholderChar}'" },
            { Mascara.celular, $"'mask':'9 9999-9999','placeholder':'{placeholderChar}'" },
            { Mascara.celular_ddd, $"'mask':'99 9 9999-9999','placeholder':'{placeholderChar}'" },
            { Mascara.data, $"'alias':'datetime','inputFormat':'dd/mm/yyyy','placeholder':'{placeholderChar}'" },
            { Mascara.data_hora, $"'alias':'datetime','inputFormat':'dd/mm/yyyy HH:MM','placeholder':'{placeholderChar}'" },
            { Mascara.email, $"'alias':'email','placeholder':'{placeholderChar}'" },
            { Mascara.decimal2, $"'mask':'9{{1,11}},99','placeholder':'{placeholderNumero}'" },
        };

        /// <summary>
        /// Este método adiciona o valor para o <c>data-inputmask</c> à tag em que ele for chamado
        /// </summary>
        /// <param name="mascara">tipo de mascara</param>
        /// <returns></returns>
        public static string Get(Mascara mascara)
        {
            return Mascaras[mascara];
        }
    }
}
