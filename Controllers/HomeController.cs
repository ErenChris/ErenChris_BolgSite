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
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View(new Post());
        }
       
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _Repository.AddPost(post);
            if (await _Repository.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }
    }
}
