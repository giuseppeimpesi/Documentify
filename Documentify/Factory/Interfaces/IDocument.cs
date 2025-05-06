namespace Documentify.Factory.Interfaces
{
    public interface IDocument<T>
    {
        FileStream? Create(string path, IList<T> items);
    }
}
