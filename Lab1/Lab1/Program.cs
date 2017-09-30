using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(new AmericanFactory());
            client.Run("American facrofy");

            Client client1 = new Client(new EuropeanFactory());
            client1.Run("European facrofy");

            Console.ReadLine();
        } 
        abstract class AbstractFactory
        {
            public abstract CarClassA CreateCarClassA();
            public abstract CarClassB CreateCarClassB();
        }
        class AmericanFactory : AbstractFactory
        {
            public override CarClassA CreateCarClassA()
            {
                Console.WriteLine("Fiat 500 was create");
                return new Fiat500();
            }

            public override CarClassB CreateCarClassB()
            {
                Console.WriteLine("Ford Fiesta was create");
                return new FordFiesta();
            }
        }
        class EuropeanFactory : AbstractFactory
        {
            public override CarClassA CreateCarClassA()
            {
                Console.WriteLine("Daewoo Matiz was create");
                return new DaewooMatiz();
            }

            public override CarClassB CreateCarClassB()
            {
                Console.WriteLine("Skoda Fabia was create");
                return new SkodaFabia();
            }
        }

        abstract class CarClassA
        { }

        abstract class CarClassB
        { }

        class Fiat500 : CarClassA
        { }

        class FordFiesta : CarClassB
        { }

        class DaewooMatiz : CarClassA
        { }

        class SkodaFabia : CarClassB
        { }

        class Client
        {
            private CarClassA carclassA;
            private CarClassB carclassB;

            public Client(AbstractFactory factory)
            {
                carclassA = factory.CreateCarClassA();
                carclassB = factory.CreateCarClassB();
            }
            public void Run(string s)
            {
                Console.WriteLine("Cars from " + s + " were create");
            }
        }
    }
}
