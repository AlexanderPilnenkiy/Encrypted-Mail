   M S B A M L   `   `   `  ����E  AMailClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MailClient
MailClient  5   	   Q MMaterialDesignColors, Version=1.2.0.325, Culture=neutral, PublicKeyToken=nullY@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColorsMaterialDesignColors 5   	       MailClient.SentMessage   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MailClient     5   	   amaterialDesignColors@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColors     5   	   U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  SentMessage��5
   	   $	��489��6   $	��489��6*    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ NoResize=�5   	   ���	#FFD50000$��1q�67   .��5      � .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�5      $��16q�5      $��50��6   .��5      ]� $��
-8,0,-15,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�5      $	��497��6    $��	0,-9,0,-8q�6,   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ MailOutline=�6.   $��29��6A   $��32��6M   $��0,2,0,0q�6X   �� 5      $��Center=�5   	   $��
5,6,0,-0.6q�5   	   $	��165��6   $��29��6)   $��Times New Roman)�65   $��18(�6R   �� 6a   $�Encrypted Mail Client��6e   6�   �� 5      -   5        $��Center=�5   	   $��	210,8,0,7q�5   	   $��65��6   $��20��6'   $��Times New Roman)�63   $��18(�6P    � Cursor$
 Hand��6^   �� 6�   $�Закрыть��6�   6�   5      5      5      �� 5   
   -   5         ��Name$ to��5      $��labatri@yandex.ru��6   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Кому��5        %  !MaterialDesignFloatingHintTextBox#��[  5!      $��20,70,227,371q�5"      
 �� SelectionBrush

 �����   
Foreground
 �����.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush
 ����� �� 
CaretBrush
 �����$��Times New Roman)�5%   #   $��18(�6@   �� 5'   
   -   5        $ theme��5'      $�� ��6!   $	 Тема��5(      #��[  5)      $��20,115,227,326q�5*      

 �����
 �����
 �����
 �����$��Times New Roman)�5-   #   $��18(�6@   �� 5/   
   -   5        $ SentText��5/       * &MaterialDesignOutlinedTextFieldTextBox#��[ 50      $	 Текст письма��51      $��20,180,81,75q�52       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�54      $��1q�6   " �� VerticalScrollBarVisibility$
 Auto=�55      
�������
 �����$��Times New Roman)�56   #   $��18(�6@   �� 58   
   -   5        $
 keyY��58      $����PFJTQUtleVZhbHVlPjxNb2R1bHVzPjJFRnFiYXBRY2k0UUV5by91aThQcjlNeTYzUlZ3djZ1S2lHUG9nUnRDSDdUZjNvK1R2QjZmcmFkVG5Oa2x0Zk03OWI3dDkwQ0JyTHorcnF6V3h5ZTJVMlluZ1dXbWh4VVYvMldqenl5RE9rZjFIbXVKdVZiSCs5Qlp6K2dTVlluMTh0aEw1T0Y2TEVvcE1zd05FNUo0Q2RtY054NmRicE10Ky9tZUZNVTl4NWNUem9sWDNSTkpoWENJclluWEE0bENNckxhdXN6OFExODBGR3pDZzBzZDdpUG9nc216WmNvM1l2eWF4UldsS2l2bENhU2d3eXF6N244Si9xVXVuNnBCVFp1MFh2dG5hSzJNb1RORm0zRXZuS0RqbHU2aGFVeUwyNDRWWXdObEhoTlhsMFBKUkpkVUhvZVcvU2ZzZ3BqbGxmdHZ1N3hnTXJ1WTBRanNNdDRRUT09PC9Nb2R1bHVzPjxFeHBvbmVudD5BUUFCPC9FeHBvbmVudD48L1JTQUtleVZhbHVlPg==��6    #��[ 59      $6	 0Публичный ключ получателя��5:      $��20,417,81,10q�5;       . $
 Wrap=�5=      $��1q�6   $
 Auto=�5>      
�������
 �����$��Times New Roman)�5?   #   $��16(�6@   �� 5A   
   -   5         ��Name$ Attach��5A       & "MaterialDesignIconForegroundButton#��[ 5B      $��423,236,15,202q�5C      $
��Auto��6&   $��49��63   .��5D        $ 
Attachment=�5E       J� RenderTransformOrigin$ 0.5,0.5&�6   $��34��6?   $��36��6K   z�5F      V� 5G      .~�5H      �� �� 5I      �� 5J       �� Angle$ -61.91Q�6*   T� 5K      5L      5N      5O      �� 5Q   
   -   5        $ fontP��5Q      #��[ 5R      $��423,290,15,148q�5S      $
��Auto��6&   $��49��63   .��5T        $ FormatFontSizeIncrease=�5U      $ 0.5,0.5&�6+   $��30��6K   $��30��6W   5V      5W      �� 5Y   
   -   5        $ fontM��5Y      #��[ 5Z      $��423,344,15,94q�5[      $
��Auto��6%   $��49��62   .��5\        $ FormatFontSizeDecrease=�5]      $ 0.5,0.5&�6+   $��30��6K   $��30��6W   5^      5_      �� 5a   
   -	   5        $
 Send��5a      #��[ 5b      $��423,423,15,15q�5c      $
��Auto��6%   $��49��62   .��5d        $
 Send=�5e      $ 0.5,0.5&�6   $��30��69   $��30��6E   5f      5g      �� 5i   
   -
   5        $ SendL��5i      #��[ 5j      $��423,423,15,15q�5k      $
��Auto��6%   $��49��62   {�.  .��5l        $ SendLock=�5m      $ 0.5,0.5&�6   $��30��6=   $��30��6I   5n      5o      #  MaterialDesignThemes.Wpf.Card  5q   
     MaterialDesignBackground#���  5r      $��275,67,10,0q�6;   .��5s      �� .+�5t      9� $��Stretch=�5u      $��Вложения��5v      $��Times New Roman)�6   $��18(�66   .��5w      �� $ Vertical=�5x   	     MaterialDesignBody#���  5y   	   $��
24,8,24,16q�5z   	   .+�5{      �� -   5         ��Name$ 
attachment��5{   #   $��11(�6f   5|      5}      �� 5~        MaterialDesignDivider#���  5      $��1��5�      $��Stretch=�5�       J� SnapsToDevicePixels . 5�      5�      5�      �� 5�   
   -   5         ��Name$ number��5�      $��447,225,10,235q�5�      $��Palatino Linotype)�5�      $��16(�5�      ���	#FFD50000.��5�       06#   5�      5�      