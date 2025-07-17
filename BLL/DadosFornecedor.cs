using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class DadosFornecedor
    {
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Cotacao objCotacao, int Tela)
        {
            Dictionary<string, string> ret = ValidaObjeto(objCotacao, Tela);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.Cotacao objCotacao, int Tela)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            switch(Tela){
            
                case 6:
                    if (objCotacao.IDCondicao_01 == null)
                    {
                        ret.Add("cbCondicao1", "Escolha a condicao que deseja.");
                    }

                    if (objCotacao.PrazoEntrega_01 == null)
                    {
                        ret.Add("cbPrazo1", "Escolha um prazo para este fornecedor.");
                    }
                break;
                
                case 7:
                    if (objCotacao.IDCondicao_02 == null)
                    {
                        ret.Add("cbCondicao2", "Escolha a condicao que deseja.");
                    }

                    if (objCotacao.PrazoEntrega_02 == null)
                    {
                        ret.Add("cbPrazo2", "Escolha um prazo para este fornecedor.");
                    }
                break;

                case 8:
                    if (objCotacao.IDCondicao_03 == null)
                    {
                        ret.Add("cbCondicao3", "Escolha a condicao que deseja.");
                    }

                    if (objCotacao.PrazoEntrega_03 == null)
                    {
                        ret.Add("cbPrazo3", "Escolha um prazo para este fornecedor.");
                    }
                break;

                case 9:
                    if (objCotacao.IDCondicao_04 == null)
                    {
                        ret.Add("cbCondicao4", "Escolha a condicao que deseja.");
                    }

                    if (objCotacao.PrazoEntrega_04 == null)
                    {
                        ret.Add("cbPrazo4", "Escolha um prazo para este fornecedor.");
                    }
                break;

                case 10:
                    if (objCotacao.IDCondicao_05 == null)
                    {
                        ret.Add("cbCondicao5", "Escolha a condicao que deseja.");
                    }

                    if (objCotacao.PrazoEntrega_05 == null)
                    {
                        ret.Add("cbPrazo5", "Escolha um prazo para este fornecedor.");
                    }
                break;

                case 11:
                    if (objCotacao.IDCondicao_06 == null)
                    {
                        ret.Add("cbCondicao6", "Escolha a condicao que deseja.");
                    }

                    if (objCotacao.PrazoEntrega_06 == null)
                    {
                        ret.Add("cbPrazo6", "Escolha um prazo para este fornecedor.");
                    }
                break;
            }
            return ret;

        }
    }
}
