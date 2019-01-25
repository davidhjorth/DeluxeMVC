using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DavidHjorthDeluxeMobler.Models
{
    public class Furniture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public int InitialCount { get; set; }
        public int Price { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public int BuyCount { get; set; }
        public int Points { get; set; }

        public static List<Furniture> CreateData()
        {
            List<Furniture> FurnitureList = new List<Furniture>();

            FurnitureList.Add(new Sofa { Id=1, Title="Vila", Count= 37, InitialCount = 100, Price = 27999, Material="Leather", Color="Brown", Length=4.8,Corner=true, SofaBed=false });
            FurnitureList.Add(new Sofa { Id=2, Title = "Nap", Count = 13, InitialCount = 100, Price = 17899, Material = "Cotton", Color = "Gray", Length = 2.2, Corner = false, SofaBed = false });
            FurnitureList.Add(new Sofa { Id=3, Title = "Granit", Count = 4, InitialCount = 100, Price = 27999, Material = "Leather", Color = "Black", Length = 3, Corner = false, SofaBed = true });
            FurnitureList.Add(new Table { Id=4, Title = "Adam", Count = 12, InitialCount = 100, Price = 5000, Material = "Oak", Color = "Black", Height = 1.5, Legs = 4});
            FurnitureList.Add(new Table { Id=5, Title = "Lang", Count = 17, InitialCount = 100, Price = 9999, Material = "Mahogany", Color = "Brown", Height = 1.35, Legs = 4});
            FurnitureList.Add(new Table { Id=6, Title = "Tree", Count = 2, InitialCount = 100, Price = 9999, Material = "Walnut", Color = "Brown", Height = 1.4, Legs = 2});
            FurnitureList.Add(new Chair { Id=7, Title = "Haveaseat", Count = 10, InitialCount = 100, Price = 1299, Material = "Birch", Color = "White", Legs = 4});

            return FurnitureList;
        }


        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/library.json");
        public static bool SaveData(List<Furniture> furniturelist)
        {
            var settings = new JsonSerializerSettings()
            {

                TypeNameHandling = TypeNameHandling.Objects
            };
            string json = JsonConvert.SerializeObject(furniturelist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);

            return true;
        }
        public static List<Furniture> GetData()
        {
            List<Furniture> data;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.Indented
                };
                var json = System.IO.File.ReadAllText(filepath);
                data = JsonConvert.DeserializeObject<List<Furniture>>(json, settings);
            }
            else
            {
                data = CreateData();
            }
            //Algoritm - +3 poäng per buy och +5 per count i examples left
            data = data.OrderBy(x => x.InitialCount).ToList();
            int points = 0;
            foreach (var d1 in data)
            {
                points = points + 5;
                d1.Points = points;
            }
            data = data.OrderBy(x => x.BuyCount).ToList();
            points = 0;
            foreach (var d2 in data)
            {
                points = points + 3;
                d2.Points += points;
            }
            data = data.OrderByDescending(x => x.Points).ToList();
            SaveData(data);
            return data;
        }

    }
    public class Sofa : Furniture

    {
        public double Length { get; set; }
        public bool Corner { get; set; }
        public bool SofaBed { get; set; }
    }

    public class Table : Furniture
    {
        public double Height { get; set; }
        public int Legs { get; set; }
    }

    public class Chair : Furniture
    { 
        public int Legs { get; set;}
    }
       
}

