using System;
using Microsoft.SqlServer.Dac;

namespace DacFx.Nuget.Test
{
    public class Program
    {
        public static void Main()
        {
            // Set the connection string
            string connectionString = "";
            DacServices service = new DacServices(connectionString);
            service.Message += (sender, e) => {
                Console.WriteLine(e.Message);
            };

            // Publish test, set dacpac path and target database
            string publishSourceDacpac = "";
            string publishTargetDatabase = "";
            using (DacPackage dacpac = DacPackage.Load(publishSourceDacpac))
            {
                service.Publish(dacpac, publishTargetDatabase, new PublishOptions());
            }

            // Extract test, set dacpac path and source database
            string extractSourceDatabase = "";
            string extractTargetDacpac = "";
            service.Extract(extractTargetDacpac, extractSourceDatabase, "testApp", new Version(1, 0));

            // Import test, set bacpac path and target database
            string importSourceBacpac = "";
            string importTargetDatabase = "";
            using (BacPackage bacpac = BacPackage.Load(importSourceBacpac))
            {
                service.ImportBacpac(bacpac, importTargetDatabase);
            }

            // Export test, set bacpac path and source database
            string exportSourceDatabase = "";
            string exportTargetBacpac = "";
            service.ExportBacpac(exportTargetBacpac, exportSourceDatabase);
        }
    }
}