namespace cwkGestao.Modelo.ValidaPR
{
    public class Tipo90
    {
        #region AtributosPrivados

        private string _cnpj;
        private string _inscricaoEstadual;

        #endregion

        #region Construtor

        public Tipo90()
        {
            Tipo = "90";
            Quantidade50 = 0;
            Quantidade53 = 0;
            Quantidade54 = 0;
            Quantidade60 = 0;
            Quantidade61 = 0;
            Quantidade75 = 0;
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Tipo "90"
        /// </summary>
        public string Tipo { get; }

        /// <summary>
        ///     CNPJ do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string CNPJ
        {
            get => _cnpj;
            set => _cnpj = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
        }

        /// <summary>
        ///     Inscrição estadual do remetente nas entradas e do destinatário nas saídas
        /// </summary>
        public string InscricaoEstadual
        {
            get => _inscricaoEstadual;
            set
            {
                if (value != null)
                    _inscricaoEstadual = value.Replace(".", "").Replace("/", "").Replace("-", "").Replace("\"", "");
                else
                    _inscricaoEstadual = "";
            }
        }

        /// <summary>
        ///     Quantidade de registros com tipo 50
        /// </summary>
        public int Quantidade50 { get; set; }

        /// <summary>
        ///     Quantidade de registros com tipo 53
        /// </summary>
        public int Quantidade53 { get; set; }

        /// <summary>
        ///     Quantidade de registros com tipo 50
        /// </summary>
        public int Quantidade54 { get; set; }

        public int Quantidade60 { get; set; }
        public int Quantidade61 { get; set; }

        /// <summary>
        ///     Quantidade de registros com tipo 75
        /// </summary>
        public int Quantidade75 { get; set; }

        public int Quantidade74 { get; set; }

        /// <summary>
        ///     Quantidade de registros no geral
        /// </summary>
        public int Quantidade99 => Quantidade50 + Quantidade53 + Quantidade54 + Quantidade60 + Quantidade61 + Quantidade75 + Quantidade74 + 3;

        /// <summary>
        ///     Método para montar a linha conforme layout do Valida-PR
        /// </summary>
        /// <returns>Linha formatada pronta para gravar</returns>
        public override string ToString()
        {
            // Monta a string para ser gravada no arquivo
            // As Colunas informadas no método Insert estão com o valor (-1) em relação ao layout da Receita
            string linha = new string(' ', 126);
            linha = linha.Insert(0, string.Format("{0,-2}", Tipo));
            linha = linha.Insert(2, string.Format("{0,-14}", string.IsNullOrEmpty(CNPJ) == false ? CNPJ : "00000000000000"));
            linha = linha.Insert(16, string.Format("{0,-14}", InscricaoEstadual != null ? InscricaoEstadual : ""));

            int pos_tipo = 20;
            int pos_reg = 22;

            //Verifica se possui registro tipo 50
            if (Quantidade50 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "50"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade50));
            }

            //Verifica se possui registro tipo 53
            if (Quantidade53 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "53"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade53));
            }

            //Verifica se possui registro tipo 54
            if (Quantidade54 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "54"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade54));
            }

            if (Quantidade60 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "60"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade60));
            }

            if (Quantidade61 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "61"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade61));
            }

            //Verifica se possui registro tipo 75
            if (Quantidade74 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "74"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade74));
            }

            //Verifica se possui registro tipo 75
            if (Quantidade75 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "75"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade75));
            }

            //Verifica se possui registro tipo 99
            if (Quantidade99 > 0)
            {
                pos_tipo = pos_tipo + 10;
                pos_reg = pos_reg + 10;

                linha = linha.Insert(pos_tipo, string.Format("{0,-2}", "99"));
                linha = linha.Insert(pos_reg, string.Format("{0:D8}", Quantidade99));
            }

            linha = linha.Insert(125, string.Format("{0,-1}", "1"));
            linha = linha.TrimEnd();

            return linha;
        }

        #endregion
    }
}