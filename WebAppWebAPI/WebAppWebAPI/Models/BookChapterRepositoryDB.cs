
using Microsoft.EntityFrameworkCore;

namespace WebAppWebAPI.Models
{
    public class BookChapterRepositoryDB : IBookChapterRepository, IDisposable
    {
        private BooksContext _booksContext;

        public BookChapterRepositoryDB(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public void Dispose()
        {
            _booksContext?.Dispose();
        }

        public void Init()
        {
            //return Task.FromResult<object?>(null);
        }

        public void Add(BookChapter chapter)
        {
            _booksContext.Add(chapter);
            _booksContext.SaveChanges();
        }

        public BookChapter? Remove(Guid id)
        {
            BookChapter? chapter = _booksContext.Chapters.SingleOrDefault<BookChapter>(c => c.Id == id);
            if(chapter == null)
                return null;
            _booksContext.Chapters.Remove(chapter);
            _booksContext.SaveChanges();
            return chapter;
        }

        public IEnumerable<BookChapter> GetAll() => _booksContext.Chapters.ToList<BookChapter>();

        public BookChapter? Find(Guid id) => _booksContext.Chapters.SingleOrDefault(c => c.Id == id);

        public void Update(BookChapter chapter)
        {
            try
            {
                //var existingEntity = _booksContext.Chapters.Local.FirstOrDefault<BookChapter>(c => c.Id == chapter.Id);
                //if (existingEntity != null)
                //    _booksContext.Entry(existingEntity).State = EntityState.Detached;

                BookChapter? chapterToUpdate = Find(chapter.Id);

                if (chapterToUpdate != null)
                {
                    chapterToUpdate.Number = chapter.Number;
                    chapterToUpdate.Title = chapter.Title;
                    chapterToUpdate.Pages = chapter.Pages;
                    _booksContext.Chapters.Update(chapterToUpdate);
                    int returnVal = _booksContext.SaveChanges();
                }
            }
            catch (Exception)
            {
               return;
            }

            return;
        }
    }
}
