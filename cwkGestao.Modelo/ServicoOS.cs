using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ServicoOS : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Resumo;
        public virtual string Resumo { get { return _Resumo; } set { _Resumo = value; } }

        private string _Descricao;
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

        private DateTime? _DataInicio;
        public virtual DateTime? DataInicio { get { return _DataInicio; } set { _DataInicio = value; } }

        private DateTime? _DataTermino;
        public virtual DateTime? DataTermino { get { return _DataTermino; } set { _DataTermino = value; } }

        private DateTime? _TempoHoras;
        public virtual DateTime? TempoHoras { get { return _TempoHoras; } set { _TempoHoras = value; } }

        private int? _IDOrdemServico;
        public virtual int? IDOrdemServico
        {
            get
            { return _IDOrdemServico; }
            set
            {
                if (value.HasValue)
                {
                    if (_OrdemServico == null)
                        _OrdemServico = new OrdemServico() { ID = value.Value };
                    else
                        _OrdemServico.ID = value.Value;
                }
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
                _OrdemServico = value;
                if (_OrdemServico != null)
                    _IDOrdemServico = _OrdemServico.ID;
                else
                    _IDOrdemServico = null;
            } 
        }

        private int? _IDServico;
        public virtual int? IDServico
        {
            get
            { return _IDServico; }
            set
            {
                if (value.HasValue)
                {
                    if (_Servico == null)
                        _Servico = new Servico() { ID = value.Value };
                    else
                        _Servico.ID = value.Value;
                }
                else
                    _Servico = null;
                _IDServico = value;

            }
        }

        private Servico _Servico;
        public virtual Servico Servico 
        { 
            get { return _Servico; } 
            set 
            { 
                _Servico = value;
                if (_Servico != null)
                    _IDServico = _Servico.ID;
                else
                    _IDServico = null;
            } 
        }

        private int? _IDResponsavel;
        public virtual int? IDResponsavel
        {
            get
            { return _IDResponsavel; }
            set
            {
                if (value.HasValue)
                {
                    if (_Responsavel == null)
                        _Responsavel = new Cw_usuario() { ID = value.Value };
                    else
                        _Responsavel.ID = value.Value;
                }
                else
                    _Responsavel = null;
                _IDResponsavel = value;
            }
        }

        private Cw_usuario _Responsavel;
        public virtual Cw_usuario Responsavel
        {
            get { return _Responsavel; }
            set
            {
                _Responsavel = value;
                if (_Responsavel != null)
                    _IDResponsavel = _Responsavel.ID;
                else
                    _IDResponsavel = null;
            }
        }

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
		