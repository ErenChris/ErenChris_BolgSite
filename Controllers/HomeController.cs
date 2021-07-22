using BolgSite.Data;
using BolgSite.Data.FileManager;
using BolgSite.Data.Repository;
using BolgSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BolgSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFileManager _fileManager;

        public HomeController(IRepository repository, IFileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var posts = _repository.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repository.GetPostById(id);
            return View(post);
        }

        [HttpGet("/Image/{Image}")]
        public IActionResult Image(string Image)
        {
            var mime = Image.Substring(Image.LastIndexOf(".") + 1);
            return new FileStreamResult(_fileManager.imageStream(Image),$"image/{mime}");
        }
    }
}
