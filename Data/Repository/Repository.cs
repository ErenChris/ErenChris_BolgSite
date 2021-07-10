using BolgSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolgSite.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _Dbc;

        public Repository(AppDbContext Dbc)
        {
            _Dbc = Dbc;
        }

        public void AddPost(Post post)
        {
            _Dbc.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _Dbc.Posts.ToList();
        }

        public Post GetPostById(int Id)
        {
            return _Dbc.Posts.FirstOrDefault(p => p.Id == Id);
        }

        public void RemovePost(int Id)
        {
            _Dbc.Posts.Remove(GetPostById(Id));
        }

        public void UpdatePost(Post post)
        {
            _Dbc.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _Dbc.SaveChangesAsync()>0)
            {
                return true;
            }

            return false;
        }

    }
}
