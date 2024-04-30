using System;
using System.Windows;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Management.Smo;

namespace Group4DesktopApp.Datatier
{
    public static class DACImporter
    {

        public static void ImportDatabaseFromDAC(string dacFilePath)
        {
            if (DatabaseVerifier.DoesDatabaseExist())
            {
                return;
            }

            string newDatabaseName = "capstone2024";

            try
            {
                Server server = new Server("(localdb)\\MSSQLLocalDB");

                // Load the .DAC file
                DacServices dacServices = new DacServices(server.ConnectionContext.ConnectionString);
                dacServices.Deploy(DacPackage.Load(dacFilePath), newDatabaseName, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
