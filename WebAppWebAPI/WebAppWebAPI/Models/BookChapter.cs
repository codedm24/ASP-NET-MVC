﻿namespace WebAppWebAPI.Models
{
    public class BookChapter
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Pages { get; set; }
    }
}
