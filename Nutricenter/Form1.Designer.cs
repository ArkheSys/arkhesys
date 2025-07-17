namespace cwkGestao.Importacao.Nutricenter
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
            this.btImportarTodos = new System.Windows.Forms.Button();
            this.labelControl11 = new System.Windows.Forms.Label();
            this.txtUnidade = new System.Windows.Forms.TextBox();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.txtDiretorio = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btImportarTodos
            // 
            this.btImportarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btImportarTodos.Location = new System.Drawing.Point(479, 71);
            this.btImportarTodos.Name = "btImportarTodos";
            this.btImportarTodos.Size = new System.Drawing.Size(103, 32);
            this.btImportarTodos.TabIndex = 4;
            this.btImportarTodos.Text = "Importar";
            this.btImportarTodos.UseVisualStyleBackColor = true;
            this.btImportarTodos.Click += new System.EventHandler(this.btImportarTodos_Click);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(15, 15);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(74, 13);
            this.labelControl11.TabIndex = 0;
            this.labelControl11.Text = "ID Unidade:";
            // 
            // txtUnidade
            // 
            this.txtUnidade.Location = new System.Drawing.Point(95, 12);
            this.txtUnidade.Name = "txtUnidade";
            this.txtUnidade.Size = new System.Drawing.Size(100, 20);
            this.txtUnidade.TabIndex = 1;
            this.txtUnidade.Text = "1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(0, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(89, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Diretório Banco:";
            // 
            // txtDiretorio
            // 
            this.txtDiretorio.Location = new System.Drawing.Point(95, 38);
            this.txtDiretorio.Name = "txtDiretorio";
            this.txtDiretorio.Size = new System.Drawing.Size(487, 20);
            this.txtDiretorio.TabIndex = 6;
            this.txtDiretorio.Text = "D:\\projetos\\Nutricenter\\Dados\\";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 115);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtDiretorio);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.txtUnidade);
            this.Controls.Add(this.btImportarTodos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importação Nutricenter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btImportarTodos;
        private System.Windows.Forms.Label labelControl11;
        private System.Windows.Forms.TextBox txtUnidade;
        private System.Windows.Forms.Label labelControl1;
        private System.Windows.Forms.TextBox txtDiretorio;
    }
}

