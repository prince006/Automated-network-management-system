using System;
using System.Runtime.InteropServices;

namespace easy_socket
{
    public class RemoteShutDown
    {

        [DllImport("advapi32.dll", EntryPoint="InitiateSystemShutdown",SetLastError=true)]
        private static extern 
            bool InitiateSystemShutdown(
                string lpMachineName,      // computer name // null for local computer
                string lpMessage,          // message to display //This parameter can be NULL if no message is required. 
                UInt32 dwTimeout,          // length of time to display
                bool bForceAppsClosed,     // force closed option
                bool bRebootAfterShutdown  // reboot option
                );

        [DllImport("advapi32.dll", EntryPoint="AbortSystemShutdown",SetLastError=true)]
        private static extern 
        bool AbortSystemShutdown(
            string lpMachineName   // computer name // null for local computer
            );

        /// <summary>
        /// Initiate local or remote system shutdown
        /// </summary>
        /// <param name="MachineName">computer name, "" for local computer</param>
        /// <param name="Message">message to display</param>
        /// <param name="Timeout">length of time to display</param>
        /// <param name="ForceAppsClosed">force closed option</param>
        /// <param name="RebootAfterShutdown">reboot option</param>
        public static void InitiateShutdown(string MachineName,string Message,UInt32 Timeout,bool ForceAppsClosed,bool RebootAfterShutdown)
        {
            Tools.API.API_error.GetLastError();// clear previous error msg if any
            if (!InitiateSystemShutdown(MachineName,Message,Timeout,ForceAppsClosed,RebootAfterShutdown))
                System.Windows.Forms.MessageBox.Show(Tools.API.API_error.GetAPIErrorMessageDescription(Tools.API.API_error.GetLastError()),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            else
                System.Windows.Forms.MessageBox.Show("Shutdown successfully initiated","Information",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
        }

        /// <summary>
        /// Abort a local or remote system shutdown previously initiated
        /// </summary>
        /// <param name="MachineName">computer name, "" for local computer</param>
        public static void AbortShutdown(string MachineName)
        {
            Tools.API.API_error.GetLastError();// clear previous error msg if any
            if (!AbortSystemShutdown(MachineName))
            {
                uint ui=Tools.API.API_error.GetLastError();
                string msg=Tools.API.API_error.GetAPIErrorMessageDescription(ui);
                if (ui==53)// because of beautiful api error msg :)
                {
                    msg+="\r\nOr shutdown not initiated";
                }
                System.Windows.Forms.MessageBox.Show(msg,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
                System.Windows.Forms.MessageBox.Show("Shutdown successfully aborted","Information",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
