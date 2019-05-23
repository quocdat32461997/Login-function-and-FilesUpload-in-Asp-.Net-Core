using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dat_q_ngo.Models;
using dat_q_ngo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace dat_q_ngo.Controllers
{
    public class VideoController : Controller
    {
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly SignInManager<ApplicationUserModel> _signInManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbcontext;

        public VideoController (
            UserManager<ApplicationUserModel> userManager,
            SignInManager<ApplicationUserModel> signInManager,
            ILogger<AccountController> logger,
            ApplicationDbContext dbcontext)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _logger = logger;
                _dbcontext = dbcontext;
            }

        [HttpGet]
        public IActionResult UploadVideo()
        {
            VideoListViewModel video_list_model = new VideoListViewModel
            {
                VideoList = _dbcontext.Videos.ToList()
            };

            return View();
        }

        [HttpPost]
        //upload video once at a time
        public async Task<IActionResult> UploadVideo(VideoViewModel model)
        {
            //Empty video
            if(model.Video == null || model.Video.Length <= 0)// || !model.Video.Any())
                return View();

            IFormFile video = model.Video;

            var filePath = Path.Combine(  
                  Directory.GetCurrentDirectory(), "wwwroot",   
                 video.FileName);  
            
            //Print file path and file name
            Console.WriteLine(filePath);
            Console.WriteLine(video.FileName);
            
            //Starts adding videos to stream and file system
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //add videos to file system
                await video.CopyToAsync(stream);
                Console.WriteLine("A new video is added to " + filePath);

                //create a new video model storing file paths
                VideoModel newVideo = new VideoModel 
                {
                    Title = model.Title,
                    UploadTime = DateTime.Now,
                    Genre = model.Genre,
                    ThumbUpCounts = 0,
                    FilePath = filePath
                };
                
                //add videos user database
                _dbcontext.Videos.Add(newVideo);
                _dbcontext.SaveChanges();
                Console.WriteLine("A new video is added to the user's list");

                //Testing
                Console.WriteLine("List videos");
                List<VideoModel> list = _dbcontext.Videos.ToList();
                Console.WriteLine(list != null);
                foreach(VideoModel x in list)
                {
                    Console.WriteLine(x.FilePath);
                }
            }

            VideoListViewModel video_list_model = new VideoListViewModel
            {
                VideoList = _dbcontext.Videos.ToList()
            };
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            //return Ok(new { count = files.Count, size, filePath});
            //after adding videos, return the list of videos
            //return View(model);
            return View(video_list_model);
        }
    }
}
