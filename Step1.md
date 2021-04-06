# Step 1 - Setup the project

## Create the Solution

* Create new, empty asp.net core webapplication 
  * project name: QuestionsApp.Web
  * solution name: QuestionsApp
  * target framework .NET 5.0
* Add new XUnit Test project (.Net Core)
  * project name: QuestionsApp.Tests
  * target framework .NET 5.0
* Save all

## Add folders and files for the Web Api

* Add the folders in the QuestionsApp.Web project
  * Api
  * Api/Models
  * Api/Controllers
  * Api/Controllers/Commands
  * Api/Controllers/Queries
* Add the classes
  * QuestionsController.cs in Api/Controllers/Queries
  * QuestionsController.cs in Api/Controllers/Commands
  * Question.cs in Api/Models

## Activate MVC for the Web Api

<details><summary>Activate and configure MVC in Startup.cs</summary>
 
~~~c#

public void ConfigureServices(IServiceCollection services)
{
	// Registration and configuration of the MVC Framework
	services.AddControllers();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
	if (env.IsDevelopment())
    {
		app.UseDeveloperExceptionPage();
	}

	app.UseRouting();

	app.UseEndpoints(endpoints =>
    {
		// Activate MVC Controllers for WebApi
		endpoints.MapControllers();

        endpoints.MapGet("/", async context =>
        {
			await context.Response.WriteAsync("Error");
        });
    });
}
~~~
</details>

## Add API Methods

### Add the ApiController and Route Attributes to the QuestionsController classes

<details><summary>QuestionsController.cs in Api/Controllers/Commands</summary>
 
~~~c#
[ApiController]
[Route("Api/Commands/[controller]/[action]")]
public class QuestionsController : ControllerBase
 ~~~
</details>

<details><summary>QuestionsController.cs in Api/Controllers/Queries</summary>
 
~~~c#
[ApiController]
[Route("Api/Queries/[controller]")]
public class QuestionsController : ControllerBase
~~~
</details>

### Add Methods to the QuestionsController classes

<details><summary>Ask and Vote to QuestionsController.cs in Api/Controllers/Commands</summary>
 
~~~c#
[HttpPut]
public IActionResult Ask([FromQuery]string content)
{
    throw new NotImplementedException();
}

[HttpPut]
public IActionResult Vote([FromQuery]int questionID)
{
    throw new NotImplementedException();
}
~~~
</details>

<details><summary>Method Get to QuestionsController.cs in Api/Controllers/Queries</summary>
 
~~~c#
[HttpGet]
public List<Question> Get()
{
    throw new NotImplementedException();
}
~~~
</details>

### Add properties to Models/Question

<details><summary>int ID, string Content, int Votes</summary>

~~~c#
public int ID { get; set; }
public string Content { get; set; }
public int Votes { get; set; }
~~~
</details>

## Add Unittests for Controllers

* Add the refererence to the QuestionsApp.Web project in the QuestionsApp.Tests project
* Add the NuGet Package FluentAssertions
* Rename the Unittest1.cs file to QuestionsTests.cs
* Remove the Test1 Method in the QuestionsTests.cs
* Add the FluentAssertions using

### Implement tests 

<details><summary>Helper methods to create instances for the controllers</summary>

~~~c#
private Web.Api.Controllers.Queries.QuestionsController NewQuery() => new();

private Web.Api.Controllers.Commands.QuestionsController NewCommand() => new();
~~~
</details>


<details><summary>Empty</summary>

~~~c#
[Fact]
public void Empty()
{
	var query = NewQuery();
	query.Get().Should().BeEmpty();
}
~~~
</details>

<details><summary>OneQuestion</summary>

~~~c#
[Fact]
public void OneQuestion()
{
	var query = NewQuery();
	var command = NewCommand();

	command.Ask("Dummy Question").Should().NotBeNull();

	query.Get().Should().HaveCount(1);
}
~~~
</details>

<details><summary>OneQuestionAndVote</summary>

~~~c#
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
~~~
</details>