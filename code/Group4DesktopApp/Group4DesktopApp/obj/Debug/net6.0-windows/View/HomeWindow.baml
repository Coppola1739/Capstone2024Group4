   M S B A M L   `   `   `  ����K  GGroup4DesktopApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Group4DesktopApp.ViewGroup4DesktopApp.View  5   	   G(clr-namespace:Group4DesktopApp.ViewModelGroup4DesktopApp.ViewModel  6;   &     Group4DesktopApp.View.HomeWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Group4DesktopApp.View     5   	   >	viewmodel(clr-namespace:Group4DesktopApp.ViewModel     6;     � Title$  
HomeWindow��5   	   $	��800��6   $
��1190��6)    � WindowStartupLocation$ CenterScreen=�66    � 	Resources 5	         SourcesTemplate �� Orientation��%D  &	        �� 5
   
   .R�5      � $��64��6   $	��229��6   .+�5      �� $
��Left=�67   $	��Top=�6R   $��24��6j   $	��226��6v   $��65,0,0,0q�6�   ��6   �� *
SourceName+6�   �� 5      $
��Left=�6   $��49��63   $��6,4,0,0q�6?   $	��Top=�6P   $��54��6h   ��6s   �� *SourceImage+�� 5      $��69,29,-66,-29q�6   $ 
Horizontal=�64   .+�5      �� $
��Wrap=�6    $��Date Created: ��64   �� 5      $
��Wrap=�6    ���	#FF0050FF$��64��6g   $	��226��6s   ��64   �� *
UploadDate+6s   5      5      5      5      .��5      � .+�5   
   �� -   5         ��Name$ SourcesList��5   C   #���   6   $��61,94,784,131q�6�   ��6X   �� *SourceDataProperty+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated$
 True�� �� UpdateSourceTrigger$ PropertyChanged=�6  �� 5   
   -   5         ��Name$ btnViewSource��5      $��View Source��6(   $��183,0,906,84q�6>   $��25��6p   $��Bottom=�6|   �� 5   
   -   5        $ btnDeleteSource��5      $��
Delete🗑��6*   $��183,0,906,50q�6=   $��25��6q   $��Bottom=�6}   
���  ��
����������	#FF750000$y�	Collapsed=�6�   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   -   5        
 ��Name$
 
lblWelcome��5      $��Welcome��6$   $��
35,55,50,0q�66   $
��Bold"�6J   $��16(�6\   $��31��6j   $	��Top=�6v   � 5   
   $��
0,94,31,84q�6   $��Right=�6#   $	��630��6?   .+�5"      � -   5         �Name$ 
uploadGrid��5"      $��Center=�6'   $��	0,118,0,0q�6D   $	��Top=�6W   $	��496��6o   $y�	Collapsed=�6|   .+�5$      �� $	��496��6   $��Center=�6)   $��Center=�6D   $	��630��6a   	 5%        5&      5(      �� 5+      -   5         ��Name$ stackFileChoose��5+      $��260,21,260,0q�66   $ Vertical=�6L   $y�	Collapsed=�6c   .+�5,      �� -   5        $ btnFileChoose��5,      $��Choose File��64   $��26��6J   $	��110��6V    K� ZIndex$ 3��6~   �� 5-      -   5        $
 lblUploadedSource��5-      $��No file chosen��67   $	��444��6P   $��Center=�6]   $ 3��6�   5/      � 51      -	   5        $ youtubeGrid��51      $��52��6,   $��38,21,10,423q�68   $y�	Collapsed=�6N   .+�52      �� $��Youtube Link:��6   $��0,0,0,26q�64   �� 54      $ 
Horizontal=�6!   $��0,26,0,0q�6:   .+�55      �� -
   5         ��Name$ txtYoutubeUrl��55   "   $	��503��69   $��19��6E   �� 56      -   5        $ btnYoutubeUpload��56   !   $��Upload��6;   $��62��6L   $��15,0,0,0q�6W   $��22��6�   57      59      �� 5:      -   5        $ 	btnCancel��5:      $��Cancel��6,   $
��Left=�6=   $��
283,78,0,0q�6X   $	��Top=�6l   $��72��6�   $y�	Collapsed=�6�   6�   �� 5<      -   5        $ MetaDataContainer��5<      $��124,21,108,0q�68   $ Vertical=�6N   .+�5=      �� -   5        $ stackMetaData��5=   !   $ Vertical=�68   $	��422��6O   $��0,70,0,0q�6\   $y�	Collapsed=�6n   .+�5>      �� $��Source Name:��6    $��26��67   $	��398��6C   �� 5?      -   5        $ txtSourceName��5?   "   $��30��6:   $	��394��6F   $��14(�6�   ��6R   �� *SourceNameProperty+$ TwoWay=�6�   �� 5@      -   5        $
 lblSrcNameError��5@       $��Source Name is required��69   
���  ��$
��Bold"�6l   $y�	Collapsed=�6~   �� 5A      $��Author First Name:��6    $��26��6=   $	��392��6I   �� 5B      -   5        $ txtAuthFirstName��5B   "   $��30��6<   $	��394��6H   $��14(�6�   ��6T   �� *AuthorFirstNameProperty+$ TwoWay=�6�   �� 5C      -   5        $
 lblAuthFirstError��5C       $#��Author First Name is required��6;   
���  ��$
��Bold"�6t   $y�	Collapsed=�6�   �� 5D      $��Author Last Name:��6    $��26��6<   $	��390��6H   �� 5E      -   5        $ txtAuthLastName��5E   "   $��30��6;   $	��394��6G   $��14(�6�   ��6S   �� *AuthorLastNameProperty+$ TwoWay=�6�   �� 5F      -   5        $
 lblAuthLastError��5F       $"��Author Last Name is required��6:   
���  ��$
��Bold"�6r   $y�	Collapsed=�6�   �� 5G      $��Title:��6    $��26��61   $	��394��6=   �� 5H      -   5        $ txtTitle��5H   "   $��30��64   $	��394��6@   $��14(�6�   ��6L   �� *TitleProperty+$ TwoWay=�6�   �� 5I      -   5        $
 lblTitleError��5I       $��Title is required��67   
���  ��$
��Bold"�6d   $y�	Collapsed=�6v   �� 5J      -   5        $ 	btnUpload��5J   !   $��Upload��64   $	��104��6E   $��25��6Q   $��0,20,0,0q�6]   6o   5K      5L      5N      �� 5P      $��SourceType:��6   $��Center=�6*   $��0,37,0,0q�6N   $��14(�6`   $��31��6n   $	��Top=�6z   $��Center=�6�   $	��110��6�   �� 5Q      -   5         ��Name$ cmbSourceType��5Q      $��0,72,0,0q�6.   $��Center=�6�   $	��242��6�   $��27��6�   $	��Top=�6  ��6@   �� *SourceTypeListProperty+ �� SelectedItem 6o   �� *SelectedSourceProperty+$ TwoWay=�$ PropertyChanged=�6  �� 5R      $��Source Upload��6   $
��Bold"�6,   $��Center=�6>   $��20(�6b   $��Center=�6p   $	��630��6�   $��37��6�   $	��Top=�6�   5T      o� 5U   
   $��52��6   $	��Top=�6   .@�5V      �� -   5        $ 	btnLogout��5V      $��Logout��6(   $��0,15,0,0q�69   6K   5W      5Y      5Z      