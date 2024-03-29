using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Group4DesktopApp.Model
{
    /// <summary>
    /// The Alert Dialog class.
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AlertDialog
    {
        /// <summary>
        /// Returns a confirmation dialog that will alert the user 
        /// if they are sure they want to edit a new note without saving
        /// </summary>
        /// <returns>a confirmation dialog that will alert the user 
        /// if they are sure they want to edit a new note without saving</returns>
        public static MessageBoxResult EditNewNoteWithoutSavingConfirm()
        {
            MessageBoxResult confirmBox = 
                System.Windows.MessageBox.Show("Are you sure you want to edit a new note without saving?", 
                "Leaving",
                System.Windows.MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            return confirmBox;
        }
        /// <summary>
        /// Returns a confirmation dialog that will alert the user 
        /// if they are sure they want to stop modifying a note
        /// </summary>
        /// <returns>a confirmation dialog that will alert the user 
        /// if they are sure they want to stop modifying a note</returns>
        public static MessageBoxResult QuitModifyingNoteConfirm()
        {
                MessageBoxResult confirmBox = 
                System.Windows.MessageBox.Show("Are you sure you want to stop modifying this note?", 
                "Leaving", 
                System.Windows.MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            return confirmBox;
        }
        /// <summary>
        /// Returns an alert dialog that will notify the user 
        /// that adding the new note failed, due to the note having empty or blank content.
        /// </summary>
        /// <returns>an alert dialog that will notify the user 
        /// that adding the new note failed, due to the note having empty or blank content.</returns>
        public static MessageBoxResult AddNoteErrorBox()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Note must not be empty",
            "Note Add Failed",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }
        /// <summary>
        /// Returns an alert dialog that will notify the user 
        /// that updating the note failed, due to the note having empty or blank content.
        /// </summary>
        /// <returns>an alert dialog that will notify the user 
        /// that updating the note failed, due to the note having empty or blank content.</returns>
        public static MessageBoxResult UpdateNoteWithEmptyTextErrorBox()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Edited note must not be empty ",
            "Note Update Failed",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }
        /// <summary>
        /// Returns an alert dialog that will notify the user 
        /// that adding the tag failed, due to the tag having an empty or blank name.
        /// </summary>
        /// <returns>an alert dialog that will notify the user 
        /// that adding the tag failed, due to the tag having an empty or blank name.</returns>
        public static MessageBoxResult TagEmptyErrorBox()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("A tag cannot be empty or blank",
            "Tag Add Error",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }
        /// <summary>
        /// Returns an alert dialog that will notify the user 
        /// that adding the tag failed, due to a tag with that name already existing.
        /// </summary>
        /// <returns>an alert dialog that will notify the user 
        /// that adding the tag failed, due to a tag with that name already existing.</returns>
        public static MessageBoxResult TagUnderNoteAlreadyExists()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("A tag with this name already exists in this note.",
            "Tag Add Error",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }
        /// <summary>
        /// Returns a confirmation dialog that will alert the user 
        /// if they are sure they want to update a note with the updated content.
        /// </summary>
        /// <returns>a confirmation dialog that will alert the user 
        /// if they are sure they want to update a note with the updated content.</returns>
        public static MessageBoxResult UpdateNoteConfirm()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Are you sure you want to UPDATE this note? Action cannot be undone.",
            "Confirm Note Update",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Question);
            return confirmBox;
        }
        /// <summary>
        /// Returns a confirmation dialog that will alert the user 
        /// if they are sure they want to permanently delete a note. The action cannot be undone.
        /// </summary>
        /// <returns>a confirmation dialog that will alert the user 
        /// if they are sure they want to permanently delete a note. The action cannot be undone.</returns>
        public static MessageBoxResult DeleteNoteConfirm()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("Are you sure you want to permanently DELETE this note? Action cannot be undone.",
            "Delete Note",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Exclamation);
            return confirmBox;
        }
        /// <summary>
        /// Returns a confirmation dialog that will alert the user 
        /// if they are sure they want to permanently delete a source and all notes attached to that source. The action cannot be undone.
        /// </summary>
        /// <returns>a confirmation dialog that will alert the user 
        /// if they are sure they want to permanently delete a source and all notes attached to that source.</returns>
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
        /// <summary>
        /// Returns an alert dialog that will notify the user 
        /// that the entered Youtube link is invalid.
        /// </summary>
        /// <returns>an alert dialog that will notify the user 
        /// that the entered Youtube link is invalid.</returns>
        public static MessageBoxResult InvalidYoutubeLinkErrorBox()
        {
            MessageBoxResult confirmBox =
            System.Windows.MessageBox.Show("The Youtube URL provided is not valid. Please try again.",
            "Youtube Link Error",
            System.Windows.MessageBoxButton.OK,
            MessageBoxImage.Error);
            return confirmBox;
        }
        /// <summary>
        /// Returns a confirmation dialog that will alert the user 
        /// if they are sure they want to logout.
        /// </summary>
        /// <returns> confirmation dialog that will alert the user 
        /// if they are sure they want to logout.</returns>
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
