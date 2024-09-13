using System.Xml.Linq;
using static System.Console;

namespace BookServiceClientApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.ReadLine();
            ReadChaptersAsync().Wait();
            ReadChapterAsync().Wait();
            ReadNotExistingChapterAsync().Wait();
            AddChapterAsync().Wait();
            UpdateChapterAsync().Wait();
            RemoveChapterAsync().Wait();
            ReadXmlAsync().Wait();
            ReadLine();
        }

        private static async Task ReadChaptersAsync()
        { 
            WriteLine(nameof(ReadChaptersAsync));
            var client = new BookChapterClient(Addresses.BaseAddress);
            IEnumerable<BookChapter>? chapters = await client.GetAllAsync(Addresses.BooksApi);
            if (chapters != null)
            {
                foreach (BookChapter chapter in chapters)
                {
                    WriteLine(chapter.Title);
                }
            }
            WriteLine();
        }

        private static async Task ReadChapterAsync()
        { 
            WriteLine(nameof(ReadChapterAsync));
            var client = new BookChapterClient(Addresses.BaseAddress);
            var chapters = await client.GetAllAsync(Addresses.BooksApi);
            Guid? id = chapters?.First().Id;
            BookChapter? chapter = await client.GetAsync(Addresses.BooksApi + id);
            WriteLine($"{chapter?.Number} {chapter?.Title}");
            WriteLine();
        }

        private static async Task ReadNotExistingChapterAsync()
        { 
            WriteLine(nameof(ReadNotExistingChapterAsync));
            string requestIdentifier = Guid.NewGuid().ToString();
            try
            {
                var client = new BookChapterClient(Addresses.BaseAddress);
                BookChapter? chapter = await client.GetAsync(Addresses.BooksApi + requestIdentifier.ToString());
                WriteLine($"{chapter?.Number} {chapter?.Title}");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                WriteLine($"book chapter with the identifier {requestIdentifier} not found");
            }
            WriteLine();
        }

        private static async Task ReadXmlAsync()
        {
            WriteLine(nameof(ReadXmlAsync));
            var client = new BookChapterClient(Addresses.BaseAddress);
            XElement chapters = await client.GetAllXmlAsync(Addresses.BooksApi);
            WriteLine(chapters);
            WriteLine();
        }

        private static async Task AddChapterAsync()
        {
            WriteLine(nameof(AddChapterAsync));
            var client = new BookChapterClient(Addresses.BaseAddress);
            BookChapter? chapter = new BookChapter
            {
                Number = 42,
                Title = "ASP.NET Web API",
                Pages=35
            };
            chapter = await client.PostAsync(Addresses.BooksApi, chapter);
            WriteLine($"added chapter {chapter?.Title} with id {chapter?.Id}");
            WriteLine();
        }

        private static async Task UpdateChapterAsync()
        { 
            WriteLine(nameof (UpdateChapterAsync));
            var client = new BookChapterClient(Addresses.BaseAddress);
            var chapters = await client.GetAllAsync(Addresses.BooksApi);
            var chapter = chapters?.SingleOrDefault(c => c.Title == "ASP.NET Web API");
            if (chapter != null) {
                chapter.Number = 32;
                chapter.Title = "Windows Apps";
                await client.PutAsync(Addresses.BooksApi + chapter.Id, chapter);
                WriteLine($"updated chapter {chapter.Title}");
            }
            WriteLine();
        }

        private static async Task RemoveChapterAsync()
        { 
            WriteLine(nameof(RemoveChapterAsync));
            var client = new BookChapterClient(Addresses.BaseAddress);
            var chapters = await client.GetAllAsync(Addresses.BooksApi);
            var chapter = chapters?.SingleOrDefault(c => c.Title == "Windows Apps");
            if (chapter != null)
            {
                await client.DeleteAsync(Addresses.BooksApi + chapter.Id);
                WriteLine($"removed chapter {chapter.Title}");
            }
            WriteLine();
        }
    }
}
