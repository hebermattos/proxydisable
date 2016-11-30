using Microsoft.Win32;
using System;
using System.Threading;

namespace ProxyDisable
{
    class Program
    {
        static void Main(string[] args)
        {

            var registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            var value = (int)registry.GetValue("ProxyEnable");

            if (value == 1)
                registry.SetValue("ProxyEnable", 0);


        }
    }
}
