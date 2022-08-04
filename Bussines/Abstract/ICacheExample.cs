using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Abstract
{
    public interface ICacheExample
    {
        void DistSetString(string key, string value);
        void DistSetList(string key);
        string DistGetValue(string key);

        void InMemSetString(string key, string value);
        void InMemSetObject(string key,object value);
        T InMemGetValue<T>(string key);
    }
}
