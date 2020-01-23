using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookAPI
{
    public class Car
    {
         [Key]
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("books")]
        public List<Book> Books { get; set; }
    }
    //public class User
    //{
    //    public int Id { get; set; }
    //    [Required]
    //    public string FirstName { get; set; }
    //    [Required]
    //    public string LastName { get; set; }

    //}
    public class Book {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        //public int UserId { get; set; }
        //public User User { get; set; }
        //[Required]
        public int CarId { get; set; }
        public Car Car { get; set; }

    }
}
