using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface Icrud<T>where T:class
    {
        void Add(T item);
        void Remove(int id);
        void View(int id);
    }
}
