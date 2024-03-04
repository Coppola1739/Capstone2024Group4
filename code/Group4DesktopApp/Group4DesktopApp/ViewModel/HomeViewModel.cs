using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Group4DesktopApp.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Source> sources;
        private ObservableCollection<String> sourcesTypes;
        private string selectedSource;
        private string sourceName;
        private string authorFirstName;
        private string authorLastName;
        private string title;


        public HomeViewModel()
        {
            this.sources = new ObservableCollection<Source>();
            this.sourcesTypes = new ObservableCollection<String>();
            this.selectedSource = String.Empty;
            this.loadSourceTypes();
            this.sourceName = String.Empty;
            this.authorFirstName = String.Empty;
            this.authorLastName = String.Empty;
            this.title = String.Empty;
        }

        public ObservableCollection<String> SourceTypeListProperty
        {
            get { return sourcesTypes; }
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

        public string SelectedSourceProperty
        {
            get { return selectedSource; }
            set
            {
                selectedSource = value;
                NotifyPropertyChanged(nameof(SelectedSourceProperty));
            }
        }

        public string SourceNameProperty
        {
            get { return sourceName; }
            set
            {
                sourceName = value;
                NotifyPropertyChanged(nameof(SourceNameProperty));
            }
        }

        public string AuthorFirstNameProperty
        {
            get { return authorFirstName; }
            set
            {
                authorFirstName = value;
                NotifyPropertyChanged(nameof(AuthorFirstNameProperty));
            }
        }

        public string AuthorLastNameProperty
        {
            get { return authorLastName; }
            set
            {
                authorLastName = value;
                NotifyPropertyChanged(nameof(AuthorLastNameProperty));
            }
        }

        public string TitleProperty
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(TitleProperty));
            }
        }

        private void updateList(int userId)
        {
            var updatedList = SourceDAL.GetAllSourcesByUserId(userId);
            var result = updatedList.Where(p => !this.sources.Any(p2 => p2.SourceId == p.SourceId));
            foreach (var source in result)
            {
                this.sources.Add(source);
            }
        }

        public bool InsertNewSource(int userId, string sourceType, Byte[] content)
        {
            Source newSource = new Source(-1,userId,this.SourceNameProperty,DateTime.Now,content, sourceType, this.AuthorFirstNameProperty, this.AuthorLastNameProperty,this.TitleProperty);
            bool success = SourceDAL.AddNewSource(userId,newSource);
            if (success)
            {
                this.updateList(userId);
            }
            return success;
        }

        public bool DeleteSource(Source source)
        {
            bool success = SourceDAL.DeleteSource(source);
            if (success)
            {
                this.sources.Remove(source);
            }
            return success;
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

        private void loadSourceTypes()
        {
            string? source;
            foreach (var sourceType in Enum.GetValues(typeof(SourceType.Enum)))
            {
                source = sourceType.ToString();
                if(source != null)
                {
                    sourcesTypes.Add(source);
                }
            }
        }
    }
}
