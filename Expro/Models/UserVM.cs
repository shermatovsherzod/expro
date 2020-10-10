using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }  
        public string PhoneNumber { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
        public UserType UserType { get; set; }
        public int GenderID { get; set; }
        public int Gender { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public string CityOther { get; set; }
        public int RegionID { get; set; }
        public string Region { get; set; }
    }
}
