using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkView
{
	class AutoRunHelper
	{
		public static void FirstRun()
		{
		    EnableAutoStart();
		}

        public static void EnableAutoStart()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            key.SetValue("NetworkViewRun", Application.ExecutablePath);
        }

        public static void DisableAutoStart()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            key.DeleteValue("NetworkViewRun");
        }

        public static bool AutoStartEnabled()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");

            if (key.GetValue("NetworkViewRun") != null)
                return true;

            return false;
        }
	}
}
