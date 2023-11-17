using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using NebulaPlugin.Application;

namespace NebulaPlugin.Application.Mongo
{
    public class MongoManagement : DatabaseManager
    {
        public override void Create(params global::System.Object[] parameters)
        {
            string connectionURL = "mongodb+srv://Victory:7Aynras2-@gameserver.ptrocqn.mongodb.net/?retryWrites=true&w=majority";
            MongoClient client = new MongoClient(connectionURL);
            var databases = client.ListDatabaseNames().ToList();
            foreach (var db in databases)
            {
                Console.WriteLine(db);
            }
        }

        public override void Read(params global::System.Object[] parameters)
        {
            //TODO: tipine göre değişkenlik gösterebilir. örneğin sadece veritabanlarını oku
            // veya veritabanlarına bağlı koleksiyonları oku vs vsvsvs
            //1. parametre veritabanı
            //2. parametre collections
            //3. parametre objectID
        }

        public override void Update(params global::System.Object[] parameters)
        {
            //1. parameters database
            //2. parameters collection
            //3. parameters id
            //4. parameters previousElements
            throw new System.NotImplementedException();
        }

        public override void Delete(params global::System.Object[] parameters)
        {
            //1.parametre veritabanı
            //2.parametre collection
            //3.parametre objectID
            throw new System.NotImplementedException();
        }
    }
}