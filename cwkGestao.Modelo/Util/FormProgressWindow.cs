using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace cwkGestao.Modelo.Util
{
    public partial class FormProgressWindow : XtraForm
    {
        private string captionPP;

        public string CaptionPP
        {
            get { return captionPP; }
            set
            {
                captionPP = value;
                //ppLoad.Caption = captionPP;
            }
        }

        private string descriptionPP;

        public string DescriptionPP
        {
            get { return descriptionPP; }
            set
            {
                descriptionPP = value;
               //ppLoad.Description = descriptionPP;
            }
        }

        public FormProgressWindow()
        {
            InitializeComponent();
        }

        ~FormProgressWindow()
        {
            //ppLoad.Dispose();
            this.Dispose();
        }
    }
}
