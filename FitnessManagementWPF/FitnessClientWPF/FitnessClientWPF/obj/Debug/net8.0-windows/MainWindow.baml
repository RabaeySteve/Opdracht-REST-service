   M S B A M L   `   `   `  ����K  GFitnessClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FitnessClientWPFFitnessClientWPF  5   	   !    FitnessClientWPF.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:FitnessClientWPF     5   	     � Title$  
MainWindow��5   	   $	��780��6   $	��700��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �  5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6     5      �� 5      $��* �6#   .+�5       �� $��0��6   $��0��6%   $��Make Reservation��65   $��24(�6M   $
��Bold"�6[   ���#2c3e50$��10q�6�   $��Center=�6�   �� 5"      $��1��6   $��0��6&   $��20q�66   .+�5#      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�66   $��0,0,0,10q�6N   .+�5$      �� $��Member:��6   $��Center=�6.   ���#34495e$��14(�6^   $��0,0,10,0q�6l   �� 5%      -   5        $��cbMember��5%       �� 
IsEditable$
 True��6/   $��14(�6o   ���#2c3e50$	��400��6�   $
��Left=�6�    �� Text 6�   ��  �� Path$ 
MemberName� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5&      �� 5(      $ 
Horizontal=�6   $	��Top=�66   $��0,0,0,10q�6N   .+�5)      �� $��Date:��6   $��Center=�6,   ���#34495e$��14(�6\   $��0,0,10,0q�6j   (  "System.Windows.Controls.DatePicker  5*      -   5        $��dpDate��5*   !   $	��200��6/   $��14(�6;   $��	23,0,10,0q�6I   �� 5+      -   5        $��	SearchBtn��5+      $��Search��6.   $��80��6?   ���#3498db
�������$��16(�6s   $
��Bold"�6�   $��0q�6�   	 � Cursor$
	 Hand��6�   6�   5,      �� 5-      $ 
Horizontal=�6   $	��Top=�66   $��0,0,0,10q�6N   5/      �� 50      $+��%Selected member reservation for date:��6   $��Center=�6J   6i   &   System.Windows.Controls.DataGrid  51      -   5        
  Name$
 ReservationsList��51         AutoGenerateColumns$ False��65   $	��100��6Q   ��6^   �� *ReservationsList+   Columns 52      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$
 Date��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *Date+6T     55      $ 	StartTime��6-   $
 Auto 6^    6@   �� *	StartTime+6^     56      $ EndTime��6-   $
 Auto 6Z    6>   �� *	EndTime+6Z     57      $ PartOfTheDay��6-   $ * 6a    6C   �� *	PartOfDay+6a     58      $ EquipmentId��6-   $
 Auto 6b    6B   �� *EquipmentId+6b     59      $ equipmentType��6-   $ * 6f    6D   �� *EquipmentType+6f   5<      �� 5=      $ 
Horizontal=�6   $	��Top=�66   $��0,0,0,10q�6N   .+�5?      �� $��Hours:��6   $��Center=�6-   ���#34495e$��14(�6]   $��0,0,10,0q�6k   �� 5@      -   5         ��Name$ Hours��5@      $��50��6Y   $��Center=�6d   $��0,0,5,0q�6   5C      �� 5F      $ 
Horizontal=�6   $	��Top=�66   $��0,0,0,10q�6N   .+�5G      �� $��
TimeSlots:��6   $��Center=�61   ���#34495e$��14(�6a   $��0,0,10,0q�6o   �� 5H      -   5        $ 	TimeSlots��5H      $
 True��62   $	��200��6D   $��Center=�6P   $��-9,0,5,0q�6k   �� 5J      -   5         ��Name$
 Find��5J      $
��Find��6+   $��80��6:   $��30��6E   $��Center=�6Q   ���#3498db
�������$��16(�6�   $
��Bold"�6�   $��0q�6�   $
	 Hand��6�   6�   5L      � 5M       5O      �� 5P      $��* �6+   �� 5R      $��* �6+   .+�5W        -   5        $
 EquipmentGrid��5W      $ False��5X      $��10q�5Y      $	��260��5Z      $	��150��5[         
IsReadOnly$
 True��5\      $��0��5]       5^        5_      $ Id��61   $
 Auto 6]    6=   �� *EquipmentId+6]     5`      $ 	Equipment��61   $
 Auto 6f    6D   �� *EquipmentType+6f   5b        5e      -	   5        $
 SecondEquipment��5e      $ False��5f      $��10q�5g      $	��260��5h      $	��150��5i      $
 True��5j      $��1��5k       5l        5m      $ Id��61   $
 Auto 6]    6=   �� *EquipmentId+6]     5n      $ 	Equipment��61   $
 Auto 6f    6D   �� *EquipmentType+6f   5p      5q      �� 5t      $ 
Horizontal=�6   $	��Top=�66   $��0,10,0,0q�6N   .+�5u      �� -
   5        $��Show Selected��5u      $	��150��65   $��10q�6A   6M   �� 5v      -   5         ��Name$ SelectedItemsDisplay��5v       $��16(�6>   $��Center=�6L   $
��Wrap=�6g   5w      �� 5y      -   5        $	��Add��5y      $	��200��6G   $��40��6S   $��10q�6_   $	��Top=�6k   $��Center=�6�   ���#3498db
�������$��16(�6�   $
��Bold"�6�   $��0q�6�   $
	 Hand��6�   5z      5{      ]� 5~   
   $��2��6   $��2��6!   .+�5      �� $��Bottom=�6   .@�5�      �� .��5�      �� -   5        $��StatusBarTxt��5�       5�      5�      5�      5�      5�      