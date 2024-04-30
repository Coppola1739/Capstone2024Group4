using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Group4DesktopApp.DAL;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Management.Smo;

namespace Group4DesktopApp.Datatier
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// The DAC Importer static class. Used to load a .DAC file, unzip it, and populate a the database in the local Server
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public static class DACImporter
    {
        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Imports the database from dac.
        /// </summary>
        /// <param name="dacFilePath">The dac file path.</param>
        public static void ImportDatabaseFromDAC(string dacFilePath)
        {
            if (DatabaseVerifier.DoesDatabaseExist())
            {
                return;
            }

            try
            {
                Server server = new Server(Connection.ServerName);

                // Load the .DAC file
                DacServices dacServices = new DacServices(server.ConnectionContext.ConnectionString);
                dacServices.Deploy(DacPackage.Load(dacFilePath), Connection.DBName, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
