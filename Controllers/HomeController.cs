using BolgSite.Data;
using BolgSite.Data.Repository;
using BolgSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());
            else
            {
                var post = _Repository.GetPostById((int)id);
                return View(post);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
                _Repository.UpdatePost(post);
            else
                _Repository.AddPost(post);

            if (await _Repository.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> RemovePost(int id)
        {
            _Repository.RemovePost(id);
            await _Repository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
