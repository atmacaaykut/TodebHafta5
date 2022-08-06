using System;
using System.Collections.Generic;
using Bussines.Abstract;
using DAL.Abstract;
using Models.Document;
using MongoDB.Bson;

namespace Bussines.Concrete
{
    public class CreditCardService: ICreditCardService
    {
        private readonly ICrediCartRepository _repository;

        public CreditCardService(ICrediCartRepository repository)
        {
            _repository = repository;
        }

        public void Add(CreditCard model)
        {
            _repository.Add(model);
        }

        public void Update(CreditCard model)
        {
            _repository.Update(model);
        }

        public void Delete(ObjectId id)
        {
            _repository.Delete(id);
        }

        public CreditCard Get(ObjectId id)
        {
            return _repository.Get(x => x.Id == id);
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return _repository.GetAll();
        }

        public void TestExceptionFilter()
        {
            throw new Exception("Test");
        }
    }
}
