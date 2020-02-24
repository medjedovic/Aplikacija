using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08OOPAplikacija
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //stampaj meni
            //unos od strane korisnika
            
            char unos = ' ';
            do
            {
                StampajMeni();
                //Još jedan način za parse, vraća true ili false + broj u out
                //varijabli ako je uspesan pase
                //int.TryParse(Console.ReadLine(), out int unos)

                unos = Console.ReadKey().KeyChar;
                switch (unos)
                {
                    case '1':
                        UnosOsobe();
                        break;
                    case '2':
                        Izmena();
                        break;
                    case '3':
                        BrisanjeOsobe();
                        break;
                    case '4':
                        //prvi način ispisa podataka o osobi
                        //foreach(Osoba o in Osoba.Osobe)
                        //{
                        //    //Console.WriteLine($"Ime: {o.ime}, Prezime: {o.prezime}, Broj telefona: {o.brojtel}!\n");
                        //    //Console.WriteLine(o); //kada se napiše samo o to je isto kao o.ToString();
                        //    Console.WriteLine(o);
                        //}
                        for (int i = 0; i < Osoba.Osobe.Count; i++)
                        {
                            Console.WriteLine($"{i+1} - {Osoba.Osobe[i]}");
                        }
                        break;
                    default:
                        Console.WriteLine("Greška u unosu!");
                        break;
                }
            } while (unos != '5');
        }

        static void Izmena()
        {
            Console.Write("\nUnesite broj telefona: ");
            string br = Console.ReadLine();

            if (!Osoba.ProveriTel(br, out Osoba o))
            {
                Console.Write("\nUnesite ime i prezime: ");
                string[] imeIprezime = Console.ReadLine().Split(' ');
                //"Esad Međedović" ---> ["Esad", "Međedović"]
                if (imeIprezime.Length != 2)
                {
                    Console.WriteLine("Greska u unosu!");
                    return;
                }
                o.ime = imeIprezime[0];
                o.prezime = imeIprezime[1];
            }
            else
            {
                Console.WriteLine("Nema tog broja :(");
            }
        }

        

        static void BrisanjeOsobe()
        {
            //TODO Napraviti brisanje da radi po indeksima
            //Korisno bi bilo koristiti for petlju standardnu da možete da 
            //ispišete indekse, i onda koristite Osoba.Osobe.RemoveAt(indeks) :) 

            Console.WriteLine("\nUnesite index osobe kojeg želite da obrišete!");
            int i = int.Parse(Console.ReadLine());

            
            if (Osoba.Osobe.Count > i-1)
            {
                Osoba.Osobe.RemoveAt(i-1);
            }
            else
            {
                Console.WriteLine("Uneti index ne ma vrijednost za osobu!");
            }
               
            



            //Console.WriteLine("\nUnesite broj telefona korisnika kojeg želite da obrišemo!");
            //string br = Console.ReadLine();
            //if (!Osoba.ProveriTel(br, out Osoba o))
            //{
            //    Osoba.Osobe.Remove(o);
            //}
            //else
            //{
            //    Console.WriteLine("Uneti broj nije ničiji broj sa imenika!");
            //}
        }


        static void UnosOsobe()
        {
            //TODO Svaki komentar koji počinje sa TODO možemo lako naći tako što odemo na View a zatim na Task
            //TODO Za domaći ova metoda u do while, i nema izlaska dok unos ne bude ispravan

            //split omogućava da na osnovu nekog karaktera dijeli string na dva dijela
            //Console.WriteLine("\nUnesite ime i prezime sa razmakom između imenai prezimena!");

            //string [] imeprezime, ne definisan niz koji nam 
            //string[] imeprezime = Console.ReadLine().Split(' ');
            //"Esad Međedović" -----> ["Esad", "Međedović"]

            string[] imeprezime;
            Console.WriteLine("\nUnesite ime i prezime sa razmakom između imena i prezimena!");
            imeprezime = Console.ReadLine().Split(' ');
  
            while (imeprezime.Length != 2) {
                Console.WriteLine("Greška u unosu imena i prezimena - Pokušajte ponovo!");
                Console.WriteLine("\nUnesite ime i prezime sa razmakom između imena i prezimena!");
                imeprezime = Console.ReadLine().Split(' ');
            }


            //if (imeprezime.Length != 2)
            //{
            //    Console.WriteLine("Greška u unosu imena i prezimena!");
            //    return;
            //}

            //TODO Za domaći broj telefona treba da bude unet u formatu gde ima 
            // / za pozivni i - između dve grupe brojeva, proveriti to
            //Console.WriteLine("\nUpišite broj telefona!");
            //string broj = Console.ReadLine();

            string broj;
            Console.WriteLine("\nUpišite broj telefona!");
            broj = Console.ReadLine();
            while (!((broj.Length > 9 || broj.Length < 18) && broj.StartsWith("+") && broj.Contains("/") && broj.Contains("-")))
            {
                Console.WriteLine("Greška u unosu broja telefona - Pokušajte ponovo!");
                Console.WriteLine("\nUpišite broj telefona takoda nema manje od 9 cifara ni više od 13 i da je formatiran +000/000-0000!");
                broj = Console.ReadLine();
            }
            

    
            //Kada nam ne treba out promjenljiva jer konstruktor automatski smešta  u ovom slučaju u list,
            //samo stavimo nakon nje _
            if (Osoba.ProveriTel(broj, out _))
            {
                //ne treba nam varijabla jer konstruktor automatski smesta
                //osobu u listu
                new Osoba(imeprezime[0], imeprezime[1], broj);
            }
            else
            {
                Console.WriteLine("Osoba sa ovim brojem telefona postoji!");
            }

        }

        static void StampajMeni()
        {
            Console.WriteLine("\n\n- - - - - - - - - - - - - ");
            Console.WriteLine("M E N I");
            Console.WriteLine("1. Unos Osobe");
            Console.WriteLine("2. Izmena Osobe");
            Console.WriteLine("3. Brisanje Osobe");
            Console.WriteLine("4. Pregled Osobe");
            Console.WriteLine("5. Izlaz iz aplikacije");
            Console.WriteLine("- - - - - - - - - - - - - ");
            Console.WriteLine("Unesite broj!");
            Console.Write("- >>- - - - - >>- - - - ->");
        }
    }
}


class Osoba
{
    public static List<Osoba> Osobe = new List<Osoba>();
    public string ime, prezime, brojtel;

    public static bool ProveriTel(string t, out Osoba ob)
    {
        foreach(Osoba o in Osobe)
        {
            if (o.brojtel == t)
            {
                //objekat koji ima broj telefona dobiće objekat osobe
                ob = o;
                return false;
            }
        }
        ob = null;
        return true;
    }


    public Osoba(string i, string p, string b)
    {
        ime = i;
        prezime = p;
        brojtel = b;
        Osobe.Add(this);
    }

    public Osoba()
    {
        Osobe.Add(this);
    }

    //override predefinisanje (pregazi) metode

    public override string ToString()
    {
        //nudi već ugrađenu metodu
        //return base.ToString();
        return $"\nIme: {ime}, Prezime: {prezime}, Broj telefona: {brojtel}!\n";
    }
}



/*
        //metoda koja mora da vrati int
        static int FooBar()
        {
            return 1;
        }

        //ovdje ne znamo da li ćemo dobiti integer ali ako ne vrati rezultat daće true ili false
        //primer metode sa out parametar kao sto je TryParse
        static bool fooBar2(int x, out int rezultat)
        {
            rezultat = 5;
            return false;
        }

    
        if (int.TryParse(Console.ReadLine(), out int unos))
        {
            Console.WriteLine("Unijeli ste cijeli broj!");
        }
        else
        {
            Console.WriteLine("Niste unijeli cijeli broj!");
        }
 */
