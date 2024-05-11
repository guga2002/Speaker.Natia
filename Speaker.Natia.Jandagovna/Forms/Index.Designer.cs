namespace Speaker.leison.Forms
{
    partial class Index
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
            this.Satauri = new System.Windows.Forms.Label();
            this.ArkhisSaxeli = new System.Windows.Forms.Label();
            this.GilakiYes = new System.Windows.Forms.Button();
            this.GilakiNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Satauri
            // 
            this.Satauri.AutoSize = true;
            this.Satauri.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Satauri.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Satauri.Location = new System.Drawing.Point(2, -1);
            this.Satauri.Name = "Satauri";
            this.Satauri.Size = new System.Drawing.Size(323, 18);
            this.Satauri.TabIndex = 0;
            this.Satauri.Text = "არხი გათიშულია ცნობილი მიზეზით?";
            this.Satauri.Click += new System.EventHandler(this.Satauri_Click);
            // 
            // ArkhisSaxeli
            // 
            this.ArkhisSaxeli.AutoSize = true;
            this.ArkhisSaxeli.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArkhisSaxeli.Location = new System.Drawing.Point(93, 31);
            this.ArkhisSaxeli.Name = "ArkhisSaxeli";
            this.ArkhisSaxeli.Size = new System.Drawing.Size(124, 20);
            this.ArkhisSaxeli.TabIndex = 1;
            this.ArkhisSaxeli.Text = "არხის  სახელი";
            // 
            // GilakiYes
            // 
            this.GilakiYes.BackColor = System.Drawing.Color.Green;
            this.GilakiYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GilakiYes.Location = new System.Drawing.Point(5, 64);
            this.GilakiYes.Name = "GilakiYes";
            this.GilakiYes.Size = new System.Drawing.Size(153, 35);
            this.GilakiYes.TabIndex = 2;
            this.GilakiYes.Text = "დიახ!";
            this.GilakiYes.UseVisualStyleBackColor = false;
            this.GilakiYes.Click += new System.EventHandler(this.GilakiYes_Click);
            // 
            // GilakiNo
            // 
            this.GilakiNo.BackColor = System.Drawing.Color.Red;
            this.GilakiNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GilakiNo.Location = new System.Drawing.Point(164, 64);
            this.GilakiNo.Name = "GilakiNo";
            this.GilakiNo.Size = new System.Drawing.Size(167, 35);
            this.GilakiNo.TabIndex = 3;
            this.GilakiNo.Text = "არა!";
            this.GilakiNo.UseVisualStyleBackColor = false;
            this.GilakiNo.Click += new System.EventHandler(this.GilakiNo_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(334, 111);
            this.ControlBox = false;
            this.Controls.Add(this.GilakiNo);
            this.Controls.Add(this.GilakiYes);
            this.Controls.Add(this.ArkhisSaxeli);
            this.Controls.Add(this.Satauri);
            this.MaximumSize = new System.Drawing.Size(350, 150);
            this.MinimumSize = new System.Drawing.Size(350, 150);
            this.Name = "Index";
            this.Text = "შეტყობინება";
            this.Load += new System.EventHandler(this.Index_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Satauri;
        private System.Windows.Forms.Label ArkhisSaxeli;
        private System.Windows.Forms.Button GilakiYes;
        private System.Windows.Forms.Button GilakiNo;
    }
}