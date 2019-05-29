using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dat_q_ngo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dat_q_ngo.Models
{
    public class VideoModel
    {
        [Key]
        //Genrerated by database
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ID {get; set;}

        [ForeignKey("ApplicationUserModel")]
        public string UserName {get; set;}

        public string Title {get; set;}

        //Genereated by database
        public DateTime UploadTime {get; set;}

        public string Genre {get; set;}

        public int ThumbUpCounts {get; set;}

        public string FileName {get; set;}

        public string FilePath {get; set;}//videos saved to this filepath
    }
}
