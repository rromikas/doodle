using System.Collections.Generic;
using System.Linq;

namespace GameServer.Patterns.Iterator
{
    public class CustomListIterator<T>: IIterator<T>
    {
        private readonly List<T> _list;
        private int _current = 0;

        public CustomListIterator(List<T> list)
        {
            _list = list;
        }

        public T First()
        {
            return _list[0];
        }

        public T Next()
        {
            return _list[_current++];
        }

        public bool HasNext()
        {
            return (_list.Count - 1) >= _current;
        }
    }
}