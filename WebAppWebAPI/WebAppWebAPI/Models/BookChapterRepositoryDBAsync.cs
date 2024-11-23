using Microsoft.EntityFrameworkCore;

namespace WebAppWebAPI.Models
{
    public class BookChapterRepositoryDBAsync : IBookChapterRepositoryAsync, IDisposable
    {
        private BooksContext _booksContext;

        public BookChapterRepositoryDBAsync(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public void Dispose()
        {
            _booksContext?.Dispose();
        }

        public Task InitAsync()
        {
            return Task.FromResult<object?>(null);
        }

        public async Task AddAsync(BookChapter chapter)
        {
            _booksContext.Add(chapter);
            await _booksContext.SaveChangesAsync();
        }

        public async Task<BookChapter?> RemoveAsync(Guid id)
        {
            BookChapter? chapter = await _booksContext.Chapters.SingleOrDefaultAsync<BookChapter>(c => c.Id == id);
            if (chapter == null)
                return null;
            _booksContext.Chapters.Remove(chapter);
            await _booksContext.SaveChangesAsync();
            return chapter;
        }

        public async Task<IEnumerable<BookChapter>> GetAllAsync() => await _booksContext.Chapters.ToListAsync<BookChapter>();

        public async Task<BookChapter?> FindAsync(Guid id) => await _booksContext.Chapters.SingleOrDefaultAsync(c => c.Id == id);

        public async Task<object?> UpdateAsync(BookChapter chapter)
        {
            try
            {
                //var existingEntity = _booksContext.Chapters.Local.FirstOrDefault<BookChapter>(c => c.Id == chapter.Id);
                //if (existingEntity != null)
                //    _booksContext.Entry(existingEntity).State = EntityState.Detached;

                BookChapter? chapterToUpdate = await FindAsync(chapter.Id);

                if (chapterToUpdate != null)
                {
                    chapterToUpdate.Number = chapter.Number;
                    chapterToUpdate.Title = chapter.Title;
                    chapterToUpdate.Pages = chapter.Pages;
                    _booksContext.Chapters.Update(chapterToUpdate);
                    int returnVal = await _booksContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return Task.FromResult<object?>(null);
        }
    }
}
