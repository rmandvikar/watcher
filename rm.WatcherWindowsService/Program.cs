using System.ServiceProcess;

namespace rm.WatcherWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new WatcherService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
