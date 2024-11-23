using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAppWebAPI.Models;

namespace WebAppWebAPI.Controllers
{
    [Produces("application/json","application/xml")]
    [Route("api/BookChaptersAsync")]
    [ApiController]
    [EnableCors("AllowAllOrigin")]
    public class BookChaptersAsyncController : Controller
    {
        private readonly IBookChapterRepositoryAsync _respository;

        public BookChaptersAsyncController(IBookChapterRepositoryAsync respository)
        {
            _respository = respository;
        }

        [HttpGet] 
        public Task<IEnumerable<BookChapter>> GetBookChapterAsync () => _respository.GetAllAsync();

        [HttpGet("{id}", Name = nameof(GetBookChapterByIdAsync))]
        public async Task<IActionResult> GetBookChapterByIdAsync(Guid id)
        { 
            BookChapter? chapter = await _respository.FindAsync(id);

            if (chapter == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(chapter);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostBookChapterAsync([FromBody]BookChapter chapter)
        {
            if (chapter == null)
                return BadRequest();

            await _respository.AddAsync(chapter);

            Console.WriteLine("Chapter Added");

            return CreatedAtRoute(nameof(GetBookChapterByIdAsync), new { id = chapter.Id }, chapter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookChapterAsync(Guid id, [FromBody] BookChapter chapter)
        { 
            if(chapter == null || id != chapter.Id)
                return BadRequest();

            if(await _respository.FindAsync(id) == null)
                return NotFound();

            await _respository.UpdateAsync(chapter);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        { 
            await _respository.RemoveAsync(id);
        }
    }
}
