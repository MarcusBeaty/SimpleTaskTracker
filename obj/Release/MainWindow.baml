   M S B A M L   `   `   `  ���� F  BobserveTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:observeTestobserveTest      observeTest.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:observeTest     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  Simple Task Tracker��$	��887��$	��575��
������� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers	 P� Property
 P� Value��%�   '��       �� " ��.���  � �� �#3d4147��  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ���  � �� �	#3F000000.���  � RowDefinitions �� ��  � ColumnDefinitions �� $
��177* ��� $
��704* �.+��� $��50��$��0q�$	��Top=� K� ZIndex$ 2�����	#FF4CB944�� $	��177��$��44��$��0,6,0,0q�$
��Left=�$	��Top=�$ 2�����	#00000000$��Century Gothic)�$��14(�
������� �� TextAlignment$ Center=�Simple Task Tracker�� $	��177��$��22��$��0,28,0,0q�$
��Left=�$	��Top=�$ 2�����	#00000000$��Century Gothic)�
�������$ Center=�Version 1.0]� $��2��$��143,0,0,223q�.+�� -    �Name$ tasks_panel��$	��734��$��50��$��	30,0,0,-5q�$��Center=�$	��Top=����	#FF3D4147]� .+�� $��
0,0,0,-274q����	#FF3D4147�� -    ��Name$ 	sel_panel��$��5.666��$��50��$��0,50,0,0q�$
��Left=�$	��Top=�$ 1�����	#FF4CB944�� -    ��Name$ tasks_button��$��50��$��0,50,0,0q�$��1q�$	��Top=�$��Center=�$��0q�$��Century Gothic)�$��14(�
���������Z� .���� $	��175��$��49��.+��� $��41.016��$
��16.5��$��0,16,0,0q�$��Center=�$��Center=�$��14(� J� RenderTransformOrigin$ 
0.366,1.75&��� $�Tasks���� $��35��$��36.25��$��
15,-26,0,5q�$
��Left=�$��Bottom=� J� OpacityMask
 �����$ 0.487,0.458&�$!��icons8-alarm-clock-50_2.png���� -   $ reports_button��$��50��$��	0,100,0,0q�$��1q�$	��Top=�$��Center=�$��0q�$��Century Gothic)�$��14(�
���������Z� .���� $	��175��$��49��.+��� $��72.516��$
��16.5��$��66.992,16,35.492,0q�$��Center=�$��Center=�$��14(�$ 
0.366,1.75&��� $�Reports���� $��35��$��36.25��$��
15,-31,0,5q�$
��Left=�$��Bottom=�
 �����$ 0.487,0.458&�$!��icons8-combo-chart-50_2.png���� -   $ settings_button��$��50��$��	0,150,0,0q�$��1q�$	��Top=�$��Center=�$��0q�$��Century Gothic)�$��14(�
���������Z� .���� $	��175��$��49��.+��� $��69.516��$��20��$��66.992,16,38.492,0q�$��Center=�$��Center=�$��14(�$ 
0.366,1.75&��� $�Settings���� $��35��$��36.25��$��
15,-29,0,5q�$
��Left=�$��Bottom=�
 �����$ 0.487,0.458&�$��icons8-settings-50_2.png�� � -     �Name$
 Main��$��2��$��1��$��0q�$��Center=�$	��Top=�  � NavigationUIVisibility$ Hidden=�