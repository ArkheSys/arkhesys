using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPessoa : IRepositorio<Pessoa>
    {
        IList<cwkGestao.Modelo.Pessoa> GetAllFornecedores();
        IList<cwkGestao.Modelo.Pessoa> GetAllVendedores();
        IList<cwkGestao.Modelo.Pessoa> GetAllEntregadores();
        IList<cwkGestao.Modelo.Pessoa> GetAllFuncionarios();
        IList<cwkGestao.Modelo.Pessoa> GetAllClientes();
        IList<cwkGestao.Modelo.Pessoa> GetAllContabilistas();

        IList<Pessoa> GetClienteTel_Clientes();

        Pessoa GetByCodigo(int codigo, string propriedadeTrue);
        Pessoa GetByNome(string value, string propriedadeTrue);
        Pessoa GetByFantasia(string fantasia);


        bool GetByCPFeCNPJ(string value, int? IDSelecionado);

        IList<Pessoa> GetAtivosByCPFeCNPJ(string value, bool bAtivo);

        Pessoa GetClienteMercadoLivre(int id);
        IList<Pessoa> GetAllOperadoresCartao();

        IList<ImpressaoEtiquetaPessoaView> GetImpressaoEtiquetaPessoa();

        IList<Pessoa> GetAllTransportadoras();

        System.Collections.Hashtable GetPessoasImportadas();

        IList<Pessoa> GetParaCadastroEngenheiro();

        int MaxCodigoIntegracao();

        Pessoa GetByIdIntegracao(string idIntegracao);
        Pessoa FindByTextField(string text);
        Pessoa FindByIDSISeCommerce(int id);

        IList<Pessoa> GetAllPorDiaPadraoLeitura(int dtInicio, int dtFim);

        IList<Pessoa> GetEntregadores();

    }
}
