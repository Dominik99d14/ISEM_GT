   M S B A M L   `   `   `  ����   SQLEasyManager    SQLEasyManager.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$
  ISEM��$	��375��$	��700��.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+�o� $	��Top=�$��0��.@�m� $��Program��.@�m� -    m�Name$# KopiowanieUzytkownikaMenuItem��$��Kopiowanie użytkownika��m� -   $ UstawieniaMenuItem��$��
Ustawienia��$y�Hidden=��� m� $
��Exit��m� $��Pomoc��.@�m� -   $ OProgramieMenuItem��$��O programie���� $��1�� �� Orientation$ Vertical=�$��10q�.+��� $��Wyszukiwarka bazy:��$
��Left=��� -    ��Name$ Wyszukiwarka_baz��$
��Left=� �� TextWrapping$
 Wrap=�$	��385��$��22���� $��Użytkownik:��$
��Left=��� -    ��Name$ UsersComboBox��$
��Left=�$	��300���� -    ��Name$# SzukaniePoNazwieBazy_CheckBox��$��Wyszukuj po nazwie bazy��$
��Left=� d� 	IsChecked$
 TrueY�$y�Hidden=��� -   $! SzukanieUzytkownik_CheckBox��$��Tylko bazy użytkownika���� -   	 ��Name$	 Ladowanie_Label��$��Label��$��Center=�� -	   
 �Name$
 Ladowanie_ProgresBar��$��19��$	��678��&   System.Windows.Controls.DataGrid  -
     Name$ DatabasesDataGrid��$��2��$��Stretch=�$��Stretch=�$��10q�   AutoGenerateColumns .     CanUserAddRows .     Columns 4  .System.Windows.Controls.DataGridCheckBoxColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Select��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	IsChecked+0  *System.Windows.Controls.DataGridTextColumn  $ 
Nazwa Bazy�� �� *	NazwaBazy+  $	 NIP�� �� *NIP+  $ Nazwa Podmiotu�� �� *NazwaPodmiotu+  $ Typ kadr�� �� *Gratyfikant+  $ Typ księgowości�� �� *TypKsiegowosci+�� $��3��$ 
Horizontal=�$
��Left=�$��10q�.+��� -    ��Name$ DodawanieBazMulti_button��$%��Dodaj zaznaczone użytkownikowi��$	��252���� -   $ UsuwanieBazMulti_button��$.��(Usuwanie zaznaczonych baz użytkownikowi��$	��205���� -   $ ZaznaczBazyButton��$��Zaznacz bazy użytkownika��$	��150���� -   $ OdznaczBazyButton��$��Odznacz wszystkie bazy��$	��150���� -   $��Load Databases��$	��150��$y�Hidden=��� -   $��
Load Users��$	��150��$y�Hidden=��� -   $��Check User Databases��$	��150��$y�Hidden=�