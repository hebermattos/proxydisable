using Microsoft.Win32;
using System.ServiceProcess;
using System.Timers;

namespace ProxyDisableService
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(3000); 
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            _timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {     
            var registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            var value = (int)registry.GetValue("ProxyEnable");

            if (value == 1)
            {
                registry.SetValue("ProxyEnable", 0);
                EventLog.WriteEntry("ProxyEnable: 0");
            }
        }

        protected override void OnStop()
        {
            _timer.Dispose();
            
        }
    }
}
