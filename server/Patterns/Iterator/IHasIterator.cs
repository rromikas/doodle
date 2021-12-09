namespace GameServer.Patterns.Iterator
{
    public interface IHasIterator<T>
    {
        IIterator<T> CreateIterator();
    }
}