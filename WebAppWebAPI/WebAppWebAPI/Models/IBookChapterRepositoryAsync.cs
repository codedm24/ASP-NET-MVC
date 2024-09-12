namespace WebAppWebAPI.Models
{
    public interface IBookChapterRepositoryAsync
    {
        Task InitAsync();
        Task AddAsync(BookChapter chapter);
        Task<BookChapter> RemoveAsync(Guid id);
        Task<IEnumerable<BookChapter>> GetAllAsync();
        Task<BookChapter> FindAsync(Guid id);
        Task UpdateAsync(BookChapter chapter);
    }
}
