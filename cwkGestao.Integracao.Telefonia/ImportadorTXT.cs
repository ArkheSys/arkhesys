using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using ADOX;
using System.IO;
using cwkGestao.Integracao.Telefonia;

namespace cwkGestao.Integracao.Telefonia
{
    public class ImportadorTXT : IImportador
    {
        private readonly string[] registrosArquivo;
        private readonly int area, range, tipoSumus;
        private static readonly string[] TipoDeUsoImportar = new string[] { "35", "36", "37", "28", "8" };
        private static readonly string[] CodigoTelefoneCelular = new string[] { "7", "8", "9" };

        public ImportadorTXT(string _caminho, int _area, int _range, int _tipoSumus)
        {
            registrosArquivo = File.ReadAllLines(_caminho, Encoding.Default);
            area = _area;
            range = _range;
            tipoSumus = _tipoSumus;
        }

        public int GetQuantidadeRegistros()
        {
            return registrosArquivo.Length;
        }

        public IEnumerable<Ligacao> Importar()
        {
            foreach (var reg in registrosArquivo)
            {
                string tipoUso = "";

                if (tipoSumus == 1)
                {
                    tipoUso = reg.Substring(94, 2).TrimEnd();
                    if (TipoDeUsoImportar.Contains(tipoUso))
                    {
                        if (CodigoTelefoneCelular.Contains(reg.Substring(15, 1)) && reg.Substring(15, 2) != "90")
                            yield return ImportarRegistro(reg, tipoUso);
                        else
                        {
                            if (reg.Substring(15, 1) == "0" && CodigoTelefoneCelular.Contains(reg.Substring(20, 1)) && reg.Substring(15, 4) != "0800")
                                yield return ImportarRegistro(reg, tipoUso);
                        }
                    }
                }
                else
                {
                    tipoUso = reg.Substring(88, 3);
                    yield return ImportarRegistro(reg, tipoUso);
                }
            }

        }

        public IEnumerable<Ligacao> ImportarFixoECelular()
        {
            foreach (var reg in registrosArquivo)
            {
                string tipoUso = "";

                if (tipoSumus == 1)
                {
                    tipoUso = reg.Substring(94, 2).Trim();
                    if (reg.Substring(15, 2) != "90" && tipoUso != "0")
                    {
                        var teste = reg.Substring(15, 4);
                        if (teste != "0800")
                            yield return ImportarRegistro(reg, tipoUso);
                    }
                    else
                    {

                        if (reg.Substring(15, 1) == "0" && reg.Substring(15, 4) != "0800" && tipoUso != "0")
                            yield return ImportarRegistro(reg, tipoUso);
                    }
                }
                else
                {
                    tipoUso = reg.Substring(88, 3);
                    yield return ImportarRegistro(reg, tipoUso);
                }
            }

        }

        private Ligacao ImportarRegistro(string reg, string tipoUsoOriginal)
        {
            int TempoLigacao = 0;
            string tUsoTemp = "";
            string cidade = "";
            string tipoUso = tipoUsoOriginal;

            switch (tipoUso)
            {
                case "1":
                    tUsoTemp = "LOCAL";
                    tipoUso = "990";
                    break;
                case "5":
                    tUsoTemp = "FIXO LOCAL";
                    tipoUso = "991";
                    break;
                case "7":
                    tUsoTemp = "FIXO LOCAL";
                    tipoUso = "992";
                    break;
                case "8":
                    tUsoTemp = "INTERURBANO";
                    tipoUso = "993";
                    break;
                case "9":
                    tUsoTemp = "INTERNACIONAL";
                    tipoUso = "994";
                    break;
                case "27":
                    tUsoTemp = "SERVIÇOS";
                    tipoUso = "995";
                    break;
                case "28":
                    tUsoTemp = "CELULAR";
                    tipoUso = "996";
                    break;
                case "31":
                    tUsoTemp = "0300";
                    tipoUso = "997";
                    break;
                case "35":
                    tUsoTemp = "CELULAR";
                    tipoUso = "998";
                    break;
                case "002": // SUMUS 2
                    tUsoTemp = "CELULAR";
                    tipoUso = "998";
                    break;
                case "36":
                    tUsoTemp = "DDI";
                    tipoUso = "999";
                    break;
                case "37":
                    tUsoTemp = "VC3";
                    break;
                case "003": // SUMUS 2
                    tUsoTemp = "VC3";
                    tipoUso = "37";
                    break;
            }
            if (tipoSumus == 1)
            {
                TempoLigacao = Convert.ToInt32(reg.Substring(55, 10).TrimEnd());
                cidade = reg.Substring(105, 20).TrimEnd();
            }
            else
            {
                TempoLigacao = Convert.ToInt32(reg.Substring(77, 10).TrimEnd());
                cidade = reg.Substring(209, 30).TrimEnd();
            }

            return new Ligacao
            {
                Cidade = cidade,
                Origem = MontarTelefoneOrigem(reg),
                Destino = MontarTelefoneDestino(reg),
                DataHora = MontarDataHora(reg),
                TempoSegundos = TempoLigacao,
                TipoUso = Convert.ToInt32(tipoUso),
                DescricaoUso = tUsoTemp,
                Horario = String.Empty,
                Uf = String.Empty,
                ValorComImposto = ValorSumus(reg, tipoUsoOriginal, TempoLigacao)
            };

        }

        private string MontarTelefoneDestino(string reg)
        {
            string tel = "";

            if (tipoSumus == 1)
                tel = reg.Substring(15, 20).TrimEnd();
            else
                tel = reg.Substring(36, 15).TrimEnd();

            switch (tel.Length)
            {
                case 13:
                    return tel.Substring(3, 10);
                case 8:
                    return String.Format("{0:00}{1}", area, tel);
                default:
                    return tel;
            }
        }

        private int CalcularMinutos(int segundos)
        {
            if (segundos <= 0)
                return 0;

            return Convert.ToInt32(Math.Ceiling(segundos / 60m));
        }

        private string ValorSumus(string reg, string tipoUso, int tempo)
        {
            var teldestino = MontarTelefoneDestino(reg);
            //var tipoUso = reg.Substring(94, 2).Trim();
            var ret = "";

            if (teldestino.Length > 8)
            {
                var telCelular = teldestino.Substring(2, 1);
                var teldestino2 = teldestino.Substring(0, 2);
                //var tempo = Convert.ToInt32(reg.Substring(55, 10).TrimEnd());
                var tempo2 = CalcularMinutos(tempo);

                switch (tipoUso)
                {
                    //???
                    case "0":
                        ret = (tempo2 * 0.09).ToString();
                        break;
                    //FIXO LOCAL
                    case "1":
                        ret = (tempo2 * 0.09).ToString();
                        break;
                    //FIXOS VIZINHOS DDD 44
                    case "5":
                        ret = (tempo2 * 0.32).ToString();
                        break;
                    //CELULAR INTERURBANO
                    case "7":
                        ret = (tempo2 * 0.69).ToString();
                        break;
                    //FIXO INTERURBANO
                    case "8":
                        ret = (tempo2 * 0.49).ToString();
                        break;
                    //INTERNACIONAL
                    case "9":
                        ret = (tempo2 * 1).ToString();
                        break;
                    //SERVIÇOS
                    case "27":
                        ret = (tempo2 * 0).ToString();
                        break;
                    //0800
                    case "28":
                        ret = (tempo2 * 0).ToString();
                        break;
                    //0300
                    case "31":
                        ret = (tempo2 * 1).ToString();
                        break;
                    //CELULAR ÁREA 44
                    case "35":
                        ret = (tempo2 * 0.41).ToString();
                        break;
                    //CELULAR ÁREA 44 (SUMUS 2)
                    case "002":
                        ret = (tempo2 * 0.41).ToString();
                        break;
                    //DDI
                    case "36":
                        ret = (tempo2 * 1.16).ToString();
                        break;
                    //VC3
                    case "37":
                         ret = (tempo2 * 0.69).ToString();
                        break;
                    //VC3 (SUMUS 2)
                    case "003":
                        ret = (tempo2 * 0.69).ToString();
                        break;
                }
                return ret;
            }
            else
            {
                return "0";
            }
        }

        private DateTime MontarDataHora(string reg)
        {
            string data = "";
            string hora = "";

            if (tipoSumus == 1)
            {
                data = reg.Substring(35, 10);
                hora = reg.Substring(45, 4).TrimEnd();
            }
            else
            {
                data = reg.Substring(57, 10);
                hora = reg.Substring(68, 4).TrimEnd();
            }

            if (hora.Length == 1)
                hora = "00" + ":" + "0" + hora.Substring(0, 1);
            if (hora.Length == 2)
                hora = "00" + ":" + hora.Substring(0, 2);
            if (hora.Length == 3)
                hora = "0" + hora[0] + ":" + hora.Substring(1, 2);
            if (hora.Length == 4)
                hora = hora.Substring(0, 2) + ":" + hora.Substring(2, 2);

            return Convert.ToDateTime(data + " " + hora);
        }

        private string MontarTelefoneOrigem(string reg)
        {
            int ramal = 0;

            if (tipoSumus == 1)
                ramal = Convert.ToInt32(reg.Substring(10, 5).TrimEnd());
            else
                ramal = Convert.ToInt32(reg.Substring(15, 4).TrimEnd());

            return String.Format("{0:00}{1:0000}{2:0000}", area, range, ramal);
        }
    }
}
