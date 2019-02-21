namespace Gene
{
    partial class frmLicenceQuote
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
            this.PnlLicenceVrn = new System.Windows.Forms.Panel();
            this.txtLicVrn = new System.Windows.Forms.TextBox();
            this.btnLicSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PnlLicenceVrn.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlLicenceVrn
            // 
            this.PnlLicenceVrn.BackColor = System.Drawing.Color.Transparent;
            this.PnlLicenceVrn.Controls.Add(this.txtLicVrn);
            this.PnlLicenceVrn.Controls.Add(this.btnLicSave);
            this.PnlLicenceVrn.Controls.Add(this.label1);
            this.PnlLicenceVrn.Location = new System.Drawing.Point(252, 99);
            this.PnlLicenceVrn.Name = "PnlLicenceVrn";
            this.PnlLicenceVrn.Size = new System.Drawing.Size(718, 252);
            this.PnlLicenceVrn.TabIndex = 6;
            // 
            // txtLicVrn
            // 
            this.txtLicVrn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicVrn.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicVrn.Location = new System.Drawing.Point(29, 95);
            this.txtLicVrn.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.txtLicVrn.Name = "txtLicVrn";
            this.txtLicVrn.Size = new System.Drawing.Size(659, 53);
            this.txtLicVrn.TabIndex = 1;
            // 
            // btnLicSave
            // 
            this.btnLicSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnLicSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLicSave.ForeColor = System.Drawing.Color.White;
            this.btnLicSave.Location = new System.Drawing.Point(530, 154);
            this.btnLicSave.Name = "btnLicSave";
            this.btnLicSave.Size = new System.Drawing.Size(158, 76);
            this.btnLicSave.TabIndex = 0;
            this.btnLicSave.Text = "Submit";
            this.btnLicSave.UseVisualStyleBackColor = false;
            this.btnLicSave.Click += new System.EventHandler(this.btnLicSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(497, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Let\'s get your vehicle details  !!";
            // 
            // frmLicenceQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1257, 510);
            this.Controls.Add(this.PnlLicenceVrn);
            this.Name = "frmLicenceQuote";
            this.Text = "frmLicenceQuote";
            this.PnlLicenceVrn.ResumeLayout(false);
            this.PnlLicenceVrn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlLicenceVrn;
        private System.Windows.Forms.TextBox txtLicVrn;
        private System.Windows.Forms.Button btnLicSave;
        private System.Windows.Forms.Label label1;
    }
}