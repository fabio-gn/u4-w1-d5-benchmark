using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace u4_w1_d5_benchmark
{
    internal class Contribuente
    {
        private string Nome { get; set; }
        private string Cognome { get; set; }
        private DateTime DataNascita { get; set; }
        private string CodiceFiscale { get; set; }
        private char Sesso { get; set; }
        private string ComuneResidenza { get; set; }
        private double RedditoAnnuale { get; set; }
        private double _impostaDaVersare;
        private double ImpostaDaVersare
        {
            get { return _impostaDaVersare; }
            set 
            {
                if (value > 0)
                {
                    this._impostaDaVersare = CalcolaImposta(value);
                }
                else
                {
                    Console.WriteLine("hai inserito un reddito non valido, premi invio per tornare alla home");
                    Console.ReadLine();
                    Start();
                }
                
            }
        }

        public Contribuente(string nome, string cognome, DateTime dataNascita, string codiceFiscale, char sesso, string comuneResidenza, double redditoAnnuale)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            CodiceFiscale = codiceFiscale;
            Sesso = sesso;
            ComuneResidenza = comuneResidenza;
            RedditoAnnuale = redditoAnnuale;
            ImpostaDaVersare = redditoAnnuale;
        }
        
        static List<Contribuente> ListaContribuenti = new List<Contribuente>();
        //procedura di avviamento dell'applicazione: è l'unica cosa da scrivere in "program"
        static public void Start()
        { Console.Clear();
            PrintView(homeView);
            Scegli();
        }
        // procedura che consente all'utente di fare una scelta numerica
        static private void Scegli()
        {
            Console.Clear();
            PrintView(homeView);
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice ==1)
            {
                Form();
            }
            else if(choice ==2)
            {
                StampaListaContribuenti();
            }
            else
            {
                Console.WriteLine("scelta non valida: devi digitare 1 o 2 e premere invio.");
                Scegli();
            }
        }

        // Variabili con memorizzate le varie "pagine" che l'utente visualizzarà
        static private string formView = "***************************************************** \r\n" +
                                         "AGGIUNGI CONTRIBUENTE \r\n \r\n" +
                                         "***************************************************** \r\n";
        static private string homeView = "********************* H O M E ********************\r\n" +
                                        "Fai la tua scelta:\r\n \r\n" +
                                        "1) Inserisci un nuovo contribuente \r\n" +
                                        "2) Visualizza la lista dei contribuenti \r\n \r\n" +
                                        "**************************************************\r\n";
        static private string listView = "*****************************************************\r\n" +
                                       "LISTA CONTRIBUENTI \r\n \r\n" +
                                       "*****************************************************\r\n";
        static private string riepilogoView = "*****************************************************\r\n" +
                                              "RIEPILOGO \r\n \r\n" +
                                              "*****************************************************\r\n";
        static private string erroreView = "************  Errore  *************";

        //procedura che che prende come argomento variabile con memorizzata la view della "pagina" in cui
        //si troverà l'utente.
        static private void PrintView(string viewName)
        {
            
            Console.WriteLine(viewName);


        }
        

        //procedura che avvia la compilazione del form che creerà un nuovo oggetto contribuente
        static private void Form()
        {
            Console.Clear();
            PrintView(formView);

            Console.Clear();
            PrintView(formView);
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();

            Console.Clear();
            PrintView(formView);
            Console.WriteLine("Cognome: ");
            string cognome = Console.ReadLine();

            Console.Clear();
            PrintView(formView);
            Console.WriteLine("Data di nascita: ( formato MM/GG/AAAA)");
            DateTime dataNascita = DateTime.Parse(Console.ReadLine());

            Console.Clear();
            PrintView(formView);
            Console.WriteLine("codice fiscale: ");
            string codiceFiscale = Console.ReadLine();

            Console.Clear();
            PrintView(formView);
            Console.WriteLine("sesso (M/F)");
            char sesso = char.Parse(Console.ReadLine());

            Console.Clear();
            PrintView(formView);
            Console.WriteLine("comune di residenza: ");
            string comuneResidenza = Console.ReadLine();

            Console.Clear();
            PrintView(formView);
            Console.WriteLine("reddito annuale:");
            double redditoAnnuo = double.Parse(Console.ReadLine());

            StampaRiepilogo(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, redditoAnnuo);
        }
        static private void StampaRiepilogo(
        string nome,
        string cognome,
        DateTime datanascita,
        string codicefiscale,
        char sesso,
        string comuneResidenza,
        double redditoAnnuo)
        {   
            Console.Clear ();
            
            PrintView(riepilogoView);
            Console.WriteLine(
                $"Nome: {nome} \r\n" +
                $"Cognome: {cognome} \r\n" +
                $"Data di nascita: {datanascita} \r\n" +
                $"Codice fiscale: {codicefiscale} \r\n" +
                $"Sesso: {sesso} \r\n" +
                $"Comune di residenza: {comuneResidenza} \r\n" +
                $"Reddito annuale: {redditoAnnuo} euro \r\n"+
                $"IMPOSTA DA VERSARE: {CalcolaImposta(redditoAnnuo)} euro \r\n" +
                $"\r\n------------------------------------------"
                );
            Console.WriteLine("I dati inseriti sono corretti? (y/n)");
            try
            {
                char ans = char.Parse(Console.ReadLine());
                if (ans == 'y')
                {
                    Contribuente contribuente = new Contribuente(nome, cognome, datanascita, codicefiscale, sesso, comuneResidenza, redditoAnnuo);
                    ListaContribuenti.Add( contribuente );
                }
                else if (ans == 'n')
                {
                    Console.Clear();

                    Console.WriteLine("Operazione annullata. premi invio per tornare alla home");
                    Console.ReadLine();

                    Scegli();
                }
                else
                {
                    Console.Clear();
                    PrintView(erroreView);
                    Console.WriteLine("carattere inserito non valido. premi invio per tornare al riepilogo");
                    Console.ReadLine();
                    StampaRiepilogo(nome, cognome, datanascita, codicefiscale, sesso, comuneResidenza, redditoAnnuo);

                }
            }
            catch(Exception ex)
            {
                Console.Clear();
                PrintView(erroreView);
                Console.WriteLine(ex.Message);
                Console.WriteLine("carattere inserito non valido. premi invio per tornare al riepilogo");
                Console.ReadLine();
                StampaRiepilogo(nome, cognome, datanascita, codicefiscale, sesso, comuneResidenza, redditoAnnuo);
            }
            
            
            Scegli();
        }
        static private void StampaListaContribuenti()
        {
            Console.Clear();
            PrintView(listView);
            if (ListaContribuenti.Count == 0)
            {
                Console.WriteLine("Non c'è ancora nessun contribuente. premi invio per tornare alla home");
                Console.ReadLine();
                Scegli();

            }
            else
            {
                for (int i = 0; i < ListaContribuenti.Count; i++)
                {
                    Console.WriteLine(
                        $"(_{i + 1}_)-----------------------------------------------------------\r\n" +
                        $"\r\n" +
                        $"Nome: {ListaContribuenti[i].Nome} \r\n" +
                        $"Cognome: {ListaContribuenti[i].Cognome} \r\n" +
                        $"Data di nascita: {ListaContribuenti[i].DataNascita} \r\n" +
                        $"Codice fiscale: {ListaContribuenti[i].CodiceFiscale} \r\n" +
                        $"Sesso: {ListaContribuenti[i].Sesso} \r\n" +
                        $"Comune di residenza: {ListaContribuenti[i].ComuneResidenza} \r\n" +
                        $"Reddito annuale: {ListaContribuenti[i].RedditoAnnuale} euro \r\n" +
                        $"IMPOSTA DA VERSARE: {ListaContribuenti[i].ImpostaDaVersare} euro\r\n" +
                        $"\r\n" +
                        $"----------------------------------------------------------------");

                }
                Console.WriteLine("premi invio per tornare alla Home");
                Console.ReadLine();
                Scegli();
            }
            
        }
        static private double CalcolaImposta(double reddito)
        {
            
            if (reddito > 0)
            {
                if (reddito < 15001)
                {
                    return reddito * 0.23;
                }
                else if (reddito < 28001)
                {
                    return (reddito - 15000) * 0.27 + 3450;
                }
                else if (reddito < 55001)
                {
                    return (reddito - 28000) * 0.38 + 6960;
                }
                else if (reddito < 75001)
                {
                    return (reddito - 55000) * 0.41 + 17220;
                }
                else
                {
                    return (reddito - 75000) * 0.43 + 25420;
                }
            }
            else
            {
                return 0;
            }
                
            
            
            
            
        }
        static private void IsDate(string data)
        {
            try
            {
                DateTime.Parse(data);
            }
            catch(Exception e)
            {   
                
                Console.WriteLine(e.Message);
                Console.WriteLine("formato data non valido. premi invio per tornare in home e riprovare.");
                Console.ReadLine ();
                Scegli();

            }
        }
        static private void IsChar(string carattere)
        {
            try
            {
                char.Parse(carattere);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine("formato non valido. premi invio per tornare in home e riprovare.");
                Console.ReadLine();
                Scegli();

            }
        }
        static private void IsDouble(string doubletype)
        {
            try
            {
                double.Parse(doubletype);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine("formato data non valido. premi invio per tornare in home e riprovare.");
                Console.ReadLine();
                Scegli();

            }
        }

    }
}
