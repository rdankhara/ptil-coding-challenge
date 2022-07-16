using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Petroineos.PositionExporterService
{
    // allows the service to be installed by the Installutil.exe tool
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            Console.WriteLine("Installing...");
            process = new ServiceProcessInstaller();
            Console.WriteLine("Process created setting account...");
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.Description = "Exports powertrade positions to csv";
            service.ServiceName = "PositionExporterService";
            service.StartType = ServiceStartMode.Automatic;
            service.DelayedAutoStart = true;
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
