using System.IO;
using System.Threading.Tasks;

namespace EnumerableStreamFileResult
{
    /// <summary>
    /// Defines an interface for a class that can write itself to a stream.
    /// </summary>
    public interface IStreamWritingAdapter<T>
    {
        string ContentType { get; }

        Task WriteHeaderAsync(Stream stream);

        Task WriteAsync(T item, Stream stream);

        Task WriteFooterAsync(Stream stream, int recordCount);
    }
}