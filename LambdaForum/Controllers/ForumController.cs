using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaForum.Data;
using LambdaForum.Data.Models;
using LambdaForum.Models.Forum;
using LambdaForum.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace LambdaForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _serviceForum;
        private readonly IPost _servicePost;

        public ForumController(IForum serviceForum)
        {
            _serviceForum = serviceForum;
        }
        public IActionResult Index()
        {
            var forums = _serviceForum.GetAll().Select(x => new ForumListingModel
            {
                Id = x.Id,
                Name = x.Title,
                Description = x.Description
            });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _serviceForum.GetById(id);

            var posts = forum.Posts;

            var postListings = posts.Select(x => new PostListingModel
            {
                AuthorID = x.User.Id,
                Id=x.Id,
                AuthorRating=x.User.Rating,
                AuthorName=x.User.UserName,
                Title=x.Title,
                DatePosted=x.Created.ToString(),
                RepliesCount=x.PostReplies.Count(),
                Forum=BuildForumListing(x)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum); 
        }

        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Description = forum.Description,
                Name = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }

    }
}