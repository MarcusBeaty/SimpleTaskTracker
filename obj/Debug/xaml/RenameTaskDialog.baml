   M S B A M L   `   `   `  ����L  HSimpleTaskTracker, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SimpleTaskTracker.XAMLSimpleTaskTracker.XAML  5   	   -    'SimpleTaskTracker.XAML.RenameTaskDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:SimpleTaskTracker.XAML     5   	   -   5          � Title$  Rename Task��5   	   $	��500��5   	   $	��170��5	   	   
�������
���  �����	#FF00CFFD � Icon$G A/SimpleTaskTracker;component/Graphics/icons8-alarm-clock-50_2.png��5   	    � 
ResizeMode$ NoResize=�5   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��9* �6   �� 5      $
��134* �6   �� 5      $	��86* �6   �� 5      $
��128* �6   �� 5      $
��127* �6   �� 5      $	��10* �6    � RowDefinitions 5   
   �� 5      $	��40* �6   �� 5       $	��24* �6   �� 5!      $	��18* �6   �� 5"      $	��29* �6   �� 5#      $	��30* �6   .+�5&   
   �� $��1��5'      $��1��5(      $��1,3,2,0q�5)      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5*      $��15(�5+      ���	#FF212121$��New Task Name:��5-       �� TextAlignment$ Center=�5.      �� 5/   
   -   5        $��	taskEntry��50      $��1��51      $��2��52      $��3��53      $��5,0,0,0q�54       �� SelectionBrush �	#FF25252556      �� 57   
   -   5        $��3��58      $��3��59      $��1,0,5,0q�5:      
�������$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5=      .��6M   Rename5?      �� 5@   
   -   5        $��3��5A      $��4��5B      $��5,0,0,0q�5C      ���	#00F1F1F1$��1q�5E      $��Cancel��5G      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5H      5I      5J      