using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CarregaProxyBoleto : IDisposable
    {
        Modelo.DataClassesDataContext db;

        public CarregaProxyBoleto(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public CarregaProxyBoleto()
        {
            db = new Modelo.DataClassesDataContext();
        }

        public List<Modelo.Objeto.pxyDocCobreBem> ColetaDocumentosVencimento(int IDempresa, DateTime DataInicial, DateTime DataFinal, int IDremessa, int Tipo, bool Impresso, bool EmailEnviado, bool PDFGerado, bool RemessaEnviada)
        {
            List<Modelo.Objeto.pxyDocCobreBem> Lista;
            Lista = (from d in db.Documentos
                     where d.IDFilial == IDempresa && d.DtVencimento >= DataInicial && d.DtVencimento <= DataFinal
                     && d.Situacao != "BxT" && d.Situacao != "Canc" && d.Tipo == (Modelo.TipoDocumentoType)Tipo
                     && (Impresso ? d.bImpresso == Impresso : (!d.bImpresso.HasValue || !d.bImpresso.Value))
                     && (EmailEnviado ? d.bEnviadoEmail == EmailEnviado : (!d.bEnviadoEmail.HasValue || !d.bEnviadoEmail.Value))
                     && (PDFGerado ? d.bGeradoPDF == PDFGerado : (!d.bGeradoPDF.HasValue || !d.bGeradoPDF.Value))
                     && (RemessaEnviada ? d.bGeradoRemessa == RemessaEnviada : (!d.bGeradoRemessa.HasValue || !d.bGeradoRemessa.Value))
                     join p in db.Pessoas on d.IDPessoa equals p.ID
                     join e in db.Enderecos on d.IDPessoa equals e.IDPessoa
                     join c in db.Cidades on e.IDCidade equals c.ID
                     join f in db.Filials on p.IDFilial equals f.ID
                     join portador in db.Portadors on d.IDPortador equals portador.ID
                     join b in db.Bancos on d.IDBanco equals b.ID
                     join a in db.Acrescimos on d.IDAcrescimo equals a.ID
                     join r in db.Remessas on IDremessa equals r.ID
                     join uf in db.UFs on c.IDUF equals uf.ID
                     join emp in db.Empresas on IDempresa equals emp.ID
                     join td in db.TipoDocumentos on d.IDTipoDocumento equals td.ID
                     select new Modelo.Objeto.pxyDocCobreBem
                     {
                         ID = d.ID,
                         Codigo = d.Codigo,
                         Filial = f.Nome,
                         Requisicao = d.NumRequisicao,
                         Cliente = p.Nome,
                         Situacao = d.Situacao,
                         NomeFantasia = p.Fantasia,
                         Portador = portador.Nome,
                         Parcela = d.Parcela,
                         Banco = b.Nome,
                         ArquivoDeLicensa = r.ArquivoDeLicensa,
                         CodigoAgencia = r.CodigoAgencia,
                         DigitoAgencia = r.DigitoAgencia,
                         ContaCorrente = r.ContaCorrente,
                         DigitoConta = r.DigitoConta,
                         CodigoCedente = r.CodigoCedente,
                         DigitoAgenciaConta = r.DigitoAgenciaConta,
                         InicioNossoNumero = "00001",
                         FimNossoNumero = "99999",
                         ProximoNossoNumero = "1",
                         Logotipo = r.Logotipo,
                         CaminhoImagem = r.CaminhoImagem,
                         LayoutBoleto = r.LayoutBoleto,
                         CaminhoLayoutBoleto = r.CaminhoLayoutBoleto,
                         MargemSuperior = 20,
                         Nome = p.Nome,
                         CPF_CNPJ = p.CNPJ_CPF,
                         Rua = e.Rua,
                         NumeroResidencia = e.Numero,
                         Bairro = e.Bairro,
                         Cidade = c.Nome,
                         Estado = uf.Nome,
                         CEP = e.CEP,
                         CodigoDocumento = d.Codigo,
                         TipoDocumentoCobranca = "DM",
                         DataDocumento = d.Dt,
                         DataVencimento = d.DtVencimento,
                         NumeroDocumento = d.NumDocumento,
                         Valor = d.Valor,
                         ValorTotal = d.ValorTotal,
                         Acrescimo = a.PercMulta,
                         Saldo = d.Saldo,
                         Juro = a.PercJuro,
                         DiaProtesto = a.DiasProtesto,
                         InstrucoesCaixa = r.InstrucoesCaixa,
                         Telefone = e.Telefone,
                         QtdParcela = d.QtParcela,
                         LayoutRemessa = r.LayoutRemessa,
                         Empresa = emp.Nome,
                         AgenciaDebito = p.Agencia,
                         ContaCorrenteDebito = p.ContaCorrente,
                         CodigoPessoa = p.Codigo,
                         TipoDocumento = td.Nome,
                         CaminhoRemessa = r.CaminhoRemessa,
                         CaminhoRetorno = r.CaminhoRetorno,
                         NomeRemessa = r.NomeRemessa,
                         NomeRetorno = r.NomeRetorno,
                         VersaoLayoutArquivo = r.VersaoLayoutArquivo,
                         VersaoLayoutLote = r.VersaoLayoutLote,
                         InstrucaoCobranca3 = r.CodigoBaixaDevolucao,
                         ValorInstrucaoCobranca = r.NumDiasBaixaDevolucao,
                         bGeraImpressao = r.bGeraImpressao,
                         bGeraPDF = r.bGeraPDF,
                         bGeraRemessa = r.bGeraRemessa,
                         Sequencia = r.Sequencia,
                         bGeraEmail = r.bGeraEmail,
                         MensagemEmail = r.MensagemEmail,
                         URLLogotipo = r.URLLogotipo,
                         URLCodigoBarras = r.URLCodigodeBarras,
                         AssuntoEmail = r.AssuntoEmail,
                         NomeEmail = r.NomeDoEmail,
                         ServidorSMTP = f.ServidorSMTP,
                         PortaSMPT = 25,
                         UsuarioEmail = f.UsuarioEmail,
                         SenhaEmail = f.SenhaEmail,
                         FormaEnvio = 1,
                         EmailRemetente = f.UsuarioEmail,
                         LayoutBoletoEmail = r.LayoutBoletoEmail,
                         BancoEmiteBoleto = r.BancoEmiteBoleto,
                         CodigoProtesto = r.CodigoProtesto,
                         NumeroDiasProtesto = r.NumeroDiasProtesto,
                         EspecieTitulo = r.EspecieTitulo,
                         VariacoCarteira = r.VariacaoCarteira,
                         ImpressoraPDF = r.ImpressoraNome,
                         BancoRemessa = r.Banco,
                         SicrediTipoCobranca = r.SicrediTipoCobranca,
                         SicredinumeroRemessa = r.SicrediNumeroRemessa,
                         IDRemessa = r.ID,
                         MovimentoRemessa = d.MovimentoRemessa,
                         DiaJuro = a.DiasJuro
                     }).ToList();
            return Lista;
        }

        public List<Modelo.Objeto.pxyDocCobreBem> ColetaDocumentosNota(int pIDNota, int IDremessa)
        {
            List<Modelo.Objeto.pxyDocCobreBem> Lista;
            Lista = (from d in db.Documentos
                     where d.Nota.ID == pIDNota
                     join p in db.Pessoas on d.IDPessoa equals p.ID
                     join e in db.Enderecos on d.IDPessoa equals e.IDPessoa
                     join c in db.Cidades on e.IDCidade equals c.ID
                     join f in db.Filials on p.IDFilial equals f.ID
                     join portador in db.Portadors on d.IDPortador equals portador.ID
                     join b in db.Bancos on d.IDBanco equals b.ID
                     join a in db.Acrescimos on d.IDAcrescimo equals a.ID
                     join r in db.Remessas on IDremessa equals r.ID
                     join uf in db.UFs on c.IDUF equals uf.ID
                     join emp in db.Empresas on d.Nota.IDFilial equals emp.ID
                     join td in db.TipoDocumentos on d.IDTipoDocumento equals td.ID
                     select new Modelo.Objeto.pxyDocCobreBem
                     {
                         ID = d.ID,
                         Codigo = d.Codigo,
                         Filial = f.Nome,
                         Requisicao = d.NumRequisicao,
                         Cliente = p.Nome,
                         Situacao = d.Situacao,
                         NomeFantasia = p.Fantasia,
                         Portador = portador.Nome,
                         Parcela = d.Parcela,
                         Banco = b.Nome,
                         ArquivoDeLicensa = r.ArquivoDeLicensa,
                         Convenio = r.Convenio,
                         CodigoAgencia = r.CodigoAgencia,
                         DigitoAgencia = r.DigitoAgencia,
                         ContaCorrente = r.ContaCorrente,
                         DigitoConta = r.DigitoConta,
                         CodigoCedente = r.CodigoCedente,
                         DigitoAgenciaConta = r.DigitoAgenciaConta,
                         InicioNossoNumero = "00001",
                         FimNossoNumero = "99999",
                         ProximoNossoNumero = "1",
                         Logotipo = r.Logotipo,
                         CaminhoImagem = r.CaminhoImagem,
                         LayoutBoleto = r.LayoutBoleto,
                         CaminhoLayoutBoleto = r.CaminhoLayoutBoleto,
                         MargemSuperior = 20,
                         Nome = p.Nome,
                         CPF_CNPJ = p.CNPJ_CPF,
                         Rua = e.Rua,
                         NumeroResidencia = e.Numero,
                         Bairro = e.Bairro,
                         Cidade = c.Nome,
                         Estado = uf.Nome,
                         CEP = e.CEP,
                         CodigoDocumento = d.Codigo,
                         TipoDocumentoCobranca = "DM",
                         DataDocumento = d.Dt,
                         DataVencimento = d.DtVencimento,
                         NumeroDocumento = d.NumDocumento,
                         Valor = d.Valor,
                         ValorTotal = d.ValorTotal,
                         Acrescimo = a.PercMulta,
                         Saldo = d.Saldo,
                         Juro = a.PercJuro,
                         DiaProtesto = a.DiasProtesto,
                         InstrucoesCaixa = r.InstrucoesCaixa,
                         Telefone = e.Telefone,
                         QtdParcela = d.QtParcela,
                         LayoutRemessa = r.LayoutRemessa,
                         VersaoLayoutArquivo = r.VersaoLayoutArquivo,
                         VersaoLayoutLote = r.VersaoLayoutLote,
                         Empresa = emp.Nome,
                         AgenciaDebito = p.Agencia,
                         ContaCorrenteDebito = p.ContaCorrente,
                         CodigoPessoa = p.Codigo,
                         TipoDocumento = td.Nome,
                         CaminhoRemessa = r.CaminhoRemessa,
                         CaminhoRetorno = r.CaminhoRetorno,
                         NomeRemessa = r.NomeRemessa,
                         NomeRetorno = r.NomeRetorno,
                         InstrucaoCobranca3 = r.CodigoBaixaDevolucao,
                         ValorInstrucaoCobranca = r.NumDiasBaixaDevolucao,
                         bGeraImpressao = r.bGeraImpressao,
                         bGeraPDF = r.bGeraPDF,
                         bGeraRemessa = r.bGeraRemessa,
                         Sequencia = r.Sequencia,
                         bGeraEmail = r.bGeraEmail,
                         MensagemEmail = r.MensagemEmail,
                         URLLogotipo = r.URLLogotipo,
                         URLCodigoBarras = r.URLCodigodeBarras,
                         AssuntoEmail = r.AssuntoEmail,
                         NomeEmail = r.NomeDoEmail,
                         ServidorSMTP = f.ServidorSMTP,
                         PortaSMPT = 25,
                         UsuarioEmail = f.UsuarioEmail,
                         SenhaEmail = f.SenhaEmail,
                         FormaEnvio = 1,
                         EmailRemetente = f.UsuarioEmail,
                         LayoutBoletoEmail = r.LayoutBoletoEmail,
                         BancoEmiteBoleto = r.BancoEmiteBoleto,
                         CodigoProtesto = r.CodigoProtesto,
                         NumeroDiasProtesto = r.NumeroDiasProtesto,
                         EspecieTitulo = r.EspecieTitulo,
                         VariacoCarteira = r.VariacaoCarteira,
                         ImpressoraPDF = r.ImpressoraNome,
                         BancoRemessa = r.Banco,
                         SicrediTipoCobranca = r.SicrediTipoCobranca,
                         SicredinumeroRemessa = r.SicrediNumeroRemessa,
                         IDRemessa = r.ID,
                         MovimentoRemessa = d.MovimentoRemessa,
                         DiaJuro = a.DiasJuro
                     }).ToList();
            return Lista;
        }

        public List<Modelo.Objeto.pxyDocCobreBem> ColetaDocumentosEmissao(int IDempresa, DateTime DataInicial, DateTime DataFinal, int IDremessa, int Tipo, bool Impresso, bool EmailEnviado, bool PDFGerado, bool RemessaEnviada)
        {
            List<Modelo.Objeto.pxyDocCobreBem> Lista;
            Lista = (from d in db.Documentos
                     where d.IDFilial == IDempresa && d.Dt >= DataInicial && d.Dt <= DataFinal
                     && d.Situacao != "BxT" && d.Situacao != "Canc" && d.Tipo == (Modelo.TipoDocumentoType)Tipo
                     && (Impresso ? d.bImpresso == Impresso : (!d.bImpresso.HasValue || !d.bImpresso.Value))
                     && (EmailEnviado ? d.bEnviadoEmail == EmailEnviado : (!d.bEnviadoEmail.HasValue || !d.bEnviadoEmail.Value))
                     && (PDFGerado ? d.bGeradoPDF == PDFGerado : (!d.bGeradoPDF.HasValue || !d.bGeradoPDF.Value))
                     && (RemessaEnviada ? d.bGeradoRemessa == RemessaEnviada : (!d.bGeradoRemessa.HasValue || !d.bGeradoRemessa.Value))
                     join p in db.Pessoas on d.IDPessoa equals p.ID
                     join e in db.Enderecos on d.IDPessoa equals e.IDPessoa
                     join c in db.Cidades on e.IDCidade equals c.ID
                     join f in db.Filials on p.IDFilial equals f.ID
                     join portador in db.Portadors on d.IDPortador equals portador.ID
                     join b in db.Bancos on d.IDBanco equals b.ID
                     join a in db.Acrescimos on d.IDAcrescimo equals a.ID
                     join r in db.Remessas on IDremessa equals r.ID
                     join uf in db.UFs on c.IDUF equals uf.ID
                     join emp in db.Empresas on IDempresa equals emp.ID
                     join td in db.TipoDocumentos on d.IDTipoDocumento equals td.ID
                     select new Modelo.Objeto.pxyDocCobreBem
                     {
                         ID = d.ID,
                         Codigo = d.Codigo,
                         Filial = f.Nome,
                         Requisicao = d.NumRequisicao,
                         Cliente = p.Nome,
                         Situacao = d.Situacao,
                         NomeFantasia = p.Fantasia,
                         Portador = portador.Nome,
                         Parcela = d.Parcela,
                         Banco = b.Nome,
                         ArquivoDeLicensa = r.ArquivoDeLicensa,
                         Convenio = r.Convenio,
                         CodigoAgencia = r.CodigoAgencia,
                         DigitoAgencia = r.DigitoAgencia,
                         ContaCorrente = r.ContaCorrente,
                         DigitoConta = r.DigitoConta,
                         CodigoCedente = r.CodigoCedente,
                         DigitoAgenciaConta = r.DigitoAgenciaConta,
                         InicioNossoNumero = "00001",
                         FimNossoNumero = "99999",
                         ProximoNossoNumero = "1",
                         Logotipo = r.Logotipo,
                         CaminhoImagem = r.CaminhoImagem,
                         LayoutBoleto = r.LayoutBoleto,
                         CaminhoLayoutBoleto = r.CaminhoLayoutBoleto,
                         MargemSuperior = 20,
                         Nome = p.Nome,
                         CPF_CNPJ = p.CNPJ_CPF,
                         Rua = e.Rua,
                         NumeroResidencia = e.Numero,
                         Bairro = e.Bairro,
                         Cidade = c.Nome,
                         Estado = uf.Nome,
                         CEP = e.CEP,
                         CodigoDocumento = d.Codigo,
                         TipoDocumentoCobranca = "DM",
                         DataDocumento = d.Dt,
                         DataVencimento = d.DtVencimento,
                         NumeroDocumento = d.NumDocumento,
                         Valor = d.Valor,
                         ValorTotal = d.ValorTotal,
                         Acrescimo = a.PercMulta,
                         Saldo = d.Saldo,
                         Juro = a.PercJuro,
                         DiaProtesto = a.DiasProtesto,
                         InstrucoesCaixa = r.InstrucoesCaixa,
                         Telefone = e.Telefone,
                         QtdParcela = d.QtParcela,
                         LayoutRemessa = r.LayoutRemessa,
                         VersaoLayoutArquivo = r.VersaoLayoutArquivo,
                         VersaoLayoutLote = r.VersaoLayoutLote,
                         Empresa = emp.Nome,
                         AgenciaDebito = p.Agencia,
                         ContaCorrenteDebito = p.ContaCorrente,
                         CodigoPessoa = p.Codigo,
                         TipoDocumento = td.Nome,
                         CaminhoRemessa = r.CaminhoRemessa,
                         CaminhoRetorno = r.CaminhoRetorno,
                         NomeRemessa = r.NomeRemessa,
                         NomeRetorno = r.NomeRetorno,
                         InstrucaoCobranca3 = r.CodigoBaixaDevolucao,
                         ValorInstrucaoCobranca = r.NumDiasBaixaDevolucao,
                         bGeraImpressao = r.bGeraImpressao,
                         bGeraPDF = r.bGeraPDF,
                         bGeraRemessa = r.bGeraRemessa,
                         Sequencia = r.Sequencia,
                         bGeraEmail = r.bGeraEmail,
                         MensagemEmail = r.MensagemEmail,
                         URLLogotipo = r.URLLogotipo,
                         URLCodigoBarras = r.URLCodigodeBarras,
                         AssuntoEmail = r.AssuntoEmail,
                         NomeEmail = r.NomeDoEmail,
                         ServidorSMTP = f.ServidorSMTP,
                         PortaSMPT = 25,
                         UsuarioEmail = f.UsuarioEmail,
                         SenhaEmail = f.SenhaEmail,
                         FormaEnvio = 1,
                         EmailRemetente = f.UsuarioEmail,
                         LayoutBoletoEmail = r.LayoutBoletoEmail,
                         BancoEmiteBoleto = r.BancoEmiteBoleto,
                         CodigoProtesto = r.CodigoProtesto,
                         NumeroDiasProtesto = r.NumeroDiasProtesto,
                         EspecieTitulo = r.EspecieTitulo,
                         VariacoCarteira = r.VariacaoCarteira,
                         ImpressoraPDF = r.ImpressoraNome,
                         BancoRemessa = r.Banco,
                         SicrediTipoCobranca = r.SicrediTipoCobranca,
                         SicredinumeroRemessa = r.SicrediNumeroRemessa,
                         IDRemessa = r.ID,
                         MovimentoRemessa = d.MovimentoRemessa,
                         DiaJuro = a.DiasJuro
                     }).ToList();
            return Lista;
        }

        public List<Modelo.Objeto.pxyDocCobreBem> ColetaDocumentos(int pID, int IDremessa, int IDempresa)
        {
            List<Modelo.Objeto.pxyDocCobreBem> Lista;
            Lista = (from d in db.Documentos
                     where d.ID == pID
                     join p in db.Pessoas on d.IDPessoa equals p.ID
                     join e in db.Enderecos on d.IDPessoa equals e.IDPessoa
                     join c in db.Cidades on e.IDCidade equals c.ID
                     join f in db.Filials on p.IDFilial equals f.ID
                     join portador in db.Portadors on d.IDPortador equals portador.ID
                     join b in db.Bancos on d.IDBanco equals b.ID
                     join a in db.Acrescimos on d.IDAcrescimo equals a.ID
                     join r in db.Remessas on IDremessa equals r.ID
                     join uf in db.UFs on c.IDUF equals uf.ID
                     join emp in db.Empresas on IDempresa equals emp.ID
                     select new Modelo.Objeto.pxyDocCobreBem
                     {
                         ID = d.ID,
                         Codigo = d.Codigo,
                         Filial = f.Nome,
                         Requisicao = d.NumRequisicao,
                         Cliente = p.Nome,
                         Situacao = d.Situacao,
                         NomeFantasia = p.Fantasia,
                         Portador = portador.Nome,
                         Parcela = d.Parcela,
                         Banco = b.Nome,
                         ArquivoDeLicensa = r.ArquivoDeLicensa,
                         Convenio = r.Convenio,
                         CodigoAgencia = r.CodigoAgencia,
                         DigitoAgencia = r.DigitoAgencia,
                         ContaCorrente = r.ContaCorrente,
                         DigitoConta = r.DigitoConta,
                         CodigoCedente = r.CodigoCedente,
                         DigitoAgenciaConta = r.DigitoAgenciaConta,
                         InicioNossoNumero = "00001",
                         FimNossoNumero = "99999",
                         ProximoNossoNumero = "1",
                         Logotipo = r.Logotipo,
                         CaminhoImagem = r.CaminhoImagem,
                         LayoutBoleto = r.LayoutBoleto,
                         CaminhoLayoutBoleto = r.CaminhoLayoutBoleto,
                         MargemSuperior = 20,
                         Nome = p.Nome,
                         CPF_CNPJ = p.CNPJ_CPF,
                         Rua = e.Rua,
                         NumeroResidencia = e.Numero,
                         Bairro = e.Bairro,
                         Cidade = c.Nome,
                         Estado = uf.Nome,
                         CEP = e.CEP,
                         CodigoDocumento = d.Codigo,
                         TipoDocumentoCobranca = "DM",
                         DataDocumento = d.Dt,
                         DataVencimento = d.DtVencimento,
                         NumeroDocumento = d.NumDocumento,
                         Valor = d.Valor,
                         ValorTotal = d.ValorTotal,
                         Acrescimo = a.PercMulta,
                         Saldo = d.Saldo,
                         Juro = a.PercJuro,
                         DiaProtesto = a.DiasProtesto,
                         InstrucoesCaixa = r.InstrucoesCaixa,
                         Telefone = e.Telefone,
                         QtdParcela = d.QtParcela,
                         LayoutRemessa = r.LayoutRemessa,
                         VersaoLayoutArquivo = r.VersaoLayoutArquivo,
                         VersaoLayoutLote = r.VersaoLayoutLote,
                         Empresa = emp.Nome,
                         AgenciaDebito = p.Agencia,
                         ContaCorrenteDebito = p.ContaCorrente,
                         CaminhoRemessa = r.CaminhoRemessa,
                         CaminhoRetorno = r.CaminhoRetorno,
                         NomeRemessa = r.NomeRemessa,
                         NomeRetorno = r.NomeRetorno,
                         InstrucaoCobranca3 = r.CodigoBaixaDevolucao,
                         ValorInstrucaoCobranca = r.NumDiasBaixaDevolucao,
                         bGeraImpressao = r.bGeraImpressao,
                         bGeraPDF = r.bGeraPDF,
                         bGeraRemessa = r.bGeraRemessa,
                         Sequencia = r.Sequencia,
                         bGeraEmail = r.bGeraEmail,
                         MensagemEmail = r.MensagemEmail,
                         URLLogotipo = r.URLLogotipo,
                         URLCodigoBarras = r.URLCodigodeBarras,
                         AssuntoEmail = r.AssuntoEmail,
                         NomeEmail = r.NomeDoEmail,
                         ServidorSMTP = f.ServidorSMTP,
                         PortaSMPT = 25,
                         UsuarioEmail = f.UsuarioEmail,
                         SenhaEmail = f.SenhaEmail,
                         FormaEnvio = 1,
                         EmailRemetente = f.UsuarioEmail,
                         LayoutBoletoEmail = r.LayoutBoletoEmail,
                         BancoEmiteBoleto = r.BancoEmiteBoleto,
                         CodigoProtesto = r.CodigoProtesto,
                         NumeroDiasProtesto = r.NumeroDiasProtesto,
                         EspecieTitulo = r.EspecieTitulo,
                         VariacoCarteira = r.VariacaoCarteira,
                         ImpressoraPDF = r.ImpressoraNome,
                         BancoRemessa = r.Banco,
                         SicrediTipoCobranca = r.SicrediTipoCobranca,
                         SicredinumeroRemessa = r.SicrediNumeroRemessa,
                         IDRemessa = r.ID,
                         MovimentoRemessa = d.MovimentoRemessa,
                         DiaJuro = a.DiasJuro
                     }).ToList();

            return Lista;
        }

        #region IDisposable Members
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion
    }
}
