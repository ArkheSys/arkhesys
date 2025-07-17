using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class AcompanhamentoOS : ModeloBase
	{
        
        public AcompanhamentoOS()
        {
            
        }
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        #region OrdemServico
        private int? _IDOrdemServico;
        public virtual int? IDOrdemServico
        {
            get { return _IDOrdemServico; }
            set
            {
                if (value.HasValue)
                    _OrdemServico = new OrdemServico() { ID = value.Value };
                else
                    _OrdemServico = null;

                _IDOrdemServico = value;
            }
        }

        private OrdemServico _OrdemServico;
        public virtual OrdemServico OrdemServico
        {
            get { return _OrdemServico; }
            set
            {
                if (value != null)
                    _IDOrdemServico = value.ID;
                else
                    _IDOrdemServico = null;

                _OrdemServico = value;
            }
        }
        #endregion

        #region ResponsavelAnt
        private int? _IDResponsavelAnt;
        public virtual int? IDResponsavelAnt
        {
            get { return _IDResponsavelAnt; }
            set
            {
                if (value != null)
                    _responsavelAnt = new Cw_usuario() { ID = value.Value };
                else
                    _responsavelAnt = null;

                _IDResponsavelAnt = value;
            }
        }
        
        private Cw_usuario _responsavelAnt;
        public virtual Cw_usuario ResponsavelAnt
        {
            get { return _responsavelAnt; }
            set
            {
                if (value != null)
                    _IDResponsavelAnt = value.ID;
                else
                    _IDResponsavelAnt = null;

                _responsavelAnt = value;
            }
        }
        #endregion

        #region ResponsavelAtu
        private int? _IDResponsavelAtu;
        public virtual int? IDResponsavelAtu
        {
            get { return _IDResponsavelAtu; }
            set
            {
                if (value.HasValue)
                    _responsavelAtu = new Cw_usuario() { ID = value.Value };
                else
                    _responsavelAtu = null;

                _IDResponsavelAtu = value;
            }
        }
        
        private Cw_usuario _responsavelAtu;
        public virtual Cw_usuario ResponsavelAtu
        {
            get { return _responsavelAtu; }
            set
            {
                if (value != null)
                    _IDResponsavelAtu = value.ID;
                else
                    _IDResponsavelAtu = null;

                _responsavelAtu = value;
            }
        }
        #endregion

        #region StatusAnt
        private int? _IDStatusAnt;
        public virtual int? IDStatusAnt
        {
            get { return _IDStatusAnt; }
            set
            {
                if (value.HasValue)
                    _statusAnt = new StatusOrdemServico() { ID = value.Value };
                else
                    _statusAnt = null;

                _IDStatusAnt = value;
            }
        }

        private StatusOrdemServico _statusAnt;
        public virtual StatusOrdemServico StatusAnt
        {
            get { return _statusAnt; }
            set
            {
                if (value != null)
                    _IDStatusAnt = value.ID;
                else
                    _IDStatusAnt = null;

                _statusAnt = value;
            }
        }
        #endregion

        #region StatusAtu
        private int? _IDStatusAtu;
        public virtual int? IDStatusAtu
        {
            get { return _IDStatusAtu; }
            set
            {
                if (value.HasValue)
                    _statusAtu = new StatusOrdemServico() { ID = value.Value };
                else
                    _statusAtu = null;

                _IDStatusAtu = value;
            }
        }

        private StatusOrdemServico _statusAtu;
        public virtual StatusOrdemServico StatusAtu
        {
            get { return _statusAtu; }
            set
            {
                if (value != null)
                    _IDStatusAtu = value.ID;
                else
                    _IDStatusAtu = null;

                _statusAtu = value;
            }
        }
        #endregion

        private DateTime? _Incdata;
        public virtual DateTime? Incdata { get { return _Incdata; } set { _Incdata = value; } }

        private string _Incusuario;
        public virtual string Incusuario { get { return _Incusuario; } set { _Incusuario = value; } }

        private DateTime? _Altdata;
        public virtual DateTime? Altdata { get { return _Altdata; } set { _Altdata = value; } }

        private string _Altusuario;
        public virtual string Altusuario { get { return _Altusuario; } set { _Altusuario = value; } }

        
        
        
        


	}
}
		