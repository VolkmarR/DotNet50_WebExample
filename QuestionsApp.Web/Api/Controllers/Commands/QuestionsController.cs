using Microsoft.AspNetCore.Mvc;
using QuestionsApp.Web.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsApp.Web.Api.Controllers.Commands
{
    [ApiController]
    [Route("Api/Commands/[controller]/[action]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionsContext _context;
        public QuestionsController(QuestionsContext context)
        {
            _context = context;
        }

        [HttpPut]
        public IActionResult Ask([FromQuery] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest("The Question Content can not be empty");

            _context.Questions.Add(new QuestionDB { Content = content });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Vote([FromQuery] int questionID)
        {
            if (!_context.Questions.Any(q => q.ID == questionID))
                return BadRequest("Invalid Question ID");

            _context.Votes.Add(new VoteDB { QuestionID = questionID });
            _context.SaveChanges();
            return Ok();
        }
    }
}
