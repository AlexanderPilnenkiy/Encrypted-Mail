   M S B A M L   `   `   `  ����E  AMailClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MailClient
MailClient  5   	       MailClient.MessageView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MailClient     5   	   U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  MessageView��5	   	   $	��489��6   $	��489��6*   $��Palatino Linotype)�5
   	   $��14(�6(    � 
ResizeMode$ NoResize=�66   $��1q�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Cursor$  ��6.   ���	#FFD50000 � WindowStyle$
 None=�6P   .��5      � $	��489��6   $	��489��6   .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�5      $��16q�5      $��50��6   .��5      ]� $��
-8,0,-15,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�5      $	��497��6    $��	0,-9,0,-8q�6,   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ MailOutline=�6.   $��29��6A   $��32��6M   $��0,2,0,0q�6X   �� 5      $��Center=�5   	   $��
5,6,0,-0.6q�5   	   $	��165��6   $��29��6)   $��Times New Roman)�65   $��18(�6R   �� 6a   $�Encrypted Mail Client��6e   6�   �� 5      -   5        $��Center=�5   	   $��	210,8,0,7q�5   	   $��65��6   $��20��6'   $��Times New Roman)�5      $��18(�66   $
 Hand��5      �� 5      $�Закрыть��6   60   5      5      5       �� 5"   
   -   5         ��Name$ IsTheme��5#      $��Times New Roman)�5"      $��18(�5$      
���   �	 �� TextWrapping$
	 Wrap=�5&      
 �� 
IsReadOnly
 . $��0q�5(      $��10,55,216,361q�5)      $
��Bold"�6'    �� SelectionOpacity$ 0Q�69   .��6N   
ТЕМА5+      #  MaterialDesignThemes.Wpf.Chip  5-   
   -   5          Name$
 Chip��5-      
���   �$��pi.16b.alex@gmail.com��5.         Icon$ P��5/   	   $	��409��6   $��Times New Roman)�6   $��18(�50      $��70,133,0,324q�51   	      IconBackground �	#FFD50000���	#12FFFFFF52        54   
   -   5        $ Chip2��54      
���   �$��pi.16b.alex@gmail.com��55      $ P��56   	   $	��250��6   $��Times New Roman)�6   $��18(�57      $��70,168,0,289q�58   	    �	#FFD50000���	#12FFFFFF59      �� 5;   
   -   5         ��Name$ dateTime��5;      $��16(�6   $��343,168,0,289q�60   $
��Left=�5<      $	��146��6+   ���	#DD858383$��Times New Roman)�5=   '   .��6D   01.12.2019 12:17:046Y   �� 5?   
   $��18(�6   $��10,133,0,325q�6   $
��Left=�5@      $��40��6+   
���   �$��Times New Roman)�5A   #   .��6@   От:6E   �� 5C   
   $��18(�6   $��10,169,0,289q�6   $
��Left=�5D      $��55��6+   
���   �$��Times New Roman)�5E   #   .��6@   	Кому:6G   �� 5G   
   -   5        $ tempt��5G      $��10,235,74,52q�6!   69   �� 5I   
   -   5        $ SentText��5I      
 .  *  &MaterialDesignOutlinedTextFieldTextBox#��[  5J      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Текст письма��5K      $��10,205,62,10q�5L       �� AcceptsReturn . $
	 Wrap=�5N      $��1q�6   " �� VerticalScrollBarVisibility$
 Auto=�5O      
������� �� 
CaretBrush
 �����$��Times New Roman)�5P   #   $��18(�6@   (  "System.Windows.Controls.WebBrowser  5R   
   -   5          Name$ browser��5R      $
��Left=�6'   $��75��6B   $��
50,250,0,0q�5S      $	��Top=�6)   $	��100��5T      $y�Hidden=�6!   �� 5V   
   -	   5         ��Name$ Answer��5V       & "MaterialDesignIconForegroundButton#��[ 5W      $��432,429,3,10q�5X      $
��Auto��6$   $��49��61   .��5Y        $ QuestionAnswer=�5Z       J� RenderTransformOrigin$ 0.5,0.5&�6#   $��30��5[      $��30��6   5\      5]      #  MaterialDesignThemes.Wpf.Card  5_   
     MaterialDesignBackground#���  5`      $��
279,54,6,0q�6;   .��5a      �� .+�5b      9� $��Stretch=�5c      $��Вложения��5d      $��Times New Roman)�6   $��18(�66   .��5e      �� $ Vertical=�5f   	     MaterialDesignBody#���  5g   	   $��
24,8,24,16q�5h   	   .+�5i      �� -
   5         ��Name$ 
attachment��5i   #   $��11(�67   6E   5j      5k      �� 5l        MaterialDesignDivider#���  5m      $��1��5n      $��Stretch=�5o       J� SnapsToDevicePixels . 5p      5q      5r      5t      5u      