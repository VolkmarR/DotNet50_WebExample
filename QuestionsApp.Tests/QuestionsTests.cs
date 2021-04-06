using FluentAssertions;
using System;
using Xunit;

namespace QuestionsApp.Tests
{
    public class QuestionsTests
    {
        private Web.Api.Controllers.Queries.QuestionsController NewQuery() => new();

        private Web.Api.Controllers.Commands.QuestionsController NewCommand() => new();

        [Fact]
        public void Empty()
        {
            var query = NewQuery();
            query.Get().Should().BeEmpty();
        }

        [Fact]
        public void OneQuestion()
        {
            var query = NewQuery();
            var command = NewCommand();

            command.Ask("Dummy Question").Should().NotBeNull();

            query.Get().Should().HaveCount(1);
        }

        [Fact]
        public void OneQuestionAndVote()
        {
            var query = NewQuery();
            var command = NewCommand();

            command.Ask("Dummy Question").Should().NotBeNull();

            var result = query.Get();
            result.Should().HaveCount(1);
            result[0].Votes.Should().Be(0);

            command.Vote(result[0].ID).Should().NotBeNull();
            result = query.Get();
            result.Should().HaveCount(1);
            result[0].Votes.Should().Be(1);
        }
    }
}
