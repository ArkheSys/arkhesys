
namespace Aplicacao.Modulos.Comercial
{
    partial class FormFrenteCaixaNotaVincularPOS
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
            this.txtTransacaoPos = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTransacaoPos
            // 
            this.txtTransacaoPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransacaoPos.Location = new System.Drawing.Point(23, 63);
            this.txtTransacaoPos.Name = "txtTransacaoPos";
            this.txtTransacaoPos.Size = new System.Drawing.Size(516, 38);
            this.txtTransacaoPos.TabIndex = 0;
            // 
            // FormFrenteCaixaNotaVincularPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 130);
            this.ControlBox = false;
            this.Controls.Add(this.txtTransacaoPos);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFrenteCaixaNotaVincularPOS";
            this.Resizable = false;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.White;
            this.Text = "Identificacao do Número da Transação POS";
            this.Shown += new System.EventHandler(this.FormFrenteCaixaNotaVincularPOS_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTransacaoPos;
    }
}