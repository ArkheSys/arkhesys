using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPessoaEmail : IRepositorio<PessoaEmail>
    {
         IList<PessoaEmail> GetEmailFinanceiroPorIDPessoa(int idPessoa);

         IList<PessoaEmail> GetEmailNFSe(int idPessoa);
    }
}

