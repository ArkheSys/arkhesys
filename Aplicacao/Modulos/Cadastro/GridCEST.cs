using System;
using System.Collections.Generic;
using cwkGestao.Modelo;

namespace Aplicacao.Cadastro
{
    public partial class GridCEST : GridGenerica<CEST>

    {
        public GridCEST(IList<CEST> lista, bool adicionaFiltro, Type formManut, params object[] parametros)
            : base(lista, adicionaFiltro, formManut, parametros)
        {
            InitializeComponent();

        }
    }
}
