using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace MongoDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = "XXXX";
            string username = "XXXX";
            string passwd = "XXXX";
            string server = "XXXX";
            int port = 0;

            string connectionString = string.Format("mongodb://{0}:{1}@{2}:{3}",
                username, passwd, server, port);
            System.Console.WriteLine(connectionString);
            var client = new MongoClient(connectionString);

            //list databases
            using (var cursor = client.ListDatabases())
            {
                foreach (var doc in cursor.ToEnumerable())
                {
                    System.Console.WriteLine(doc.ToString());
                }
            }

            var database = client.GetDatabase(dbName);

            var document = new BsonDocument
            {
                {"name", string.Format("john doe {0}", Guid.NewGuid())},
                {"age", new Random().Next(18, 80)},
                {"city", "Madrid" }
               
            };

            var collection = database.GetCollection<BsonDocument>("people");
            collection.InsertOne(document);

            System.Console.WriteLine("END");
        }
    }
}
