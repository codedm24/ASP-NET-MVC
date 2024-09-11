using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppWebAPI.Models;

namespace WebAppWebAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookChaptersController : ControllerBase
    {
        private readonly IBookChapterRepository _repository;

        public BookChaptersController(IBookChapterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<BookChapter> GetBookChapters() => _repository.GetAll();

        [HttpGet("{id}", Name = nameof(GetBookChapterById))]
        public IActionResult GetBookChapterById(Guid id)
        {
            BookChapter? chapter = _repository.Find(id);
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
        public IActionResult PostBookChapter([FromBody] BookChapter chapter)
        {
            if (chapter == null)
                return BadRequest();

            _repository.Add(chapter);
            return CreatedAtRoute(nameof(GetBookChapterById), new { id = chapter.Id }, chapter);
        }

        [HttpPut("{id}")]
        public IActionResult PutBookChapter(Guid id, [FromBody] BookChapter chapter)
        {
            if (chapter == null || id != chapter.Id)
                return BadRequest();

            if (_repository.Find(id) == null)
                return NotFound();

            _repository.Update(chapter);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _repository.Remove(id);
        }
    }
}
