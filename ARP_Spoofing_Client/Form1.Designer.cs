using System.Net;
using SharpPcap;
using SharpPcap.LibPcap;
using PacketDotNet;
using System;
using System.Linq;
using System.Collections.Generic;
using ARP_Spoofing_Client.ObjectSerializer;
namespace ARP_Spoofing_Client
{
    partial class ARP_Spoofing_Client
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
            this.ARP_Stop = new System.Windows.Forms.Button();
            this.ARP_Start = new System.Windows.Forms.Button();
            this.Source = new System.Windows.Forms.Label();
            this.Source_Ip = new System.Windows.Forms.Label();
            this.Source_Mac = new System.Windows.Forms.Label();
            this.textBox_Source_Ip = new System.Windows.Forms.TextBox();
            this.textBox_Source_Mac = new System.Windows.Forms.TextBox();
            this.textBox_Source_Mask = new System.Windows.Forms.TextBox();
            this.textBox_Target_Mac = new System.Windows.Forms.TextBox();
            this.textBox_Target_Ip = new System.Windows.Forms.TextBox();
            this.Target_Mac = new System.Windows.Forms.Label();
            this.Target_Ip = new System.Windows.Forms.Label();
            this.Target = new System.Windows.Forms.Label();
            this.Mask = new System.Windows.Forms.Label();
            this.textBox_Server_Ip = new System.Windows.Forms.TextBox();
            this.Server_Ip = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.State = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ARP_Stop
            // 
            this.ARP_Stop.Enabled = false;
            this.ARP_Stop.Location = new System.Drawing.Point(455, 421);
            this.ARP_Stop.Name = "ARP_Stop";
            this.ARP_Stop.Size = new System.Drawing.Size(94, 29);
            this.ARP_Stop.TabIndex = 0;
            this.ARP_Stop.Text = "停送ARP";
            this.ARP_Stop.UseVisualStyleBackColor = true;
            this.ARP_Stop.Click += new System.EventHandler(this.ARP_Stop_Click);
            // 
            // ARP_Start
            // 
            this.ARP_Start.Location = new System.Drawing.Point(340, 421);
            this.ARP_Start.Name = "ARP_Start";
            this.ARP_Start.Size = new System.Drawing.Size(94, 29);
            this.ARP_Start.TabIndex = 1;
            this.ARP_Start.Text = "連送ARP";
            this.ARP_Start.UseVisualStyleBackColor = true;
            this.ARP_Start.Click += new System.EventHandler(this.ARP_Start_Click);
            // 
            // Source
            // 
            this.Source.AutoSize = true;
            this.Source.Location = new System.Drawing.Point(39, 163);
            this.Source.Name = "Source";
            this.Source.Size = new System.Drawing.Size(57, 19);
            this.Source.TabIndex = 2;
            this.Source.Text = "Source";
            // 
            // Source_Ip
            // 
            this.Source_Ip.AutoSize = true;
            this.Source_Ip.Location = new System.Drawing.Point(39, 209);
            this.Source_Ip.Name = "Source_Ip";
            this.Source_Ip.Size = new System.Drawing.Size(22, 19);
            this.Source_Ip.TabIndex = 3;
            this.Source_Ip.Text = "IP";
            // 
            // Source_Mac
            // 
            this.Source_Mac.AutoSize = true;
            this.Source_Mac.Location = new System.Drawing.Point(39, 251);
            this.Source_Mac.Name = "Source_Mac";
            this.Source_Mac.Size = new System.Drawing.Size(43, 19);
            this.Source_Mac.TabIndex = 4;
            this.Source_Mac.Text = "MAC";
            // 
            // textBox_Source_Ip
            // 
            this.textBox_Source_Ip.Location = new System.Drawing.Point(106, 201);
            this.textBox_Source_Ip.Name = "textBox_Source_Ip";
            this.textBox_Source_Ip.Size = new System.Drawing.Size(161, 27);
            this.textBox_Source_Ip.TabIndex = 5;
            this.textBox_Source_Ip.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox_Source_Mac
            // 
            this.textBox_Source_Mac.Location = new System.Drawing.Point(106, 243);
            this.textBox_Source_Mac.Name = "textBox_Source_Mac";
            this.textBox_Source_Mac.Size = new System.Drawing.Size(161, 27);
            this.textBox_Source_Mac.TabIndex = 6;
            this.textBox_Source_Mac.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox_Source_Mask
            // 
            this.textBox_Source_Mask.Location = new System.Drawing.Point(106, 290);
            this.textBox_Source_Mask.Name = "textBox_Source_Mask";
            this.textBox_Source_Mask.Size = new System.Drawing.Size(161, 27);
            this.textBox_Source_Mask.TabIndex = 13;
            this.textBox_Source_Mask.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // textBox_Target_Mac
            // 
            this.textBox_Target_Mac.Location = new System.Drawing.Point(388, 243);
            this.textBox_Target_Mac.Name = "textBox_Target_Mac";
            this.textBox_Target_Mac.Size = new System.Drawing.Size(161, 27);
            this.textBox_Target_Mac.TabIndex = 11;
            this.textBox_Target_Mac.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox_Target_Ip
            // 
            this.textBox_Target_Ip.Location = new System.Drawing.Point(388, 201);
            this.textBox_Target_Ip.Name = "textBox_Target_Ip";
            this.textBox_Target_Ip.Size = new System.Drawing.Size(161, 27);
            this.textBox_Target_Ip.TabIndex = 10;
            this.textBox_Target_Ip.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // Target_Mac
            // 
            this.Target_Mac.AutoSize = true;
            this.Target_Mac.Location = new System.Drawing.Point(321, 251);
            this.Target_Mac.Name = "Target_Mac";
            this.Target_Mac.Size = new System.Drawing.Size(43, 19);
            this.Target_Mac.TabIndex = 9;
            this.Target_Mac.Text = "MAC";
            // 
            // Target_Ip
            // 
            this.Target_Ip.AutoSize = true;
            this.Target_Ip.Location = new System.Drawing.Point(321, 209);
            this.Target_Ip.Name = "Target_Ip";
            this.Target_Ip.Size = new System.Drawing.Size(22, 19);
            this.Target_Ip.TabIndex = 8;
            this.Target_Ip.Text = "IP";
            // 
            // Target
            // 
            this.Target.AutoSize = true;
            this.Target.Location = new System.Drawing.Point(321, 163);
            this.Target.Name = "Target";
            this.Target.Size = new System.Drawing.Size(54, 19);
            this.Target.TabIndex = 7;
            this.Target.Text = "Target";
            // 
            // Mask
            // 
            this.Mask.AutoSize = true;
            this.Mask.Location = new System.Drawing.Point(39, 298);
            this.Mask.Name = "Mask";
            this.Mask.Size = new System.Drawing.Size(46, 19);
            this.Mask.TabIndex = 12;
            this.Mask.Text = "Mask";
            // 
            // textBox_Server_Ip
            // 
            this.textBox_Server_Ip.Location = new System.Drawing.Point(125, 54);
            this.textBox_Server_Ip.Name = "textBox_Server_Ip";
            this.textBox_Server_Ip.Text = "127.0.0.1";
            this.textBox_Server_Ip.Size = new System.Drawing.Size(161, 27);
            this.textBox_Server_Ip.TabIndex = 15;
            this.textBox_Server_Ip.TextChanged += new System.EventHandler(this.textBox1_TextChanged_2);
            // 
            // Server_Ip
            // 
            this.Server_Ip.AutoSize = true;
            this.Server_Ip.Location = new System.Drawing.Point(39, 62);
            this.Server_Ip.Name = "Server_Ip";
            this.Server_Ip.Size = new System.Drawing.Size(71, 19);
            this.Server_Ip.TabIndex = 14;
            this.Server_Ip.Text = "Server IP";
            this.Server_Ip.Click += new System.EventHandler(this.Server_Ip_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(306, 54);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(69, 29);
            this.Connect.TabIndex = 16;
            this.Connect.Text = "連線";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // State
            // 
            this.State.AutoSize = true;
            this.State.ForeColor = System.Drawing.SystemColors.Highlight;
            this.State.Location = new System.Drawing.Point(397, 57);
            this.State.Name = "State";
            this.State.Size = new System.Drawing.Size(54, 19);
            this.State.TabIndex = 17;
            this.State.Text = "未連線";
            // 
            // ARP_Spoofing_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 477);
            this.Controls.Add(this.State);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.textBox_Server_Ip);
            this.Controls.Add(this.Server_Ip);
            this.Controls.Add(this.textBox_Source_Mask);
            this.Controls.Add(this.Mask);
            this.Controls.Add(this.textBox_Target_Mac);
            this.Controls.Add(this.textBox_Target_Ip);
            this.Controls.Add(this.Target_Mac);
            this.Controls.Add(this.Target_Ip);
            this.Controls.Add(this.Target);
            this.Controls.Add(this.textBox_Source_Mac);
            this.Controls.Add(this.textBox_Source_Ip);
            this.Controls.Add(this.Source_Mac);
            this.Controls.Add(this.Source_Ip);
            this.Controls.Add(this.Source);
            this.Controls.Add(this.ARP_Start);
            this.Controls.Add(this.ARP_Stop);
            this.Name = "ARP_Spoofing_Client";
            this.Text = "ARP Spoofing Client";
            this.Load += new System.EventHandler(this.ARP_Spoofing_Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ARP_Stop;
        private System.Windows.Forms.Button ARP_Start;
        private System.Windows.Forms.Label Source;
        private System.Windows.Forms.Label Source_Ip;
        private System.Windows.Forms.Label Source_Mac;
        private System.Windows.Forms.TextBox textBox_Source_Ip;
        private System.Windows.Forms.TextBox textBox_Source_Mac;
        private System.Windows.Forms.TextBox textBox_Target_Ip;
        private System.Windows.Forms.TextBox textBox_Target_Mac;
        private System.Windows.Forms.Label Target_Mac;
        private System.Windows.Forms.Label Target_Ip;
        private System.Windows.Forms.Label Target;
        private System.Windows.Forms.TextBox textBox_Source_Mask;
        private System.Windows.Forms.Label Mask;
        private System.Windows.Forms.TextBox textBox_Server_Ip;
        private System.Windows.Forms.Label Server_Ip;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label State;
    }
}

