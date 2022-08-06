using System.Collections.Generic;
using Models.Document;
using MongoDB.Bson;

namespace Bussines.Abstract
{
    public interface ICreditCardService
    {
        void Add(CreditCard model);
        void Update(CreditCard model);
        void Delete(ObjectId id);
        CreditCard Get(ObjectId id);
        IEnumerable<CreditCard> GetAll();
        void TestExceptionFilter();
    }
}
