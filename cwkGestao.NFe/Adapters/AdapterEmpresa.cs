using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkNotaFiscalEletronica.Interfaces;
using MO = Modelo;
using cwkGestao.NFe.Adapters.NH;
using cwkGestao.NFe.Adapters.Linq;
using Modelo;

namespace cwkGestao.NFe.Adapters
{
    abstract class AdapterEmpresa : IEmpresa
    {
        public static AdapterEmpresa Produce(object empresa)
        {
            if (empresa.GetType() == typeof(MO.Filial))
                return new LinqEmpresa((MO.Filial)empresa);

            if (empresa.GetType() == typeof(cwkGestao.Modelo.Filial))
                return new NHEmpresa((Modelo.Filial)empresa);

            if (empresa.GetType().Name == "FilialProxy")
                return new NHEmpresa((Modelo.Filial)empresa);

            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.Cliente ou cwkGestao.Modelo.Cliente");
        }

        public abstract decimal AliqSimplesSubst
        {
            get;
        }

        public abstract int Ambiente
        {
            get;
        }

        public abstract string Bairro
        {
            get;
        }

        public abstract string CEP
        {
            get;
        }

        public abstract string Certificado
        {
            get;
        }

        public abstract string Cidade
        {
            get;
        }

        public abstract string CidadeIBGE
        {
            get;
        }

        public abstract string Cnpj
        {
            get;
        }

        public abstract string Complemento
        {
            get;
        }

        public abstract DateTime ContingenciaDataHora
        {
            get;
        }

        public abstract string ContingenciaJustificativa
        {
            get;
        }

        public abstract string EmailSenha
        {
            get;
        }

        public abstract string EmailUsuario
        {
            get;
        }

        public abstract string Endereco
        {
            get;
        }

        public abstract string Fantasia
        {
            get;
        }

        public abstract string Fax
        {
            get;
        }

        public abstract int FilialNF
        {
            get;
        }

        public abstract string Inscricao
        {
            get;
        }

        public abstract string Nome
        {
            get;
        }

        public abstract string Numero
        {
            get;
        }

        public abstract string Responsavel
        {
            get;
        }

        public abstract string ServidorSMTP
        {
            get;
        }

        public abstract string Telefone
        {
            get;
        }

        public abstract string Tipo
        {
            get;
        }

        public abstract cwkNotaFiscalEletronica.EmpresaCRT TipoCRT
        {
            get;
        }

        public abstract cwkNotaFiscalEletronica.TipoST TipoST
        {
            get;
        }

        public abstract string UF
        {
            get;
        }

        public abstract string UFIBGE
        {
            get;
        }

        public abstract int PortaSMTP
        {
            get;
        }

        public abstract string VersaoEsquema
        {
            get;
        }

        public abstract string DiretorioPadrao
        {
            get;
        }

        public abstract string PinNfe
        {
            get;
        }

        public abstract bool GMailAutenticacao
        {
            get;
        }

        public abstract int ComponenteDfe
        {
            get;
        }
        public abstract string FileCertificado
        {
            get;
        }
        public abstract bool CacheCertificado
        {
            get;
        }
        public abstract string CIdToken
        {
            get;
        }
        public abstract string Csc
        {
            get;
        }
        public abstract int QrCode
        {
            get;
        }
        public abstract string PinCert
        {
            get;
        }
    }
}
