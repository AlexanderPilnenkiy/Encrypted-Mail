   M S B A M L   `   `   `  ����E  AMailClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MailClient
MailClient  5   	       MailClient.ChooseMail   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MailClient     5   	   U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   ���	#FFD50000$��1q�5   2     � Title$  
ChooseMail��5	   	   $	��500��6   $	��415��6)    � WindowStyle$
 None=�65    � 
ResizeMode$ NoResize=�6H    � WindowStartupLocation$ CenterScreen=�6^   .��5
      � .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�5      $��16q�5      $��50��6   .��5      ]� $��
-8,0,-15,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�5      $	��497��6    $��	0,-9,0,-8q�6,   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ MailOutline=�6.   $��29��6A   $��32��6M   $��0,2,0,0q�6X   �� 5      $��Center=�5   	   $��	5,6,0,9.4q�5   	   $	��165��6   $��19��6(   $��Times New Roman)�64   $��18(�6Q   �� 6`   $�Encrypted Mail Client��6d   6�   �� 5      -   5        $��Center=�5   	   $��	140,8,0,7q�5   	   $��65��6   $��20��6'   $��Times New Roman)�63   $��14(�6P    � Cursor$
 Hand��6^   �� 6�   $�Закрыть��6�   6�   5      5      5      �� 5   
   -   5         ��Name$ MSList��5      	 �� SelectionMode$	 Multiple=�6#   $��10,56,9.6,54.4q�5      ���	#89FF0000$��Times New Roman)�6=   $��15(�6Z   
 �� View
 5       �� 5!      -   5        
MSGrid��.P�5"      �� $�� ��6%   -   5        	boxes�� �� Width$ 30��5"   >    �� CellTemplate 5#      �� 5$      .R�5%   "   ��  ��Name$ 	checkMail��5(   !   $��25��5%   +   $��25��66    d� 	IsChecked 5&   !   ��  �� RelativeSource ��  �� AncestorType$ ListBoxItemL� �� Path$ 
IsSelected� �� Mode$ TwoWay=�5(   !   5)      5+      �� 5,      -   5        Who��$	 170��5.       �� DisplayMemberBinding 5-      �� *Name+5.      5/      51      �� 53   
   -   5         ��Name$ deleteMS��53       &  "MaterialDesignIconForegroundButton#��[  54      $��10,454,367,8q�55      $��38��6$   $��38��6/   .��56        $
 Plus=�57       J� RenderTransformOrigin$ 0.5,0.5&�6   58      59      �� 5;   
   -   5        $	 Del��5;      #��[  5<      $��53,453,324,9q�5=      $��38��6$   $��38��6/   .��5>        $ Delete=�5?      $ 0.5,0.5&�6   5@      5A      �� 5C   
   -   5         ��Name$ number��5F      $��Times New Roman)�5C      $��16(�5D      
���   �$��10,60,245,405q�5F       .��67   " Ящики на аккаунте5H      �� 5J   
   -	   5        $ enter��5J      $
 Hand��6       MaterialDesignOutlinedButton#��[ 5K      
���  ��
�������
���   �$
��Auto��5M      $��180,454,10,9q�6   $��Times New Roman)�5N   &   $��16(�6C   .��6Q   
Войти5P      5Q      5R      