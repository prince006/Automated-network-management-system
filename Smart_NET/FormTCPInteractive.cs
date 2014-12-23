using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Smart_NET
{
	/// <summary>
	/// Summary description for FormTCPInteractive.
	/// </summary>
	public class FormTCPInteractive : Smart_NET.CommonInteractiveForm
	{
        #region design
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;



		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel_interactive.SuspendLayout();
            this.panel_cmd.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_send_data_options.SuspendLayout();
            this.groupBox_clt_to_srv_options.SuspendLayout();
            this.groupBox_srv_to_clt_options.SuspendLayout();
            this.groupBox_close.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_interactive
            // 
            this.panel_interactive.Name = "panel_interactive";
            // 
            // panel_cmd
            // 
            this.panel_cmd.Name = "panel_cmd";
            // 
            // panel1
            // 
            this.panel1.Name = "panel1";
            // 
            // textBox_editable
            // 
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Name = "panel2";
            // 
            // button_clear
            // 
            this.button_clear.Name = "button_clear";
            this.button_clear.TabIndex = 7;
            // 
            // groupBox_send_data_options
            // 
            this.groupBox_send_data_options.Name = "groupBox_send_data_options";
            this.groupBox_send_data_options.TabIndex = 5;
            // 
            // radioButton_send_to_clt
            // 
            this.radioButton_send_to_clt.Name = "radioButton_send_to_clt";
            // 
            // radioButton_send_to_srv
            // 
            this.radioButton_send_to_srv.Name = "radioButton_send_to_srv";
            // 
            // groupBox_clt_to_srv_options
            // 
            this.groupBox_clt_to_srv_options.Name = "groupBox_clt_to_srv_options";
            this.groupBox_clt_to_srv_options.TabIndex = 1;
            // 
            // radioButton_clt_to_srv_ask
            // 
            this.radioButton_clt_to_srv_ask.Name = "radioButton_clt_to_srv_ask";
            this.radioButton_clt_to_srv_ask.TabIndex = 1;
            // 
            // radioButton_clt_to_srv_block
            // 
            this.radioButton_clt_to_srv_block.Name = "radioButton_clt_to_srv_block";
            this.radioButton_clt_to_srv_block.TabIndex = 2;
            // 
            // radioButton_clt_to_srv_allow
            // 
            this.radioButton_clt_to_srv_allow.Name = "radioButton_clt_to_srv_allow";
            // 
            // groupBox_srv_to_clt_options
            // 
            this.groupBox_srv_to_clt_options.Name = "groupBox_srv_to_clt_options";
            this.groupBox_srv_to_clt_options.TabIndex = 0;
            // 
            // radioButton_srv_to_clt_ask
            // 
            this.radioButton_srv_to_clt_ask.Name = "radioButton_srv_to_clt_ask";
            this.radioButton_srv_to_clt_ask.TabIndex = 1;
            // 
            // radioButton_srv_to_clt_block
            // 
            this.radioButton_srv_to_clt_block.Name = "radioButton_srv_to_clt_block";
            this.radioButton_srv_to_clt_block.TabIndex = 2;
            // 
            // radioButton_srv_to_clt_allow
            // 
            this.radioButton_srv_to_clt_allow.Name = "radioButton_srv_to_clt_allow";
            // 
            // button_close
            // 
            this.button_close.Name = "button_close";
            this.button_close.TabIndex = 8;
            // 
            // button_send
            // 
            this.button_send.Name = "button_send";
            this.button_send.TabIndex = 6;
            // 
            // checkBox_send_hexa_data
            // 
            this.checkBox_send_hexa_data.Name = "checkBox_send_hexa_data";
            this.checkBox_send_hexa_data.TabIndex = 4;
            // 
            // checkBox_transmit_close
            // 
            this.checkBox_transmit_close.Name = "checkBox_transmit_close";
            this.checkBox_transmit_close.TabIndex = 2;
            // 
            // groupBox_close
            // 
            this.groupBox_close.Name = "groupBox_close";
            this.groupBox_close.TabIndex = 3;
            // 
            // checkBox_close_server_socket
            // 
            this.checkBox_close_server_socket.Name = "checkBox_close_server_socket";
            // 
            // checkBox_close_client_socket
            // 
            this.checkBox_close_client_socket.Name = "checkBox_close_client_socket";
            // 
            // FormTCPInteractive
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(560, 406);
            this.Name = "FormTCPInteractive";
            this.panel_interactive.ResumeLayout(false);
            this.panel_cmd.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox_send_data_options.ResumeLayout(false);
            this.groupBox_clt_to_srv_options.ResumeLayout(false);
            this.groupBox_srv_to_clt_options.ResumeLayout(false);
            this.groupBox_close.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        #endregion

        #region constructor / destructor
		public FormTCPInteractive()
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
            this.remove_events();
            this.socket_to_clt.close();
            this.socket_to_srv.close();
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #endregion

        #region members
        private easy_socket.tcp.Socket_Data socket_to_clt;
        private easy_socket.tcp.Socket_Data socket_to_srv;

        // events are here to avoid mixing comment and data of differents events
        private System.Threading.AutoResetEvent evtNotWrittingData;
        private System.Threading.ManualResetEvent evtStop;
        private System.Threading.ManualResetEvent evtConnectedToServer;
        private System.Threading.WaitHandle[] HexaViewWaitHandles;


        #endregion

        public void new_tcp_interactive(System.Net.Sockets.Socket s,string remote_srv_ip,int remote_srv_port)
        {
            this.evtNotWrittingData=new System.Threading.AutoResetEvent(true);
            this.evtStop=new System.Threading.ManualResetEvent(false);
            this.evtConnectedToServer=new System.Threading.ManualResetEvent(false);
            this.HexaViewWaitHandles=new System.Threading.WaitHandle[]{this.evtStop,this.evtNotWrittingData};
            
            System.Net.IPEndPoint ipep=(System.Net.IPEndPoint)s.RemoteEndPoint;
            this.Text="Transfer between client "+ipep.Address.ToString()+":"+ipep.Port+" and server "+remote_srv_ip+":"+remote_srv_port.ToString();
            this.socket_to_clt=new easy_socket.tcp.Socket_Data(s);
            this.socket_to_clt.event_Socket_Data_Closed_by_Remote_Side+= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_clt.event_Socket_Data_DataArrival +=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_clt.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);

            this.socket_to_srv=new easy_socket.tcp.Socket_Data();
            this.socket_to_srv.event_Socket_Data_Closed_by_Remote_Side+= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_srv.event_Socket_Data_Connected_To_Remote_Host +=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket_to_srv.event_Socket_Data_DataArrival +=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_srv.event_Socket_Data_Error+=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);
            this.socket_to_srv.connect(remote_srv_ip,remote_srv_port);
        }

        protected void remove_events()
        {
            this.socket_to_clt.event_Socket_Data_Closed_by_Remote_Side-= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_clt.event_Socket_Data_DataArrival-=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_clt.event_Socket_Data_Error-=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);

            this.socket_to_srv.event_Socket_Data_Closed_by_Remote_Side-= new easy_socket.tcp.Socket_Data_Closed_by_Remote_Side_EventHandler(socket_closed_by_remote_side);
            this.socket_to_srv.event_Socket_Data_Connected_To_Remote_Host-=new easy_socket.tcp.Socket_Data_Connected_To_Remote_Host_EventHandler(socket_connected_to_remote_host);
            this.socket_to_srv.event_Socket_Data_DataArrival-=new easy_socket.tcp.Socket_Data_DataArrival_EventHandler(socket_data_arrival);
            this.socket_to_srv.event_Socket_Data_Error-=new easy_socket.tcp.Socket_Data_Error_EventHandler(socket_error);
        }

        protected void socket_error(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_Exception e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            this.add_info("Socket Error: "+e.exception.Message);
            this.check_close(sender);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected void socket_closed_by_remote_side(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;
            if (sender==this.socket_to_srv)
                this.evtConnectedToServer.Reset();
            this.add_info("Connection closed by "+sender.RemoteIP+":"+sender.RemotePort);
            this.check_close(sender);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected void check_close(easy_socket.tcp.Socket_Data sender)
        {
            if (this.checkBox_transmit_close.Checked)
            {
                this.enable_state(false);
                if (sender==this.socket_to_clt)
                {
                    this.socket_to_srv.close();
                    this.evtConnectedToServer.Reset();
                }
                else
                    this.socket_to_clt.close();
            }
            else
            {
                if (sender==this.socket_to_clt)
                {
                    this.radioButton_send_to_srv.Checked=true;
                    this.radioButton_send_to_clt.Enabled=false;
                }
                else
                {
                    this.radioButton_send_to_clt.Checked=true;
                    this.radioButton_send_to_srv.Enabled=false;
                }
            }
        }
        protected void socket_connected_to_remote_host(easy_socket.tcp.Socket_Data sender, EventArgs e)
        {
            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            this.evtConnectedToServer.Set();

            this.add_info("Connected to "+sender.RemoteIP+":"+sender.RemotePort);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected void socket_data_arrival(easy_socket.tcp.Socket_Data sender, easy_socket.tcp.EventArgs_ReceiveDataSocket e)
        {
            int res;
            System.Threading.WaitHandle[] mHexaViewWaitHandles=new System.Threading.WaitHandle[]{this.evtStop,this.evtConnectedToServer};

            // if sender is client
            if (sender==this.socket_to_clt)
            {
                // wait to be connected to server before trying to translate data
                res=System.Threading.WaitHandle.WaitAny(mHexaViewWaitHandles);
                if (res==0) // close event
                    return;
            }

            res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            System.Drawing.Color color=System.Drawing.Color.Black;
            bool b_transmit=false;
            if (sender==this.socket_to_clt)
            {
                // transfer data if required
                if (this.radioButton_clt_to_srv_allow.Checked)
                    b_transmit=true;
                else if (this.radioButton_clt_to_srv_block.Checked)
                    b_transmit=false;
                else// query
                {
                    string msg="Do you want to allow the transfer of following data from "+sender.RemoteIP+":"+sender.RemotePort.ToString()+"?\r\n"
                                +"Hexa Data:\r\n"
                                +this.split_in_multiple_lines(easy_socket.hexa_convert.byte_to_hexa(e.buffer),50)
                                +"Text Data:\r\n"
                                +this.split_in_multiple_lines(System.Text.Encoding.Default.GetString(e.buffer),50);
                                
                    b_transmit=(MessageBox.Show(this,msg,"Tcp Interactive",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes);
                }
                if (b_transmit)
                {
                    this.socket_to_srv.send(e.buffer);
                    color=System.Drawing.Color.Blue;
                }
                else
                    color=System.Drawing.Color.Violet;
            }
            else
            {
                // transfer data if required
                if (this.radioButton_srv_to_clt_allow.Checked)
                    b_transmit=true;
                else if (this.radioButton_srv_to_clt_block.Checked)
                    b_transmit=false;
                else// query
                {
                    string msg="Do you want to allow the transfer of following data from "+sender.RemoteIP+":"+sender.RemotePort.ToString()+"?\r\n"
                        +"Hexa Data:\r\n"
                        +this.split_in_multiple_lines(easy_socket.hexa_convert.byte_to_hexa(e.buffer),50)
                        +"Text Data:\r\n"
                        +this.split_in_multiple_lines(System.Text.Encoding.Default.GetString(e.buffer),50);
                    b_transmit=(MessageBox.Show(this,msg,"Tcp Interactive",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes);
                }
                if (b_transmit)
                {
                    this.socket_to_clt.send(e.buffer);
                    color=System.Drawing.Color.Black;
                }
                else
                    color=System.Drawing.Color.Red;
            }
            if (b_transmit)
                this.add_info("Data from "+sender.RemoteIP+":"+sender.RemotePort.ToString());
            else
                this.add_info("Data from "+sender.RemoteIP+":"+sender.RemotePort.ToString()+"(Blocked)");
            // color depends of sender && user action
            this.add_data(e.buffer,color);
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        #region GUI events

        protected override void button_close_Click(object sender, System.EventArgs e)
        {
            if (this.checkBox_close_client_socket.Checked)
                this.socket_to_clt.close();
            if (this.checkBox_close_server_socket.Checked)
            {
                this.socket_to_srv.close();
                this.evtConnectedToServer.Reset();
            }
        }

        protected override void button_send_Click(object sender, System.EventArgs e)
        {
            byte[] data=this.get_data();
            if (data==null)
                return;

            int res=System.Threading.WaitHandle.WaitAny(this.HexaViewWaitHandles);
            if (res==0) // close event
                return;

            if (this.radioButton_send_to_clt.Checked)
            {
                this.socket_to_clt.send(data);
                this.add_info("Data send to "+this.socket_to_clt.RemoteIP+":"+this.socket_to_clt.RemotePort.ToString());
                this.add_data(data,System.Drawing.Color.Gray);
            }
            else
            {
                this.socket_to_srv.send(data);
                this.add_info("Data send to "+this.socket_to_srv.RemoteIP+":"+this.socket_to_srv.RemotePort.ToString());
                this.add_data(data,System.Drawing.Color.Cyan);
            }
            this.textBox_editable.Text="";
            this.refresh_data();
            this.evtNotWrittingData.Set();
        }

        protected override void FormTCPInteractive_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.evtStop.Set();
        }
        #endregion

	}
}
