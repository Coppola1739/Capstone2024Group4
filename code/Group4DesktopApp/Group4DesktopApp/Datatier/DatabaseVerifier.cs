using Group4DesktopApp.DAL;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Group4DesktopApp.Datatier
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// The Database Verifier class, used to aid creating the database. 
    /// Contains the file path to the used .DAC file 
    /// and checks if the used database if the application exists.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public static class DatabaseVerifier
    {
        private static readonly string workingDirectory = Environment.CurrentDirectory;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private static readonly string dataDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.FullName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.     
        
        /// <summary>
        /// The dac filepath
        /// </summary>
        public static readonly string DACFile = dataDirectory + "\\Data\\capstone2024.dacpac";

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Checks if the database exists.
        /// </summary>
        /// <returns>true if database is found, false otherwise</returns>
        public static bool DoesDatabaseExist()
        {
            try
            {
                Server server = new Server(Connection.ServerName);

                Database database = server.Databases[Connection.DBName];

                return (database != null);
            }
            catch (ConnectionFailureException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
