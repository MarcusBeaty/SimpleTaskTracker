   M S B A M L   `   `   `  ����L  HSimpleTaskTracker, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SimpleTaskTracker.XAMLSimpleTaskTracker.XAML  5      &     SimpleTaskTracker.XAML.stopwatch   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:SimpleTaskTracker.XAML     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        $
��auto��5      $��353.176��5	      |�.   � FocusVisualStyle  5
      Z� .��5      � $��Center=�6   
�������$	��704��6;    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      $��23 �6   �� 5      $��22 �6   �� 5      $��23 �6   �� 5      �� 5      �� 5       � RowDefinitions 5!   
   �� 5"      $
��118* �6   �� 5#      $	��17* �6   �� 5$      $	��85* �6   �� 5%      $	��16* �6   �� 5&      $	��23* �6   �� 5'      $	��30* �6   �� 5(      $	��10* �6   �� 5)      $	��30* �6   �� 5*      $	��24* �6   .+�5,   
   �� -   5        $��sw_Name��5-      $��2��5.      $��4��5/      $��0��50      $	��120��51      $��
6,-1,16,16q�52       �� Padding$ 0,90,0,0q�53      $��Center=�54      $��Center=�55      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�56      $��24(�57      $�� ��58      �� 59   
   -   5         ��Name$ ClockIn��5:      $��5��5;      $��2��5<      $��1q�5=      ���	#FF00C770$��Clock-In��5@      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5A      $
��Bold"�5B      
�������$��2��5C       �� 5D   
   -   5        $ Resume��5E      $��5��5F      $��2��5G      $��2��5H      $��128.8��5I      ���	#FF00C770$��Resume��5L      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5M      
�������$
��Bold"�5O      $y�Hidden=�5P      �� 5Q   
   -   5        $ ClockOut��5R      $��5��5S      $��2��5T      $��2��5U      ���	#FFFF5E5E$��	Clock-Out��5X      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5Y      $
��Bold"�5Z      
�������$y�Hidden=�5\      {�.  5]      �� 5^   
   -   5        $ 
StartBreak��5_      $��5��5`      $��5��5a      $��1q�5b      ���	#FFF3C13B$��Start Break��5e      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5f      $
��Bold"�5g      
�������{�.  $��2��5i      �� 5j   
   -   5        $ EndBreak��5k      $��5��5l      $��5��5m      $��2��5n      $y�Hidden=�5o      ���	#FFF3C13B$��	End Break��5r      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5s      $
��Bold"�5t      
�������{�.  5v      �� 5w   
   -   5         ��Name$
 Time��5x      $��2��5y      $��9��5z      $	��378��5{      $��80��5|      $��	46,3,46,2q�5}      $��Center=�5~      $��Center=�5      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5�      $��65(�5�      
���   �$��00:00:00.000��5�       �� TextAlignment$ Center=�5�      $
��Wrap=�5�      ��5�      Z� 5�      �� 5�   
   -	   5        $ SW_Name��5�      $��2��5�      $��9��5�      $	��466��5�      $��0,0,4,15q�5�      $��Center=�5�      $��Center=�5�      $��Arial)�5�      $��24(�5�      $
��Bold"�5�      ���	#FF474747$ Center=�5�      $
��Wrap=�5�      ��5�      �� *	_taskName+��5�      Z� 5�      �� 5�   
   -
   5        $
��Edit��5�   D   $��15.4,83,50,15.4q�6S   $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�6m   ���	#FF3D4147���	#FF707070$��8��6�   $��11(�6�   ���	#FFEDEDED  6(   Z� 5�      �� 5�   
   $��9��5�      $��50,76,50,40q�5�      ���	#FF56CD4D5�      5�      5�      