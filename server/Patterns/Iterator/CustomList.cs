using System.Collections.Generic;

namespace GameServer.Patterns.Iterator
{
    public class CustomList<T>: IHasIterator<T>
    {
        private List<T> _list;

        public CustomList()
        {
            _list = new List<T>();
        }

        public CustomList(List<T> list)
        {
            _list = list;
        }
        public void Add(T value)
        {
            _list.Add(value);
        }

        public void Remove(int index)
        {
            _list.RemoveAt(index);
        }
        
        public IIterator<T> CreateIterator()
        {
            return new CustomListIterator<T>(_list);
        }
    }
}