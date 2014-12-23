using System;
using System.Security.Principal;

namespace Tools.GUI.Windows.ErrorReport
{
    /// <summary>
    /// Summary description for Cuser_member.
    /// </summary>
    public class Cuser_group
    {
        public static string[] get_groups()
        {
            System.Collections.ArrayList al=new System.Collections.ArrayList(10);
            WindowsPrincipal id=new WindowsPrincipal(WindowsIdentity.GetCurrent());
            Array wbirFields = Enum.GetValues(typeof(WindowsBuiltInRole));
            foreach (object roleName in wbirFields)
            {
                try
                {
                    if (id.IsInRole((WindowsBuiltInRole)roleName))
                        al.Add(roleName.ToString());
                } 
                catch (Exception)
                {
                    // System.Windows.Forms.MessageBox.Show("Could not obtain role for "+roleName+" RID");
                }
            }
            if (al.Count==0)
                return null;
            return (string[])al.ToArray(typeof(string));
        }
    }
}