using System;
using System.Collections.Generic;

namespace SQLEasyManager
{
    public class BazyDanych_class  // Zmieniono z 'internal' na 'public'
    {
        public string NazwaBazy { get; set; }
        public string NazwaPodmiotu { get; set; }
        public string NIP { get; set; }
        public string TypBazy { get; set; }
        public List<string> DbOwners { get; set; }
        public bool IsChecked { get; set; }
        public string Gratyfikant { get; set; }
        public string TypKsiegowosci { get; set; }


        public BazyDanych_class(string baza)
        {
            NazwaBazy = baza;
            DbOwners = new List<string>();
            IsChecked = false;
            NIP = string.Empty;
            NazwaPodmiotu = string.Empty;
            TypBazy = string.Empty;
        }

        public override string ToString()
        {
            return NazwaBazy;
        }
    }
}
