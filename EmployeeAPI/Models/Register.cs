using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class User
    {
        [Key]
       public int Id { get; set; }
       public string UserName { get; set; }       
       public string FirstName { get; set; }
       public string LastName { get; set; }
        public string Password { get; set; }
        public string  Token { get; set; }

        //[JsonIgnore]
        //public string Password { get; set; }
    }
}
