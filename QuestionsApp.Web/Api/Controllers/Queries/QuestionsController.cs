using Microsoft.AspNetCore.Mvc;
using QuestionsApp.Web.Api.Models;
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
        [HttpGet]
        public List<Question> Get()
        {
            throw new NotImplementedException();
        }
    }
}
