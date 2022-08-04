using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace DAL.MongoBase
{
    public interface IDocumentRepository<T> where T:class
    {
        void Add(T document);
        void Update(T document);
        void Delete(ObjectId id);
        T Get(ObjectId id);
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null);

    }
}
