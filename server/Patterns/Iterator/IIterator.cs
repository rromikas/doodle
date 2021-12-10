namespace GameServer.Patterns.Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        bool HasNext();
    }
}