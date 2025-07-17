using System;

namespace cwkGestao.Modelo
{
#pragma warning disable CS0659 // "ModeloBase" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    public abstract class ModeloBase
#pragma warning restore CS0659 // "ModeloBase" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    {
        public ModeloBase()
        {
            IncData = DateTime.Now;
            AltData = DateTime.Now;
            try
            {
                IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
            }
            catch
            {
            }
        }

        public abstract int ID { get ; set ; }

        public override bool Equals(object obj)
        {
            if (obj is ModeloBase)
            {
                if (this.ID == 0)
                    try
                    {
                        return obj.GetType().GetProperty("Sequencia").GetValue(obj, null).Equals(this.GetType().GetProperty("Sequencia").GetValue(this, null));

                    }
                    catch
                    {
                        ModeloBase objeto = (ModeloBase)obj;
                        return objeto.ID == this.ID;
                    }
                else
                {
                    ModeloBase objeto = (ModeloBase)obj;
                    return objeto.ID == this.ID;
                }
            }
            else
                return false;
        }

        public virtual ModeloBase Clonar()
        {
            ModeloBase clone = this.MemberwiseClone() as ModeloBase;

            clone.ID = 0;
            return clone;
        }

        public override string ToString()
        {
            try { return this.GetType().GetProperty("Nome").GetValue(this, null).ToString(); }
            catch { return base.ToString(); }
        }

        public virtual string IncUsuario { get; set; }

        public virtual DateTime? IncData { get; set; }

        public virtual string AltUsuario { get; set; }

        public virtual DateTime? AltData { get; set; }

    }
}
