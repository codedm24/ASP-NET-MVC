using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookServiceClientApp
{
    public class BookChapterClient : HttpClientHelper<BookChapter>
    {
        public BookChapterClient(string baseAddress) : base(baseAddress)
        { 
        }

        public override async Task<IEnumerable<BookChapter>?> GetAllAsync(string requestUri)
        { 
            IEnumerable<BookChapter>? chapters = await base.GetAllAsync(requestUri);
            return chapters?.OrderBy(x => x.Number);
        }

        public async Task<XElement> GetAllXmlAsync(string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                HttpResponseMessage resp = await client.GetAsync(requestUri);
                Console.WriteLine($"status from GET {resp.StatusCode}");
                resp.EnsureSuccessStatusCode();
                string xml = await resp.Content.ReadAsStringAsync();
                XElement chapters = XElement.Parse(xml);
                return chapters;
            }
        }
    }
}
