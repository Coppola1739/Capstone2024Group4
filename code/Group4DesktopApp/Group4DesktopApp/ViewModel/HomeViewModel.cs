using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Source> sources;

        public HomeViewModel()
        {
            this.sources = new ObservableCollection<Source>();
        }

        public ObservableCollection<Source> SourceDataProperty
        {
            get { return sources; }
            set
            {
                sources = value;
                NotifyPropertyChanged(nameof(SourceDataProperty));

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void PopulateSourcesByID(int userId)
        {
            this.sources = SourceDAL.GetAllSourcesByUserId(userId);
        }

        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
