﻿#pragma checksum "..\..\..\..\View\SourcePageWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C477000D875616CF3CCC23C4A9A112D0E3DA30BC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Group4DesktopApp.View;
using Group4DesktopApp.ViewModel;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Group4DesktopApp.View {
    
    
    /// <summary>
    /// SourcePageWindow
    /// </summary>
    public partial class SourcePageWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser pdfViewer;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstNotes;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBackHome;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSourceTitle;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNoteBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddNote;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid NoteModifyGrid;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdateNote;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelModify;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\View\SourcePageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Web.WebView2.Wpf.WebView2 youtubePlayer;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Group4DesktopApp;component/view/sourcepagewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\SourcePageWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.pdfViewer = ((System.Windows.Controls.WebBrowser)(target));
            return;
            case 2:
            this.lstNotes = ((System.Windows.Controls.ListBox)(target));
            
            #line 22 "..\..\..\..\View\SourcePageWindow.xaml"
            this.lstNotes.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstNotes_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnBackHome = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\View\SourcePageWindow.xaml"
            this.btnBackHome.Click += new System.Windows.RoutedEventHandler(this.btnBackHome_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblSourceTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.txtNoteBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnAddNote = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\View\SourcePageWindow.xaml"
            this.btnAddNote.Click += new System.Windows.RoutedEventHandler(this.btnAddNote_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NoteModifyGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.btnUpdateNote = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\View\SourcePageWindow.xaml"
            this.btnUpdateNote.Click += new System.Windows.RoutedEventHandler(this.btnUpdateNote_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancelModify = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\View\SourcePageWindow.xaml"
            this.btnCancelModify.Click += new System.Windows.RoutedEventHandler(this.btnCancelModify_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.youtubePlayer = ((Microsoft.Web.WebView2.Wpf.WebView2)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

