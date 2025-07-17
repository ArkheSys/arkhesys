using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cwkGestao.Integracao.Telefonia
{
    public class ImportadorExcel : IImportador
    {
        private readonly List<Ligacao> ligacoesImportadas = new List<Ligacao>();
        private readonly string nomeTabela;
        private readonly string nomeArquivo;
        private bool Algar = false;

        public ImportadorExcel(string caminho, bool Algar = false)
        {
            this.nomeArquivo = caminho;
            this.nomeTabela = new FileInfo(this.nomeArquivo).Name.Replace(".csv", "");
            this.Algar = Algar;
        }

        public int GetQuantidadeRegistros()
        {
            return File.ReadLines(this.nomeArquivo).Count<string>();
        }

        public IEnumerable<Ligacao> Importar()
        {
            StreamReader rd = new StreamReader(this.nomeArquivo);
            string linha = (string)null;
            string[] linhaseparada = (string[])null;
            while ((linha = rd.ReadLine()) != null)
            {
                linhaseparada = linha.Split(';');
                if (linhaseparada[7].ToString() != "4308" && linhaseparada[7].ToString() != "10432" && (linhaseparada[8].ToString() != "DESCRICAO" && linhaseparada[3].ToString() != "0"))
                    if (Algar)
                        yield return this.CriarLigacaoAlgar(linhaseparada);
                    else
                        yield return this.CriarLigacao(linhaseparada);
            }
            rd.Close();
        }

        private Ligacao CriarLigacao(string[] leitor)
        {
            string message = "";
            try
            {
                string str1 = leitor[8];
                string str2;
                if (str1 == "MINUTOS LOCAIS DDR MINUTO - VOZ")
                {
                    str1 = "PR AREA 44";
                    str2 = "PR";
                }
                else
                    str2 = "";
                string str3 = leitor[2].ToString().Substring(6, 2) + "/" + leitor[2].ToString().Substring(4, 2) + "/" + leitor[2].ToString().Substring(0, 4) + " " + leitor[3].ToString();
                string str4 = leitor[7].ToString().Replace(".0", "").Replace(" ", "");
                message = leitor[5].ToString().Replace(".0", "").Replace(" ", "");
                string str5 = leitor[4].ToString().Replace(".0", "").Replace(" ", "");
                string str6 = "N";
                string str7 = TimeSpan.Parse(leitor[9].ToString().Replace(".0", "").Replace(" ", "")).TotalSeconds.ToString().Replace(".0", "");
                string str8 = leitor[7].ToString();
                if (!(message == "44999959619"))
#pragma warning disable CS0642 // Instrução empty possivelmente incorreta
                    ;
#pragma warning restore CS0642 // Instrução empty possivelmente incorreta
                if (str8 == "DDD")
                    str8 = "1200";
                if (str8 == "VC")
                    str8 = "1600";
                if (str8 == "VCAC")
                    str8 = "1600";
                if (str8 == "VCL")
                    str8 = "1600";
                string str9 = leitor[11].ToString().Replace(".0", "").Replace(" ", "");
                return new Ligacao()
                {
                    Cidade = str1.ToString(),
                    DataHora = Convert.ToDateTime(str3),
                    DescricaoUso = str4.ToString(),
                    Destino = message.ToString(),
                    Horario = str6.ToString(),
                    Origem = str5.ToString(),
                    TempoSegundos = Convert.ToInt32(str7.ToString()),
                    TipoUso = Convert.ToInt32(str8),
                    Uf = str2,
                    ValorComImposto = str9.ToString()
                };
            }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
            catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
            {
                throw new Exception(message);
            }
        }

        private Ligacao CriarLigacaoAlgar(string[] leitor)
        {
            try
            {
                if (leitor[0] == "EMPRESA")
                    return null;
                //string str1 = leitor[8];
                //string str2;
                //if (str1 == "MINUTOS LOCAIS DDR MINUTO - VOZ")
                //{
                //    str1 = "PR AREA 44";
                //    str2 = "PR";
                //}
                //else
                //    str2 = "";
                //string str3 = leitor[2].ToString().Substring(6, 2) + "/" + leitor[2].ToString().Substring(4, 2) + "/" + leitor[2].ToString().Substring(0, 4) + " " + leitor[3].ToString();
                //string str4 = leitor[7].ToString().Replace(".0", "").Replace(" ", "");
                //message = leitor[5].ToString().Replace(".0", "").Replace(" ", "");
                //string str5 = leitor[4].ToString().Replace(".0", "").Replace(" ", "");
                //string str6 = "N";
                //string str7 = TimeSpan.Parse(leitor[9].ToString().Replace(".0", "").Replace(" ", "")).TotalSeconds.ToString().Replace(".0", "");
                //string str8 = leitor[7].ToString();
                //if (!(message == "44999959619"))
                //    ;
                //if (str8 == "DDD")
                //    str8 = "1200";
                //if (str8 == "VC")
                //    str8 = "1600";
                //if (str8 == "VCAC")
                //    str8 = "1600";
                //if (str8 == "VCL")
                //    str8 = "1600";
                //string str9 = leitor[11].ToString().Replace(".0", "").Replace(" ", "");

                if (string.IsNullOrEmpty(leitor[6]))
                    return null;

                string TipoUso = leitor[2];
                if (TipoUso == "DDD")
                    TipoUso = "1200";
                if (TipoUso == "VC")
                    TipoUso = "1600";
                if (TipoUso == "VCAC" || TipoUso == "AC")
                    TipoUso = "1600";
                if (TipoUso == "VCL")
                    TipoUso = "1600";
                if (TipoUso == "Local" || TipoUso == "DLC")
                    TipoUso = "6330";

                TimeSpan TempoSegundos = TimeSpan.FromMinutes(Convert.ToDouble(leitor[8]));
                return new Ligacao()
                {
                    Cidade = leitor[10],
                    DataHora = Convert.ToDateTime(leitor[6]),
                    DescricaoUso = leitor[5],
                    Destino = leitor[11],
                    Horario = leitor[7],
                    Origem = leitor[1],
                    TempoSegundos = Convert.ToInt32(TempoSegundos.TotalSeconds),
                    Uf = leitor[10],
                    TipoUso = Convert.ToInt32(TipoUso),
                    ValorComImposto = leitor[13]
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Ligacao> ImportarFixoECelular()
        {
            StreamReader rd = new StreamReader(this.nomeArquivo);
            string linha = (string)null;
            string[] linhaseparada = (string[])null;
            while ((linha = rd.ReadLine()) != null)
            {
                linhaseparada = linha.Split(';');
                if (linhaseparada[7].ToString() != "4308" && linhaseparada[7].ToString() != "10432" && (linhaseparada[8].ToString() != "DESCRICAO" && linhaseparada[3].ToString() != "0"))
                    if (Algar)
                        yield return this.CriarLigacaoAlgar(linhaseparada);
                //else
                //    yield return this.CriarLigacao(linhaseparada);
            }
            rd.Close();
        }
    }
}
