using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Refit
{
    public abstract class ContentSerializer : IContentSerializer
    {
        public abstract Task<T> DeserializeAsync<T>(HttpContent content);

        public virtual Task<HttpContent> SerializeAsFormDataAsync<T>(T item, RefitSettings settings)
        {
            var content = item is string str ?
                (HttpContent)new StringContent(Uri.EscapeDataString(str), Encoding.UTF8, "application/x-www-form-urlencoded")
                : new FormUrlEncodedContent(new FormValueMultimap(item, settings));
            return Task.FromResult(content);
        }
        public abstract Task<HttpContent> SerializeAsync<T>(T item);
    }
}
