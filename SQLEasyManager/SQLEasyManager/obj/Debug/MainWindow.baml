   M S B A M L   `   `   `  ����  SQLEasyManager    SQLEasyManager.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Database Lister��5   	   $	��375��6!   $	��525��6.   .��5      � .+�5   
   �� -   5         ��Name$ DatabasesListBox��5      $��Center=�6,   $	��200��6I   $	��Top=�6V   $	��505��6n   $��0,99,0,0q�6z   ��5      �� 5      .R�5	      �  � ColumnDefinitions 5
      �� 5      $
��Auto �6/   �� 5      $��* �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   .+�5      �� $��Center=�6C   $��0��6^    d� 	IsChecked 6#   �� *	IsChecked+6^   �� 5      $��Center=�6?   $��1��6Z   $
��Left=�6j   ��6$   �� *	NazwaBazy+6j   �� 5      $��Center=�69   $��2��6T   $
��Left=�6d   $��10,0,0,0q�6   ��6$   �� *NIP+6   �� 5      $��Center=�6=   $��3��6X   $
��Left=�6h   $��10,0,0,0q�6�   ��6$   �� *	TypBazy+6�   5      5      5      �� 5   
   -   5        $��Load Databases��5      $
��Left=�6*   $	��Top=�6E   $	��150��6]   $��
10,304,0,0q�6i   $y�Hidden=�6�   �� 5   
   $��Wyszukiwarka bazy:��6   $
��Left=�6-   $��	10,10,0,0q�6H   $	��Top=�6[    J� RenderTransformOrigin$ -1.202,-1.424&�6s   �� 5   
   -   5         ��Name$ Wyszukiwarka_baz��5      $
��Left=�6,   $��
130,14,0,0q�6G    �� TextWrapping$
 Wrap=�6[   $	��Top=�6o   $	��385��6�   $��22��6�   6�   �� 5   
   $��Użytkownik:��6   $
��Left=�6&   $��	10,41,0,0q�6A   $	��Top=�6T   �� 5   
   -   5         ��Name$ UsersComboBox��5      $
��Left=�6*   $	��Top=�6E   $	��300��6]   $��	88,45,0,0q�6i   6�   �� 5   
   -   5        $��
Load Users��5      $
��Left=�6&   $	��Top=�6A   $	��150��6Y   $��165,304,0,0q�6e   $y�Hidden=�6�   �� 5   
   -   5        $��Check User Databases��5      $
��Left=�60   $	��Top=�6K   $	��150��6c   $��322,304,0,0q�6o   $y�Hidden=�6�   �� 5   
   -   5         ��Name$! SzukanieUzytkownik_CheckBox��5      $��Wyszukuj po użytkowniku��68   $
��Left=�6Z   $��	10,72,0,0q�6u   $	��Top=�6�   6�   �� 5    
   -   5        $# SzukaniePoNazwieBazy_CheckBox��5       $��Wyszukuj po nazwie bazy��6:   $
��Left=�6\   $��
262,72,0,0q�6w   $	��Top=�6�   $
 TrueY�6�   $y�Hidden=�6�   �� 5!   
   -	   5        	 ��Name$	 DodawanieBazMulti_button��5!      $%��Dodaj zaznaczone użytkownikowi��63   $
��Left=�6\   $��
10,304,0,0q�6w   $	��Top=�6�   $	��252��6�   6�   �� 5"   
   -
   5        $	 UsuwanieBazMulti_button��5"      $.��(Usuwanie zaznaczonych baz użytkownikowi��62   $
��Left=�6d   $��267,304,0,0q�6   $	��Top=�6�   $	��205��6�   6�   5#      5$      