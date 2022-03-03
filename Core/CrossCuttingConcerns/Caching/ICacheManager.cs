using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);//cacheden okuma
        void Add(string key,object data,int duration);//cache ekleme.Duration = ne kadar duracak onu belirtir.
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);//Get ile başlayan bütün cache leri sil gibi
    }
}
