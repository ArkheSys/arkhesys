using System.ComponentModel;

namespace cwkGestao.Integracao.ACBr.Core.Boleto
{
    public enum ACBrTipoCobranca
    {
        [Description("0 - Nenhum")] cobNenhum = 0,
        [Description("1 - Banco do Brasil")] cobBancoDoBrasil = 1,
        [Description("2 - Banco Santander")] cobSantander = 2,
        [Description("3 - Caixa Economica Federal")] cobCaixaEconomica = 3,
        [Description("4 - Caixa Economica Federal (Sicob)")] cobCaixaSicob = 4,
        [Description("5 - Bradesco")] cobBradesco = 5,
        [Description("6 - Itau")] cobItau = 6,
        [Description("7 - Banco Mercantil")] cobBancoMercantil = 7,
        [Description("8 - Sicred")] cobSicred = 8,
        [Description("9 - Bancoob")] cobBancoob = 9,
        [Description("10 - Banrisul")] cobBanrisul = 10,
        [Description("11 - Banestes")] cobBanestes = 11,
        [Description("12 - Banco HSBC")] cobHSBC = 12,
        [Description("13 - Banco do Nordeste")] cobBancoDoNordeste = 13,
        [Description("14 - Banco BRB")] cobBRB = 14,
        [Description("15 - BicBanco")] cobBicBanco = 15,
        [Description("16 - Bradesco SICOOB")] cobBradescoSICOOB = 16,
        [Description("17 - Banco Safra")] cobBancoSafra = 17,
        [Description("18 - Banco Safra Bradesco")] cobSafraBradesco = 18,
        [Description("19 - Banco CECRED")] cobBancoCECRED = 19,
        [Description("20 - Banco da Amazonia")] cobBancoDaAmazonia = 20,
        [Description("21 - Banco do Brasil SICOOB")] cobBancoDoBrasilSICOOB = 21,
        [Description("22 - Uniprime")] cobUniprime = 22,
        [Description("23 - Unicred")] cobUnicredRS = 23,
        [Description("24 - Banese")] cobBanese = 24,
        [Description("25 - CredSis")] cobCrediSIS = 25,
        [Description("26 - UniCredES")] cobUnicredES = 26,
        [Description("27 - Banco CredsolSCRS")] cobBancoCresolSCRS = 27,
        [Description("28 - CitiBank")] cobCitiBank = 28,
        [Description("29 - Banco ABC Brasil")] cobBancoABCBrasil = 29,
        [Description("30 - Daycoval")] cobDaycoval = 30,
        [Description("31 - Uniprime NortePR")] cobUniprimeNortePR = 31,
        [Description("32 - Banco Pine")] cobBancoPine = 32,
        [Description("33 - Banco Pine Bradesco")] cobBancoPineBradesco = 33,
        [Description("34 - UniCredSC")] cobUniCredSC = 34,
        // Novos bancos integrados / 27/02/2022 André       
        [Description("35 - BancoAlfa")] cobBancoAlfa = 35,
        [Description("36 - BancoDoBrasilAPI")] cobBancoDoBrasilAPI = 36,
        [Description("37 - BancoDoBrasilWS")] cobBancoDoBrasilWS = 37,
        [Description("38 - BancoCresol")] cobBancoCresol = 38,
        [Description("39 - MoneyPlus ")] cobMoneyPlus = 39,
        [Description("40 - BancoC6")] cobBancoC6 = 40,
        [Description("41 - BancoRendimento")] cobBancoRendimento = 41,
        [Description("42 - BancoInter")] cobBancoInter = 42,
        [Description("43 - BancoSofisaSantander")] cobBancoSofisaSantander = 43,
        [Description("44 - BS2")] cobBS2 = 44
    }
}