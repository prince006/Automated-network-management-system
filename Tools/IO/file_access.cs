using System;
using System.IO;
namespace Tools.IO
{
    public class file_access
    {

        public static string read(string file_name)
        {
            try
            {
                string        data;

                FileStream fs=new FileStream(file_name,FileMode.Open ,FileAccess.Read,FileShare.ReadWrite );
                StreamReader sr=new StreamReader(fs,System.Text.Encoding.Default);
                data=sr.ReadToEnd();
                sr.Close();
                fs.Close();
                return data;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return "";
            }
        }

        public static byte[] read_binary(string file_name)
        {
            try
            {
                FileStream    fs;
                long        file_size;

                fs = new FileStream(file_name, FileMode.Open ,FileAccess.Read,FileShare.ReadWrite );
                file_size = fs.Length;
          
                byte[] buffer = new byte[(int)file_size];
                fs.Read(buffer, 0, (int)file_size);
                fs.Close();
                return buffer;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }

        public static void write_binary(string file_name,byte[] buffer)
        {
            try
            {
                write_binary(file_name,buffer,false);
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void write_binary(string file_name,byte[] buffer,bool append)
        {
            try
            {
                FileStream        fs;

                if (append)
                {
                    fs=new FileStream(file_name,FileMode.Append ,FileAccess.Write,FileShare.ReadWrite);
                }
                else
                {
                    if (File.Exists(file_name)) File.Delete(file_name);
                    fs=new FileStream(file_name,FileMode.OpenOrCreate ,FileAccess.Write,FileShare.ReadWrite);
                }
                fs.Write(buffer,0,buffer.Length);
                fs.Close();
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void write_binary_at(string file_name,byte[] buffer,long position)
        {
            try
            {
                FileStream        fs;

                fs=new FileStream(file_name,FileMode.OpenOrCreate ,FileAccess.Write,FileShare.ReadWrite);
                fs.Seek(position,System.IO.SeekOrigin.Begin);
                fs.Write(buffer,0,buffer.Length);
                fs.Close();
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }


        public static void write(string file_name,string buffer)
        {
            try
            {
                write(file_name,buffer,false);
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void write(string file_name,string buffer,bool append)
        {
            try
            {
                FileStream        fs;

                if (append)
                {
                    fs=new FileStream(file_name,FileMode.Append ,FileAccess.Write,FileShare.ReadWrite);
                }
                else
                {
                    if (File.Exists(file_name)) File.Delete(file_name);
                    fs=new FileStream(file_name,FileMode.OpenOrCreate ,FileAccess.Write,FileShare.ReadWrite);
                }
                StreamWriter sw=new StreamWriter(fs);
                sw.Write(buffer);
                sw.Close();
                fs.Close();
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}