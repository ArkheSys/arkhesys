using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cwkGestao.Negocio;
using System.Threading;

namespace CwkGestao.Integracao.PafEcf.Salc.Controle
{
    public class MonitorPasta
    {
        private FileSystemWatcher Watcher;
        public void MonitorarPasta(string Caminho, string TipoArquivo, FileSystemEventHandler LeitorArquivo)
        {
            try
            {
                if (!Directory.Exists(Caminho))
                {
                    throw new Exception("O Diretório informado não existe.");
                }
                if (String.IsNullOrEmpty(TipoArquivo))
                {
                    throw new Exception("O tipo de arquivo à ser monitorado não foi informado");
                }
                Watcher = new FileSystemWatcher(Caminho);
                //Watcher.Filter = String.IsNullOrEmpty(TipoArquivo) ? "*.*" : TipoArquivo;
                Watcher.Filter = "*.*";
                Watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
                Watcher.InternalBufferSize = 65536;
                Watcher.Created += new FileSystemEventHandler(LeitorArquivo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void Start()
        {
            if (Watcher == null)
            {
                throw new Exception("O monitor de arquivos não foi iniciado");
            }
            Watcher.EnableRaisingEvents = true;
        }
        public void Stop()
        {
            if (Watcher == null)
            {
                throw new Exception("O monitor de arquivos não foi iniciado");
            }
            Watcher.EnableRaisingEvents = false;
        }
    }
}
