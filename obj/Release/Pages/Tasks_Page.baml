   M S B A M L   `   `   `  ���� F  BobserveTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:observeTest.PagesobserveTest.Pages  "    observeTest.Pages.Tasks_Page   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:observeTest.Pages     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       N� Title$  
Tasks_Page��$	��694��$	��410�� N� 	KeepAlive . $	��Top=�$��Center=�$��0,48,0,0q�.��� $	��Top=�$
��Left=�$	��668��.+��� -    ��Name$ tabCtrl��$��0,-23,-27,0q�$	��Top=�$��0q�$��0q�$
��Left=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName P� 
SourceName��%�  '��       '�� �    �� " ��.���  � ��
 ���� ��  � ��$ 70����  � ��$ Century Gothic)���  � �� �� " ��.R��� $��Border��
�������$��0,1,1,0q�
������ ._��� $ ContentSite��$��Center=�$
��Left=�$��Header��$
��10,2q�	 P� 
 �@��
IsSelected .  . ���  � �� �#ffff�� $ Border�� � �� �#121212P� $ Border��
 �@J�IsMouseOver . . ���  � ��
 ������� $ Border�� � �� �#3a3a3aP� 
 �@��
IsSelected . . ���  � ��
 �   ��� $ Border�� � �� �	#FF4CB944�� $ Border�� � ��$ 	0,3.5,0,0q��� $ Border�� � ��
 �   ��� $ Border�� � ��
 ������� " ��.���  � �� �#3d4147���� " ��.R��  � RowDefinitions �� $
��Auto ��� .+��� $
��auto��$
��Auto=�$��Disabled=�$��	-1,-5,0,0q�$��0q�.����  ��Name$ HeaderPanel��$��0q� K� IsItemsHost . �� $��1��$��20q�$��SelectedContent��.@��� -    ��Name$ 	addTabItm��$��35��$��30��$��0q�|�.  $��0q����� $��WrapWithOverflow=�$��30(�$��Center=�$��Center=�$��Adobe Arabic)�$
��Bold"�
�������+���� " ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��� $��Border��-   
�������$��0,1,1,0q����#303030._��� $ ContentSite��$��Center=�$
��Left=�$��Header��$
��10,2q�	 P� $ ContentSite��
 �@J�IsMouseOver . . ��� $ Border�� � ��
 ������� $ Border�� � �� �#404040