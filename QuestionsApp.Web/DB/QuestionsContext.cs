using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsApp.Web.DB
{
    public class QuestionsContext : DbContext
    {
        public QuestionsContext(DbContextOptions options) : base(options)
        { }

        public DbSet<QuestionDB> Questions { get; set; }
        public DbSet<VoteDB> Votes { get; set; }
    }
}
