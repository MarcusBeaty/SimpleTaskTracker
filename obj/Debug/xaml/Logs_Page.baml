   M S B A M L   `   `   `  ����L  HSimpleTaskTracker, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SimpleTaskTracker.XAMLSimpleTaskTracker.XAML  5      &     SimpleTaskTracker.XAML.Logs_Page   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:SimpleTaskTracker.XAML     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  	Logs_Page��5      $	��668��5	      $	��545��5
      $��0q�5      $��Center=�5      $	��Top=�5       N� 	KeepAlive . .��5      �  � RowDefinitions 5   
   �� 5      $
��476* �6   �� 5      $	��37* �6   �� 5      $	��32* �6   .+�5   
   �� $	��671��5      $��-3,434,0,24q�5      $��Center=�5      $��Center=�5      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5      $��14(�5      $
��Bold"�5       J� RenderTransformOrigin$ 0.5,0.5&�5       $a��[Note: Removing an entry from the Reports Sheet will also remove the corresponding Task Tab.��5!       �� TextAlignment$ Center=�5"      ��5#      Z� z�5$      V� 5%      .~�5&      �� �� 5'       �� AngleX$	 0.5Q�6$   �� 5(      T� 5)       T� X$ 0.633Q�6)   5*      5,      �� 5-   
   -   5        $��
Delete_Btn��5.      $��1��5/      {�.  $��245,0,243,1.667q�51      ���	#FFE86048���	#FF707070$��Delete��55      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�56      $��14(�57      
�������58      &   System.Windows.Controls.DataGrid  5:   
   -   5          Name$ dataGrid��5;      $	��668��5<      $	��344��5=      $��0,65,0,0q�5>      $
��Left=�5?      $	��Top=�5@         AutoGenerateColumns .  ���	#FFF0F0F0	   CanUserAddRows	 .  
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 * 5D      $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�5E         RowHeaderWidth$ 0Q�5F         SelectionUnit$
 Cell=�5G       � ContextMenu 5H      �� 5I      .@�5J      m�  m� Command$ 	SelectAll��6   65   m� 5K      $
 Copy��6   60   5L         Columns 5N      4  .System.Windows.Controls.DataGridTemplateColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Width$
 auto 6)      HeaderTemplate 5Q      �� 5R      .R�5S      ��  ��Name$ checkBoxHeader��5U   !   -   5         d� 	IsChecked 5W   !   �� *
Selected+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5X   !   5Y         CellTemplate 5[      �� 5\      .R�5]      �� $ checkBox��5^   !   -   5        $��Center=�5_   !    d� IsThreeState .   5b   !   �� *
Selected+$ TwoWay=�$ PropertyChanged=�5c   !   5d      5f      0  *System.Windows.Controls.DataGridTextColumn  5h         Header$
 Task��5j         
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 5i      �� *Task+$ TwoWay=�5k        5l      $ Clock-In��5n       .  5m      �� *	ClockIn+$ TwoWay=�$ PropertyChanged=�5o        5p      $ 	Clock-Out��5r       .  5q      �� *
ClockOut+$ TwoWay=�5s        5t      $ Total (Hours)��5v       .  5u      �� *Total+$ TwoWay=�5w        5x      $ Last Closed��5z       .  5y      �� *
LastClosed+$ TwoWay=�5{        5~      $ Selected��5�       .    
Visibility$ Hidden=�5�       5      �� *
Selected+$ TwoWay=�$ PropertyChanged=�5�      5�      5�      5�      