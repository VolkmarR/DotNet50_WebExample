using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuestionsApp.Web.DB;
using QuestionsApp.Web.Hubs;
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
        private readonly IHubContext<QuestionsHub> _hub;
        public QuestionsController(QuestionsContext context, IHubContext<QuestionsHub> hub)
        {
            _hub = hub;
            _context = context;
        }

        private void RefreshClients()
        {
            _hub?.Clients.All.SendAsync("refresh").Wait();
        }

        [HttpPut]
        public IActionResult Ask([FromQuery] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest("The Question Content can not be empty");

            _context.Questions.Add(new QuestionDB { Content = content });
            _context.SaveChanges();

            RefreshClients();

            return Ok();
        }

        [HttpPut]
        public IActionResult Clear()
        {
            _context.Database.EnsureDeleted();
            RefreshClients();
            return Ok();
        }

        [HttpPut]
        public IActionResult Vote([FromQuery] int questionID)
        {
            if (!_context.Questions.Any(q => q.ID == questionID))
                return BadRequest("Invalid Question ID");
           
            _context.Votes.Add(new VoteDB { QuestionID = questionID });
            _context.SaveChanges();

            RefreshClients();

            return Ok();
        }
    }
}
