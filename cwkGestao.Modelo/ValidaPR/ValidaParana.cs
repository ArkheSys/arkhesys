using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cwkGestao.Modelo.ValidaPR
{
    public class ValidaPR
    {
        private readonly List<RegistroOrdem> _registroOrdem;
        private StreamWriter arquivoTexto;

        public ValidaPR()
        {
            Registro10 = new Tipo10();
            Registro11 = new Tipo11();
            ListaRegistro50 = new List<Tipo50>();
            ListaRegistro53 = new List<Tipo53>();
            ListaRegistro54 = new List<Tipo54>();
            ListaRegistro60 = new List<Tipo60>();
            ListaRegistro61 = new List<Tipo61>();
            ListaRegistro74 = new List<Tipo74>();
            ListaRegistro75 = new List<Tipo75>();
            Registro90 = new Tipo90();

            _registroOrdem = new List<RegistroOrdem>();
        }

        public Tipo10 Registro10 { get; set; }
        public Tipo11 Registro11 { get; set; }
        public List<Tipo50> ListaRegistro50 { get; set; }
        public List<Tipo53> ListaRegistro53 { get; set; }
        public List<Tipo54> ListaRegistro54 { get; set; }
        public List<Tipo60> ListaRegistro60 { get; set; }
        public List<Tipo74> ListaRegistro74 { get; set; }
        public List<Tipo75> ListaRegistro75 { get; set; }
        public List<Tipo61> ListaRegistro61 { get; set; }
        public Tipo90 Registro90 { get; set; }

        #region Métodos

        //Processo para gerar o arquivo para valida pr
        private void CriarArquivo(string caminhoArquivo, string nomeArquivo)
        {
            arquivoTexto = new StreamWriter(caminhoArquivo + nomeArquivo);
        }

        private void GravarInformacaoArquivo()
        {
            foreach (RegistroOrdem ro in _registroOrdem) arquivoTexto.WriteLine(ro.Linha);
        }

        private void FecharArquivo()
        {
            arquivoTexto.Close();
            arquivoTexto.Dispose();
        }

        public void GeraArquivo(string Diretorio, string NomeArquivo)
        {
            //Grava Registro Tipo 10
            RegistroOrdem ro10 = new RegistroOrdem();
            ro10.Linha = Registro10.ToString();
            ro10.Ordem = "10";
            _registroOrdem.Add(ro10);

            //Grava Registro Tipo 11
            RegistroOrdem ro11 = new RegistroOrdem();
            ro11.Linha = Registro11.ToString();
            ro11.Ordem = "11";
            _registroOrdem.Add(ro11);

            //Grava Registro Tipo 50
            foreach (Tipo50 registro50 in ListaRegistro50)
            {
                RegistroOrdem ro50 = new RegistroOrdem();
                ro50.Linha = registro50.ToString();
                ro50.Ordem = "50" + ro50.Linha.Substring(30, 8);

                _registroOrdem.Add(ro50);
            }

            //Grava Registro Tipo 53
            foreach (Tipo53 registro53 in ListaRegistro53)
            {
                RegistroOrdem ro53 = new RegistroOrdem();
                ro53.Linha = registro53.ToString();
                ro53.Ordem = "53" + ro53.Linha.Substring(30, 8);

                _registroOrdem.Add(ro53);
            }

            //Grava Registro Tipo 54
            foreach (Tipo54 registro54 in ListaRegistro54)
            {
                RegistroOrdem ro54 = new RegistroOrdem();
                ro54.Linha = registro54.ToString();
                ro54.Ordem = "54" + ro54.Linha.Substring(2, 14) + ro54.Linha.Substring(18, 3) + ro54.Linha.Substring(21, 6) + ro54.Linha.Substring(34, 3);

                _registroOrdem.Add(ro54);
            }

            foreach (Tipo60 registro60 in ListaRegistro60)
            {
                RegistroOrdem ro60 = new RegistroOrdem();
                ro60.Linha = registro60.Registro;
                ro60.Ordem = "60" + ro60.Linha;

                _registroOrdem.Add(ro60);
            }

            foreach (Tipo61 registro61 in ListaRegistro61)
            {
                RegistroOrdem ro61 = new RegistroOrdem();
                ro61.Linha = registro61.ToString();
                ro61.Ordem = "61" + ro61.Linha;

                _registroOrdem.Add(ro61);
            }

            foreach (Tipo74 registro74 in ListaRegistro74)
            {
                RegistroOrdem ro74 = new RegistroOrdem();
                ro74.Linha = registro74.ToString();
                ro74.Ordem = "74" + ro74.Linha.Substring(2, 22);

                _registroOrdem.Add(ro74);
            }

            //Grava Registro Tipo 75
            IList<Tipo75> ListaRegistro75Unica = ListaRegistro75.GroupBy(s => s.CodigoProduto).Select(s => s.First()).ToList();
            foreach (Tipo75 registro75 in ListaRegistro75Unica)
            {
                RegistroOrdem ro75 = new RegistroOrdem();
                ro75.Linha = registro75.ToString();
                ro75.Ordem = "75" + ro75.Linha.Substring(18, 14);

                _registroOrdem.Add(ro75);
            }

            //Atualiza os totais de registros
            Registro90.Quantidade50 = ListaRegistro50.Count;
            Registro90.Quantidade53 = ListaRegistro53.Count;
            Registro90.Quantidade54 = ListaRegistro54.Count;
            Registro90.Quantidade60 = ListaRegistro60.Count;
            Registro90.Quantidade61 = ListaRegistro61.Count;
            Registro90.Quantidade74 = ListaRegistro74.Count;
            Registro90.Quantidade75 = ListaRegistro75Unica.Count;

            //Grava Registro Tipo 90
            RegistroOrdem ro90 = new RegistroOrdem();
            ro90.Linha = Registro90.ToString();
            ro90.Ordem = "90";
            _registroOrdem.Add(ro90);

            //cria arquivo txt;
            CriarArquivo(Diretorio, NomeArquivo);

            //grava todos os registros no arquivo;
            GravarInformacaoArquivo();

            //fecha o arquivo;
            FecharArquivo();
        }

        #endregion
    }
}