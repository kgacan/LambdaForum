using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaForum.Data;
using LambdaForum.Data.Models;
using LambdaForum.Models.Post;
using LambdaForum.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LambdaForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _servicePost;
        private readonly IForum _serviceForum;
        private static UserManager<ApplicationUser> _userManager; 
        public PostController(IPost servicePost, IForum serviceForum, UserManager<ApplicationUser> userManager)
        {
            _servicePost = servicePost;
            _serviceForum = serviceForum;
            _userManager = userManager;

        }
        public IActionResult Index(int id)
        {
            var post = _servicePost.GetById(id);

            var model = new PostIndexModel
            {
                Id=post.Id,
                AuthorId=post.User.Id,
                AuthorImageUrl=post.User.ProfileImageUrl,
                AuthorName=post.User.UserName,
                AuthorRating=post.User.Rating,
                Created=post.Created,
                PostContent=post.Content,
                Title=post.Title,
                PostReplies=post.PostReplies.Select(x=> new PostReplyModel
                {
                    AuthorId=x.User.Id,
                    AuthorImageUrl=x.User.ProfileImageUrl,
                    AuthorName=x.User.UserName,
                    AuthorRating=x.User.Rating,
                    Id=x.Id,
                    Created=x.Created,
                    PostId=x.Post.Id,
                    ReplyContent=x.Content
                })
            };
            return View(model);
        }

        public IActionResult Create(int id)
        {
            //id is Forum Id

            var forum = _serviceForum.GetById(id);

            var model = new NewPostModel
            {
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                ForumName = forum.Title,
                AuthorName = User.Identity.Name,

            };

          return View(model);
        }

       [HttpPost]
       public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

            await _servicePost.Add(post);

            return RedirectToAction(nameof(Index), new { id = post.Id });

        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _serviceForum.GetById(model.ForumId);

            var post = new Post
            {
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Title = model.Title,
                Forum = forum
            };
            return post;
        }
    }
}