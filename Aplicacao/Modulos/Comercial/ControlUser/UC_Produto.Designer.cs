namespace Aplicacao.Modulos.Comercial.ControlUser
{
    partial class UC_Produto
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.TXT_Produto = new System.Windows.Forms.Label();
            this.TXT_Preco = new System.Windows.Forms.Label();
            this.pictureEdit = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TXT_Produto
            // 
            this.TXT_Produto.BackColor = System.Drawing.Color.White;
            this.TXT_Produto.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_Produto.ForeColor = System.Drawing.Color.DimGray;
            this.TXT_Produto.Location = new System.Drawing.Point(3, 2);
            this.TXT_Produto.Name = "TXT_Produto";
            this.TXT_Produto.Size = new System.Drawing.Size(100, 82);
            this.TXT_Produto.TabIndex = 0;
            this.TXT_Produto.Text = "label1";
            this.TXT_Produto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TXT_Produto.Click += new System.EventHandler(this.ovTXT_Produto_Click);
            this.TXT_Produto.MouseEnter += new System.EventHandler(this.ovTXT_Produto_MouseEnter);
            this.TXT_Produto.MouseLeave += new System.EventHandler(this.ovTXT_Produto_MouseLeave);
            this.TXT_Produto.MouseHover += new System.EventHandler(this.ovTXT_Produto_MouseHover);
            // 
            // TXT_Preco
            // 
            this.TXT_Preco.BackColor = System.Drawing.Color.White;
            this.TXT_Preco.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_Preco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TXT_Preco.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TXT_Preco.Location = new System.Drawing.Point(3, 86);
            this.TXT_Preco.Name = "TXT_Preco";
            this.TXT_Preco.Size = new System.Drawing.Size(100, 31);
            this.TXT_Preco.TabIndex = 1;
            this.TXT_Preco.Text = "label2";
            this.TXT_Preco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TXT_Preco.Click += new System.EventHandler(this.ovTXT_Produto_Click);
            this.TXT_Preco.MouseEnter += new System.EventHandler(this.ovTXT_Produto_MouseEnter);
            this.TXT_Preco.MouseLeave += new System.EventHandler(this.ovTXT_Produto_MouseLeave);
            this.TXT_Preco.MouseHover += new System.EventHandler(this.ovTXT_Produto_MouseHover);
            // 
            // pictureEdit
            // 
            this.pictureEdit.BackColor = System.Drawing.Color.White;
            this.pictureEdit.Location = new System.Drawing.Point(108, 2);
            this.pictureEdit.Margin = new System.Windows.Forms.Padding(1);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Size = new System.Drawing.Size(79, 116);
            this.pictureEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureEdit.TabIndex = 2;
            this.pictureEdit.TabStop = false;
            this.pictureEdit.Click += new System.EventHandler(this.ovTXT_Produto_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.TXT_Produto);
            this.panel1.Controls.Add(this.pictureEdit);
            this.panel1.Controls.Add(this.TXT_Preco);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 124);
            this.panel1.TabIndex = 3;
            // 
            // UC_Produto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UC_Produto";
            this.Size = new System.Drawing.Size(199, 129);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TXT_Produto;
        private System.Windows.Forms.Label TXT_Preco;
        private System.Windows.Forms.PictureBox pictureEdit;
        private System.Windows.Forms.Panel panel1;
    }
}
