using System;
namespace easy_socket
{
    public class Cmultiple_elements_parsing
    {

        /// parse multiple ushort splitted by ; or using ushort1-ushort2 to get all value from ushort1 to ushort2

        public static ushort[] Parse_ushort(string text)
        {
            System.Collections.ArrayList al=new System.Collections.ArrayList();
            string[] str_array=text.Split(";".ToCharArray());
            string[] str_array2;
            int pos_minus;
            ushort min;
            ushort max;
            ushort us_cpt2;
            try
            {
                for (int cpt=0;cpt<str_array.Length;cpt++)
                {
                    if (str_array[cpt]=="")
                        continue;
                    pos_minus=str_array[cpt].IndexOf("-");
                    if (pos_minus>=0)
                    {
                        str_array2=str_array[cpt].Split("-".ToCharArray());
                        min=System.Convert.ToUInt16(str_array2[0]);
                        max=System.Convert.ToUInt16(str_array2[1]);
                        if (min>max)
                            throw new System.Exception("Error min value is upper than max value");
                        for (us_cpt2=min;us_cpt2<=max;us_cpt2++)
                        {
                            al.Add(System.Convert.ToUInt16(us_cpt2));
                            if (us_cpt2==ushort.MaxValue)
                                break;// else infinite loop if max=ushort.MaxValue
                        }
                    }
                    else
                        al.Add(System.Convert.ToUInt16(str_array[cpt]));
                }
                return (ushort[])al.ToArray(typeof(ushort));
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }

        /// parse multiple ip splitted by ; or using ip1-ip2 to get all value from ip1 to ip2

        public static string[] Parse_ip(string text)
        {
            System.Collections.ArrayList al=new System.Collections.ArrayList();
            string[] str_array=text.Split(";".ToCharArray());
            string[] str_array2;
            int pos_minus;
            UInt32 min;
            UInt32 max;
            UInt32 ui_cpt2;
            try
            {
                for (int cpt=0;cpt<str_array.Length;cpt++)
                {
                    if (str_array[cpt]=="")
                        continue;
                    pos_minus=str_array[cpt].IndexOf("-");
                    if (pos_minus>=0)
                    {
                        str_array2=str_array[cpt].Split("-".ToCharArray());

                        min=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(str_array2[0]).GetAddressBytes(),0);
                        min=easy_socket.network_convert.switch_UInt32(min);
                        max=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(str_array2[1]).GetAddressBytes(),0);
                        max=easy_socket.network_convert.switch_UInt32(max);
                        if (min>max)
                            throw new System.Exception("Error min value is upper than max value");
                        for (ui_cpt2=min;ui_cpt2<=max;ui_cpt2++)
                        {
                            al.Add((new System.Net.IPAddress(easy_socket.network_convert.switch_UInt32(ui_cpt2))).ToString());
                            if (ui_cpt2==UInt32.MaxValue)// else infinite loop if max=UInt32.MaxValue
                                break;
                        }
                    }
                    else
                        al.Add(str_array[cpt]);
                }
                return (string[])al.ToArray(typeof(string));
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }


        /// parse multiple ip splitted by ; or using ip1-ip2. return a CMinMax array

        public static CMinMax[] Parse_ip_limits(string text)
        {
            System.Collections.ArrayList al=new System.Collections.ArrayList();
            string[] str_array=text.Split(";".ToCharArray());
            string[] str_array2;
            int pos_minus;
            UInt32 min;
            UInt32 max;
            try
            {
                for (int cpt=0;cpt<str_array.Length;cpt++)
                {
                    if (str_array[cpt]=="")
                        continue;
                    pos_minus=str_array[cpt].IndexOf("-");
                    if (pos_minus>=0)
                    {
                        str_array2=str_array[cpt].Split("-".ToCharArray());

                        min=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(str_array2[0]).GetAddressBytes(),0);
                        min=easy_socket.network_convert.switch_UInt32(min);
                        max=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(str_array2[1]).GetAddressBytes(),0);
                        max=easy_socket.network_convert.switch_UInt32(max);
                        if (min>max)
                            throw new System.Exception("Error min value is upper than max value");
                        al.Add(new CMinMax(min,max));
                    }
                    else
                    {
                        min=System.BitConverter.ToUInt32(System.Net.IPAddress.Parse(str_array[cpt]).GetAddressBytes(),0);
                        al.Add(new CMinMax(min,min));
                    }
                }
                return (CMinMax[])al.ToArray(typeof(CMinMax));
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }    

        /// parse ip:port to return an CHost

        public static CHost Parse_IP_two_points_Port(string text)
        {
            try
            {
                if (text=="")
                    return new CHost();
                string[] array=text.Split(':');
                if (array.Length!=2)
                    throw new Exception("Format must be like 127.0.0.1:80 \r\nCurrently it's \""+text+"\"");
                return new CHost(array[0].Trim(),System.Convert.ToUInt16(array[1].Trim()));
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return new CHost();
            }
        }
    }
    public class CMinMax
    {
        public UInt32 min=0;
        public UInt32 max=0;
        public CMinMax(UInt32 min,UInt32 max)
        {
            this.min=min;
            this.max=max;
        }
    }
    public class CHost
    {
        private bool bEmpty=true;
        public string ip="127.0.0.1";
        public ushort port=80;
        public CHost()
        {
            this.bEmpty=true;
        }
        public CHost(string ip,ushort port)
        {
            this.ip=ip;
            this.port=port;
            this.bEmpty=false;
        }
        public override bool Equals(object o)
        {
            if (!(o is CHost))
                return false;
            CHost x=(CHost)o;
            if ((x.ip==this.ip)&&(x.port==this.port))
                return true;
            return false;
        }

        public bool IsEmpty()
        {
            return this.bEmpty;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(CHost x, CHost y)
        {
            if (x.IsEmpty()&&(!y.IsEmpty()))
                return false;
            if (y.IsEmpty()&&(!x.IsEmpty()))
                return false;
            if ((x.ip==y.ip)&&(x.port==y.port))
                return true;
            return false;
        }
        public static bool operator !=(CHost x, CHost y)
        {
            return !(x==y);
        }
    }
}