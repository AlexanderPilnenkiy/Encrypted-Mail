   M S B A M L   `   `   `  ����Q  MMaterialDesignColors, Version=1.2.0.325, Culture=neutral, PublicKeyToken=nullY@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColorsMaterialDesignColors  5   	   E AMailClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MailClient
MailClient 5	   	      MailClient.StartWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   amaterialDesignColors@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColors     5   	   U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $localclr-namespace:MailClient  5	   	     � Title$  Вход��5
   	   $	��261��6   $	��300��6#    � WindowStyle$
 None=�6/    � 
ResizeMode$ NoResize=�6B    � Topmost .  � WindowStartupLocation$ CenterScreen=�6g   ���	#FFCD0707$��1q�6�   .��5      � $��0,0,0,48q�6   .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�5      $��16q�5      $��50��6   .��5      ]� $��-8,0,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�5      $	��282��6    $��	0,-9,0,-8q�6,   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ MailOutline=�6.   $��29��5      $��32��6"   $��0,3,0,0q�6-   �� 5      $��Center=�5      $��
5,8,0,-2.6q�5      $	��170��6)   $��29��65   $��Times New Roman)�5      $��18(�62   �� 5      $�Encrypted Mail Client��6   6>   �� 5      -   5        $��Center=�5   	   $��
20,6,0,4.4q�5   	   $��50��6   $��24��6(   $��Palatino Linotype)�64   $��16(�6S    � Cursor$
 Hand��6a   �� 6�   $�
Выход��6�   6�   5      5      5      � 5!   
   -   5        	 �Name$	 Fields��5!      $��10,71,90,260q�6   .+�5"      �� -   5        
 ��Name$

 mail��5"      $��qwerty��6$   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Аккаунт��5#       %  !MaterialDesignFloatingHintTextBox#��[  5$      $��34,-11,-39.6,-36q�5%       �� SelectionBrush
 �����   
Foreground
 �����.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush
 ����� �� 
CaretBrush
 �����$��18(�5(   #   $��Times New Roman)�61   G� 5*      -   5         G�Name$ password��5+       G� Password$ qwerty��6   $ Пароль��5,      
 �����
 �����  MaterialDesignPasswordBox#��[ 5/      $��33,43,-40.6,-90q�66   $��Center=�6O   $	��206��6l   $��18(�6x   $��Times New Roman)�6�   50      � 52   
   -   5        $	 Button��52      $��	0,0,0,-35q�6   .+�53      �� -   5        $
 Hand��53         MaterialDesignOutlinedButton#��[ 54      
���  ��
�������
���   �$
��Auto��55   I   $��43,173,47,38q�6W   $��Times New Roman)�6�   $��18(�6�   .��6�   
Войти57      58      � 5:   
   -   5        $	 Create��5:      $��	0,0,0,-35q�6   .+�5;      �� -   5        $
 Hand��5;      #��[ 5<      
���  ��
�������
���   �$
��Auto��5=   I   $��43,215,47,-4q�6W   $��Times New Roman)�6�   $��18(�6�   .��6�   Создать аккаунт5?      5@      � 5B   
   -	   5        $	 grid_cd��5B      $��Center=�6    $	��230��5C      $��	0,0,0,-50q�6   $��Center=�5D      $	��298��6*   ���	#CC040303$y�Hidden=�5E   &   .+�5F      �� $��80,140,73,50q�6   $!��Загрузка почты��6*   $��Palatino Linotype)�6C   $��18(�6b   ���	#FFD500006p   � 5G      -
   5         �Name$ progress��5G       % !MaterialDesignCircularProgressBar#���  5H      $��Center=�5I      $��16q�5J       � IsIndeterminate .  � Value$ 99.99Q�5L      $��40��6!   $��40��6,   ���	#FFD5000068   5M      5O      5P      