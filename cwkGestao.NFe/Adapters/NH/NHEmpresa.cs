using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.NFe.Adapters.NH
{
    internal class NHEmpresa : AdapterEmpresa
    {
        Modelo.Filial filial;

        public NHEmpresa(Modelo.Filial _filial)
        {
            filial = _filial;
        }

        public override decimal AliqSimplesSubst
        {
            get { return filial.AliqSimplesSubst; }
        }

        public override int Ambiente
        {
            get { return filial.Ambiente; }
        }

        public override string Bairro
        {
            get { return filial.Bairro; }
        }

        public override string CEP
        {
            get { return filial.CEP; }
        }

        public override string Certificado
        {
            get { return filial.Certificado; }
        }

        public override string Cidade
        {
            get { return filial.Cidade.Nome; }
        }

        public override string CidadeIBGE
        {
            get { return filial.Cidade.IBGE; }
        }

        public override string Cnpj
        {
            get { return filial.CNPJ; }
        }

        public override string Complemento
        {
            get { return ""; }
        }

        public override DateTime ContingenciaDataHora
        {
            get { return filial.HoraScan; }
        }

        public override string ContingenciaJustificativa
        {
            get { return filial.JustificativaScan; }
        }

        public override string EmailSenha
        {
            get { return filial.SenhaEmail; }
        }

        public override string EmailUsuario
        {
            get { return filial.UsuarioEmail; }
        }

        public override string Endereco
        {
            get { return filial.Endereco; }
        }

        public override string Fantasia
        {
            get { return ""; }
        }

        public override string Fax
        {
            get { return filial.Fax; }
        }

        public override int FilialNF
        {
            get { return filial.FilialNF; }
        }

        public override string Inscricao
        {
            get { return filial.Inscricao; }
        }

        public override string Nome
        {
            get { return filial.Nome; }
        }

        public override string Numero
        {
            get { return filial.Numero; }
        }

        public override string Responsavel
        {
            get { return filial.Responsavel; }
        }

        public override string ServidorSMTP
        {
            get { return filial.ServidorSMTP; }
        }

        public override string Telefone
        {
            get { return filial.Telefone; }
        }

        public override string Tipo
        {
            get { return filial.Tipo; }
        }

        public override cwkNotaFiscalEletronica.EmpresaCRT TipoCRT
        {
            get
            {
                switch (filial.SimplesNacional)
                {
                    case 0: return cwkNotaFiscalEletronica.EmpresaCRT.SimplesNacional;
                    case 1: return cwkNotaFiscalEletronica.EmpresaCRT.SimplesNacionalExcedido;
                    case 2: return cwkNotaFiscalEletronica.EmpresaCRT.Normal;
                    default: return cwkNotaFiscalEletronica.EmpresaCRT.SimplesNacional;
                }

            }
        }

        public override cwkNotaFiscalEletronica.TipoST TipoST
        {
            get
            {
                switch (filial.TipoST)
                {
                    case 0: return cwkNotaFiscalEletronica.TipoST.Substituído;
                    case 1: return cwkNotaFiscalEletronica.TipoST.Substituto;
                }
                return cwkNotaFiscalEletronica.TipoST.Substituído;
            }
        }

        public override string UF
        {
            get { return filial.Cidade.UF.Sigla; }
        }

        public override string UFIBGE
        {
            get { return filial.Cidade.UF.IBGE; }
        }

        public override int PortaSMTP
        {
            get { return filial.PortaSmtp; }
        }

        public override string VersaoEsquema
        {
            get { return filial.VersaoEsquema; }
        }

        public override string DiretorioPadrao
        {
            get { return filial.DiretorioPadraoNFe; }
        }

        public override string PinNfe
        {
            get { return filial.PinNfe; }
        }

        public override bool GMailAutenticacao
        {
            get { return filial.GMailAutenticacao; }
        }
		
		public override int ComponenteDfe
        {
            get { return filial.ComponenteDfe; }
        }		
		public override string FileCertificado
        {
            get { return filial.FileCertificado; }
        }		
		public override bool CacheCertificado
        {
            get { return filial.CacheCertificado; }
        }
        public override string CIdToken
        {
            get { return filial.CIdToken; }
        }        
        public override string Csc
        {
            get { return filial.Csc; }
        }
        public override int QrCode
        {
            get { return filial.QrCode; }
        }
		public override string PinCert
        {
            get { return filial.PinCert; }
        }
		
    }
}
