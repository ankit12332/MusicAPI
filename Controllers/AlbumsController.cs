using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music_Api.Data;
using Music_Api.Helpers;
using Music_Api.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Music_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public AlbumsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Album album)
        {
            var imageUrl = await FileHelper.UploadImage(album.Image);
            album.ImageUrl = imageUrl;

            await _dbContext.Albums.AddAsync(album);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await (from album in _dbContext.Albums
                                select new
                                {
                                    Id = album.Id,
                                    AlbumName = album.Name,
                                    ImageUrl = album.ImageUrl,
                                }).ToListAsync();
            return Ok(albums);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> AlbumDetails(int albumId)
        {
            var albumDetails = await _dbContext.Albums.Where(a=>a.Id == albumId).Include(a => a.Songs).ToListAsync();
            return Ok(albumDetails);
        }
    }
}
