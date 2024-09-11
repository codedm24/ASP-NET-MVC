namespace WebAppWebAPI.Models
{
    public interface IBookChapterRepository
    {
        void Init();
        void Add(BookChapter bookChapter);
        IEnumerable<BookChapter> GetAll();
        BookChapter? Find(Guid id);
        BookChapter? Remove(Guid id);
        void Update(BookChapter bookChapter);
    }
}
