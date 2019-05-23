using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using dat_q_ngo.Models;

namespace dat_q_ngo.ViewModels
{
    public class VideoListViewModel
    {
        public List<VideoModel> VideoList {get; set;}
    }
}
