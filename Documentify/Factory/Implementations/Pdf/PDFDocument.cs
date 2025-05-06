using Documentify.Factory.Interfaces;

namespace Documentify.Factory.Implementations.Pdf
{
    public class PDFDocument<T> : IDocument<T>
    {
        public FileStream? Create(string name, IList<T> items) => throw new NotImplementedException();
    }
}
