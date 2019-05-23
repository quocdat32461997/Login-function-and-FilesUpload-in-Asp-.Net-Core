using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dat_q_ngo.ViewModels
{
    public class VideoViewModel
    {
        [Required]
        [Key]
        [Display(Name = "Title")]
        public string Title {get; set;}

        [Required]
        [Display(Name = "Genere")]
        public string Genre {get; set;}

        [Required]
        //public List<IFormFile> Videos {get; set;}
        public IFormFile Video {get; set;}
    }
}
