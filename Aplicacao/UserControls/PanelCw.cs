using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao.UserControls
{
    public class PanelCw : Panel
    {
        #region Propriedades

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color BorderColor { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int BorderThinckness { get; set; }

        #endregion

        #region Construtor

        public PanelCw()
        {
            BorderColor = Color.Black;
            BorderThinckness = 1;
        }

        #endregion

        #region Método

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (BorderColor.IsEmpty) return;

            var halfThinckness = BorderThinckness / 2;
            using (var p = new Pen(BorderColor, BorderThinckness))
                e.Graphics.DrawRectangle(p, new Rectangle(
                    halfThinckness,
                    halfThinckness,
                    ClientSize.Width - BorderThinckness,
                    ClientSize.Height - BorderThinckness));
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            if (BorderColor.IsEmpty) return;

            var halfThinkness = BorderThinckness / 2;
            using (var p = new Pen(BorderColor, BorderThinckness))
                CreateGraphics().DrawRectangle(p, new Rectangle(
                    halfThinkness,
                    halfThinkness, ClientSize.Width - BorderThinckness,
                    ClientSize.Height - BorderThinckness));

            Refresh();
        }

        #endregion
    }
}
