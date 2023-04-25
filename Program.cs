using System;
using System.Collections.Generic;

namespace todo
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :");
            Console.WriteLine("******************************************* (1) Board Listelemek (2) Board'a Kart Eklemek (3) Board'dan Kart Silmek (4) Kart Taşımak");
            int secim=Convert.ToInt32(Console.ReadLine());
        }
    }

    class Board{
        private string Header;
        private string Content;
        private string Personel;
        private Size size;
        private Line line;

        public Board(string header,string cont,string pers,Size boyut){
            this.Header=header;
            this.Content=cont;
            this.Personel=pers;
            this.size=boyut;
            this.line=Line.TODO;
        }

        public Board(Line hat){ // override , güncelleme yapar LINE üzerinde
            this.line=hat;
        }
        
    }

    public enum Size{
        XS,S,M,L,XL
    }

    public enum Line{
        TODO,INPROGRESS,DONE
    }
}