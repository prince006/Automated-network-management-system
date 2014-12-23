using System;

namespace Smart_NET
{
	/// <summary>
	/// Summary description for COptions.
	/// </summary>

	public class COptions
	{
		// tcp server
		public string textBox_TCP_Server_IP="127.0.0.1";
		public string textBox_TCP_Server_Port="6500";

		// tcp client
		public string textBox_TCP_Client_IP="127.0.0.1";
		public string textBox_TCP_Client_Port="6500";
		public bool checkBox_TCP_Client_telnet_protocol=false;
		public bool checkBox_TCP_Client_Specify_local_port=false;
		public string textBox_TCP_Client_local_port="1443";

		// udp server
		public string textBox_UDP_Server_IP="127.0.0.1";
		public string textBox_UDP_Server_Port="7000";
		public bool checkBox_UDP_Server_echo=false;
		public bool checkBox_Bind_To_IpAddrAny=false;

		// udp client
		public string textBox_UDP_Client_IP="127.0.0.1";
		public string textBox_UDP_Client_Port="7000";
		public bool checkBox_UDP_Client_Specify_local_port=false;
		public string textBox_UDP_Client_local_port="53";
		public bool checkBox_UDP_Client_watch_for_reply=false;


		// icmp
		public string textBox_icmp_ip="127.0.0.1";
		public string textBox_icmp_delay_for_reply="3000";
		public string textBox_icmp_packet_ttl="128";
		public string textBox_icmp_ping_number="3";
		public string textBox_icmp_delay_between_ping_sending="0";
		public bool checkBox_icmp_looping_ping=false;
		public bool checkBox_icmp_may_broadcast=false;
		public string textBox_icmp_start_with_hop="1";
		public string textBox_icmp_end_with_hop="20";
		public bool checkBox_icmp_resolve_adresses=true;

		// transparent proxy
		public string textBox_interactive_tcp_proxy_ip="127.0.0.1";
		public string textBox_interactive_tcp_proxy_port="6500";
		public string textBox_interactive_udp_proxy_ip="127.0.0.1";
		public string textBox_interactive_udp_proxy_port="6500";
		public string textBox_interactive_remote_host_ip="127.0.0.1";
		public string textBox_interactive_remote_host_port="6501";

		// dns
		public string textBox_dns_ip="127.0.0.1";

		// whois
		public string textBox_whois_ip="";
		public bool radioButton_whois_auto_find=true;
		public bool radioButton_whois_use_following=false;
		public string textBox_whois_server="127.0.0.1";

		// arp
		public string textBox_arp_ip="10.0.0.1";

		// outside ip
		public string comboBox_outside_ip_server="checkip.dyndns.org";

		// wake on lan
		public string textBox_WOL_mac_address="00-01-02-AF-A3-C0";
		public string textBox_WOL_broadcast_ip="255.255.255.255";
		public string textBox_WOL_udp_port="1111";

		// remote shutdown
		public string textBox_remote_shutdown_computer_name="\\\\computer_name\\";
		public string textBox_remote_shutdown_message="Administrator is going to shutdown your computer";
		public string numericUpDown_remote_shutdown_timeout="20";
		public bool checkBox_remote_shutdown_force_close=true;
		public bool checkBox_remote_shutdown_reset=false;

		public COptions()
		{

		}

		public static COptions Load(string FileName)
		{
			return (COptions)Tools.Xml.XML_access.XMLDeserializeObject(FileName,typeof(COptions));
		}

		public bool Save(string FileName)
		{
			return Tools.Xml.XML_access.XMLSerializeObject(FileName,this,typeof(COptions));
		}
	}

}
