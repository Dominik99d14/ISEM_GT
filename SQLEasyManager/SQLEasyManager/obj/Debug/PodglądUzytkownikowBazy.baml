   M S B A M L   `   `   `  ����  SQLEasyManager-    'SQLEasyManager.PodglądUzytkownikowBazy   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Database Users��5   	   $	��300��6    $	��400��6-   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5
   
   �� -   5         ��Name$ UsersListBox��5
      $��Stretch=�6(   $��Stretch=�6F   $��0��6b   ��5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      $��* �6/   �� 5      $
��Auto �6/   .+�5      �� $��Center=�65   $��0��6P   ��6$   �� 6P   �� 5      -   5        $��Remove��5   !   $��1��6^   $��5,0,0,0q�6n   
 � Tag 6N   �� 6n   5      5      5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   $��1��6f   .+�5      �� -   5         ��Name$ UsersComboBox��5      $	��200��6.   $��10q�6:   �� 5      -   5        $��Add db_owner��5      $��10q�6E   5      5      5      