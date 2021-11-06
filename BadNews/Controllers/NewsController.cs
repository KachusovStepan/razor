using System;
using BadNews.ModelBuilders.News;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsModelBuilder newsModelBuilder;

        public NewsController(INewsModelBuilder newsModelBuilder)
        {
            this.newsModelBuilder = newsModelBuilder;
        }
        
        // GET
        public IActionResult Index(int? year, int pageIndex = 0)
        {
            var model = newsModelBuilder.BuildIndexModel(pageIndex, true, year);
            
            return View(null, model);
        }
        // 2.2

        public IActionResult FullArticle(Guid id)
        {
            var model = newsModelBuilder.BuildFullArticleModel(id);
            if (model is null)
            {
                return NotFound();
            }
            
            return View(null, model);
        }
    }
}