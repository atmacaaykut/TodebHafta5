using DAL.Abstract;
using DAL.MongoBase;
using Microsoft.Extensions.Configuration;
using Models.Document;
using MongoDB.Driver;

namespace DAL.Concrete.Mongo
{
    public class CreditCardRepository:DocumentRepository<CreditCard>,ICrediCartRepository
    {
        private const string CollectionName = "CreditCard";

        public CreditCardRepository(MongoClient client, IConfiguration configuration) : base(client, configuration, CollectionName)
        {
        }
    }
}
