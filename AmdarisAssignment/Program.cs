using System;
using System.Collections.Generic;
using System.Linq;

namespace AmdarisAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Filtering();
            //Ordering();
            //Quantifiers();
            //Projection();
            //Grouping();
            //Generation();
            //ElementOperators();
            //DataConversion();
            //Aggregation();
            //SetOperations();
            //Joins();

            ImportantPacket packet = new ImportantPacket { Object = "windscreen", CityId = 2, Weight = 8.1, TimeLimit = TimeSpan.Parse("12:00:00") };
            packet.Remind();
            Console.WriteLine(4.Power(3));
            Console.WriteLine("default text".FirstWord());
        }

        private static void PrintCollection<T>(IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                Console.WriteLine(item);
            }
        }

        public static void Filtering()
        {
            // Where (deffered execution)
            var result = _packets.Where(packet => packet.Weight >= 5);
            // Skip
            result = _packets.Skip(4);
            // SkipWhile
            result = _packets.SkipWhile(packet => packet.CityId == 1);
            // Take
            result = _packets.Take(3);
            // TakeWhile
            result = _packets.TakeWhile(packet => packet.CityId == 1);
            // Chunk
            //result = _packets.Chunk(3);

            // OfType
            result = _packets.OfType<ImportantPacket>();
        }

        public static void Ordering()
        {
            // OrderBy, ThenBy
            var result = _packets.OrderBy(x => x.Weight).ThenBy(x => x.Object).ThenBy(x => x.CityId).ToList();
            // OrderByDescending, ThenByDescending
            result = _packets.OrderByDescending(x => x.Weight).ThenBy(x => x.Object).ThenByDescending(x => x.CityId).ToList();
            // Reverse
            result.Reverse();
        }

        public static void Quantifiers()
        {
            // Any
            var result = _packets.Any(x => x.Weight > 10);
            // All
            result = _packets.All(x => x.Weight > 10);
            // Contains
            result = _packets.Contains(new Packet { Object = "phone", Weight = 1, CityId = 1 });
            // SequenceEqual
            result = _packets.SequenceEqual(_packets);
        }

        public static void Projection()
        {
            // Select (anonymus types)
            var result = _packets.Select(x => x.Object);
            // SelectMany
            var result2 = _cities.SelectMany(x => x.Packets);
        }

        public static void Grouping()
        {
            // GroupBy
            var result = _packets.GroupBy(x => x.CityId);

            //foreach (var city in result)
            //{
            //    Console.WriteLine(city.Key);
            //    foreach (var packet in city)
            //    {
            //        Console.WriteLine(packet);
            //    }
            //}
        }

        public static void Generation()
        {
            // DefaultIfEmpty
            var result = new List<Packet> { }.DefaultIfEmpty(new Packet() {Object = "Default" });
            // Empty
            //result = _packets.Empty();
            var result2 = Enumerable.Empty<string>();
            // Range
            var result3 = Enumerable.Range(1, 3);
            // Repeat
            var result4 = Enumerable.Repeat("result", 5);
        }

        public static void ElementOperators()
        {
            // First, FirstOrDefault
            var result = _packets.First(x => x.Weight == 7.5);
            // Last, LastOrDefault
            result = _packets.Last(x => x.Weight == 7.5);
            // Single, SingleOrDefault
            result = _packets.SingleOrDefault(x => x.Weight == 0.2);
            // ElementAt, ElementAtOrDefault
            result = _packets.ElementAtOrDefault(12);
        }

        public static void DataConversion()
        {
            // Cast (throws InvalidCastException if unable to cast an item in the collection)
            var result = _packets.Cast<Object>();
            // ToDictionary (simply by a key or with element selector)
            var result2 = _cities.ToDictionary(x => x.Id);
            // ToLookup
            var result3 = _packets.ToLookup(x => x.CityId);
            //foreach (var group in result3)
            //{
            //    Console.WriteLine($"{group.Key}:");
            //    foreach (var packet in group)
            //    {
            //        Console.WriteLine($"{packet}");
            //    }
            //}
        }

        public static void Aggregation()
        {
            // Aggregate
            var result = _cities.Aggregate("", (previousResult, city) => previousResult + city.Name + " ");
            // Average
            var result2 = _packets.Average(x => x.Weight);
            // Count, LongCount
            var result3 = _packets.Count(x => x.CityId == 1);
            // Max, MaxBy
            var result4 = _packets.Max(x => x.Weight);
            // Min, MinBy
            var result5 = _packets.Min(x => x.Weight);
            // Sum
            var result6 = _packets.Sum(x => x.Weight);
        }

        public static void SetOperations()
        {
            List<int> _numbers = new List<int> { 1, 2, 3, 3, 4, 5, 5, 5, 4 };
            List<int> _numbers2 = new List<int> { 1, 5, 5, 4 };
            // Distinct, DistinctBy
            var result = _numbers.Distinct();
            // Except, ExeceptBy
            result = _numbers.Except(_numbers2);
            // Intersect, IntersectBy
            result = _numbers.Intersect(_numbers2);
            // Union, UnionBy (distinct union)
            result = _numbers.Union(_numbers2);
            // Concat (non distinct)
            result = _numbers.Concat(_numbers2);
        }

        public static void Joins()
        {
            // Join (also with query language)
            var result = _packets.Join(_cities,
                packet => packet.CityId,
                city => city.Id,
                (packet, city) => new { packet.Object, city.Name }
                );

            var result2 = from packet in _packets
                          join city in _cities on packet.CityId equals city.Id
                          select new { packet.Object, city.Name };

            // GroupJoin
            //var studentsByFaculty = from faculty in _faculties
            //                        join student in _students on faculty.Id equals student.FacultyId into studentsGroup
            //                        select new { faculty.Name, studentsGroup };

            var result3 = from city in _cities
                          join packet in _packets on city.Id equals packet.CityId into packetGroup
                          select new { city.Name, packetGroup };

            //foreach (var packetGroup in result3)
            //{
            //    Console.WriteLine(packetGroup.Name);
            //    foreach (var packet in packetGroup.packetGroup)
            //    {
            //        Console.WriteLine($"  {packet}");
            //    }
            //}


            int[] _numbers = { 1, 2, 3, 4 };
            string[] _words = { "one", "two", "three" };
            // Zip
            var result4 = _numbers.Zip(_words);
        }

        private static readonly IEnumerable<Packet> _packets= CreatePacketList();
        private static IEnumerable<Packet> CreatePacketList() => new List<Packet>
            {
                new Packet { Object = "phone", CityId = 1, Weight = 1 },
                new Packet { Object = "vacuum cleaner", CityId = 1, Weight = 10.5 },
                new Packet { Object = "laptop", CityId = 2, Weight = 2.3 },
                new Packet { Object = "TV", CityId = 6, Weight = 4 },
                new ImportantPacket { Object = "windscreen", CityId = 2, Weight = 8.1, TimeLimit = TimeSpan.Parse("12:00:00") },
                new ImportantPacket { Object = "shirt", CityId = 3, Weight = 0.2, TimeLimit = TimeSpan.Parse("10:00:00") },
                new Packet { Object = "keyboard", CityId = 1, Weight = 7.5 },
                new Packet { Object = "wheel", CityId = 4, Weight = 7.5 },
                new Packet { Object = "documents", CityId = 5, Weight = 0.1 },
                new Packet { Object = "book", CityId = 5, Weight = 0.3 }
            };

        private static readonly IEnumerable<City> _cities = CreateCityList();
        private static IEnumerable<City> CreateCityList() => new List<City>
            {
                new City { Id = 1, Name = "Bacau",
                    Packets = new List<Packet> { new Packet { Object = "p1" }, new Packet { Object = "p2" } } },
                new City { Id = 2, Name = "Suceava",
                    Packets = new List<Packet> { new Packet { Object = "p3" }, new Packet { Object = "p4" } } },
                new City { Id = 3, Name = "Iasi",
                    Packets = new List<Packet> { new Packet { Object = "p5" }, new Packet { Object = "p6" } } },
                new City { Id = 4, Name = "Focsani", Packets = new List<Packet>{ } },
                new City { Id = 5, Name = "Roman", Packets = new List<Packet>{ } },
                new City { Id = 6, Name = "Piatra Neamt", Packets = new List<Packet>{ } }
            };
    }

    public class Packet
    {
        public string Object { get; set; }
        public int CityId { get; set; }
        public double Weight { get; set; }
        public override string ToString()
        {
            return Object + ", weight " + Weight + ", city " + CityId;
        }
    }

    public class ImportantPacket : Packet
    {
        public TimeSpan TimeLimit { get; set; }
        public override string ToString()
        {
            return Object + ", weight " + Weight + ", city " + CityId + " TIME LIMIT: " + TimeLimit;
        }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Packet> Packets { get; set; }
        public override string ToString()
        {
            return Id + " " + Name;
        }
    }

    public static class MyExtensions
    {
        public static void Remind(this ImportantPacket packet)
        {
            Console.WriteLine($"{packet.Object} must be delivered before {packet.TimeLimit}!");
        }

        public static int Power(this int number, int power)
        {
            int result = 1;
            for (int i = 1; i <= power; i++)
                result *= number;
            return result;
        }

        public static string FirstWord(this string text)
        {
            int pos = text.IndexOf(' ');
            if (pos == -1)
                pos = text.Length;
            return text.Substring(0, pos);
        }
    }
}

