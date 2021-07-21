using BolgSite.Data.FileManager;
using BolgSite.Data.Repository;
using BolgSite.Models;
using BolgSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolgSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IRepository _Repository;
        private readonly IFileManager _FileManager;

        public AdminPanelController(IRepository Repository, IFileManager FileManager)
        {
            _Repository = Repository;
            _FileManager = FileManager;
        }

        public IActionResult Index()
        {
            var posts = _Repository.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PostViewModel());
            else
            {
                var post = _Repository.GetPostById((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Body = post.Body,
                    Title = post.Title
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel PVM)
        {
            Post post = new Post
            {
                Id = PVM.Id,
                Body = PVM.Body,
                Title = PVM.Title,
                ImagePath = await _FileManager.SaveImage(PVM.Image)
            };

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
