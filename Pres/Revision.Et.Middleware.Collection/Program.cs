using System;
using System.Collections.Generic;

namespace Revision.Et.Middleware.Collection
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");
            queue.Enqueue("five");

            Console.WriteLine(queue.Peek());
            Console.ReadLine();

            Stack<string> stack = new Stack<string>();
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");

            var test = stack.Pop();

            Console.WriteLine(stack.Peek());
            Console.WriteLine(test);
            Console.ReadLine();

            HashSet<int> hashset = new HashSet<int>();
            hashset.Add(1);
            hashset.Add(2);
            hashset.Add(3);
            hashset.Add(4);
            hashset.Add(5);
            if(!hashset.Add(5))
            {
                Console.WriteLine("Le chiffre 5 est déjà présent dans la liste");
            }

            HashSet<Person> groupe = new HashSet<Person>();
            // HashSet peut avoir <Person,Animal> par exemple
            Person person1 = new Person
            {
                Nom = "Maxime",
                Prenom = "Maxime"
            };
            Person person2 = new Person
            {
                Nom = "Maxime",
                Prenom = "Maxime"
            };

            groupe.Add(person1);

            if(groupe.Add(person2))
            {
                Console.WriteLine("La personne 2 n'est pas encore présente"); // sans un equals spécifique on entre ici
            }
            else
            {
                Console.WriteLine("La personne 2 est déjà présente");
            }

            Console.ReadLine();

            Dictionary<int, string> dico = new Dictionary<int, string>();

            TupleAnimalPerson test2 = new TupleAnimalPerson(new Person(),new Animal());
            (Person,Animal) response = test2.GetMasterAndPet();
            Console.WriteLine(response.Item1.Nom + " " + response.Item1.Prenom + " a comme animal : "+response.Item2.Nom);
            

            TupleAnimalPerson test3 = new TupleAnimalPerson
            {
                Master = new Person
                {
                    Nom = "Maxime",
                    Prenom = "Maxime"
                },
                Pet = new Animal
                {
                    Nom = "Toto",
                    Race = "Chien"
                }
            };
            var (master, pet) = test3.GetMasterAndPet();
            Console.WriteLine(master.Prenom + " : " +pet.Nom);

            TupleAnimalPerson test4 = new TupleAnimalPerson
            {
                Master = new Person
                {
                    Nom = "Maxime",
                    Prenom = "Maxime"
                },
                Pet = new Animal
                {
                    Nom = "Toto",
                    Race = "Chien"
                }
            };
            (Person master, Animal pet) response2 = test4.GetMasterAndPet();
            Console.WriteLine(master.Prenom + " : " + pet.Nom);
            Console.WriteLine(response2.master.Nom + " : " + response2.pet.Nom);
        }
    }

    public class Person
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Person()
        {
            this.Nom = "Bonnet";
            this.Prenom = "Léo";
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Person p = (Person)obj;
                return this.Nom.Equals(p.Nom) && this.Prenom.Equals(p.Prenom);
            }
        }

        public override int GetHashCode()
        {
            int test = 0;
            foreach (var c in this.Nom)
            {
                test += c;
            }
            foreach (var c in this.Prenom)
            {
                test += c;
            }

            return test;
        }
    }

    public class Animal
    {
        public string Nom { get; set; }
        public string Race { get; set; }

        public Animal()
        {
            this.Nom = "Ody";
            this.Race = "Chien";
        }
    }

    public class TupleAnimalPerson
    {
        public Person Master { get; set; }
        public Animal Pet { get; set; }

        public TupleAnimalPerson(Person person, Animal animal)
        {
            this.Master = person;
            this.Pet = animal;
        }
        public TupleAnimalPerson()
        {
        }

        public (Person person, Animal animal) GetMasterAndPet()
        {
            return (this.Master, this.Pet);
        }
    }
}
