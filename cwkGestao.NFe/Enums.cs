using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.NFe
{
    public class Enums
    {
        public enum Ambiente
        {
            Producao = 1,
            Homologacao = 2
        }

        public enum Danfe
        {
            Retrato = 1,
            Paisagem = 2
        }

        public enum FormaEmissao
        {
            Normal = 1,
            Contingencia = 2
        }

        public enum TipoCertificado
        {
            ckFile = 0,
            ckSmartCard = 1,
            ckMemory = 2,
            ckLocalMachine = 3,
            ckActiveDirectory = 4

        }

        public enum VersaoXML
        {
            v3 = 0,
            v4 = 1
        }
    }
}
