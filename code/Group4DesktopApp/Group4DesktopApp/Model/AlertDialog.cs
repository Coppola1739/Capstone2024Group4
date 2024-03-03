using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Group4DesktopApp.Model
{
    public static class AlertDialog
    {
        public static MessageBoxResult EditNewNoteWithoutSavingConfirm()
        {
            MessageBoxResult confirmBox = 
                System.Windows.MessageBox.Show("Are you sure you want to edit a new note without saving?", 
                "Leaving",
                System.Windows.MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            return confirmBox;
        }

        public static MessageBoxResult QuitModifyingNoteConfirm()
        {
                MessageBoxResult confirmBox = 
                System.Windows.MessageBox.Show("Are you sure you want to stop modifying this note?", 
                "Leaving", 
                System.Windows.MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            return confirmBox;
        }

        public static MessageBoxResult AddNoteErrorBox()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Note must not be empty",
            "Note Add Failed",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }

        public static MessageBoxResult UpdateNoteWithEmptyTextErrorBox()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Edited note must not be empty ",
            "Note Update Failed",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }

        public static MessageBoxResult UpdateNoteConfirm()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Are you sure you want to update this note? Action cannot be undone.",
            "Confirm Note Update",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Question);
            return confirmBox;
        }
    }
}
