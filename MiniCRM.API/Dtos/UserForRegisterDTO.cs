using System;
using System.ComponentModel.DataAnnotations;

namespace MiniCRM.API.Dtos
{
    public class UserForRegisterDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        [StringLength(8,MinimumLength =4,ErrorMessage ="password between 4 and 8 char")]
        public string password { get; set; }
        public DateTime Created { get; set; }
          public UserForRegisterDTO()
          {
              Created = DateTime.Now;
             
          }
    }
}