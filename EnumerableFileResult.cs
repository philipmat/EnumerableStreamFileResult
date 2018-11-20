using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EnumerableStreamFileResult
{
    class EnumerableFileStreamer<T> : FileResult
           where T : IStreamWriteable
    {
        private readonly IEnumerable<T> _enumeration;

        public EnumerableFileStreamer(IEnumerable<T> enumeration, string contentType) : base(contentType)
        {
            _enumeration = enumeration ?? throw new ArgumentNullException(nameof(enumeration));
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            SetContentType(context);
            SetContentDispositionHeader(context);

            await WriteContent(context).ConfigureAwait(false);
        }

        private async Task WriteContent(ActionContext context)
        {
            var body = context.HttpContext.Response.Body;
            bool first = true;
            T lastItem = default(T);
            foreach (var item in _enumeration)
            {
                if (first)
                {
                    await item.WriteHeaderAsync(body).ConfigureAwait(false);
                    first = false;
                }
                await item.WriteAsync(body).ConfigureAwait(false);
                lastItem = item;
            }

            if (lastItem.Equals(default(T)))
            {
                await lastItem.WriteFooterAsync(body).ConfigureAwait(false);
            }

            await base.ExecuteResultAsync(context).ConfigureAwait(false);
        }

        private void SetContentDispositionHeader(ActionContext context)
        {
            var headers = context.HttpContext.Response.Headers;
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = FileDownloadName,
                Inline = true,
            };

            headers.Add("Content-Disposition", new Microsoft.Extensions.Primitives.StringValues(cd.ToString()));
        }

        private void SetContentType(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
        }
    }
}