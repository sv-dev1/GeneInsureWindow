﻿namespace Gene
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
            this.btnNewQuote = new System.Windows.Forms.Button();
            this.btnRenew = new System.Windows.Forms.Button();
            this.btnQuickPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClaim = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewQuote
            // 
            this.btnNewQuote.BackColor = System.Drawing.Color.White;
            this.btnNewQuote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewQuote.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewQuote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnNewQuote.Location = new System.Drawing.Point(312, 313);
            this.btnNewQuote.Name = "btnNewQuote";
            this.btnNewQuote.Padding = new System.Windows.Forms.Padding(5);
            this.btnNewQuote.Size = new System.Drawing.Size(480, 108);
            this.btnNewQuote.TabIndex = 0;
            this.btnNewQuote.Text = "New Quote";
            this.btnNewQuote.UseVisualStyleBackColor = false;
            this.btnNewQuote.Click += new System.EventHandler(this.btnNewQuote_Click);
            // 
            // btnRenew
            // 
            this.btnRenew.BackColor = System.Drawing.Color.White;
            this.btnRenew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnRenew.FlatAppearance.BorderSize = 8;
            this.btnRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenew.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRenew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnRenew.Location = new System.Drawing.Point(312, 459);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(229, 108);
            this.btnRenew.TabIndex = 1;
            this.btnRenew.Text = "Renew  P";
            this.btnRenew.UseVisualStyleBackColor = false;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // btnQuickPrint
            // 
            this.btnQuickPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnQuickPrint.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnQuickPrint.FlatAppearance.BorderSize = 5;
            this.btnQuickPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuickPrint.ForeColor = System.Drawing.Color.White;
            this.btnQuickPrint.Location = new System.Drawing.Point(312, 594);
            this.btnQuickPrint.Name = "btnQuickPrint";
            this.btnQuickPrint.Size = new System.Drawing.Size(480, 108);
            this.btnQuickPrint.TabIndex = 3;
            this.btnQuickPrint.Text = "Quick Print";
            this.btnQuickPrint.UseVisualStyleBackColor = false;
            this.btnQuickPrint.Click += new System.EventHandler(this.btnQuickPrint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.label1.Location = new System.Drawing.Point(432, 524);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "Policy";
            // 
            // btnClaim
            // 
            this.btnClaim.BackColor = System.Drawing.Color.White;
            this.btnClaim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnClaim.FlatAppearance.BorderSize = 8;
            this.btnClaim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnClaim.Location = new System.Drawing.Point(564, 461);
            this.btnClaim.Name = "btnClaim";
            this.btnClaim.Size = new System.Drawing.Size(228, 106);
            this.btnClaim.TabIndex = 7;
            this.btnClaim.Text = "Claim      P";
            this.btnClaim.UseVisualStyleBackColor = false;
            this.btnClaim.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.label2.Location = new System.Drawing.Point(656, 524);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Register";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Gene.Properties.Resources.IMG1;
            this.pictureBox2.Location = new System.Drawing.Point(886, 428);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(559, 342);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::Gene.Properties.Resources.geneinsure_logo;
            this.pictureBox1.Location = new System.Drawing.Point(930, 242);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(7);
            this.pictureBox1.Size = new System.Drawing.Size(489, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1556, 779);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClaim);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnQuickPrint);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.btnNewQuote);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewQuote;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.Button btnQuickPrint;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClaim;
        private System.Windows.Forms.Label label2;
    }
}

