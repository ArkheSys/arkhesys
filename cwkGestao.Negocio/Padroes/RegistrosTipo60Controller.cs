using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.IO;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public partial class RegistrosTipo60Controller : BaseController<RegistrosTipo60>
    {
        #region Singleton
        private static RegistrosTipo60Controller instance;
        private static IRepositorioRegistrosTipo60 repositorioRegistrosTipo60;

        private RegistrosTipo60Controller()
        { }

        static RegistrosTipo60Controller()
        {
            instance = new RegistrosTipo60Controller();
            repositorioT = RepositorioFactory<RegistrosTipo60>.GetRepositorio();
            repositorioRegistrosTipo60 = (IRepositorioRegistrosTipo60)repositorioT;
        }

        public static RegistrosTipo60Controller Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(RegistrosTipo60 objeto)
        {
            return new Dictionary<string, string>();
        }

        public void LeRegistroTipo60M()
        {
            //IRepositorioRegistrosTipo60 RepositorioRegistroTipo60;

            String Caminho = PegaCaminho();
            StringBuilder sb = new StringBuilder();
            Int32[] Sizes = { 2, 1, 8, 20, 3, 2, 6, 6, 6, 3, 16, 16 };
            Int32 i = 0;

            if (repositorioRegistrosTipo60.VerificaRegistrosDeHojeSubtipoM())
            {
                if (File.Exists(Caminho))
                {
                    StreamReader sr = new StreamReader(Caminho);

                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        String[] Array = line.Split(new String[] { ":" }, StringSplitOptions.None);
                        if (i == 8)
                            sb.Append(Array[1].Substring(Array[1].Length - Sizes[i], Sizes[i]).Replace(" ", "0"));
                        else if (i <= 9 && i != 8)
                            sb.Append(Array[1].Substring(Array[1].Length - Sizes[i], Sizes[i]));
                        else
                            sb.Append(Array[1].Replace(",", "").Substring(Array[1].Length - (Sizes[i] + 1), Sizes[i]).Replace(" ", "0"));
                        i++;
                        sr.ReadLine();
                    }
                    sb.Append("                                      ");
                    sr.Close();
                }


                RegistrosTipo60 RT60 = new RegistrosTipo60();
                RT60.Dt = DateTime.Now.Date;
                RT60.TipoRegistro = "M";
                RT60.Registro = sb.ToString();
                Salvar(RT60, Acao.Incluir);
            }

            File.Delete(Caminho);
        }

        public void LeRegistroTipo60A()
        {
            String Caminho = PegaCaminho();
            StringBuilder sb = new StringBuilder();
            Int32[] Sizes = { 2, 1, 8, 20 };
            Int32 i = 0;
            StreamReader sr = new StreamReader(Caminho);


            if (repositorioRegistrosTipo60.VerificaRegistrosDeHojeSubtipoA())
            {
                if (File.Exists(Caminho))
                {
                    while (i <= 3)
                    {
                        String line = sr.ReadLine();
                        String[] Array = line.Split(new String[] { ":" }, StringSplitOptions.None);
                        sb.Append(Array[1].Substring(Array[1].Length - Sizes[i], Sizes[i]));
                        i++;
                        sr.ReadLine();
                    }
                }

                while (!sr.EndOfStream)
                {
                    StringBuilder Cabecalho = new StringBuilder();
                    Cabecalho.Append(sb.ToString());
                    String line = sr.ReadLine();
                    String[] Array = line.Split(new String[] { ":" }, StringSplitOptions.None);
                    String IdentificadorSitTrib = Array[0].Replace("Cancelamentos", "CANC").Replace("Descontos", "DESC");
                    Cabecalho.Append(IdentificadorSitTrib.Substring(0, 4).Replace(".", " "));
                    Cabecalho.Append(Array[1].Replace(",", "").Substring(Array[1].Length - 13, 12).Replace(" ", "0"));

                    Cabecalho.Append("                                                                               ");

                    RegistrosTipo60 RT60 = new RegistrosTipo60();
                    RT60.Dt = DateTime.Now.Date;
                    RT60.TipoRegistro = "A";
                    RT60.Registro = Cabecalho.ToString();
                    Salvar(RT60, Acao.Incluir);

                    sr.ReadLine();
                    //StreamWriter sw = new StreamWriter(@"D:\Nova pasta\RetornoMestre" + i++ + ".txt", true, Encoding.ASCII);
                    //sw.Write(Cabecalho.ToString());
                    //sw.Close();
                }
                sr.Close();
                File.Delete(Caminho);
            }
        }

        public void LeRegistrosTipo60()
        {
            //IRepositorioRegistrosTipo60 RepositorioRegistroTipo60;

            String Caminho = PegaCaminho();
            StringBuilder sb = new StringBuilder();

            if (repositorioRegistrosTipo60.VerificaRegistrosDeHojeSubtipoM())
            {
                if (File.Exists(Caminho))
                {
                    StreamReader sr = new StreamReader(Caminho);

                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        RegistrosTipo60 RT60 = new RegistrosTipo60();

                        RT60.Dt = DateTime.Now.Date;
                        RT60.TipoRegistro = line.Substring(2, 1);
                        RT60.Registro = line;
                        Salvar(RT60, Acao.Incluir);
                    }
                    sr.Close();
                }
            }

            File.Delete(Caminho);
        }

        private String PegaCaminho()
        {
            String Caminho = global::Modelo.cwkGlobal.DirApp + @"\BemaFI32.ini";

            if (File.Exists(Caminho))
            {
                StreamReader sr = new StreamReader(Caminho);

                while (!sr.EndOfStream)
                {
                    String Linha = sr.ReadLine();
                    String[] Array = Linha.Split(new String[] { "=" }, StringSplitOptions.None);
                    if (Array[0] == "Path")
                    {
                        return Array[1] + "RETORNO.TXT";
                    }
                }
            }
            return String.Empty;
        }

        public IList<RegistrosTipo60> PegaRegistros(DateTime DataInicial, DateTime DataFinal)
        {
            return repositorioRegistrosTipo60.PegaRegistros(DataInicial, DataFinal);
        }

        public IList<pxyRegistro60R> GetPxyRegistro60R(DateTime DataInicial, DateTime DataFinal)
        {
            return repositorioRegistrosTipo60.GetPxyRegistro60R(DataInicial, DataFinal);
        }
    }
}
