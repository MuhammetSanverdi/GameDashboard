using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Net.Security;
using Microsoft.Extensions.Configuration;
using GameDashboard.Domain.Concrete;

namespace GameDashboard.Persistence.Context
{
    public class ConfigrationMongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private MongoClient _client;
        public IMongoDatabase Database;

        public ConfigrationMongoDbService(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoDBSettings:ConnectionString");
            var mongoDatabaseName = configuration.GetConnectionString("MongoDBSettings:DatabaseName");


            var settings = MongoClientSettings.FromUrl(new MongoUrl(mongoConnectionString));
            _client = new MongoClient(settings);

            Database = _client.GetDatabase(mongoDatabaseName);
            
        }       
      
    }

}
