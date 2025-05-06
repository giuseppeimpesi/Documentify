using Documentify.Factory.Enums;
using Documentify.Factory.Implementations.Pdf;
using Documentify.Factory.Implementations.Xls;
using Documentify.Factory.Interfaces;

namespace Documentify.Factory
{
    public class DocumentFactory
    {
        public IDocument<T> GetDocument<T>(DocumentType documentType) => documentType switch
        {
            DocumentType.PDF => new PDFDocument<T>(),
            DocumentType.EXCEL => new XLSDocument<T>(),

            _ => throw new ArgumentException("Invalid document type")
        };
    }
}

