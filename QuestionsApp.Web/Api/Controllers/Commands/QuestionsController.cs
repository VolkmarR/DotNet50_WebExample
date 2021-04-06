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
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Vote([FromQuery] int questionID)
        {
            throw new NotImplementedException();
        }
    }
}
