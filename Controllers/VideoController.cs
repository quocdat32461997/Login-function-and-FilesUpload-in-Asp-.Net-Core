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
using Microsoft.AspNetCore.Authorization;

namespace dat_q_ngo.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> UploadVideo()
        {
            //ApplicationUserModel user = new ApplicationUserModel();
            if(!_signInManager.IsSignedIn(User))
                return View();//if not signed in, redirect to VideoUpload page
            ApplicationUserModel user = await _userManager.GetUserAsync(User);
            List<VideoModel> list = _dbcontext.Videos.ToList();
            List<VideoModel> videoList = list.FindAll(
                delegate(VideoModel v)
                {
                    return v.UserName == user.UserName;
               }
            );
            ViewData["VideoList"] = videoList;
            List<VideoViewModel> videoViewList = new List<VideoViewModel>();
            foreach(VideoModel x in videoList)
            {
                Console.WriteLine(x.UserName);
                Console.WriteLine(x.FileName);
            }
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
                  Directory.GetCurrentDirectory(), "MyStaticFiles",   
                 video.FileName);  
            
            //Print file path and file name
            Console.WriteLine(filePath);
            Console.WriteLine(video.FileName);
            
            //Starts adding videos to stream and file system
            ApplicationUserModel user = await _userManager.GetUserAsync(User);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //add videos to file system
                await video.CopyToAsync(stream);
                Console.WriteLine("A new video is added to " + filePath);

                //create a new video model storing file paths
                VideoModel newVideo = new VideoModel 
                {
                    Title = model.Title,
                    UserName = user.UserName,
                    UploadTime = DateTime.Now,
                    Genre = model.Genre,
                    ThumbUpCounts = 0,
                    FileName = video.FileName,
                    FilePath = filePath
                };
                
                //add videos to users and  database
                if(user.Videos == null)//initiate list
                    user.Videos = new List<VideoModel>();
                user.Videos.Add(newVideo);
                _dbcontext.Videos.Add(newVideo);
                _dbcontext.SaveChanges();
                Console.WriteLine("A new video is added to the user's list");
            }
            //Testing
            Console.WriteLine("List videos");
            List<VideoModel> list = _dbcontext.Videos.ToList();
            List<VideoModel> videoList = list.FindAll(
                delegate(VideoModel v)
                {
                    return v.UserName == user.UserName;
                }
            );
            foreach(VideoModel x in videoList)
            {
                Console.WriteLine(x.UserName);
            }
            ViewData["VideoList"] = videoList;
            /*VideoListViewModel video_list_model = new VideoListViewModel
            {
                VideoList = _dbcontext.Videos.ToList()
            };*/
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            //return Ok(new { count = files.Count, size, filePath});
            //after adding videos, return the list of videos
            //return View(model);
            return View();
        }
    }
}
