using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MediSync.Models
{
    public class UserLogin
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string userPassword { get; set; }
    }

    public class CurrentUserModel
    {
        public int currentUserID { get; set; }
        public string currentUserName { get; set; }
        public int? currentUserReferenceToId { get; set; }
        public string currentUserRole { get; set; }
        public string currentUserFirstName { get; set; }
        public string currentUserLastName { get; set; }

    }
}