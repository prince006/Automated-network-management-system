using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Smart_NET
{
	/// <summary>
	/// Summary description for Form_Agreement.
	/// </summary>
	public class Form_Agreement : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.CheckBox checkBox_agreement;
        private System.Windows.Forms.TextBox textBox_agreement;
        private bool b_agreement;

		public Form_Agreement()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
            this.button_ok.Enabled=false;
            this.b_agreement=false;
		}

        public bool get_agreement()
        {
            this.ShowDialog();
            return this.b_agreement;
        }
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Agreement));
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.checkBox_agreement = new System.Windows.Forms.CheckBox();
            this.textBox_agreement = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(80, 104);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(160, 104);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // checkBox_agreement
            // 
            this.checkBox_agreement.Location = new System.Drawing.Point(8, 80);
            this.checkBox_agreement.Name = "checkBox_agreement";
            this.checkBox_agreement.Size = new System.Drawing.Size(264, 16);
            this.checkBox_agreement.TabIndex = 0;
            this.checkBox_agreement.Text = "I understand and I accept the conditions";
            this.checkBox_agreement.CheckedChanged += new System.EventHandler(this.checkBox_agreement_CheckedChanged);
            // 
            // textBox_agreement
            // 
            this.textBox_agreement.Location = new System.Drawing.Point(8, 8);
            this.textBox_agreement.Multiline = true;
            this.textBox_agreement.Name = "textBox_agreement";
            this.textBox_agreement.ReadOnly = true;
            this.textBox_agreement.Size = new System.Drawing.Size(296, 64);
            this.textBox_agreement.TabIndex = 3;
            this.textBox_agreement.Text = resources.GetString("textBox_agreement.Text");
            // 
            // Form_Agreement
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(312, 136);
            this.Controls.Add(this.textBox_agreement);
            this.Controls.Add(this.checkBox_agreement);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Agreement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agreement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        private void button_ok_Click(object sender, System.EventArgs e)
        {
            this.b_agreement=true;
            this.Close();
        }

        private void button_cancel_Click(object sender, System.EventArgs e)
        {
            this.Close();        
        }

        private void checkBox_agreement_CheckedChanged(object sender, System.EventArgs e)
        {
            this.button_ok.Enabled=this.checkBox_agreement.Checked;
        }
	}
}
