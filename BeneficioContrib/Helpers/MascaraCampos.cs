namespace BeneficioContrib.Helpers
{
    /// <summary>
    /// Classe para ajudar a colocar máscara nos campos das views. Esta classe depende do bundle da biblioteca jquery-inputmask.
    /// </summary>
    public class MascaraCampos
    {
        /// <summary>
        /// Tipos de máscaras disponíveis
        /// </summary>
        public enum Mascara
        {
            /// <summary>
            /// 'mask':'99.999.999.9999-99'
            /// </summary>
            cnpj,

            /// <summary>
            /// 'mask':'999.999.999-99'
            /// </summary>
            cpf,

            /// <summary>
            /// 'mask':'99.999.999-*'
            /// </summary>
            rg,

            /// <summary>
            /// 'mask':'999.99999.99.9'
            /// </summary>
            pis_pasep,

            /// <summary>
            /// 'mask':'9999-9999'
            /// </summary>
            telefone,

            /// <summary>
            /// 'mask':'99 9999-9999'
            /// </summary>
            telefone_ddd,

            /// <summary>
            /// 'mask':'9 9999-9999'
            /// </summary>
            celular,

            /// <summary>
            /// 'mask':'99 9 9999-9999'
            /// </summary>
            celular_ddd,

            /// <summary>
            /// 'alias':'datetime','inputFormat':'dd/mm/yyyy'
            /// </summary>
            data,

            /// <summary>
            /// 'alias':'datetime','inputFormat':'dd/mm/yyyy HH:MM'
            /// </summary>
            data_hora,

            /// <summary>
            /// 'alias':'email'
            /// </summary>
            email,

            /// <summary>
            /// 'mask':'9{1,11},99'
            /// </summary>
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
