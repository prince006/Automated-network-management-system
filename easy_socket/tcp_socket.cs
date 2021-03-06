using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace easy_socket.tcp
{

#region globals event declaration

    #region Socket_Server events

    public delegate void Socket_Server_Error_EventHandler(Socket_Server sender, EventArgs_Exception e);
    public delegate void Socket_Server_New_Connection_EventHandler(Socket_Server sender, EventArgs_Socket s);
    public delegate void Socket_Server_Stopped_EventHandler(Socket_Server sender, EventArgs e);// no event args
    public delegate void Socket_Server_Started_EventHandler(Socket_Server sender, EventArgs e);// no event args

    #endregion

    #region Socket_Data events
        public delegate void Socket_Data_Error_EventHandler(Socket_Data sender, EventArgs_Exception e);
        public delegate void Socket_Data_DataArrival_EventHandler(Socket_Data sender, EventArgs_ReceiveDataSocket e);
        public delegate void Socket_Data_Closed_by_Remote_Side_EventHandler(Socket_Data sender, EventArgs e);// no event args
        public delegate void Socket_Data_Connected_To_Remote_Host_EventHandler(Socket_Data sender, EventArgs e);// no event args
        public delegate void Socket_Data_Send_Progress_EventHandler(Socket_Data sender, EventArgs_Send_Progress_Socket_Data e);
        public delegate void Socket_Data_Send_Completed_EventHandler(Socket_Data sender, EventArgs e);// no event args
    #endregion

#endregion

#region eventArgs object
    /// <summary>
    /// used by Socket_Data_Send_Progress_EventHandler
    /// contains bytes_sent,bytes_remaining,bytes_total
    /// </summary>
    public class EventArgs_Send_Progress_Socket_Data:EventArgs
    {
        /// <summary>
        /// number of byte that have been sent
        /// </summary>
        public readonly int bytes_sent;
        /// <summary>
        /// number of byte that remain to be sent
        /// </summary>
        public readonly int bytes_remaining;
        /// <summary>
        /// number of total bytes (bytes_sent+bytes_remaining)
        /// </summary>
        public readonly int bytes_total;
        public EventArgs_Send_Progress_Socket_Data(int bytes_sent,int bytes_remaining,int bytes_total)
        {
            this.bytes_sent=bytes_sent;
            this.bytes_remaining=bytes_remaining;
            this.bytes_total=bytes_total;
        }
    }


    /// <summary>
    /// used by Socket_Data_DataArrival_EventHandler
    /// contains buffer,buffer_size
    /// </summary>
    public class EventArgs_ReceiveDataSocket:EventArgs
    {
        /*
        /// <summary>
        /// data in string
        /// </summary>
        public readonly string data;
        */
        /// <summary>
        /// data in byte[]
        /// </summary>
        public readonly byte[] buffer;
        /// <summary>
        /// data length for data or buffer
        /// </summary>
        public readonly int buffer_size;
        //public EventArgs_ReceiveDataSocket(string strdata,byte[] bytebuffer,int intdata_size)
        public EventArgs_ReceiveDataSocket(byte[] bytebuffer,int intdata_size)
        {
            //data=strdata;
            buffer=bytebuffer;
            buffer_size=intdata_size;
        }
    }


    /// <summary>
    /// used to raise Exception
    /// contains exception
    /// </summary>
    public class EventArgs_Exception:EventArgs
    {
        public readonly Exception exception;
        public EventArgs_Exception(Exception e)
        {
            exception=e;
        }
    }


    /// <summary>
    /// used by event_Socket_Server_New_Connection
    /// contains socket
    /// </summary>
    public class EventArgs_Socket:EventArgs
    {
        public Socket socket;
        public EventArgs_Socket(Socket s)
        {
            socket=s;
        }
    }


#endregion

#region class Socket_Server
    public class Socket_Server
    {

    #region events
        
        public event Socket_Server_Error_EventHandler event_Socket_Server_Error;
        public event Socket_Server_New_Connection_EventHandler event_Socket_Server_New_Connection;
        public event Socket_Server_Stopped_EventHandler event_Socket_Server_Stopped;
        public event Socket_Server_Started_EventHandler event_Socket_Server_Started;

    #endregion

    #region const
        
        private const int default_port=0;//6500;
        private const int default_max_length_queue_for_pending_connections=10;

    #endregion

    #region members

        private int port;
        private string ip;
        private Socket srvsocket;
        private int max_length_queue_for_pending_connections;

    #endregion

    #region members_access

        public void set_max_length_queue_for_pending_connections(int max,bool restart_server_if_needed)
        {
            try
            {
                this.max_length_queue_for_pending_connections=max;
                if (!restart_server_if_needed) return ;
                listen();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public int get_max_length_queue_for_pending_connections()
        {
            return max_length_queue_for_pending_connections;
        }

        public void set_port(int port,bool restart_server_if_needed)
            // return the value of listen
        {
            try
            {
                this.port=port;
                if (!restart_server_if_needed) return ;
                listen();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int get_port()
        {
            return port;
        }
        public void set_ip(string ip,bool restart_server_if_needed)
        {
            try
            {
                this.ip=ip;
                if (!restart_server_if_needed) return ;
                listen();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string get_ip()
        {
            return this.ip;
        }


    #endregion

    #region constructors

        private void common_constructor()
        {
            this.max_length_queue_for_pending_connections=default_max_length_queue_for_pending_connections;
        }

        public Socket_Server()
        {
            this.port=default_port;
            this.common_constructor();
        }


        public Socket_Server(int port)
        {
            this.port=port;
            this.common_constructor();
        }


        public Socket_Server(string ip)
        {
            this.ip=ip;
            this.common_constructor();
        }

        public Socket_Server(string ip,int port)
        {
            this.ip=ip;
            this.port=port;
            this.common_constructor();
        }


    #endregion

    #region methodes

        public void start()
        {
            ThreadStart myThreadStart = new ThreadStart(listen);
            Thread myThread =new Thread(myThreadStart);
            myThread.Start();
        }

        public void stop()
        {
            try
            {
                if (this.srvsocket!=null) 
                    this.srvsocket.Close();
            }
            catch (Exception e)
            {
                if (this.event_Socket_Server_Error!=null)
                    this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
            }
        }

        /// <summary>
        /// make all needed to launch server
        /// send event_Socket_Server_Started if success
        /// </summary>
        private void listen()
        {
            try
            {
                IPHostEntry iphe;
                IPAddress ipaddr;
                IPEndPoint localep;

                //stop server if it was runing
                this.stop();

                // resolve ip
                if (ip=="" || ip==null) 
                {
                    ipaddr=System.Net.IPAddress.Any;
                }
                else
                {
                    try
                    {
                        // don't resolve if ip is like xxx.yyy.uuu.vvv
                        ipaddr=System.Net.IPAddress.Parse(ip);
                    }
                    catch
                    {
                        // else resolve
                        try
                        {
                            iphe=System.Net.Dns.Resolve(ip);
                        }
                        catch(Exception e)
                        {
                            this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
                            return;
                        }
                        ipaddr=iphe.AddressList[0];
                    }
                }
                // make local end point
                localep = new IPEndPoint(ipaddr,port);
                // create socket
                this.srvsocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                this.srvsocket.Bind(localep);
                this.port=((IPEndPoint)this.srvsocket.LocalEndPoint).Port;// in case of null port given
                this.srvsocket.Listen(max_length_queue_for_pending_connections);
                // send event_Socket_Server_Started
                if (this.event_Socket_Server_Started!=null)
                    this.event_Socket_Server_Started(this,new EventArgs());
                // accept connections
                Socket mySocket;
                while(true)
                {
                    mySocket=srvsocket.Accept();//blocking
                    // send event_Socket_Server_New_Connection
                    if (event_Socket_Server_New_Connection!=null)
#if (!DEBUG)
                        try// avoid to close server on some programmer's error in the eventhandler function
#endif
                        {
                            this.event_Socket_Server_New_Connection(this,new EventArgs_Socket(mySocket));
                        }
#if (!DEBUG)
                        catch{}
#endif
                }

            }

            catch (Exception e)
            {
                if (e is SocketException)
                {
                    SocketException se;
                    se=(SocketException) e;
                    if (se.ErrorCode==10004)// accept canceled because of socket closing
                    {
                        if (this.event_Socket_Server_Stopped!=null)
                            this.event_Socket_Server_Stopped(this,new EventArgs());
                        return;
                    }
                }
                // send event_Socket_Server_Error
                if (this.event_Socket_Server_Error!=null)
                    this.event_Socket_Server_Error(this,new EventArgs_Exception(e));
                // close socket
                this.stop();
                // send event_Socket_Server_Stopped
                if (this.event_Socket_Server_Stopped!=null)
                    this.event_Socket_Server_Stopped(this,new EventArgs());
            }
        }


    #endregion

    }
#endregion

#region class Socket_Data
    public class Socket_Data
    {

        #region events
        public event Socket_Data_Error_EventHandler event_Socket_Data_Error;
        public event Socket_Data_DataArrival_EventHandler event_Socket_Data_DataArrival;
        public event Socket_Data_Closed_by_Remote_Side_EventHandler event_Socket_Data_Closed_by_Remote_Side;
        public event Socket_Data_Connected_To_Remote_Host_EventHandler event_Socket_Data_Connected_To_Remote_Host;
        public event Socket_Data_Send_Progress_EventHandler event_Socket_Data_Send_Progress;
        public event Socket_Data_Send_Completed_EventHandler event_Socket_Data_Send_Completed;
        #endregion

        #region const
        private const int default_buffer_size=4096;
        #endregion

        #region members
        private                 IPEndPoint remote_IPendpoint;
        private                 Socket datasocket;
        private int             buffer_size;
        private string          my_connect_remote_ip;
        private int             my_connect_remote_port;
        private System.Collections.Queue send_queue;
        private bool            locally_closed=false;
        private bool            bfirst_connect=true;
        private bool            b_cancel_send=false;
        private int             my_local_port;
        private System.Threading.ManualResetEvent evt_more_data_to_send=new System.Threading.ManualResetEvent(false);
        private System.Threading.ManualResetEvent evt_close=new System.Threading.ManualResetEvent(false);
        private System.Threading.AutoResetEvent evt_unlocked_send_queue=new System.Threading.AutoResetEvent(true);


        #endregion

        #region members_access
        public bool Connected
        {
            get
            {
               return this.datasocket.Connected;
            }
        }
        public IPEndPoint LocalIPEndPoint
        {
            get
            {
                return (IPEndPoint)datasocket.LocalEndPoint;
            }
        }
        public IPEndPoint RemoteIPEndPoint
        {
            get
            {
                return this.remote_IPendpoint;
            }
        }
        public string RemoteIP
        {
            get
            {
                return this.my_connect_remote_ip;
            }
        }
        public ushort RemotePort
        {
            get
            {
                return (ushort)this.my_connect_remote_port;
            }            
        }
    #endregion

    #region constructors

        #region constructors used for connecting to remote ip

        /// <summary>
        /// used for client socket. Just add event and use the connect methode after this constructor  
        /// </summary>
        public Socket_Data()
        {
            this.buffer_size=default_buffer_size;
            this.my_local_port=0;

            // create a socket
            this.datasocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

            this.common_constructor();
        }

        /// <summary>
        /// used for client socket. Just add event and use the connect methode after this constructor
        /// </summary>
        /// <param name="buffer_size">size of buffer used for sending data</param>
        public Socket_Data(int buffer_size)
        {
            this.buffer_size=buffer_size;
            this.my_local_port=0;

            // create a socket
            this.datasocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

            this.common_constructor();
        }
        /// <summary>
        /// used for client socket. Just add event and use the connect methode after this constructor
        /// </summary>
        /// <param name="buffer_size">size of buffer used for sending data</param>
        public Socket_Data(int buffer_size,int local_port)
        {
            this.buffer_size=buffer_size;
            this.my_local_port=local_port;

            // create a socket
            this.datasocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            this.common_constructor();
        }
        private void common_constructor()
        {
            // create a queue for sending messages
            this.send_queue=new System.Collections.Queue();
        }
        #endregion

        #region constructors used after an accept
        
        /// <summary>
        /// used after an accept
        /// launch a thread to receive data
        /// </summary>
        /// <param name="s"></param>
        public Socket_Data(Socket s)
        {
            this.buffer_size=default_buffer_size;
            this.common_constructor_with_connected_socket(s);
        }


        /// <summary>
        /// used after an accept
        /// launch a thread to receive data
        /// <param name="buffer_size">let you specify the size of the buffer used to send data</param>
        /// <param name="s"></param>
        /// <param name="buffer_size"></param>
        /// </summary>
        public Socket_Data(Socket s,int buffer_size)
        {
            this.buffer_size=buffer_size;
            this.common_constructor_with_connected_socket(s);
        }

        /// <summary>
        /// launch thread to receive data
        /// </summary>
        /// <param name="s"></param>
        private void common_constructor_with_connected_socket(Socket s)
        {
            this.bfirst_connect=false;
            this.datasocket=s;
            this.remote_IPendpoint=(IPEndPoint)datasocket.RemoteEndPoint;
            this.my_connect_remote_ip=this.remote_IPendpoint.Address.ToString();
            this.my_connect_remote_port=(ushort)this.remote_IPendpoint.Port;
            // start thread to receive data (call allow_receive())
            ThreadStart myThreadStartrcv = new ThreadStart(allow_receive);
            Thread myThreadrcv =new Thread(myThreadStartrcv);
            myThreadrcv.Start();

            // restart thread to send data
            ThreadStart myThreadStart = new ThreadStart(this.my_send);
            Thread myThread =new Thread(myThreadStart);
            myThread.Start();

            this.common_constructor();
        }

        #endregion


    #endregion

    #region methodes

		private Thread RecvThread =null;
		private Thread SendThread =null;

        System.Threading.AutoResetEvent evt_close_UnLocked=new System.Threading.AutoResetEvent(true);
        public void close()
        {
            try
            {
                this.evt_close_UnLocked.WaitOne();
                this.evt_close_UnLocked.Reset();
                if (!this.locally_closed)
                {
                    this.locally_closed=true;
                    // don't close data socket if it has never been connected
                    if(!this.bfirst_connect)
                    {
                        if (this.datasocket!=null) 
                        {
                            this.evt_close.Set();
                            if (this.datasocket.Connected)
                                this.datasocket.Shutdown(SocketShutdown.Both);

                            this.datasocket.Close();
                        }
                    }
                }
                this.evt_close_UnLocked.Set();
            }
            catch (Exception e)
            {
                this.evt_close_UnLocked.Set();// avoid dead lock
				this.Threaded_event_Socket_Data_Error(e);
            }
        }

        /// <summary>
        /// don't use after an accept: connection is lost
        /// </summary>
        public void reconnect()
        {
            try
            {
                if (!this.locally_closed)
                    this.close();
                // create a new socket
                this.datasocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                this.first_connect();
            }
            catch (Exception e)
            {
				this.Threaded_event_Socket_Data_Error(e);
                this.close();
            }
        }

        #region connect 
        /// <summary>
        /// don't use after an accept: connection is already done
        /// </summary>
        public void connect(string remote_ip,int remote_port)
        {
            this.my_connect_remote_ip=remote_ip;
            this.my_connect_remote_port=remote_port;

            if (this.bfirst_connect)
            {
                this.first_connect();
                return;
            }
            // as port and ip have been change, connect just acts like reconnect
            this.reconnect();
            
        }
        private void first_connect()
        {

			// wait for the end of thread allow_receive 
			//(the recv is blocking and if we return now and try to connect without waiting, 
			// the current closing can raise an error in allow_receive that will close next opened socket)
			if (this.RecvThread!=null)
			{
				if (this.RecvThread.IsAlive)
				{
					// Wait until Thread finishes. Join also has overloads
					// that take a millisecond interval or a TimeSpan object.
					this.RecvThread.Join();
					this.RecvThread=null;
				}
			}

			if (this.SendThread!=null)
			{
				if (this.SendThread.IsAlive)
				{
					// Wait until Thread finishes. Join also has overloads
					// that take a millisecond interval or a TimeSpan object.
					this.SendThread.Join();
					this.SendThread=null;
				}
			}

            this.evt_close.Reset();
            try
            {
                this.locally_closed=false;
                this.bfirst_connect=false;
                IPEndPoint ipep = new IPEndPoint(System.Net.IPAddress.Any, this.my_local_port);
                EndPoint ep = (EndPoint)ipep;
                this.datasocket.Bind(ep);   
            }
            catch (System.Exception e)
            {
                this.Threaded_event_Socket_Data_Error(e);
                this.close();
            	return;
            }

            // start new thread calling my_connect() (my_connect call allow_receive() after being connected
            ThreadStart myThreadStart = new ThreadStart(this.my_connect);
            this.RecvThread =new Thread(myThreadStart);
            this.RecvThread.Start();
        }

        /// <summary>
        /// connect to remote host and prepare socket to receive data
        /// send event_Socket_Data_Connected_To_Remote_Host
        /// </summary>
        private void my_connect()
        {
            try
            {
                IPHostEntry iphe;
                IPAddress ipaddr;
                IPEndPoint remoteep;
                // making end point
                try
                {
                    // don't resolve if ip is like xxx.yyy.uuu.vvv
                    ipaddr=System.Net.IPAddress.Parse(this.my_connect_remote_ip);
                }
                catch
                {
                    // else resolve
                    iphe=System.Net.Dns.Resolve(this.my_connect_remote_ip);
                    ipaddr=iphe.AddressList[0];
                }
                remoteep = new IPEndPoint(ipaddr,this.my_connect_remote_port);
                this.remote_IPendpoint=remoteep;

                // connecting
                datasocket.Connect(remoteep);

                // clear send queue
                this.send_queue.Clear();
                // restart thread to send data
                ThreadStart myThreadStart = new ThreadStart(this.my_send);
                this.SendThread =new Thread(myThreadStart);
                this.SendThread.Start();


                // sending the connect event 
                if(event_Socket_Data_Connected_To_Remote_Host!=null)
                    event_Socket_Data_Connected_To_Remote_Host(this,new EventArgs());

                // start thread to receive data [Warning this is a blocking call (no code should be after this line in this thread)]
                allow_receive();
            }
            catch (Exception e)
            {
                this.Threaded_event_Socket_Data_Error(e);
                this.close();
            }
        }


        #endregion

        #region send

        public void cancel_send()
        {
            this.b_cancel_send=true;
        }
        /// <summary>
        /// send data to remote host dequeueing send_queue
        /// send event_Socket_Data_Send_Progress
        /// send event_Socket_Data_Send_Completed
        /// </summary>
        private void my_send()
        {
            
            try
            {
                byte[] local_buffer;
                int bufsize;
                int nbbytesent;
                int nbbyteremaining;
                int data_send_size;
                int cpt;
                bool b_send_sent_completed=true;
                System.Threading.WaitHandle[] wait_handles=new System.Threading.WaitHandle[2];
                wait_handles[0]=this.evt_close;
                wait_handles[1]=this.evt_more_data_to_send;

                int res_wait;

                for(;;)
                {
                    res_wait=System.Threading.WaitHandle.WaitAny(wait_handles);
                    if (res_wait==0)
                        // exit thread
                        return;
                    this.evt_more_data_to_send.Reset();
                    while(this.send_queue.Count>0) // while there's data to send
                    {
                        // dequeue data of send_queue
                        this.evt_unlocked_send_queue.WaitOne();
                        local_buffer=(byte[])this.send_queue.Dequeue();
                        this.evt_unlocked_send_queue.Set();
                        bufsize=local_buffer.Length;
                        nbbytesent=0;
                        nbbyteremaining=bufsize;
                        data_send_size=0;
                        cpt=0;
                        //data_send_size=datasocket.Send(local_buffer); // changed to control the amount of data sent
                        while (nbbyteremaining>buffer_size)
                        {
                            if (this.b_cancel_send)
                            {
                                this.evt_unlocked_send_queue.WaitOne();
                                this.send_queue.Clear();
                                this.evt_unlocked_send_queue.Set();

                                this.b_cancel_send=false;
                                nbbyteremaining=0;
                                b_send_sent_completed=false;
                                break;
                            }
                            if (this.locally_closed)return;
                            if (!this.datasocket.Connected) return;
                            // send a buffer with length=buffer_size
                            data_send_size=datasocket.Send(local_buffer,cpt*buffer_size,buffer_size,SocketFlags.None);
                            nbbytesent+=data_send_size;
                            nbbyteremaining-=data_send_size;
                            cpt++;
                            //event send progress
                            if(this.event_Socket_Data_Send_Progress!=null)
                                this.event_Socket_Data_Send_Progress(this,new EventArgs_Send_Progress_Socket_Data(nbbytesent,nbbyteremaining,bufsize));
                        }
                        if (nbbyteremaining>0)//nbbyteremaining < buffer_size we should send a buffer with length=nbbyteremaining
                        {
                            // send the end of the data
                            if (!this.datasocket.Connected) return;
                            data_send_size=datasocket.Send(local_buffer,cpt*buffer_size,nbbyteremaining,SocketFlags.None);
                            nbbytesent+=data_send_size;
                            nbbyteremaining-=data_send_size;
                            //event send progress
                            if(this.event_Socket_Data_Send_Progress!=null)
                                this.event_Socket_Data_Send_Progress(this,new EventArgs_Send_Progress_Socket_Data(nbbytesent,nbbyteremaining,bufsize));
                        }
                        if (b_send_sent_completed)
                        {
                            //event send completed
                            if(this.event_Socket_Data_Send_Completed!=null)
                                this.event_Socket_Data_Send_Completed(this,new EventArgs());
                        }
                    }
                    b_send_sent_completed=true;
                }
            }
            catch (Exception e)
            {
                // remove all data to be send
                this.evt_unlocked_send_queue.WaitOne();
                this.send_queue.Clear();
                this.evt_unlocked_send_queue.Set();
                
                // restart thread to send data
				ThreadStart myThreadStart = new ThreadStart(this.my_send);
				this.SendThread =new Thread(myThreadStart);
				this.SendThread.Start();
                if (this.locally_closed)
                    return;
                if (e is SocketException)
                {
                    SocketException se=(SocketException)e;
                    if (se.ErrorCode==10004) // close methode as been called
                    {
                        return;
                    }

                    this.close();
                    if (se.ErrorCode==10053) // socket closed by remote side
                    {
                        this.Threaded_event_Socket_Data_Closed_by_Remote_Side();
                        return;
                    }
                }
                this.Threaded_event_Socket_Data_Error(e);
            }
        }


        /// <summary>
        /// used for sending bytes
        /// launch thread for sending or enqueue data if a thread is allready used for sending
        /// </summary>
        /// <param name="buffer"></param>
        public void send(byte[] buffer)
        {
            if (buffer==null) return;
            if (buffer.Length==0) return;
            this.evt_unlocked_send_queue.WaitOne();
            this.send_queue.Enqueue(buffer);
            this.evt_unlocked_send_queue.Set();
            this.evt_more_data_to_send.Set();
        }

        /// <summary>
        /// used for sending string
        /// launch thread for sending or enqueue data if a thread is allready used for sending
        /// </summary>
        /// <param name="buffer"></param>
        public void send(string buffer)
        {
            if (buffer==null) return;
            if (buffer.Length==0) return;
            byte[] sendBytes;
            sendBytes = Encoding.Default.GetBytes(buffer);
            this.send(sendBytes);
        }


        #endregion

        /// <summary>
        ///  begin looping receive
        ///  send event_Socket_Data_DataArrival each time data are received
        ///  send event_Socket_Data_Closed_by_Remote_Side and close the socket if remote side disconnect
        /// </summary>
        private void allow_receive()
        {
            try
            {
                byte[] RecvBytes;
                int tmp_recvbytes;
                int available_size=0;
                int old_available_size;
                this.locally_closed=false;

                // wait until event_Socket_Data_DataArrival is set
                while (this.event_Socket_Data_DataArrival==null)
                {
                    // wait 100 ms 
                    if (this.evt_close.WaitOne(100,false))
                        return;// if connection has been closed
                }

                do 
                {
                    RecvBytes= new byte[available_size];
                    if(!this.datasocket.Connected) return;
                    // receive data
                    tmp_recvbytes = this.datasocket.Receive(RecvBytes, available_size , SocketFlags.None);//blocking
                    // old_available_size contain the amount of data received by the last receive
                    old_available_size=available_size;
                    // get the amount of data that remain to receive
                    available_size=this.datasocket.Available;

                    if (tmp_recvbytes>0)
                    {
                        /*
                        event_Socket_Data_DataArrival(this,
                            new EventArgs_ReceiveDataSocket(Encoding.Default.GetString(RecvBytes, 0, tmp_recvbytes),RecvBytes,tmp_recvbytes));
                        */
                        if (this.event_Socket_Data_DataArrival!=null)
                        {
#if (!DEBUG)
                            try// avoid to close server on some programmer's error in the eventhandler function
#endif
                            {
                                this.event_Socket_Data_DataArrival(this,
                                    new EventArgs_ReceiveDataSocket(RecvBytes,tmp_recvbytes));
                            }
#if (!DEBUG)
                            catch{}
                        
#endif
                        }
                    }
                
                }while (available_size!=0 || old_available_size!=0);// while socket not closed by remote side
                // socket closed by remote side
                // send event_Socket_Data_Closed_by_Remote_Side 
                if (!this.locally_closed)
                {
                    this.Threaded_event_Socket_Data_Closed_by_Remote_Side();
                    // close socket
                    this.close();
                }
            }
            catch (Exception e)
            {
                
                if (e is SocketException)
                {
                    SocketException se=(SocketException)e;
                    if (se.ErrorCode==10004) // close method as been called
                    {
                        return;
                    }

                    
                    if (se.ErrorCode==10053 || se.ErrorCode==10054) // socket closed by remote side
                    {
                        if (locally_closed) return;
						this.Threaded_event_Socket_Data_Closed_by_Remote_Side();
                        return;
                    }

                }
                // if methode of data socket called during it's closing
                if (e is System.ObjectDisposedException) return;
                // other errors
                this.close();
				this.Threaded_event_Socket_Data_Error(e);
            }
        }

		private void Threaded_event_Socket_Data_Closed_by_Remote_Side()
		{
			ThreadStart myThreadStart = new ThreadStart(this.Threaded_event_Socket_Data_Closed_by_Remote_Side_Thread);
			Thread myThread =new Thread(myThreadStart);
			myThread.Start();
		}
		private void Threaded_event_Socket_Data_Closed_by_Remote_Side_Thread()
		{
			if (this.event_Socket_Data_Closed_by_Remote_Side!=null)
				this.event_Socket_Data_Closed_by_Remote_Side(this,new EventArgs());
		}

		public class Socket_Data_Error_Threaded
		{
			private Socket_Data socket;
			private Exception e;

			public Socket_Data_Error_Threaded (Socket_Data socket,Exception e)
			{
				this.socket = socket;
				this.e = e;
			}

			public void Start()
			{
				if(this.socket.event_Socket_Data_Error!=null)
					this.socket.event_Socket_Data_Error(this.socket,new EventArgs_Exception(this.e));
			}
		}

		private void Threaded_event_Socket_Data_Error(Exception e)
		{
			Socket_Data_Error_Threaded ThreadInfos= new Socket_Data_Error_Threaded(this,e);
			ThreadStart myThreadStart = new ThreadStart(ThreadInfos.Start);
			Thread myThread =new Thread(myThreadStart);
			myThread.Start();
		}
    #endregion
    

    }
#endregion

}
