using System;
using System.Collections.Generic;

namespace todo
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Dictionary<int,string> kisiler=new Dictionary<int, string>();
            kisiler.Add(1,"Ali Soy");
            kisiler.Add(2,"Ali Hacıveli");
            kisiler.Add(3,"Danny Baker");
            kisiler.Add(4,"Tonny Hill");
            Kartlar kart=new Kartlar();

            first:
            Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :");
            Console.WriteLine("******************************************* (1) Board Listelemek (2) Board'a Kart Eklemek (3) Board'dan Kart Silmek (4) Kart Taşımak");
            try{
                int secim=Convert.ToInt32(Console.ReadLine());
                switch(secim){
                        case 1://Listele
                            kart.KartListele();
                            goto first;
                        break;
                        case 2://Ekle
                            Console.WriteLine("Baslık giriniz:");
                            string b=Console.ReadLine();
                            Console.WriteLine("İcerik giriniz:");
                            string i=Console.ReadLine();
                            int ss2;
                            ss1:
                            Console.WriteLine("Büyüklük seciniz -> XS(1),S(2),M(3),L(4),XL(5):");
                            try{
                                int s=Convert.ToInt32(Console.ReadLine());
                                ss2=s;
                                if(s<1 && s>5){
                                    Console.WriteLine("Hatalı giris.Tekrar deneyiniz! 1-5");
                                    goto ss1;
                                }
                            }catch(ArgumentException e){
                                Console.WriteLine("Hatalı giris.Tekrar deneyiniz!");
                                goto ss1;
                            }
                            
                            kk2:
                            bool kisiOK=false;
                            Console.WriteLine("Kisi seciniz:");
                            int k=Convert.ToInt32(Console.ReadLine());
                            foreach(var x in kisiler){
                                if(x.Key==k){
                                    kisiOK=true;
                                }
                            }
                            if(kisiOK==false){
                                goto kk2;
                            }
                            kart.KartEkle(b,i,k,(Size)ss2);
                            goto first;
                        break;
                        case 3://Sil
                        break;
                        case 4://Taşı
                        break;

                }

            }catch(ArgumentException e ){
                Console.WriteLine("Hatalı giriş !"+e.Message.ToString());
            }
            
        }
    }

    public class Board{
        private string Header;
        private string Content;
        private int Personel;
        private Size size;
        private Line line;

        public string baslık(){
            return this.Header;
        }
        public string icerik(){
            return this.Content;
        }
        public int personel(){
            return this.Personel;
        }
        public Size büyüklük(){
            return this.size;
        }
        public Line grup(){
            return this.line;
        }

        public Board(string header,string cont,int persID,Size boyut){
            this.Header=header;
            this.Content=cont;
            this.Personel=persID;
            this.size=boyut;
            this.line=Line.TODO;
        }

        public Board(Line hat){ // override , güncelleme yapar LINE üzerinde
            this.line=hat;
        }
        
    }

    public enum Size{
        XS=1,S=2,M=3,L=4,XL=5
    }

    public enum Line{
        TODO,INPROGRESS,DONE
    }

    class Kartlar{
        public static List<Board> kart=new List<Board>();
        
        public void KartEkle(string header,string cont,int pers,Size boyut){
            kart.Add(new Board(header,cont,pers,boyut));
        }
        
        public void KartListele(){
            foreach(var v in kart){
                if(v.grup()==Line.TODO){
                    Console.WriteLine("TODO Line");
                    Console.WriteLine("************************");
                    Console.WriteLine("Başlık:{0}",v.baslık());
                    Console.WriteLine("İçerik:{0}",v.icerik());
                    Console.WriteLine("Atanan Kişi:{0}",v.personel());
                    Console.WriteLine("Büyüklük:{0}",v.büyüklük());
                }

                if(v.grup()==Line.INPROGRESS){
                    Console.WriteLine("INPROGRESS Line");
                    Console.WriteLine("************************");
                    Console.WriteLine("Başlık:{0}",v.baslık());
                    Console.WriteLine("İçerik:{0}",v.icerik());
                    Console.WriteLine("Atanan Kişi:{0}",v.personel());
                    Console.WriteLine("Büyüklük:{0}",v.büyüklük());
                }

                if(v.grup()==Line.DONE){
                    Console.WriteLine("DONE Line");
                    Console.WriteLine("************************");
                    Console.WriteLine("Başlık:{0}",v.baslık());
                    Console.WriteLine("İçerik:{0}",v.icerik());
                    Console.WriteLine("Atanan Kişi:{0}",v.personel());
                    Console.WriteLine("Büyüklük:{0}",v.büyüklük());
                }
                
            }
        }
    }
}