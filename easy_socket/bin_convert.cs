using System;

namespace easy_socket
{

    public class bin_convert
    {
        public static byte strbit_to_byte(string bit)
        {
            byte ret=0;
            string bit_value="0";
            byte b=0;
            for (int cpt=1;cpt<=bit.Length;cpt++)
            {
                bit_value=bit.Substring(bit.Length-cpt,1);
                if ((bit_value!="0")||(bit_value!="1"))
                    break;
                b=System.Convert.ToByte(bit_value);
                ret|=(byte)((b<<(cpt-1))&0xFF);
            }
            return ret;
        }
    }
}
