using System.Threading.Tasks;

namespace Interfaces
{
    public interface BaseInterface<T>where T:class
    {
        void Add(T item);
        void Remove(int id);
        void View(int id);
    }
}
