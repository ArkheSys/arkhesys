using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioConfiguracao : IRepositorio<Configuracao>
    {
        void InativarCliente();
    }    
}

