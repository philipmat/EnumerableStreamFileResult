using System.IO;
using System.Threading.Tasks;

namespace EnumerableStreamFileResult
{
    /// <summary>
    /// Defines an interface for a class that can write itself to a stream.
    /// </summary>
    public interface IStreamWriteable
    {
        Task WriteHeaderAsync(Stream stream);

        Task WriteAsync(Stream stream);

        Task WriteFooterAsync(Stream stream);
    }
}