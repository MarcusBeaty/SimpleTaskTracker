   M S B A M L   `   `   `  ���� L  HSimpleTaskTracker, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SimpleTaskTracker.XAMLSimpleTaskTracker.XAML  &     SimpleTaskTracker.XAML.Logs_Page   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:SimpleTaskTracker.XAML     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       N� Title$  	Logs_Page��$	��668��$	��545��$��0q�$��Center=�$	��Top=� N� 	KeepAlive . .���  � RowDefinitions �� $
��476* ��� $	��37* ��� $	��32* �.+��� $	��671��$��-3,434,0,24q�$��Center=�$��Center=�$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�$��14(�$
��Bold"� J� RenderTransformOrigin$ 0.5,0.5&�$a��[Note: Removing an entry from the Reports Sheet will also remove the corresponding Task Tab.�� �� TextAlignment$ Center=���Z� z�V� .~��� ��  �� AngleX$	 0.5Q��� T�  T� X$ 0.633Q��� -   $��
Delete_Btn��$��1��{�.  $��245,0,243,1.667q����	#FFE86048���	#FF707070$��Delete��$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�$��14(�
�������&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$	��668��$	��344��$��0,65,0,0q�$
��Left=�$	��Top=�   AutoGenerateColumns .  ���	#FFF0F0F0	   CanUserAddRows	 .  
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 * $8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�   RowHeaderWidth$ 0Q�   SelectionUnit$
 Cell=� � ContextMenu �� .@�m�  m� Command$ 	SelectAll��m� $
 Copy��   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width$
 auto    HeaderTemplate �� .R���  ��Name$ checkBoxHeader��-    d� 	IsChecked �� *
Selected+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   CellTemplate �� .R��� $ checkBox��-   $��Center=� d� IsThreeState .   �� *
Selected+$ TwoWay=�$ PropertyChanged=�0  *System.Windows.Controls.DataGridTextColumn     Header$
 Task��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Task+$ TwoWay=�  $ Clock-In�� .  �� *	ClockIn+$ TwoWay=�$ PropertyChanged=�  $ 	Clock-Out�� .  �� *
ClockOut+$ TwoWay=�  $ Total (Hours)�� .  �� *Total+$ TwoWay=�  $ Last Closed�� .  �� *
LastClosed+$ TwoWay=�  $ Selected�� .    
Visibility$ Hidden=� �� *
Selected+$ TwoWay=�$ PropertyChanged=�