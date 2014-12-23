using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Smart_NET
{
	/// <summary>
	/// Summary description for FormTCPInteractiveProxyServer.
	/// </summary>
	public class FormTCPInteractiveProxyServer : Smart_NET.FormTCPServer
	{
        private string remote_srv_ip="127.0.0.1";
        private int remote_srv_port=6501;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        public FormTCPInteractiveProxyServer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
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
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Name = "panel";
            // 
            // textBox_editable
            // 
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.TabIndex = 0;
            // 
            // panel_control
            // 
            this.panel_control.Name = "panel_control";
            this.panel_control.TabIndex = 1;
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.TabIndex = 0;
            // 
            // FormTCPInteractiveProxyServer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(368, 342);
            this.Name = "FormTCPInteractiveProxyServer";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public void new_tcp_interactive_proxy_server(string srv_proxy_ip,int srv_proxy_port,string remote_srv_ip,int remote_srv_port)
        {
            base.new_tcp_server(srv_proxy_ip,srv_proxy_port);
            this.Text="Interactive Proxy server on "+srv_proxy_ip+":"+srv_proxy_port.ToString()+" for "+remote_srv_ip+":"+remote_srv_port.ToString();
            this.remote_srv_ip=remote_srv_ip;
            this.remote_srv_port=remote_srv_port;
        }

        protected override void make_new_form_with_principal_thread(System.Net.Sockets.Socket socket)
        {
            FormTCPInteractive frm_clt=new FormTCPInteractive();
            frm_clt.set_mdi_parent(this.MdiParent);
            frm_clt.Show();
            frm_clt.new_tcp_interactive(socket,this.remote_srv_ip,this.remote_srv_port);
        }
	}
}
