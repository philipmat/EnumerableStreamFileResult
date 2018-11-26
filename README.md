# EnumerableStreamFileResult

Example of efficiently writing an `IEnumerable` to a `FileResult`.

The `EnumerableFileResult<T>` class can be initialized
with an `IEnumerable<T>` and when executed by the
ASP.NET Core pipeline in enumerates through the
enumerable and using a custom adapter, `IStreamWritingAdapter<T>`,
writes each item to the response stream (`HttpContext.Response.Body`).

More details and reasoning at: http://philipm.at/2018/enumerablefilestreamer.html
