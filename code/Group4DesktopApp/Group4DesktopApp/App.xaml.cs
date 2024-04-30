using Group4DesktopApp.Datatier;
using Group4DesktopApp.Resources;
using Group4DesktopApp.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Group4DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.loadDatabaseFromArtifacts();
            this.deletePreviousResources();
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

        }

        private void loadDatabaseFromArtifacts()
        {
            if (!DatabaseVerifier.DoesDatabaseExist())
            {
                NonModalAlertDialog alertWindow = new NonModalAlertDialog("Loading database from artifacts.");
                alertWindow.Show();
                DACImporter.ImportDatabaseFromDAC(DatabaseVerifier.DACFile);
                if (alertWindow.IsActive)
                {
                    alertWindow.Hide();
                }
                MessageBox.Show("Database imported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            if (SourceResources.isAnyDirectoryExisting())
            {
                Thread.Sleep(2000);
                this.deletePreviousResources();
            }
        }

        private void deletePreviousResources()
        {
            string[] resources = SourceResources.ResourceDirectories.Values.ToArray();
            foreach (string resource in resources)
            {
                if (Directory.Exists(resource))
                {
                    try
                    {
                        Directory.Delete(resource, true);
                    }
                    catch (System.IO.IOException)
                    {
                        Debug.WriteLine($"Could not Delete {Path.GetDirectoryName(resource)}");
                    }
                }
            }

        }
    }
}
