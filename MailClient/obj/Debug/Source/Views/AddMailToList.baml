   M S B A M L   `   `   `  ����E  AMailClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MailClient
MailClient  5   	       MailClient.AddMailToList   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MailClient     5   	   U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  AddMailToList��5	   	   $	��250��6   $	��320��6,    � WindowStyle$
 None=�68    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�6   ���	#FFD50000$��1q�5   !   .��5      � .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�5      $��16q�5      $��50��6   .��5      ]� $��
-8,0,-15,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�5      $	��307��6    $��	0,-9,0,-8q�6,   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ MailOutline=�6.   $��29��6A   $��32��6M   $��0,2,0,0q�6X   �� 5      $��Center=�5   	   $��
5,6,0,-0.6q�5   	   $	��163��6   $��29��6)   $��Times New Roman)�65   $��18(�6R   �� 6a   $�Encrypted Mail Client��6e   6�   �� 5      -   5        $��Center=�5   	   $��
46,8,0,6.4q�5   	   $��53��6   $��20��6(   $��Times New Roman)�64   $��14(�6Q    � Cursor$
 Hand��6_   �� 6�   $�Закрыть��6�   6�   5      5      5      �� 5   
   -   5         ��Name$
 mail��5      $�� ��6    )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 E-Mail��5       %  !MaterialDesignFloatingHintTextBox#��[  5       $��45,70,45.4,137q�5!      
 �� SelectionBrush

 �����   
Foreground
 �����.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush
 ����� �� 
CaretBrush
 �����$��Times New Roman)�5$   #   G� 5&   
   -   5         G�Name$ password��5'       G� Password$  ��6   $	 Пароль��5(      
 �����
 �����  MaterialDesignPasswordBox#��[ 5+      $��44,116,44.4,91q�66   $��Center=�6N   $	��230��6k   $��Times New Roman)�6w   �� 5-   
   -   5        $
 Hand��5-         MaterialDesignOutlinedButton#��[ 5.      
���  ��
�������
���   �$
��Auto��50      $��45,183,45,21q�6   $��Times New Roman)�51      $��14(�6*   .��68   Добавить ящик53      55      56      