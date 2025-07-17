using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Negocio;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Integracao
{
    public class BConfiguraçãoMagento
    {
        //verifica se existe configuração do Magento
        public bool GetConfiguracaoMagento()
        {
            var configMagento = MagentoConfiguracaoController.Instancia.GetConfiguracao();

            if (configMagento != null)
                return true;

            return false;
        }
    }
}
