using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaForum.Data;
using LambdaForum.Models.Post;
using LambdaForum.Models.Reply;
using Microsoft.AspNetCore.Mvc;

namespace LambdaForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _servicePost;
        public PostController(IPost servicePost)
        {
            _servicePost = servicePost;
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
    }
}