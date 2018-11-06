namespace AES
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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.lblPlain = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCiphertext = new System.Windows.Forms.TextBox();
            this.txtKeyResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartRoundResult = new System.Windows.Forms.TextBox();
            this.txtSubBytesResult = new System.Windows.Forms.TextBox();
            this.txtShiftRowsResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMixColumnsResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(22, 106);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(149, 58);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.Text = "engjellnishliufiek";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(22, 47);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(149, 20);
            this.txtKey.TabIndex = 0;
            this.txtKey.Text = "hekuran";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(19, 20);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(28, 13);
            this.lblKey.TabIndex = 2;
            this.lblKey.Text = "Key:";
            this.lblKey.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblPlain
            // 
            this.lblPlain.AutoSize = true;
            this.lblPlain.Location = new System.Drawing.Point(19, 80);
            this.lblPlain.Name = "lblPlain";
            this.lblPlain.Size = new System.Drawing.Size(53, 13);
            this.lblPlain.TabIndex = 3;
            this.lblPlain.Text = "Message:";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(62, 266);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(109, 23);
            this.btnEncrypt.TabIndex = 2;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ciphertext";
            // 
            // txtCiphertext
            // 
            this.txtCiphertext.Location = new System.Drawing.Point(22, 193);
            this.txtCiphertext.Multiline = true;
            this.txtCiphertext.Name = "txtCiphertext";
            this.txtCiphertext.Size = new System.Drawing.Size(149, 56);
            this.txtCiphertext.TabIndex = 5;
            // 
            // txtKeyResult
            // 
            this.txtKeyResult.Location = new System.Drawing.Point(827, 47);
            this.txtKeyResult.Multiline = true;
            this.txtKeyResult.Name = "txtKeyResult";
            this.txtKeyResult.ReadOnly = true;
            this.txtKeyResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtKeyResult.Size = new System.Drawing.Size(148, 318);
            this.txtKeyResult.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(824, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Keys";
            // 
            // txtStartRoundResult
            // 
            this.txtStartRoundResult.Location = new System.Drawing.Point(183, 47);
            this.txtStartRoundResult.Multiline = true;
            this.txtStartRoundResult.Name = "txtStartRoundResult";
            this.txtStartRoundResult.ReadOnly = true;
            this.txtStartRoundResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStartRoundResult.Size = new System.Drawing.Size(148, 318);
            this.txtStartRoundResult.TabIndex = 8;
            // 
            // txtSubBytesResult
            // 
            this.txtSubBytesResult.Location = new System.Drawing.Point(337, 47);
            this.txtSubBytesResult.Multiline = true;
            this.txtSubBytesResult.Name = "txtSubBytesResult";
            this.txtSubBytesResult.ReadOnly = true;
            this.txtSubBytesResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSubBytesResult.Size = new System.Drawing.Size(148, 318);
            this.txtSubBytesResult.TabIndex = 9;
            // 
            // txtShiftRowsResult
            // 
            this.txtShiftRowsResult.Location = new System.Drawing.Point(491, 47);
            this.txtShiftRowsResult.Multiline = true;
            this.txtShiftRowsResult.Name = "txtShiftRowsResult";
            this.txtShiftRowsResult.ReadOnly = true;
            this.txtShiftRowsResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShiftRowsResult.Size = new System.Drawing.Size(148, 318);
            this.txtShiftRowsResult.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Start Round";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "After SubBytes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(488, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "After Shift Rows";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(642, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "After Mix Columns";
            // 
            // txtMixColumnsResult
            // 
            this.txtMixColumnsResult.Location = new System.Drawing.Point(645, 47);
            this.txtMixColumnsResult.Multiline = true;
            this.txtMixColumnsResult.Name = "txtMixColumnsResult";
            this.txtMixColumnsResult.ReadOnly = true;
            this.txtMixColumnsResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMixColumnsResult.Size = new System.Drawing.Size(148, 318);
            this.txtMixColumnsResult.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 450);
            this.Controls.Add(this.txtMixColumnsResult);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtShiftRowsResult);
            this.Controls.Add(this.txtSubBytesResult);
            this.Controls.Add(this.txtStartRoundResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKeyResult);
            this.Controls.Add(this.txtCiphertext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.lblPlain);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtMessage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblPlain;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCiphertext;
        private System.Windows.Forms.TextBox txtKeyResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartRoundResult;
        private System.Windows.Forms.TextBox txtSubBytesResult;
        private System.Windows.Forms.TextBox txtShiftRowsResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMixColumnsResult;
    }
}

