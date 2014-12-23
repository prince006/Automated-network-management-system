using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace Smart_NET
{

    public class FormTCPServer : Smart_NET.CommonTelnetForm
    {

        private System.ComponentModel.IContainer components = null;

        public FormTCPServer()
        {
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
        }

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            this.srv.stop();
            srv.event_Socket_Server_Error-=new easy_socket.tcp.Socket_Server_Error_EventHandler(server_error);
            srv.event_Socket_Server_New_Connection-=new easy_socket.tcp.Socket_Server_New_Connection_EventHandler(server_new_connection);
            srv.event_Socket_Server_Started-=new easy_socket.tcp.Socket_Server_Started_EventHandler(server_started);
            srv.event_Socket_Server_Stopped-=new easy_socket.tcp.Socket_Server_Stopped_EventHandler(server_stopped);
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Designer generated code
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_stop = new System.Windows.Forms.Button();
            this.button_restart = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(368, 342);
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 256);
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.Size = new System.Drawing.Size(256, 85);
            this.textBox_editable.TabIndex = 0;
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.button_stop);
            this.panel_control.Controls.Add(this.button_restart);
            this.panel_control.Location = new System.Drawing.Point(256, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(112, 342);
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.Size = new System.Drawing.Size(256, 256);
            this.textBox_telnet.TabIndex = 0;
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(20, 24);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(72, 23);
            this.button_stop.TabIndex = 0;
            this.button_stop.Text = "Stop";
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_restart
            // 
            this.button_restart.Location = new System.Drawing.Point(20, 56);
            this.button_restart.Name = "button_restart";
            this.button_restart.Size = new System.Drawing.Size(72, 23);
            this.button_restart.TabIndex = 1;
            this.button_restart.Text = "Restart";
            this.button_restart.Click += new System.EventHandler(this.button_restart_Click);
            // 
            // FormTCPServer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(368, 342);
            this.Name = "FormTCPServer";
            this.panel.ResumeLayout(false);
            this.panel_control.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion




        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_restart;

        private int myport;

        private easy_socket.tcp.Socket_Server srv;
        public void new_tcp_server(string ip,int port)
        {
            this.Text="TCP Server IP: "+ip+" Port: "+port.ToString();
            myport=port;
            srv=new easy_socket.tcp.Socket_Server(ip,port);
            srv.event_Socket_Server_Error+=new easy_socket.tcp.Socket_Server_Error_EventHandler(server_error);
            srv.event_Socket_Server_New_Connection +=new easy_socket.tcp.Socket_Server_New_Connection_EventHandler(server_new_connection);
            srv.event_Socket_Server_Started +=new easy_socket.tcp.Socket_Server_Started_EventHandler(server_started);
            srv.event_Socket_Server_Stopped +=new easy_socket.tcp.Socket_Server_Stopped_EventHandler(server_stopped);
            srv.start();
        }
        private void server_error(easy_socket.tcp.Socket_Server s,easy_socket.tcp.EventArgs_Exception e)
        {
            this.textBox_telnet_add("Error: " + e.exception.Message+ "\r\n");
            this.enable_button(false);
        }

        private void server_new_connection(easy_socket.tcp.Socket_Server s,easy_socket.tcp.EventArgs_Socket e)
        {
            System.Net.IPEndPoint ipep=(System.Net.IPEndPoint)e.socket.RemoteEndPoint;
            this.textBox_telnet_add("New tcp client from "+ ipep.Address +" port " + ipep.Port.ToString() +".\r\n");
            this.Invoke(new make_new_form_with_principal_thread_Handler(make_new_form_with_principal_thread), new Object[] {e.socket});
        }


        public delegate void make_new_form_with_principal_thread_Handler(System.Net.Sockets.Socket socket);


        protected virtual void make_new_form_with_principal_thread(System.Net.Sockets.Socket socket)
        {
            FormTCPClient frm_clt=new FormTCPClient();
            frm_clt.set_mdi_parent(this.MdiParent);
            frm_clt.Show();
            frm_clt.new_tcp_client(socket,myport);
        }

        private void server_started(easy_socket.tcp.Socket_Server s,System.EventArgs e)
        {
            this.textBox_telnet_add("Server started.\r\n");
            this.enable_button(true);
        }
        private void server_stopped(easy_socket.tcp.Socket_Server s,System.EventArgs e)
        {
            this.textBox_telnet_add("Server stopped.\r\n");
            this.enable_button(false);
        }
        private void enable_button(bool b_started)
        {
            this.button_restart.Enabled=!b_started;
            this.button_stop.Enabled=b_started;
        }
        private void button_stop_Click(object sender, System.EventArgs e)
        {
            srv.stop();
        }

        private void button_restart_Click(object sender, System.EventArgs e)
        {
            srv.start();
        }

    }
}

