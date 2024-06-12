﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppMusic1.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int Duration { get; set; }
    }
}
