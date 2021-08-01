using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelbiBackend.Entites;

namespace WelbiBackend.Proxy
{
    public interface IWebService<T>
    {
        List<T> LookUpALLItems(String endPoint);

        T LookUpItem(String endPoint, int id);

        bool Insert(T item, String endPoint);

        bool Update(T item, String endPoint);
    }
}
