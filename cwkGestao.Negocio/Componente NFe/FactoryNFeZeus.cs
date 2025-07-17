using cwkGestao.Modelo;
using DFe.Utils;
using cwkNotaFiscalEletronica;
using NFe.Utils;
using DFe.Classes.Flags;

namespace cwkGestao.Negocio.Componente_NFe
{
    internal static class FactoryNFeZeus
    {
        public static ConfiguracaoApp _configuracoes;

        public static ConfiguracaoServico Build(Filial pFilial, int pModelo, string diretorioPadrao)
        {
            _configuracoes = new ConfiguracaoApp();

            //Configuração Serviço
            _configuracoes.CfgServico.Certificado = ConfgCertificado(pFilial);
            _configuracoes.CfgServico.cUF = Funcoes.RetornaUF(pFilial.Cidade.UF.Sigla);
            _configuracoes.CfgServico.tpAmb = Funcoes.RetornaTipoAmbiente(TipoAmb(pFilial));
            _configuracoes.CfgServico.tpEmis = Funcoes.RetornarTipoEmissao(pFilial.FormaEmissao.ToString());
            _configuracoes.CfgServico.ModeloDocumento = ModeloDocument(pModelo);

            if (_configuracoes.CfgServico.tpAmb != TipoAmbiente.Producao)
            {
                _configuracoes.CfgServico.DiretorioSalvarXml = diretorioPadrao + "\\LogHom";
            }
            else
            {
                _configuracoes.CfgServico.DiretorioSalvarXml = diretorioPadrao + "\\Log";
            }
                               

            return _configuracoes.CfgServico;
        }

        private static ConfiguracaoCertificado ConfgCertificado(Filial pFilial)
        {
            ConfiguracaoCertificado ConfgCertificado;
            TipoCertificado tipo;

            switch (pFilial.TipoCertificadoNFe)
            {
                case 0: tipo = TipoCertificado.A1Repositorio; break;
                case 1: tipo = TipoCertificado.A1Arquivo; break;
                case 2: tipo = TipoCertificado.A3; break;
                case 3: tipo = TipoCertificado.A1ByteArray; break;
                default:
                    tipo = TipoCertificado.A1Repositorio;
                    break;
            }

            switch (tipo)
            {

                case TipoCertificado.A1Arquivo:
                    ConfgCertificado = new ConfiguracaoCertificado
                    {
                        TipoCertificado = TipoCertificado.A1Arquivo,
                        Arquivo = pFilial.Certificado,
                        Senha = pFilial.PinNfe
                    };
                    break;
                case TipoCertificado.A1Repositorio:
                    ConfgCertificado = new ConfiguracaoCertificado
                    {
                        TipoCertificado = TipoCertificado.A1Repositorio,
                        Serial = pFilial.Certificado
                    };
                    break;
                case TipoCertificado.A3:
                    ConfgCertificado = new ConfiguracaoCertificado
                    {
                        TipoCertificado = TipoCertificado.A3,
                        Serial = pFilial.Certificado,
                        Senha = pFilial.PinNfe
                    };
                    break;
                default:
                    ConfgCertificado = new ConfiguracaoCertificado
                    {
                        TipoCertificado = TipoCertificado.A1Repositorio,
                        Serial = pFilial.Certificado,
                    };
                    break;
            }

            return ConfgCertificado;
        }

        private static ModeloDocumento ModeloDocument(int pModelo)
        {
            ModeloDocumento modelo;

            switch (pModelo)
            {
                case 55:
                    modelo = ModeloDocumento.NFe;
                    break;
                case 65:
                    modelo = ModeloDocumento.NFCe;
                    break;
                case 58:
                    modelo = ModeloDocumento.MDFe;
                    break;
                default:
                    modelo = ModeloDocumento.NFe;
                    break;
            }

            return modelo;
        }

        private static int TipoAmb(Filial pFilial)
        {
            int tipoAmbiente;

            switch (pFilial.Ambiente)
            {
                case 0:
                    tipoAmbiente = 1;
                    break;
                case 1:
                    tipoAmbiente = 2;
                    break;
                default:
                    tipoAmbiente = 1;
                    break;
            }

            return tipoAmbiente;
        }


    }
}
