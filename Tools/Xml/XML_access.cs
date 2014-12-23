using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace Tools.Xml
{
    public class XML_access
    {

        public static bool XMLSerializeObject(string filename,object obj,System.Type typeof_object)
        {
            Stream fs=null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof_object);
                // Create an XmlTextWriter using a FileStream.
                fs = new FileStream(filename, FileMode.Create,System.IO.FileAccess.ReadWrite );
                XmlWriter writer = new XmlTextWriter(fs,System.Text.Encoding.Unicode);
                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, obj);
                writer.Close();
                fs.Close();
				return true;
            }
            catch(Exception e)
            {
                if (fs!=null)
                    fs.Close();
                System.Windows.Forms.MessageBox.Show( e.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				return false;
            }
        }
        /// <summary>
        /// simple sample of call
        /// OrderedItem i=(OrderedItem)XMLDeserializeObject("simple.xml",typeof(OrderedItem));
        /// sample of call for ArrayList
        /// System.Collections.ArrayList al=new System.Collections.ArrayList((OrderedItem[])XMLDeserializeObject("sampleal.xml",typeof(OrderedItem[])));
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="typeof_object"></param>
        /// <returns></returns>
        public static object XMLDeserializeObject(string filename,System.Type typeof_object)
        {   
			try
			{
				// Create an instance of the XmlSerializer specifying type and namespace.
				XmlSerializer serializer = new XmlSerializer(typeof_object);
				// A FileStream is needed to read the XML document.
				FileStream fs = new FileStream(filename, FileMode.Open);
				XmlReader reader = new XmlTextReader(fs);
				// Declare an object variable of the type to be deserialized.
				object obj;
				// Use the Deserialize method to restore the object's state.
				obj = serializer.Deserialize(reader);
				reader.Close();
				fs.Close();
				return obj;
			}
			catch(Exception e)
			{
				// return null;
				return Activator.CreateInstance(typeof_object);
			}
        }
    }
}