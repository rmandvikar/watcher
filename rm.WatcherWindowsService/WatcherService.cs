using System;
using System.ServiceProcess;

namespace rm.WatcherWindowsService
{
    public partial class WatcherService : ServiceBase
    {
        public WatcherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                rm.Watcher.Program.Start();
            }
            catch (Exception)
            { }
        }

        protected override void OnStop()
        {
        }
    }
}
