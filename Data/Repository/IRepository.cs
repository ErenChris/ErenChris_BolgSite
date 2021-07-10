using BolgSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolgSite.Data.Repository
{
    public interface IRepository
    {
        void AddPost(Post post);
        void RemovePost(int Id);
        void UpdatePost(Post post);
        Post GetPostById(int Id);
        List<Post> GetAllPosts();

        Task<bool> SaveChangesAsync();
    }
}
