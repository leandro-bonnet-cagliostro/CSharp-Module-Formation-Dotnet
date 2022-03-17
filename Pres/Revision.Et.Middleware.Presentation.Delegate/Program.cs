using System;

namespace Revision.Et.Middleware.Presentation.Delegate
{
    class Program
    {
        delegate void Del(string str); // pointeur vers différentes méthodes (pour le moment vers aucune méthode)
        delegate void EventHandler(object sender, EventArgs e); // rajout inutile ici
        static void Main(string[] args)
        {
            Del delegate1 = Notify;
            Del delegate2 = HelloWorld;
            Del delegate3 = delegate1 + delegate2;

            delegate1 += HelloWorld;
            delegate1.Invoke("Max");
            delegate2.Invoke("Maxime");
            delegate3.Invoke("Toto");

            //var test = delegate3.GetInvocationList();
            // faire un for pour parcourir toutes les méthodes dans les tests test.Method.Name

            Action<string> action = Notify;
            action += HelloWorld;
            action.Invoke("Toto");

            // il existe aussi func pour renvoyerquelque chose

            // EventHandler e;

            // doc delegate
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates

            // doc predicate
            // https://docs.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-6.0


            Console.WriteLine("========================");
            var test = delegate1.GetInvocationList();
            foreach (var element in test)
            {
                Console.WriteLine(element.Method.Name);
                if (element.Method.Name.Equals("HelloWorld"))
                {

                    //Console.WriteLine("Salut à toutes");
                    element.DynamicInvoke("salut à tous");
                }
                if (element.Method.Name == "HelloWorld")
                {

                    //Console.WriteLine("Salut à toutes");
                    element.DynamicInvoke("salut à toutes");
                }
                //Console.WriteLine(element + " " + element.Method + " " + element.Method.Name);
            }

            Console.ReadLine();

        }

        static void Notify(string name)
        {
            Console.WriteLine($"Notification received for : {name}");
        }

        static void HelloWorld(string name)
        {
            Console.WriteLine($"Hello world ! {name}");
        }
    }
}
