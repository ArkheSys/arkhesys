namespace cwkGestao.Importacao.ArquivosNFe
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDiretorioXmlDestinatario = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDiretorioLog = new DevExpress.XtraEditors.TextEdit();
            this.sbImportar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiretorioXmlDestinatario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiretorioLog.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDiretorioXmlDestinatario
            // 
            this.txtDiretorioXmlDestinatario.Location = new System.Drawing.Point(122, 12);
            this.txtDiretorioXmlDestinatario.Name = "txtDiretorioXmlDestinatario";
            this.txtDiretorioXmlDestinatario.Size = new System.Drawing.Size(440, 20);
            this.txtDiretorioXmlDestinatario.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Dir. XmlDestinatário:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(75, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Dir. Log:";
            // 
            // txtDiretorioLog
            // 
            this.txtDiretorioLog.Location = new System.Drawing.Point(122, 38);
            this.txtDiretorioLog.Name = "txtDiretorioLog";
            this.txtDiretorioLog.Size = new System.Drawing.Size(440, 20);
            this.txtDiretorioLog.TabIndex = 3;
            // 
            // sbImportar
            // 
            this.sbImportar.Location = new System.Drawing.Point(469, 64);
            this.sbImportar.Name = "sbImportar";
            this.sbImportar.Size = new System.Drawing.Size(93, 23);
            this.sbImportar.TabIndex = 4;
            this.sbImportar.Text = "Importar";
            this.sbImportar.Click += new System.EventHandler(this.sbImportar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 96);
            this.Controls.Add(this.sbImportar);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtDiretorioLog);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtDiretorioXmlDestinatario);
            this.Name = "Form1";
            this.Text = "Importação Arquivos NFe";
            ((System.ComponentModel.ISupportInitialize)(this.txtDiretorioXmlDestinatario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiretorioLog.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtDiretorioXmlDestinatario;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDiretorioLog;
        private DevExpress.XtraEditors.SimpleButton sbImportar;
    }
}

