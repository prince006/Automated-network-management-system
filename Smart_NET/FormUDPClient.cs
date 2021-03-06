using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace Smart_NET
{
    public class FormUDPClient : Smart_NET.CommonTelnetForm
    {
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.CheckBox checkBox_crlf;
        private System.Windows.Forms.CheckBox checkBox_send_data_on_return;
        private System.Windows.Forms.CheckBox checkBox_send_hexa_data;
        private System.Windows.Forms.Button button_send_file;
        private System.ComponentModel.IContainer components = null;

        public FormUDPClient()
        {
            InitializeComponent();
            Tools.GUI.XPStyle.MakeXPStyle(this);
            this.enable_textBox_editable();
            this.textBox_editable.Focus();
        }

        /// <summary>
        /// Nettoyage des ressources utilis�es.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
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
        /// M�thode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette m�thode avec l'�diteur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_send = new System.Windows.Forms.Button();
            this.checkBox_crlf = new System.Windows.Forms.CheckBox();
            this.checkBox_send_data_on_return = new System.Windows.Forms.CheckBox();
            this.checkBox_send_hexa_data = new System.Windows.Forms.CheckBox();
            this.button_send_file = new System.Windows.Forms.Button();
            this.panel_control.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(368, 358);
            // 
            // textBox_editable
            // 
            this.textBox_editable.Location = new System.Drawing.Point(0, 268);
            this.textBox_editable.Name = "textBox_editable";
            this.textBox_editable.Size = new System.Drawing.Size(256, 89);
            this.textBox_editable.TabIndex = 0;
            this.textBox_editable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_editable_KeyPress);
            this.textBox_editable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_editable_KeyUp);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.button_send_file);
            this.panel_control.Controls.Add(this.checkBox_send_hexa_data);
            this.panel_control.Controls.Add(this.checkBox_send_data_on_return);
            this.panel_control.Controls.Add(this.checkBox_crlf);
            this.panel_control.Controls.Add(this.button_send);
            this.panel_control.Location = new System.Drawing.Point(256, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(112, 358);
            this.panel_control.TabIndex = 1;
            // 
            // textBox_telnet
            // 
            this.textBox_telnet.Name = "textBox_telnet";
            this.textBox_telnet.Size = new System.Drawing.Size(256, 268);
            this.textBox_telnet.TabIndex = 2;
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(19, 16);
            this.button_send.Name = "button_send";
            this.button_send.TabIndex = 0;
            this.button_send.Text = "Send";
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // checkBox_crlf
            // 
            this.checkBox_crlf.Checked = true;
            this.checkBox_crlf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_crlf.Location = new System.Drawing.Point(8, 48);
            this.checkBox_crlf.Name = "checkBox_crlf";
            this.checkBox_crlf.Size = new System.Drawing.Size(96, 32);
            this.checkBox_crlf.TabIndex = 1;
            this.checkBox_crlf.Text = "send \\r\\n after data";
            // 
            // checkBox_send_data_on_return
            // 
            this.checkBox_send_data_on_return.Checked = true;
            this.checkBox_send_data_on_return.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_send_data_on_return.Location = new System.Drawing.Point(4, 155);
            this.checkBox_send_data_on_return.Name = "checkBox_send_data_on_return";
            this.checkBox_send_data_on_return.Size = new System.Drawing.Size(104, 48);
            this.checkBox_send_data_on_return.TabIndex = 3;
            this.checkBox_send_data_on_return.Text = "Send data when return key is pressed";
            // 
            // checkBox_send_hexa_data
            // 
            this.checkBox_send_hexa_data.Location = new System.Drawing.Point(8, 136);
            this.checkBox_send_hexa_data.Name = "checkBox_send_hexa_data";
            this.checkBox_send_hexa_data.Size = new System.Drawing.Size(104, 16);
            this.checkBox_send_hexa_data.TabIndex = 2;
            this.checkBox_send_hexa_data.Text = "Send hexa data";
            this.checkBox_send_hexa_data.CheckedChanged += new System.EventHandler(this.checkBox_send_hexa_data_CheckedChanged);
            // 
            // button_send_file
            // 
            this.button_send_file.Location = new System.Drawing.Point(19, 232);
            this.button_send_file.Name = "button_send_file";
            this.button_send_file.TabIndex = 4;
            this.button_send_file.Text = "Send File";
            this.button_send_file.Click += new System.EventHandler(this.button_send_file_Click);
            // 
            // FormUDPClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(368, 358);
            this.Name = "FormUDPClient";
            this.panel_control.ResumeLayout(false);

        }
        #endregion


        easy_socket.udp.Socket_Data clt;
        public void new_udp_client(string ip,int port,bool force_local_port,int local_port)
        {
            this.Text="UDP Send to "+ip+":"+port.ToString();
            if (force_local_port)
            {
                try
                {
                    this.Text+=" local port:"+local_port.ToString();
                    clt=new easy_socket.udp.Socket_Data(local_port,ip,port);//error are not catch
                }
                catch (Exception e)
                {
                    this.textBox_telnet_add(e.Message+"\r\n");
                    if (e is System.Net.Sockets.SocketException)
                    {
                        System.Net.Sockets.SocketException se=(System.Net.Sockets.SocketException)e;
                        if (se.ErrorCode==10048)
                        {
                            if (!this.textBox_telnet.Text.EndsWith("\r\n"))    this.textBox_telnet_add("\r\n");
                            this.textBox_telnet_add("Please wait few seconds before retry.\r\n");
                        }
                    }
                    return;
                }
            }
            else
            {
                clt=new easy_socket.udp.Socket_Data(ip,port);
            }
            clt.event_Socket_Data_Error+=new easy_socket.udp.Socket_Data_Error_EventHandler(client_error);
        }

        private void client_error(easy_socket.udp.Socket_Data s,easy_socket.udp.EventArgs_Exception e)
        {
            if (!this.textBox_telnet.Text.EndsWith("\r\n"))    this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add("Error: " + e.exception.Message+ "\r\n");
        }

        private void button_send_Click(object sender, System.EventArgs e)
        {
            send();
        }
        private void send()
        {
            string strdata;
            strdata=this.textBox_editable.Text;

            if (strdata=="")
                return;
            if (this.checkBox_send_hexa_data.Checked)
            {
                byte[] b=easy_socket.hexa_convert.hexa_to_byte(strdata);
                if (b==null)
                {
                    MessageBox.Show(this,"Please enter hexa data","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                else
                    clt.send(b);
            }
            else
            {
                if (this.checkBox_crlf.Checked)
                    strdata+="\r\n";
                clt.send(strdata);
            }

            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
            this.textBox_telnet_add(strdata);
            this.textBox_editable.Text="";
            if (!this.textBox_telnet.Text.EndsWith("\r\n")) this.textBox_telnet_add("\r\n");
        }

        protected void textBox_editable_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!this.checkBox_send_data_on_return.Checked)
                return;
            if (e.KeyChar==13) 
                send();
        }
        private void textBox_editable_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!this.checkBox_send_data_on_return.Checked)
                return;
            if (e.KeyValue==13) 
                this.textBox_editable.Text="";
        }

        private void checkBox_send_hexa_data_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox_send_hexa_data.Checked)
            {
                // disable incompatible options (\r\n)
                this.checkBox_crlf.Checked=false;
                this.checkBox_crlf.Enabled=false;
            }
            else
                this.checkBox_crlf.Enabled=true;
        }

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private void button_send_file_Click(object sender, System.EventArgs e)
        {
            this.openFileDialog=new System.Windows.Forms.OpenFileDialog();;
            this.openFileDialog.CheckFileExists=true;
            this.openFileDialog.Filter="All files (*.*)|*.*";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            this.openFileDialog.ShowDialog(this);
        }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.textBox_telnet_add("File sent :"+this.openFileDialog.FileName+"\r\n");
            byte[] DataBuffer=Tools.IO.file_access.read_binary(this.openFileDialog.FileName);
            this.openFileDialog.Dispose();
            this.clt.send(DataBuffer);
        }

    }
}

