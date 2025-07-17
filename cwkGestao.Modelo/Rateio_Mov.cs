using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Rateio")]
    public class Rateio_Mov : ModeloBase, IEquatable<Rateio_Mov>, ICloneable
    {
        public override int ID { get; set; }
        public virtual Movimento Movimento { get; set; }
        [InformacaoExibicao(1, "Projeto", 90)]
        public virtual Projeto Projeto { get; set; }
        [InformacaoExibicao(2, "Valor", 90, InformacaoExibicao.HAlinhamento.Direita,InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
        [InformacaoExibicao(0, "Sequencia", 90)]
        public virtual int Sequencia { get; set; }

        #region IEquatable<Rateio_Mov> Members

        public virtual bool Equals(Rateio_Mov other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Valor.Equals(other.Valor) && Sequencia.Equals(other.Sequencia) && 
                Movimento.ID.Equals(other.Movimento.ID) && Projeto.ID.Equals(other.Projeto.ID);
        }

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashID = ID.GetHashCode();

            //Get hash code for the Code field.
            int hashValor = Valor.GetHashCode();

            int hashSequencia = Sequencia.GetHashCode();

            int hashMovimento = Movimento.ID.GetHashCode();

            int hashProjeto = Projeto.ID.GetHashCode();

            //Calculate the hash code for the product.
            return hashID ^ hashValor ^ hashSequencia ^ hashMovimento ^ hashProjeto;
        }

        #endregion

        #region ICloneable Members

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
