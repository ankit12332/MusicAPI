using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music_Api.Models
{
    public class Song
    {
        /*public int Id { get; set; }
        [Required(ErrorMessage = "Title Cant be null")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Language Cant be null")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Duration Cant be null")]
        public string Duration { get; set; }*/
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public DateTime UploadedDate { get; set; }
        public bool IsFeatured { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile AudioFile { get; set; }
        public string AudioUrl { get; set; }


        public int ArtistId { get; set; }
        public int? AlbumId { get; set; }
    }
}
