   M S B A M L   `   `   `  ���� L  HSimpleTaskTracker, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SimpleTaskTracker.XAMLSimpleTaskTracker.XAML  '    !SimpleTaskTracker.XAML.Tasks_Page   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:SimpleTaskTracker.XAML     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     N� Title$  
Tasks_Page��|�. $
��Auto��$	��410��$��0,48,0,0q�$	��Top=� � IsSharedSizeScope .  N� 	KeepAlive .  � UseLayoutRounding . .��� $	��Top=� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* �.+��� -    ��Name$ tabCtrl��$��5��$��5��$��	0,-23,0,0q�$��0q�$	��Top=�$��Stretch=�$��Center=�$��0q� .  � FocusVisualStyle Z�  � 	Resources 	 �� 
TargetType
 �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName P� 
SourceName��%  '��       '�� �    �� "	 ��.��� 
 � ��
 ���� �� 
 � ��$ 70���� 
 � ��$8 2/SimpleTaskTracker;component/Fonts/#Century Gothic)��� 
 � �� �� " ��.R��� $��Border��
������ 
�������$��0,1,1,0q�._��� $ ContentSite��$
��10,2q�$
��Left=�$��Center=�$��Header�� P�  �@��
IsSelected .  . ��� 
 � �� �#ffff�� $ Border��
 � �� �#121212P� $ Border�� �@J�IsMouseOver . . ��� 
 � ��
 ������� $ Border��
 � �� �#3a3a3aP�  �@��
IsSelected . . ��� 
 � ��
 �   ��� $ Border��
 � �� �	#FF4CB944�� $ Border��
 � ��$ 	0,3.5,0,0q��� $ Border��
 � ��
 �   ��� $ Border��
 � ��
 ������� "	 ��.��� 
 � �� �#3d4147���� " ��.R��  �� $
��Auto ��� .+��� $��1��$��3��$
��Auto��$
��auto��$��0,-5,0,0q�$��0q�$��Stretch=�$
��Left=� . $
��Auto=� . $��Disabled=� Z� .����  ��Name$ HeaderPanel��$��0q� K� IsItemsHost . �� $��1��$��20q�$��SelectedContent��.@��� -    ��Name$ 	addTabItm��$��35��$��30��$��0q�$��0q�|�.  ���� $��Center=�$��Center=�$6��0/SimpleTaskTracker;component/Fonts/#Adobe Arabic)�$��30(�$
��Bold"�
�������$��WrapWithOverflow=�+���� "	 ��.��� 
 �@�OverridesDefaultStyle . �� 
 � �� �� " ��.R��� $��Border��-   ���#303030
�������$��0,1,1,0q�._��� $ ContentSite��$
��10,2q�$
��Left=�$��Center=�$��Header�� P� $ ContentSite�� �@J�IsMouseOver . . ��� $ Border��
 � ��
 ������� $ Border��
 � �� �#404040