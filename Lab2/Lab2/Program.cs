/* Labwork 2
 * Variat 6
 * Autor Yulia Chyzh
 *
 * Паттерн Компоновщик (Composite) объединяет группы объектов в древовидную структуру по принципу
 * "часть-целое и позволяет клиенту одинаково работать как с отдельными объектами, так и с группой объектов.
 * Когда использовать компоновщик?
 * Когда объекты должны быть реализованы в виде иерархической древовидной структуры
 * Когда клиенты единообразно должны управлять как целыми объектами, так и их составными частями.
 * То есть целое и его части должны реализовать один и тот же интерфейс
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            // creating army of all character
            Army army = new Detachment("Army of all characters");

            // creating groups of elfs, orcs, minotaurs, centaurs, cyclopes, dragons, hydras, knights
            Army groupOfElfs = new Detachment("Detachment of elfs");
            Army groupOfOrcs = new Detachment("Detachment of orcs");
            Army groupOfMinotaurs = new Detachment("Detachment of minotaurs");
            Army groupOfCentaurs = new Detachment("Detachment of centaurs");
            Army groupOfCyclopes = new Detachment("Detachment of cyclopes");
            Army groupOfDragons = new Detachment("Detachment of dragons");
            Army groupOfHydras = new Detachment("Detachment of hydras");
            Army groupOfKnights = new Detachment("Detachment of knights");

            // creating reqiment of elfs and orcs; minotaurs and centaurs; cyclopes, dragons and hydras
            Army regimentOfElfsOrcs = new Detachment("Reqiment of elfs and orcs");
            Army regimentOfMinotaursCentaurs = new Detachment("Reqiment of minotaurs and centaurs");
            Army regimentOfCyclopesDragonsHydras = new Detachment("Reqiment of cyclopes, dragons and hydras");
            
            // creating characters of elf, orc, minotaur, centaur, cyclope, dragon, hydra, knight
            Army Elf = new Character("Elf");
            Army Orc = new Character("Orc");
            Army Minotaur = new Character("Minotaur");
            Army Centaur = new Character("Centaur");
            Army Cyclope = new Character("Cyclope");
            Army Dragon = new Character("Dragon");
            Army Hydra = new Character("Hydra");
            Army Knight = new Character("Knight");

            // adding characters in groups
            groupOfElfs.Add(Elf);
            groupOfOrcs.Add(Orc);
            groupOfMinotaurs.Add(Minotaur);
            groupOfCentaurs.Add(Centaur);
            groupOfCyclopes.Add(Cyclope);
            groupOfDragons.Add(Dragon);
            groupOfHydras.Add(Hydra);
            groupOfKnights.Add(Knight);
            
            // adding groups in reqiments
            regimentOfElfsOrcs.Add(groupOfElfs);
            regimentOfMinotaursCentaurs.Add(groupOfMinotaurs);
            regimentOfCyclopesDragonsHydras.Add(groupOfCyclopes);

            // adding all reqiment to army
            army.Add(regimentOfElfsOrcs);
            army.Add(regimentOfMinotaursCentaurs);
            army.Add(regimentOfCyclopesDragonsHydras);
            
            army.Print();
            Console.ReadLine();

        }
    }

    abstract class Army
    {
        protected string name;

        public Army(string name)
        {
            this.name = name;
        }

        public virtual void Print()
        {
            Console.WriteLine(name);
        }
        public virtual void Add(Army component) { }
    }
    class Detachment : Army
    {
        private List<Army> components = new List<Army>();

        public Detachment(string name)
            : base(name)
        { }

        public override void Add(Army component)
        {
            components.Add(component);
        }

        public override void Print()
        {
            Console.WriteLine("Nodes " + name);
            Console.WriteLine("Subnodes:");
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Print();
            }
        }
    }
    class Character : Army
    {
        public Character(string name)
            : base(name)
        { }

        public override void Add(Army component)
        {
            throw new NotImplementedException();
        }
    }
}
