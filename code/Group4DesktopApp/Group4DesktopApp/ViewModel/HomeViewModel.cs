using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Group4DesktopApp.ViewModel
{
    /// <summary>
    /// The Home Window ViewModel
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Source> sources;
        private ObservableCollection<String> sourcesTypes;
        private string selectedSource;
        private string sourceName;
        private string authorFirstName;
        private string authorLastName;
        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
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
        /// <summary>
        /// Gets the source type list property.
        /// </summary>
        /// <value>
        /// The source type list property.
        /// </value>
        public ObservableCollection<String> SourceTypeListProperty
        {
            get { return sourcesTypes; }
        }
        /// <summary>
        /// Gets or sets the source data property.
        /// </summary>
        /// <value>
        /// The source data property.
        /// </value>
        public ObservableCollection<Source> SourceDataProperty
        {
            get { return sources; }
            set
            {
                sources = value;
                NotifyPropertyChanged(nameof(SourceDataProperty));

            }
        }
        /// <summary>
        /// Gets or sets the selected source property.
        /// </summary>
        /// <value>
        /// The selected source property.
        /// </value>
        public string SelectedSourceProperty
        {
            get { return selectedSource; }
            set
            {
                selectedSource = value;
                NotifyPropertyChanged(nameof(SelectedSourceProperty));
            }
        }
        /// <summary>
        /// Gets or sets the source name property.
        /// </summary>
        /// <value>
        /// The source name property.
        /// </value>
        public string SourceNameProperty
        {
            get { return sourceName; }
            set
            {
                sourceName = value;
                NotifyPropertyChanged(nameof(SourceNameProperty));
            }
        }
        /// <summary>
        /// Gets or sets the author first name property.
        /// </summary>
        /// <value>
        /// The author first name property.
        /// </value>
        public string AuthorFirstNameProperty
        {
            get { return authorFirstName; }
            set
            {
                authorFirstName = value;
                NotifyPropertyChanged(nameof(AuthorFirstNameProperty));
            }
        }
        /// <summary>
        /// Gets or sets the author last name property.
        /// </summary>
        /// <value>
        /// The author last name property.
        /// </value>
        public string AuthorLastNameProperty
        {
            get { return authorLastName; }
            set
            {
                authorLastName = value;
                NotifyPropertyChanged(nameof(AuthorLastNameProperty));
            }
        }
        /// <summary>
        /// Gets or sets the title property.
        /// </summary>
        /// <value>
        /// The title property.
        /// </value>
        public string TitleProperty
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(TitleProperty));
            }
        }
        /// <summary>
        /// Calls the Data Access Layer to insert the new source to the database.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="content">The content.</param>
        /// <returns>True if source was successfully added, false otherwise.</returns>
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
        /// <summary>
        /// Calls the Data Access Layer to delete the source from the database.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>True if source was successfully deleted, false otherwise.</returns>
        public bool DeleteSource(Source source)
        {
            bool success = SourceDAL.DeleteSource(source);
            if (success)
            {
                this.sources.Remove(source);
            }
            return success;
        }
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Populates the sources by user ID.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
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

        private void updateList(int userId)
        {
            var updatedList = SourceDAL.GetAllSourcesByUserId(userId);
            var result = updatedList.Where(p => !this.sources.Any(p2 => p2.SourceId == p.SourceId));
            foreach (var source in result)
            {
                this.sources.Add(source);
            }
        }
    }
}
