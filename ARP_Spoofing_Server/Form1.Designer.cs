
namespace ARP_Spoofing_Server
{
    partial class ARP_Spoofing_Server
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ARP_Source = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Source_Ip = new System.Windows.Forms.Label();
            this.Source_Mac = new System.Windows.Forms.Label();
            this.Target_Mac = new System.Windows.Forms.Label();
            this.Target_Ip = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.ARP_Target = new System.Windows.Forms.Label();
            this.Listen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ARP_Source
            // 
            this.ARP_Source.AutoSize = true;
            this.ARP_Source.Location = new System.Drawing.Point(30, 116);
            this.ARP_Source.Name = "ARP_Source";
            this.ARP_Source.Size = new System.Drawing.Size(90, 19);
            this.ARP_Source.TabIndex = 0;
            this.ARP_Source.Text = "ARP Source";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(58, 143);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(176, 27);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(316, 143);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(176, 27);
            this.textBox2.TabIndex = 2;
            // 
            // Source_Ip
            // 
            this.Source_Ip.AutoSize = true;
            this.Source_Ip.Location = new System.Drawing.Point(30, 151);
            this.Source_Ip.Name = "Source_Ip";
            this.Source_Ip.Size = new System.Drawing.Size(22, 19);
            this.Source_Ip.TabIndex = 3;
            this.Source_Ip.Text = "IP";
            this.Source_Ip.Click += new System.EventHandler(this.label1_Click);
            // 
            // Source_Mac
            // 
            this.Source_Mac.AutoSize = true;
            this.Source_Mac.Location = new System.Drawing.Point(272, 151);
            this.Source_Mac.Name = "Source_Mac";
            this.Source_Mac.Size = new System.Drawing.Size(38, 19);
            this.Source_Mac.TabIndex = 4;
            this.Source_Mac.Text = "Mac";
            // 
            // Target_Mac
            // 
            this.Target_Mac.AutoSize = true;
            this.Target_Mac.Location = new System.Drawing.Point(272, 248);
            this.Target_Mac.Name = "Target_Mac";
            this.Target_Mac.Size = new System.Drawing.Size(38, 19);
            this.Target_Mac.TabIndex = 9;
            this.Target_Mac.Text = "Mac";
            // 
            // Target_Ip
            // 
            this.Target_Ip.AutoSize = true;
            this.Target_Ip.Location = new System.Drawing.Point(30, 248);
            this.Target_Ip.Name = "Target_Ip";
            this.Target_Ip.Size = new System.Drawing.Size(22, 19);
            this.Target_Ip.TabIndex = 8;
            this.Target_Ip.Text = "IP";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(316, 240);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(176, 27);
            this.textBox3.TabIndex = 7;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(58, 240);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(176, 27);
            this.textBox4.TabIndex = 6;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // ARP_Target
            // 
            this.ARP_Target.AutoSize = true;
            this.ARP_Target.Location = new System.Drawing.Point(30, 213);
            this.ARP_Target.Name = "ARP_Target";
            this.ARP_Target.Size = new System.Drawing.Size(87, 19);
            this.ARP_Target.TabIndex = 5;
            this.ARP_Target.Text = "ARP Target";
            // 
            // Listen
            // 
            this.Listen.Location = new System.Drawing.Point(30, 37);
            this.Listen.Name = "Listen";
            this.Listen.Size = new System.Drawing.Size(94, 29);
            this.Listen.TabIndex = 10;
            this.Listen.Text = "開始監聽";
            this.Listen.UseVisualStyleBackColor = true;
            this.Listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // ARP_Spoofing_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 329);
            this.Controls.Add(this.Listen);
            this.Controls.Add(this.Target_Mac);
            this.Controls.Add(this.Target_Ip);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.ARP_Target);
            this.Controls.Add(this.Source_Mac);
            this.Controls.Add(this.Source_Ip);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ARP_Source);
            this.Name = "ARP_Spoofing_Server";
            this.Text = "ARP Spoofing Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ARP_Source;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Source_Ip;
        private System.Windows.Forms.Label Source_Mac;
        private System.Windows.Forms.Label Target_Mac;
        private System.Windows.Forms.Label Target_Ip;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label ARP_Target;
        private System.Windows.Forms.Button Listen;
    }
}

