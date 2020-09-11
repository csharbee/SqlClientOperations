using System;
using System.Collections.Generic;
using System.Text;

namespace SqlClientOperations
{
    public interface ISqlQueryProcessors<T> where T : class
    {
        bool Insert(T model);
        bool Update(int Id, T model);
        bool Delete(int Id);
        List<T> Select();
    }
}
