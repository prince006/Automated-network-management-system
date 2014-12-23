using System;

namespace easy_socket
{

    public class Cchecksum
    {
        public static UInt16 checksum(byte[] buffer)
        {
            return Cchecksum.checksum(buffer,false);
        }

        public static UInt16 checksum(byte[] buffer,bool returned_value_in_network_order)
        {
            int iCheckSum = 0;
            if (buffer!=null)
            {
                if (buffer.Length%2==0)
                {
                    for (int i= 0; i < buffer.Length; i+= 2) 
                        iCheckSum += BitConverter.ToUInt16(buffer,i);
                }
                else
                {
                    for (int i= 0; i < buffer.Length-1; i+= 2) 
                        iCheckSum += BitConverter.ToUInt16(buffer,i);
                    iCheckSum += buffer[buffer.Length-1];
                }
            }
            iCheckSum = (iCheckSum >> 16) + (iCheckSum & 0xffff);
            iCheckSum += (iCheckSum >> 16);
            iCheckSum=~iCheckSum;
            if (!returned_value_in_network_order)
            {
                byte MSB=(byte)((iCheckSum>>8)&0xff);
                byte LSB=(byte)(iCheckSum&0xff);
                iCheckSum=(LSB<<8)+MSB;
            }
            return (UInt16)iCheckSum;
        }
    }
}
