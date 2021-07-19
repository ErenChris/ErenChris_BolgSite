using BolgSite.Data;
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
        private readonly IRepository _Repository;

        public HomeController(IRepository Repository)
        {
            _Repository = Repository;
        }

        public IActionResult Index()
        {
            var posts = _Repository.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _Repository.GetPostById(id);
            return View(post);
        }
    }
}
