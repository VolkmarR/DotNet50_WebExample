# Step 2 - Web Api implementation

## Add Nuget packages for database access

* Add the following NuGet package to both projects
  * Microsoft.EntityFrameworkCore.InMemory

## Implement the database model

### Add folders and classes for the database model

* Add new folder DB to the QuestionsApp.Web project
* Add new classes for the database model
  * QuestionsContext.cs
  * QuestionDB.cs
  * VoteDB.cs
  
### Add properties for the database model

<details><summary>Add properties to QuestionDB</summary>
 
~~~c#
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int ID { get; set; }
public string Content { get; set; }
public ICollection<VoteDB> Votes { get; set; }
~~~
</details>

<details><summary>Add properties to VoteDB</summary>

~~~c#
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int ID { get; set; }
public int QuestionID { get; set; }
public QuestionDB Question { get; set; }
~~~
</details>

### Implement the QuestionsContext

<details><summary>Add DbContext as base class and add constructor with DbContextOptions parameter for dependency</summary>

~~~c#
public class QuestionsContext : DbContext
{
    public QuestionsContext(DbContextOptions options) : base(options)
    { }
}
~~~
</details>

<details><summary>Add DbSet for Questions and Votes to QuestionsContext</summary>

~~~c#
public DbSet<QuestionDB> Questions { get; set; }
public DbSet<VoteDB> Votes { get; set; }
~~~
</details>

### Configure EntityFramework in startup to use InMemoryDatabase

<details><summary>ConfigureServices</summary>

~~~c#
// Configuration for Entity Framework
services.AddDbContext<QuestionsContext>(options => options.UseInMemoryDatabase("Dummy"));
~~~
</details>

## Add the QuestionsContext as dependency to the controllers

<details><summary>Add a constructor to both controllers for dependency injection</summary>

~~~c#
private readonly QuestionsContext _context;
public QuestionsController(QuestionsContext context)
{
    _context = context;
}
~~~
</details>

### Fix the errors in the UnitTests

<details><summary>Add a QuestionContext property to the QuestionsTests class and initialize it in the constructor</summary>

~~~c#
private readonly QuestionsContext _context;

public QuestionsTests()
{
	var options = new DbContextOptionsBuilder<QuestionsContext>().
						UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
	_context = new QuestionsContext(options);
}
~~~
</details>

<details><summary>Change the NewQuery and NewCommand helper methods to use the QuestionsContext</summary>

~~~c#
private Web.Api.Controllers.Queries.QuestionsController NewQuery() => new(_context)

private Web.Api.Controllers.Commands.QuestionsController NewCommand() => new(_context)
~~~
</details>

## Implement Queries and Commands

### QuestionsController.cs in Api/Controllers/Queries

<details><summary>Implement the Get method</summary>

~~~c#
return (from q in _context.Questions
        select new Question { ID = q.ID, Content = q.Content, Votes = q.Votes.Count() }).ToList();
~~~
</details>

### QuestionsController.cs in Api/Controllers/Commands

<details><summary>Implement the Ask method</summary>

~~~c#
if (string.IsNullOrWhiteSpace(content))
    return BadRequest("The Question Content can not be empty");

_context.Questions.Add(new QuestionDB { Content = content });
_context.SaveChanges();
return Ok();
~~~
</details>

<details><summary>Implement the Vote method</summary>

~~~c#
if (!_context.Questions.Any(q => q.ID == questionID))
    return BadRequest("Invalid Question ID");

_context.Votes.Add(new VoteDB { QuestionID = questionID });
_context.SaveChanges();
return Ok();
~~~
</details>

### Run the UnitTests to check, if the Controller are working as expected
