   M S B A M L   `   `   `  ���� L  HSimpleTaskTracker, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SimpleTaskTracker.XAMLSimpleTaskTracker.XAML  '    !SimpleTaskTracker.XAML.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:SimpleTaskTracker.XAML     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  Simple Task Tracker��$	��887��$	��575��
������� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers	 P� Property
 P� Value��%�   '��       �� " ��.���  � �� �#3d4147��  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ���  � �� �#2e3135.���  � RowDefinitions �� $	��25* ��� $	��25* ��� $	��25* ��� $	��25* ��� $
��148* ��� $	��25* � � ColumnDefinitions �� $
��177* ��� $
��704* �.+��� $��2��$��50��$��0q�$	��Top=� K� ZIndex$ 2�����	#FF4CB944�� $��	0,-16,0,0q�$��Center=�$ 2�����	#00000000$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�$��14(�
������� �� TextAlignment$ Center=�Simple Task Tracker�� $	��177��$��22��$��0,28,0,0q�$
��Left=�$	��Top=�$ 2�����	#00000000$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�
�������$ Center=�$��Version 1.1��]� $��5��$��2��$��143,0,0,162q�.+�� -    �Name$ tasks_panel��$	��734��$��50��$��	30,0,0,-5q�$��Center=�$	��Top=����	#FF3D4147]� $��6��.+�� $��0,0,0,-6q����	#FF3D4147�� -    ��Name$ 	sel_panel��$��1��$��6��$��50��$��0q�$
��Left=�$ 1�����	#FF4CB944�� -    ��Name$ tasks_button��$��1��$��2��$��1q�$	��Top=�$��Center=����	#3F000000$��0q�$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�$��14(�
���������Z� .���� $	��175��$��50��.+��� $��41.016��$
��16.5��$��0,16,0,0q�$��Center=�$��Center=�$��14(� J� RenderTransformOrigin$ 
0.366,1.75&��� $�Tasks���� $��35��$��36.25��$��
15,-26,0,5q�$
��Left=�$��Bottom=� J� OpacityMask
 �����$ 0.487,0.458&�$G��A/SimpleTaskTracker;component/Graphics/icons8-alarm-clock-50_2.png���� -   $ reports_button��$��2��$��3��$��1q�$	��Top=�$��Center=�$��0q�$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�$��14(�
���������Z� .���� $	��175��$��50��.+��� $��72.516��$
��16.5��$��66.992,16,35.492,0q�$��Center=�$��Center=�$��14(�$ 
0.366,1.75&��� $�Reports���� $��35��$��36.25��$��
15,-31,0,5q�$
��Left=�$��Bottom=�
 �����$ 0.487,0.458&�$G��A/SimpleTaskTracker;component/Graphics/icons8-combo-chart-50_2.png���� -   $ settings_button��$��3��$��1q�$	��Top=�$��Center=�$��0q�$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�$��14(�
���������Z� .���� $	��175��$��50��.+��� $��69.516��$��20��$��66.992,16,38.492,0q�$��Center=�$��Center=�$��14(�$ 
0.366,1.75&��� $�Settings���� $��35��$��36.25��$��
15,-29,0,5q�$
��Left=�$��Bottom=�
 �����$ 0.487,0.458&�$D��>/SimpleTaskTracker;component/Graphics/icons8-settings-50_2.png���� -   $ about_button��$��5��$��50��$��0q�$��1q�$	��Top=�$��Center=�$��0q�$8��2/SimpleTaskTracker;component/Fonts/#Century Gothic)�$��14(�
���������Z� .���� $	��175��.+��� $��69.516��$��20��$��66.992,16,38.492,0q�$��Center=�$��Center=�$��14(�$ 
0.366,1.75&��� $�About���� $��35��$��36.25��$��
15,-29,0,5q�$
��Left=�$��Bottom=�
 �����$ 0.487,0.458&�$>��8/SimpleTaskTracker;component/Graphics/icons8-info-50.png�� � -     �Name$
 Main��$��6��$��1��$��0q�$��Stretch=�$	��Top=�  � NavigationUIVisibility$ Hidden=�