using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace MongoDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = "XXXX";
            string username = ConfigurationManager.AppSettings["mongo-user"];
            string passwd = ConfigurationManager.AppSettings["mongo-pass"]; ;
            string server = ConfigurationManager.AppSettings["mongo-server"]; ;
            int port = int.Parse( ConfigurationManager.AppSettings["mongo-port"] ) ;

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
