using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dat_q_ngo.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace dat_q_ngo.Models
{
    public class ApplicationUserModel : IdentityUser
    {
       //public string UserName {get; set;}

        public string Name {get; set;}
        
        //public string Email {get; set;}

        public string Password {get; set;}

        //List of videos belonged to the user
        public List<VideoModel> Videos {get; set;}
    }
}
