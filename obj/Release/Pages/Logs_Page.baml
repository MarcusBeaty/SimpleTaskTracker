   M S B A M L   `   `   `  ���� F  BobserveTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:observeTest.PagesobserveTest.Pages  !    observeTest.Pages.Logs_Page   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:observeTest.Pages     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       N� Title$  	Logs_Page�� N� 	KeepAlive . $	��668��$	��545��$	��Top=�$��Center=�$��0q�.���  � RowDefinitions �� $
��476* ��� $	��37* ��� $	��32* �.+��� $��-3,434,0,24q�$��Center=�$��14(�$a��[Note: Removing an entry from the Reports Sheet will also remove the corresponding Task Tab.�� J� RenderTransformOrigin$ 0.5,0.5&�$
��Bold"�$	��671��$��Center=� �� TextAlignment$ Center=�$��Century Gothic)���Z� z�V� .~��� ��  �� AngleX$	 0.5Q��� T�  T� X$ 0.633Q��� -   $��245,0,243,1.667q�$��Delete�����	#FFE86048
�������$��Century Gothic)�$��14(����	#FF707070$��1��&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$	��668��$	��344��$
��Left=�$	��Top=�   AutoGenerateColumns .  	   CanUserAddRows	 .  
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 *    RowHeaderWidth$ 0Q�   SelectionUnit$
 Cell=�$��Century Gothic)����	#FFF0F0F0$��0,65,0,0q�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width$
 auto    HeaderTemplate �� .R���  ��Name$ checkBoxHeader��-    d� 	IsChecked �� *
Selected+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   CellTemplate �� .R��� $ checkBox��$��Center=� d� IsThreeState .   �� *
Selected+$ TwoWay=�$ PropertyChanged=�0  *System.Windows.Controls.DataGridTextColumn     Header$
 Task��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Task+$ TwoWay=�  $ Clock-In�� .  �� *	ClockIn+$ TwoWay=�$ PropertyChanged=�  $ 	Clock-Out�� .  �� *
ClockOut+$ TwoWay=�  $ Total�� .  �� *Total+$ TwoWay=�  $ Last Closed�� .  �� *
LastClosed+$ TwoWay=�  $ Selected�� .    
Visibility$ Hidden=� �� *
Selected+$ TwoWay=�$ PropertyChanged=�