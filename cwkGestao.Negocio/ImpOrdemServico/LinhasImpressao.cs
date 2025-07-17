using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.ImpOrdemServico
{
    class LinhasImpressao : IList<string>
    {
        private List<string> lista;

        public int LinhasPagina { get; private set; }
        private int count;
        private int paginas;

        public LinhasImpressao(int linhasPagina)
        {
            lista = new List<string>();
            LinhasPagina = linhasPagina;
            count = 0;
            paginas = 1;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return lista.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return lista.GetEnumerator();
        }

        public void Add(string item)
        {
            lista.Add(item);
            count++;
            if (count > 0 && (count % LinhasPagina == 0))
            {
                throw new EstouroDePaginaException();
            }
        }

        public void AddQuebraPagina(int linhas)
        {
            paginas++;
            for (int i = 0; i < linhas; i++)
            {
                lista.Add(" ");   
            }
        }

        public void Add(string marcador, string item)
        {
            lista.Add(marcador + item);
            count++;
            if (count > 0 && (count % LinhasPagina == 0))
            {
                throw new EstouroDePaginaException();
            }
        }

        public void Clear()
        {
            lista.Clear();
        }

        public bool Contains(string item)
        {
            return lista.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            lista.CopyTo(array, arrayIndex);
        }

        public bool Remove(string item)
        {
            return lista.Remove(item);
        }

        public int Count
        {
            get { return lista.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(string item)
        {
            return lista.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            lista.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            lista.RemoveAt(index);
        }

        public string this[int index]
        {
            get { return lista[index]; }
            set { lista[index] = value; }
        }

        public void TryAddLista(string marcador, IList<string> formatProdutos)
        {
            for (int index = 0; index < formatProdutos.Count; index++)
            {
                try
                {
                    string formatProduto = formatProdutos[index];
                    this.Add(marcador, formatProduto);
                }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
                catch (EstouroDePaginaException e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
                {
                    throw new EstouroDePaginaException(formatProdutos.Skip(index + 1).ToList());
                }
            }
        }

        public void TryAddCabecalho(string marcador, IList<string> formatProdutos)
        {
            if ((count + formatProdutos.Count) < LinhasPagina * paginas)
            {
                foreach (string formatProduto in formatProdutos)
                {
                    this.Add(marcador, formatProduto);
                }
            }
            else
            {
                while (count < LinhasPagina * paginas)
                    count++;
                throw new EstouroDePaginaException(true);
            }
        }

    }
    public class EstouroDePaginaException : Exception
    {
        public IList<string> ListaQueDeuPau { get; private set; }
        public bool ListaEhCabecalho { get; private set; }
        public EstouroDePaginaException()
        {
        }

        public EstouroDePaginaException(bool listaEhCabecalho)
        {
            ListaEhCabecalho = listaEhCabecalho;
        }

        public EstouroDePaginaException(IList<string> listaQueDeuPau)
        {
            this.ListaQueDeuPau = listaQueDeuPau;
        }
    }
}
