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
    public class ArtistsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public ArtistsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artist artist)
        {
            var imageUrl = await FileHelper.UploadImage(artist.Image);
            artist.ImageUrl = imageUrl;

            await _dbContext.Artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await (from artist in _dbContext.Artists
                          select new
                          {
                            Id = artist.Id,
                            ArtistName = artist.Name,
                            ImageUrl = artist.ImageUrl,
                          }).ToListAsync();
            return Ok(artists);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ArtistDetails(int artistId)
        {
            var artistDetails = await _dbContext.Artists
                .Where( a=>a.Id == artistId )
                .Include ( a => a.Songs )
                .ToListAsync();
            return Ok(artistDetails);
        }
    }
}
