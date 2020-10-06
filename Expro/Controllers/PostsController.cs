using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostService PostService;

        public PostsController(IPostService postService)
        {
            PostService = postService;
        }

        public IActionResult Index()
        {
            var curUser = accountUtil.GetCurrentUser(User);

            //create
            var newPost = new Post()
            {
                Title = "adasdf",
                AuthorID = curUser.ID
            };
            PostService.Add(newPost, curUser.ID);

            List<Post> newPosts = new List<Post>()
            {
                new Post()
                {
                    Title = "A1",
                    AuthorID = curUser.ID
                },
                new Post()
                {
                    Title = "A2",
                    AuthorID = curUser.ID
                },
            };
            PostService.AddCollection(newPosts, curUser.ID);


            //update
            var oldPost = PostService.GetByID(1);
            oldPost.Title = "B1";
            PostService.Update(oldPost, curUser.ID);

            var oldPost7 = PostService.GetByID(2);
            oldPost.Title = "B2";
            var oldPost8 = PostService.GetByID(3);
            oldPost.Title = "B3";

            List<Post> oldPosts = new List<Post>()
            {
                oldPost7,
                oldPost8
            };
            PostService.UpdateCollection(oldPosts, curUser.ID);


            ////delete
            //var oldPost = PostService.GetByID(6);
            //PostService.Delete(oldPost, curUser.ID);

            //var oldPost7 = PostService.GetByID(7);
            //var oldPost8 = PostService.GetByID(8);

            //List<Post> oldPosts = new List<Post>()
            //{
            //    oldPost7,
            //    oldPost8
            //};
            //PostService.DeleteCollection(oldPosts, curUser.ID);


            ////deletePermanently
            //var oldPost = PostService.GetByID(6);
            //PostService.DeletePermanently(oldPost, curUser.ID);

            //var oldPost7 = PostService.GetByID(7);
            //var oldPost8 = PostService.GetByID(8);

            //List<Post> oldPosts = new List<Post>()
            //{
            //    oldPost7,
            //    oldPost8
            //};
            //PostService.DeletePermanentlyCollection(oldPosts, curUser.ID);

            return View();
        }

        public IActionResult TestF()
        {

            return View();
        }

        [HttpPost]
        public JsonResult TestAjaxClick(int id)
        {
            var curUser = accountUtil.GetCurrentUser(User);

            var newPost = new Post()
            {
                Title = "Hello world",
                AuthorID = curUser.ID
            };
            PostService.Add(newPost, curUser.ID);


            string result = "Added " + id;
            return Json(result);
        }
    }
}
