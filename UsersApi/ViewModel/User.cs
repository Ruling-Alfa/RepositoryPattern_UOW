using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersApi.ViewModel
{
    [Table("Users")]
    public record User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
