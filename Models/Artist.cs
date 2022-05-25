using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music_Api.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        /////////////////////////////////////////////////////
        public ICollection <Album> Album { get; set; }
        public ICollection <Song> Songs { get; set; }
    }
}
