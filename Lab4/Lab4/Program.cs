/* Labwork 4
 * Variat 5
 * Autor Yulia Chyzh
 * 
 * Разработать структуру данных для хранения информации о кинотеатрах города. 
 * Для кинотеатра хранится информация: наименование кинотеатра, вместительность (количество мест), год постройки,
 * ранг кинотеатра (для просмотра видеофильмов,  для просмотра широкоформатных фильмов, наличие
 * стереоформатного оборудования и т.п.).
 * 
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Program
    {
        public class Cinema
        {
            public int id;
            public string grp;
            public string name;
            public int capacity;
            public int year;
            public string type;

            public Cinema(int id, string grp, string name, int capacity, int year, string type)
            {
                this.id = id;
                this.grp = grp;
                this.name = name;
                this.capacity = capacity;
                this.year = year;
                this.type = type;
            }

            public override string ToString()
            {
                return "(id=" + this.id.ToString() + "; grp=" + this.grp + "; name=" + this.name + "; capacity=" + this.capacity.ToString() + "; year=" + this.year.ToString() + "; type=" + this.type + ")";
            }
        }

        public class CinemaComparison : IEqualityComparer<Cinema>
        {
            public bool Equals(Cinema x, Cinema y)
            {
                bool Result = false;
                if (x.id == y.id && x.grp == y.grp && x.name == y.name && x.capacity == y.capacity && x.year == y.year && x.type == y.type)
                    Result = true;
                return Result;
            }
            public int GetHashCode(Cinema obj)
            {
                return obj.id;
            }
        }

        public class CinemaLink
        {
            public int d1;
            public int d2;
            public CinemaLink(int i1, int i2)
            {
                this.d1 = i1;
                this.d2 = i2;
            }
        }

        static List<Cinema> d1 = new List<Cinema>()
        {
            new Cinema(1, "group1", "Butterfly", 1000, 2010, "multiplex"),
            new Cinema(2, "group1", "Salut", 500, 2004, "second-run"),
            new Cinema(3, "group1", "Sputnik", 300, 2006, "3d"),
            new Cinema(4, "group2", "Blockbaster", 1500, 2012,"imax"),
            new Cinema(5, "group2", "Lubava", 700, 2014, "multiplex"),
            new Cinema(6, "group2", "Kyiv", 400, 1998, "second-run")

        };

        static List<Cinema> d2 = new List<Cinema>()
        {
            new Cinema(1, "group2", "Oskar", 1000, 2011, "multiplex"),
            new Cinema(2, "group2", "CinemaCity", 700, 2012, "second-run"),
            new Cinema(3, "group3", "Leipzig", 600, 2007, "3d"),
            new Cinema(4, "group3", "Florence", 500, 2013,"imax"),
            new Cinema(5, "group3", "SkyMall", 700, 2006, "multiplex"),
            new Cinema(6, "group3", "Bratislava", 400, 1999, "second-run")
        };

        static List<Cinema> d1_for_distinct = new List<Cinema>()
        {
            new Cinema(1, "group1", "Butterfly", 1000, 2010, "multiplex"),
            new Cinema(1, "group1", "Butterfly", 1500, 2017, "imax"),
            new Cinema(2, "group1", "Salut", 500, 2004, "second-run"),
            new Cinema(2, "group1", "Salut", 600, 2005, "3d")
        };

        static List<CinemaLink> lnk = new List<CinemaLink>()
        {
            new CinemaLink(1, 1),
            new CinemaLink(1, 2),
            new CinemaLink(1, 4),
            new CinemaLink(1, 6),
            new CinemaLink(2, 1),
            new CinemaLink(2, 2),
            new CinemaLink(2, 4),
            new CinemaLink(5, 1),
            new CinemaLink(5, 2),
            new CinemaLink(6, 2)

        };

        static void Main(string[] args)
        {
            Console.WriteLine("===Виведення усiх елементiв першої групи для порiвняння===");
            var q1 = from x in d1
                     select x;
            foreach (var x in q1)
                Console.WriteLine(x);
            Console.WriteLine("===Виведення усiх елементiв другої групи для порiвняння===");
            var q12 = from x in d2
                     select x;
            foreach (var x in q12)
                Console.WriteLine(x);

            Console.WriteLine("===Виведення назв кiнотеатрiв першої групи===");
            var q2 = from x in d1
                     select x.name;
            foreach (var x in q2)
                Console.WriteLine(x);

            Console.WriteLine("===Виведення усiх кiнотеатрiв, назва яких починається на S===");
            var q4 = from x in d1
                     where x.name[0]=='S' && (x.grp == "group1" || x.grp == "group2")
                     select x;
            foreach (var x in q4)
                Console.WriteLine(x);
            var q42 = from x in d2
                      where x.name[0] == 'S' && (x.grp == "group1" || x.grp == "group2" || x.grp == "group3")
                     select x;
            foreach (var x in q42)
                Console.WriteLine(x);

            Console.WriteLine("===Виведення назв та типiв усiх кiнотеатрiв===");
            var q5 = from x in d1
                     select new { name = x.name, type = x.type };
            foreach (var x in q5)
                Console.WriteLine(x);
            var q52 = from x in d2
                      select new { name = x.name, type = x.type };
            foreach (var x in q52)
                Console.WriteLine(x);

            Console.WriteLine("===Виведення назв кiнотеатрів, згрупованих по роках===");
            var q16 = from x in d1.Union(d2) group x by x.year into g select new { Key = g.Key, Name = g };
            foreach (var x in q16)
            {
                Console.WriteLine(x.Key);
                var q100 = from a in x.Name
                     select new { name = a.name };
                foreach (var y in q100)
                    Console.WriteLine(y);
            }

            Console.WriteLine("===Сортування кiнотеатрiв за роками створення===");
            var q6 = from x in d1
                     orderby x.year descending
                     select x;
            foreach (var x in q6)
                Console.WriteLine(x);

            Console.WriteLine("===Виведення назв кiнотеатрів, згрупованих по роках та мiсткостi ===");
            var q17 = from x in d1.Union(d2) group x by new { x.year, x.capacity } into g select new { Key = g.Key, Name = g };
            foreach (var x in q17)
            {
                Console.WriteLine(x.Key);
                var q101 = from a in x.Name
                           select new { name = a.name };
                foreach (var y in q101)
                    Console.WriteLine(y);
            }

           Console.WriteLine("===Cross Join (Inner Join) з використанням Join===");
            var q8 = from x in d1
                     join y in d2 on x.id equals y.id
                     select new { c1 = x.capacity, c2 = y.capacity };
            foreach (var x in q8)
                Console.WriteLine(x);

            Console.WriteLine("===Cross Join i Group Join===");
            var q11 = from x in d1
                      join y in d2 on x.id equals y.id into temp
                      from t in temp
                      select new { name1 = x.name, name2 = t.name, cnt = temp.Count() };
            foreach (var x in q11)
                Console.WriteLine(x);

            Console.WriteLine("===Outer Join===");
            var q13 = from x in d1
                      join y in d2 on x.id equals y.id into temp
                      from t in temp.DefaultIfEmpty()
                      select new { name1 = x.name, name2 = ((t == null) ? "null" : t.name) };
            foreach (var x in q13)
                Console.WriteLine(x);

            Console.WriteLine("===Iмiтацiя зв'язку один-до-багатьох===");
            var lnk1 = from x in d1
                       join l in lnk on x.id equals l.d1 into temp
                       from t1 in temp
                       join y in d2 on t1.d2 equals y.id into temp2
                       from t2 in temp2
                       select new { id1 = x.id, id2 = t2.id };
            foreach (var x in lnk1)
                Console.WriteLine(x);

            Console.WriteLine("===Iмiтацiя зв'язку один-до-багатьох та перевiрка умови===");
            var lnk2 = from x in d1
                       join l in lnk on x.id equals l.d1 into temp
                       from t1 in temp
                       join y in d2 on t1.d2 equals y.id into temp2
                       where temp2.Any(t => t.year == 2012)
                       select x;
            foreach (var x in lnk2)
                Console.WriteLine(x);
        }
    }
}

