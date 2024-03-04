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
        public static MessageBoxResult NoteAddSuccess()
        {
            MessageBoxResult confirmBox =
                System.Windows.MessageBox.Show("Note added successfully!",
                "Note Added",
                System.Windows.MessageBoxButton.OK,
                MessageBoxImage.Information);
            return confirmBox;
        }
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
            System.Windows.MessageBox.Show("Are you sure you want to UPDATE this note? Action cannot be undone.",
            "Confirm Note Update",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Question);
            return confirmBox;
        }

        public static MessageBoxResult DeleteNoteConfirm()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Are you sure you want to permanently DELETE this note? Action cannot be undone.",
            "Delete Note",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Exclamation);
            return confirmBox;
        }

        public static MessageBoxResult DeleteSourceConfirm()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Are you sure you want to permanently DELETE this Source?\n" +
            "WARNING: All notes under this source will also be removed ",
            "Delete Source",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Exclamation);
            return confirmBox;
        }

        public static MessageBoxResult InvalidYoutubeLinkErrorBox()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("The Youtube URL provided is not valid. Please try again.",
            "Youtube Link Error",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }

        public static MessageBoxResult LogoutConfirm()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Are you sure you Logout of this account?",
            "Logout",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Exclamation);
            return confirmBox;
        }
    }
}
