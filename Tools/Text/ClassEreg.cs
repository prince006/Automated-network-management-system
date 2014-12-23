using System;

namespace Tools.Text
{
    /// <summary>
    /// done to give similare use as php
    /// </summary>
	public class ClassEreg
	{
        public static bool ereg(string pattern,string input_string,ref string[] strregs)
        {
            if (input_string=="")
                return false;
            if (!System.Text.RegularExpressions.Regex.IsMatch(input_string,pattern,System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                return false;
            System.Text.RegularExpressions.Match m=System.Text.RegularExpressions.Regex.Match(input_string,pattern,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            strregs=new string[m.Groups.Count];
            for (int cpt=0;cpt<m.Groups.Count;cpt++)
            {
                if (m.Groups[cpt].Captures.Count>0)
                    strregs[cpt]=m.Groups[cpt].Captures[0].Value;
                else
                    strregs[cpt]="";
            }

            return true;
        }
        public static bool ereg(string pattern,string input_string)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input_string,pattern,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
	}
}
