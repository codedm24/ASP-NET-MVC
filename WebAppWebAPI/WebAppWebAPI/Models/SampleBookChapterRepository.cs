
using System.Collections.Concurrent;

namespace WebAppWebAPI.Models
{
    public class SampleBookChapterRepository : IBookChapterRepository
    {
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters = new ConcurrentDictionary<Guid, BookChapter>();

        public void Init()
        {
            Add(new BookChapter
            {
                Number = 1,
                Title = "Application Architectures",
                Pages = 35
            });
            Add(new BookChapter
            {
                Number = 2,
                Title = "Core C#",
                Pages = 42
            });
        }

        public void Add(BookChapter bookChapter)
        {
            bookChapter.Id = Guid.NewGuid();
            _chapters[bookChapter.Id] = bookChapter;
        }

        public BookChapter? Find(Guid id)
        {
            BookChapter? chapter;
            _chapters.TryGetValue(id, out chapter);
            return chapter;
        }

        public IEnumerable<BookChapter> GetAll() => _chapters.Values;


        public BookChapter? Remove(Guid id)
        {
            BookChapter? removed;
            _chapters.TryRemove(id, out removed);
            return removed;
        }

        public void Update(BookChapter bookChapter)
        {
            _chapters[bookChapter.Id] = bookChapter;
        }
    }
}
