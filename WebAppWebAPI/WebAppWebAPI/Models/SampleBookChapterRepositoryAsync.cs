
using System.Collections.Concurrent;

namespace WebAppWebAPI.Models
{
    public class SampleBookChapterRepositoryAsync : IBookChapterRepositoryAsync
    {
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters =
            new ConcurrentDictionary<Guid, BookChapter>();

        public async Task InitAsync()
        {
            await AddAsync(new BookChapter { 
                Number = 1,
                Title = "Application Architectures",
                Pages = 35
            });
        }

        public Task AddAsync(BookChapter chapter)
        {
            chapter.Id = Guid.NewGuid();
            _chapters[chapter.Id]  = chapter;
            return Task.FromResult<object>(null);
        }

        public Task<BookChapter> FindAsync(Guid id)
        {
            BookChapter chapter;
            _chapters.TryGetValue(id, out chapter);
            return Task.FromResult<BookChapter>(chapter);
        }

        public Task<IEnumerable<BookChapter>> GetAllAsync() => Task.FromResult<IEnumerable<BookChapter>>(_chapters.Values);

        public Task<BookChapter> RemoveAsync(Guid id)
        {
            BookChapter removed;
            _chapters.TryRemove(id, out removed);
            return Task.FromResult<BookChapter>(removed);
        }

        public Task UpdateAsync(BookChapter chapter)
        {
            _chapters[chapter.Id] = chapter;
            return Task.FromResult<object>(null);
        }
    }
}
