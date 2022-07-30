using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL.EfBase
{
    public class EfBaseRepository<T,TBContext>:IEfBaseRepository<T> 
        where T:class
        where TBContext:DbContext
    {
        protected readonly TBContext Context;
        public EfBaseRepository(TBContext context)
        {
            Context = context;
        }

        public T Add(T entity)
        {
            return Context.Add(entity).Entity;
             
        }

        public T Update(T entity)
        {
            Context.Update(entity);
            return entity;
        }

        /// <summary>
        /// null ise bütün dataları dönecek, değil ise koşulu sağlayan dataları dönecek
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
                return Context.Set<T>().ToList();
            else
            {
                

                return Context.Set<T>().Where(expression);

            }
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            //Context.Set<T>().Where(expression).FirstOrDefault();

            return Context.Set<T>().FirstOrDefault(expression);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
