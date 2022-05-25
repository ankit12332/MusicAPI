﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music_Api.Data;
using Music_Api.Helpers;
using Music_Api.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Music_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public SongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Song song)
        {
            var imageUrl = await FileHelper.UploadImage(song.Image);
            song.ImageUrl = imageUrl;

            var audioUrl = await FileHelper.UploadFile(song.AudioFile);
            song.AudioUrl = audioUrl;

            song.UploadedDate = DateTime.Now;

            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await (from song in _dbContext.Songs
                                 select new
                                 {
                                     Id = song.Id,
                                     SongName = song.Title,
                                     Duration = song.Duration,
                                     UploadedDate = song.UploadedDate,
                                     ImageUrl = song.ImageUrl,
                                     AudioUrl = song.AudioUrl,

                                 }).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FeaturedSongs()
        {
            var songs = await (from song in _dbContext.Songs
                               where song.IsFeatured == true
                               select new
                               {
                                   Id = song.Id,
                                   SongName = song.Title,
                                   Duration = song.Duration,
                                   UploadedDate = song.UploadedDate,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl,

                               }).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songs = await (from song in _dbContext.Songs
                               orderby song.UploadedDate descending
                               select new
                               {
                                   Id = song.Id,
                                   SongName = song.Title,
                                   Duration = song.Duration,
                                   UploadedDate = song.UploadedDate,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl,

                               }).Take(15).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(string query)
        {
            var songs = await (from song in _dbContext.Songs
                               where song.Title.StartsWith(query)
                               select new
                               {
                                   Id = song.Id,
                                   SongName = song.Title,
                                   Duration = song.Duration,
                                   UploadedDate = song.UploadedDate,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl,

                               }).Take(15).ToListAsync();
            return Ok(songs);
        }
    }
}
