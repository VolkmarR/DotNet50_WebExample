using Microsoft.AspNetCore.Mvc;
using QuestionsApp.Web.Api.Models;
using QuestionsApp.Web.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsApp.Web.Api.Controllers.Queries
{
    [ApiController]
    [Route("Api/Queries/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionsContext _context;
        public QuestionsController(QuestionsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Question> Get()
        {
            return (from q in _context.Questions
                    select new Question { ID = q.ID, Content = q.Content, Votes = q.Votes.Count() }).ToList();
        }
    }
}
