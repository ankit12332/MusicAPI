using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Api.Data;
using Music_Api.Helpers;
using Music_Api.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoSongsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public DemoSongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

            
        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Songs.ToListAsync());
        }

        // GET api/<SongsController>/5
        [HttpGet("[action]/{id}")]
        public int Test(int id)
        {
            return id;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound("No Record Found against this ID");
            }
            return Ok(song);
        }



        /*        // POST api/<SongsController>
                [HttpPost]
                public async Task<IActionResult> Post([FromBody] Song song)
                {
                    await _dbContext.Songs.AddAsync(song);
                    await _dbContext.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status201Created);
                }*/

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Song song)
        {
            var imageUrl = await FileHelper.UploadImage(song.Image);
            song.ImageUrl = imageUrl;

            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songObj)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if(song == null)
            {
                return NotFound("No Record Found against this ID");
            }
            else
            {
                song.Title = songObj.Title;
                song.Duration = songObj.Duration;
                await _dbContext.SaveChangesAsync();
                return Ok("Record Updated Successfully");
            }  
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var songs = await _dbContext.Songs.FindAsync(id);
            if(songs == null)
            {
                return NotFound("No Record Found against this ID");
            }
            else
            {
                _dbContext.Songs.Remove(songs);
                await _dbContext.SaveChangesAsync();
                return Ok("Record Deleted");
            }
        }
    }
}
