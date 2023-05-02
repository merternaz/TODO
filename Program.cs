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
            PersoInfo personelInfo=new PersoInfo();

            foreach(var x in kisiler){
                personelInfo.PersoEkle((int)x.Key,x.Value.ToString());
            }

            first:
            Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :");
            Console.WriteLine("******************************************* (1) Board Listelemek (2) Board'a Kart Eklemek (3) Board'dan Kart Silmek (4) Kart Taşımak");
            try{
                int secim=Convert.ToInt32(Console.ReadLine());
                switch(secim){
                        case 1://Listele
                            kart.KartListele(personelInfo);
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
                                Console.WriteLine("Hatalı kisi secildi.Personel bulunamadı!");
                                goto kk2;
                            }
                            kart.KartEkle(b,i,k,(Size)ss2);
                            goto first;
                        break;
                        case 3://Sil
                            sil:
                            Console.WriteLine("Öncelikle silmek istediğiniz kartı seçmeniz gerekiyor.Lütfen kart başlığını yazınız:");
                            string entry=Console.ReadLine();

                            kart.RemoveCard(entry);

                            if(!kart.KartSilindimi()){
                                tekrar:
                                Console.WriteLine("Aradığınız krtiterlere uygun kart board'da bulunamadı. Lütfen bir seçim yapınız.");
                                Console.WriteLine("Silmeyi sonlandırmak için : (1)");
                                Console.WriteLine("Yeniden denemek için : (2)");
                                int del_selection=Convert.ToInt32(Console.ReadLine());
                                if(del_selection<1 && del_selection>2){//del_selection!=1 || del_selection!=2
                                    goto tekrar;
                                }else{
                                    if(del_selection==1){
                                        goto first;
                                    }
                                    if(del_selection==2){
                                        goto sil;
                                    }
                                }
                            }else{
                                kart.KartListele(personelInfo);
                            }

                        break;
                        case 4://Taşı
                        updt:
                        Console.WriteLine("Öncelikle tasimak istediğiniz kartı seçmeniz gerekiyor.Lütfen kart başlığını yazınız:");
                            string moveHeader=Console.ReadLine();

                            kart.SearchCard(moveHeader);
                            if(!kart.KartBulundumu()){
                                tekrar2:
                                Console.WriteLine("Aradığınız krtiterlere uygun kart board'da bulunamadı. Lütfen bir seçim yapınız.");
                                Console.WriteLine("Silmeyi sonlandırmak için : (1)");
                                Console.WriteLine("Yeniden denemek için : (2)");
                                int upd_selection=Convert.ToInt32(Console.ReadLine());
                                if(upd_selection<1 && upd_selection>2){//upd_selection!=1 || upd_selection!=2
                                    goto tekrar2;
                                }else{
                                    if(upd_selection==1){
                                        goto first;
                                    }
                                    if(upd_selection==2){
                                        goto updt;
                                    }
                                }
                            }else{
                                Console.WriteLine("Bulunan Kart Bilgileri:************************************** Başlık :{0}"+
                                " İçerik :{1}"+ 
                                "Atanan Kişi :{2}"+
                                " Büyüklük :{3}"+
                                " Line :{4}",kart.BulByHeader(moveHeader).baslık(),
                                kart.BulByHeader(moveHeader).icerik(),
                                personelInfo.PersonelCagir(kart.BulByHeader(moveHeader).personel()).GetName(),
                                kart.BulByHeader(moveHeader).büyüklük(),
                                kart.BulByHeader(moveHeader).grup());
                                tekrar3:
                                Console.WriteLine("Lütfen taşımak istediğiniz Line'ı seçiniz: (1) TODO (2) IN PROGRESS (3) DONE");
                                int line_ndx=Convert.ToInt32(Console.ReadLine());
                                if(line_ndx<1 && line_ndx>3){
                                    goto tekrar3;
                                }else{
                                    
                                        kart.BulByHeader(moveHeader).UpdateLine(line_ndx);
                                        kart.KartListele(personelInfo);
                                        goto first;
                                    
                                }
                            }
                        break;

                }

            }catch(ArgumentException e ){
                Console.WriteLine("Hatalı giriş !"+e.Message.ToString());
                goto first;
            }catch(FormatException f){
                Console.WriteLine("Hatalı FORMAT !"+f.Message.ToString());
                goto first;
            }
            
        }
    }

    public class Board{
        private string Header;
        private string Content;
        private int Personel;
        private Size size;
        private Line line;
        private int ID;
        int unq=0;

        public string baslık(){
            return this.Header;
        }
        public string icerik(){
            return this.Content;
        }
        public int personel(){
            return this.Personel;// Kişi ID dönmesin. string verileri dönsün
        }
        public Size büyüklük(){
            return this.size;
        }
        public Line grup(){
            return this.line;
        }
        public int CardID(){
            return this.ID;
        }

        
        int UniqueID(){
            unq++;
            return unq;
        }

        public void UpdateLine(int hat){//Line hat
            if(hat==1){
                this.line=Line.TODO;
            }
            if(hat==2){
                this.line=Line.INPROGRESS;
            }
            if(hat==3){
                this.line=Line.DONE;
            }
        }

        public Board(string header,string cont,int persID,Size boyut){
            this.Header=header;
            this.Content=cont;
            this.Personel=persID;
            this.size=boyut;
            this.line=Line.TODO;
            this.ID=UniqueID();
        }

        public Board(Line hat){ // override , güncelleme yapar LINE üzerinde
            this.line=hat;
        }
        
    }

    public enum Size{
        XS=1,S=2,M=3,L=4,XL=5
    }

    public enum Line{
        TODO=1,INPROGRESS=2,DONE=3
    }

    class Kartlar{
        public static List<Board> kart=new List<Board>();
        
        public void KartEkle(string header,string cont,int pers,Size boyut){
            kart.Add(new Board(header,cont,pers,boyut));
        }

        private bool deletedCard=false;
        private bool isCard=false;
        public Board BulByHeader(string hd){
            return kart.Find(x=>x.baslık()==hd);
        }
         public Board BulByLine(Line li){
            return kart.Find(x=>x.grup()==li);
        }

        public void SearchCard(string entry){
            isCard=false;
            foreach(var x in kart){
                if(x.baslık()==entry){
                    
                    isCard=true;
                }
            }
        }

        public void RemoveCard(string entry){
            deletedCard=false;
            int indexCounter=0;
            List<int> willBeDelete=new List<int>();
            foreach(var x in kart){
                if(x.baslık()==entry){
                    //kart.Remove(x);
                    willBeDelete.Add(indexCounter);
                    Console.WriteLine("Silinecek index:"+indexCounter+"//"+x.CardID());
                    deletedCard=true;
                }
                indexCounter++;
            }

            foreach(var ndx in willBeDelete){
                kart.RemoveAt(ndx);
               // kart.remo
            }
        }

        public bool KartSilindimi(){
            return deletedCard;
        }
        public bool KartBulundumu(){
            return isCard;
        }
        
        public void KartListele(PersoInfo personelBilgileri){
            int counter1=0,counter2=0,counter3=0;
            List<Board> re_kart1=new List<Board>();
            List<Board> re_kart2=new List<Board>();
            List<Board> re_kart3=new List<Board>();
            

            foreach(var v in kart){
                if(v.grup()==Line.TODO){
                    re_kart1.Add(v);//new Board(v.baslık(),v.icerik(),v.personel(),v.büyüklük())
                    counter1++;
                }
                if(v.grup()==Line.INPROGRESS){
                    re_kart2.Add(v);
                    counter2++;
                }
                if(v.grup()==Line.DONE){
                    re_kart3.Add(v);
                    counter3++;
                }
                
                }

            Console.WriteLine("TODO Line");
            Console.WriteLine("************************");
            if(re_kart1.Count==0){
                Console.WriteLine(" ~ BOŞ ~");
                Console.WriteLine("");
            }else{
                foreach(var a in re_kart1){               
                    
                    Console.WriteLine("Başlık:{0}",a.baslık());
                    Console.WriteLine("İçerik:{0}",a.icerik());
                    Console.WriteLine("Atanan Kişi:{0}",personelBilgileri.PersonelCagir(a.personel()).GetName());
                    Console.WriteLine("Büyüklük:{0}",a.büyüklük());
                    Console.WriteLine("-");
                    Console.WriteLine("");
                    
                }

            }
            
            Console.WriteLine("INPROGRESS Line");
            Console.WriteLine("************************");
             if(re_kart2.Count==0){
                Console.WriteLine(" ~ BOŞ ~");
                Console.WriteLine("");
            }else{
                foreach(var b in re_kart2){   
                
                    
                    Console.WriteLine("Başlık:{0}",b.baslık());
                    Console.WriteLine("İçerik:{0}",b.icerik());
                    Console.WriteLine("Atanan Kişi:{0}",personelBilgileri.PersonelCagir(b.personel()).GetName());
                    Console.WriteLine("Büyüklük:{0}",b.büyüklük());
                    Console.WriteLine("-");
                    Console.WriteLine("");
                }
            }
            

            Console.WriteLine("DONE Line");
            Console.WriteLine("************************");
             if(re_kart3.Count==0){
                Console.WriteLine(" ~ BOŞ ~");
                Console.WriteLine("");
                }else{
                    foreach(var c in re_kart3){  
                        
                        Console.WriteLine("Başlık:{0}",c.baslık());
                        Console.WriteLine("İçerik:{0}",c.icerik());
                        Console.WriteLine("Atanan Kişi:{0}",personelBilgileri.PersonelCagir(c.personel()).GetName());
                        Console.WriteLine("Büyüklük:{0}",c.büyüklük());
                        Console.WriteLine("-");
                        Console.WriteLine("");
                    }   
                
                
                    }
        }
    

    
   

    }

     class PersoInfo{
        public static List<Personeller> per=new List<Personeller>();
            public void PersoEkle(int id,string name){
                per.Add(new Personeller(id,name));
            }

            public Personeller PersonelCagir(int id){
                return per.Find(x=>x.GetID()==id);
            }
        }

        class Personeller{
        private int ID;
        private string Name;
        //private string Surname;
        //public static List<Personeller> per=new List<Personeller>();
        public Personeller(int id,string name){
            this.ID=id;
            this.Name=name;
            //this.Surname=surname;
        }

        public int GetID(){
            return this.ID;
        }

        public string GetName(){
            return this.Name;
        }

        


    }

}