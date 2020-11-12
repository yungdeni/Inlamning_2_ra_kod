using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    /* CLASS: Person
    * PURPOSE: a person entry in the address list
    */
    class Person
    {
        public string name, address, phone, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; phone = T; email = E;
        }
        /* METHOD: Person (constructor)
        * PURPOSE: Asks user for input if class initiliazed with no parameters
        * PARAMETERS: None
        */
        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            this.name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            this.address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            this.phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            this.email = Console.ReadLine();
        }
        /* METHOD: Person.Print
        * PURPOSE: write a person entry to the address list file
        * PARAMETERS: None
        * RETURN VALUE: void
        */
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", this.name, this.address, this.phone, this.email);
        }
        /* METHOD: Person.Set
        * PURPOSE: Changes a specific field according to parameter values
        * PARAMETERS: String inputField representing a field to be changed. String inputValue representing the value to change the field to
        * RETURN VALUE: void
        */
        public void Set(string inputField, string inputValue)
        {
            switch (inputField)
            {
                case "namn":
                    this.name = inputValue;
                    break;
                case "adress":
                    this.address = inputValue;
                    break;
                case "telefon":
                    this.phone = inputValue;
                    break;
                case "email":
                    this.email = inputValue;
                    break;
                default:
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = new List<Person>();
            LoadList(Dict);
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {

                    Dict.Add(new Person());
                }
                else if (command == "ta bort")
                {
                    RemovePerson(Dict);
                }
                else if (command == "visa")
                {
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        Dict[i].Print();
                    }
                }
                else if (command == "ändra")
                {
                    ChangeDetails(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }
        /* METHOD: ChangeDetails (static)
        * PURPOSE: Searches a list for an object matching a name field and changes the fields in that object
        * PARAMETERS: List<Person>
        * RETURN VALUE: void
        */
        private static void ChangeDetails(List<Person> Dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string nameToChange = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == nameToChange) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", nameToChange);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string fieldToChange = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fieldToChange, nameToChange);
                string newValue = Console.ReadLine();
                Dict[found].Set(fieldToChange, newValue);
            }
        }
        /* METHOD: RemovePerson (static)
        * PURPOSE: Searches a list for an object matching a name field and removes the object from the list
        * PARAMETERS: List<Person>
        * RETURN VALUE: void
        */
        private static void RemovePerson(List<Person> Dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string nameToRemove = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == nameToRemove) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", nameToRemove);
            }
            else
            {
                Dict.RemoveAt(found);
            }
        }
        /* METHOD: LoadList (static)
        * PURPOSE: Reads a file and initializes objects into the list
        * PARAMETERS: List<Person>
        * RETURN VALUE: void
        */
        private static void LoadList(List<Person> Dict)
        {
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    string[] word = line.Split('#');
                    // Console.WriteLine("{0}, {1}, {2}, {3}", word[0], word[1], word[2], word[3]);
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }
    }
}
