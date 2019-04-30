using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaForum.Data;
using LambdaForum.Models.Forum;
using Microsoft.AspNetCore.Mvc;

namespace LambdaForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _serviceForum;

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

        //public IActionResult Topic(int id)
        //{
        //    var forum = _serviceForum.GetById(id);
        //}
    }
}