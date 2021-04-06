# Step 3 - Add web site

## Server Configration

* Add a new folder called wwwroot

### Activate Static Files in the startup

<details><summary>Configure (before app.UseRouting...)</summary>

~~~c#
// activate static files serving (before UseRouting)
app.UseDefaultFiles();
app.UseStaticFiles();
~~~
</details>

## Website

* Add an index.html file to wwwroot

<details><summary>Add Twitter Bootstrap, Vue.JS and axios scripts imports</summary>

~~~Html
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" integrity="sha384-B0vP5xmATw1+K9KRQjQERJvTumQW0nPEzvF6L/Z6nronJ3oUOFUFpCjEUQouq2+l" crossorigin="anonymous">    
    <script src="https://cdn.jsdelivr.net/npm/vue@3.0.11/dist/vue.global.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/axios@0.21.1/dist/axios.min.js" integrity="sha256-JLmknTdUZeZZ267LP9qB+/DT7tvxOOKctSKeUC2KT6E=" crossorigin="anonymous"></script>
    <title>Ask your questions</title>
</head>
<body>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-Piv4xVNRyMGpqkS2by6br4gNJ7DXjqk09RmUpJ8jgGtD7zP9yug3goQfGII0yAns" crossorigin="anonymous"></script><
</body>
~~~
</details>

<details><summary>Create the layout</summary>

~~~Html
<nav class="navbar navbar-dark bg-dark">
    <a class="navbar-brand" href="#">Questions</a>
    <button class="btn btn-light my-2 my-sm-0" data-toggle="collapse" data-target="#questionBox">Ask</button>
</nav>

<div id="questionView">
    <div class="collapse" id="questionBox">
        <div class="container mt-4">
            <div class="card card-body">
                <div class="row">
                    <div class="col-11">
                        <input type="text" class="form-control" v-model="newQuestion" placeholder="Question">
                    </div>
                    <div class="col-1">
                        <button class="btn btn-primary btn-block" v-on:click="add"
                                data-toggle="collapse" data-target="#questionBox">Send</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container mt-4">
        <table class="table">
            <thead>
                <tr>
                    <th scope="col" class="col-8">Question</th>
                    <th scope="col" class="col-3">Votes</th>
                    <th scope="col" class="col-1"></th>
                </tr>
            </thead>
            <tbody>
                <!-- display a tablerow for every item in the questionList -->
                <tr v-for="item in questionList">
                    <td>{{ item.content }}</td>
                    <td>{{ item.votes }}</td>
                    <td>
                      <button class="btn" style="font-size: xx-small" v-on:click="vote(item)">&#x25B2;</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
~~~
</details>
  
<details><summary>Add the Vue app to make the site dynamic</summary>

~~~Html
<script>
    // vue app for the page
    var app = Vue.createApp({
        data: () => ({
            // the list of questions, displayed in the table
            questionList: [],
            // the value of the "add new question" edit
            newQuestion: '',
        }),
        methods: {
            // adds a new question to the list
            add: function (event) {
                // if the question is empty, call stopPropagation to stop twitter bootstrap from colapsing the card
                if (this.newQuestion == '') {
                    event.stopPropagation();
                    return;
                }
                // call the api to add a new question
                axios
                    .put('api/Commands/Questions/ask?' + $.param({ content: this.newQuestion }))
                    .then(() => {
                        this.getQuestions();
                        this.newQuestion = '';
                    })
                    .catch(function (error) { alert(error.response.data); });
            },
            vote: function (question) {
                // call the api to increment the votes of the question
                axios
                    .put('api/Commands/Questions/vote?' + $.param({ questionId: question.id }))
                    .then(() => this.getQuestions())
                    .catch(function (error) { alert(error.response.data); });
            },
            getQuestions: function () {
                axios
                    .get('api/Queries/Questions')
                    .then(response => (this.questionList = response.data))
            }
        },
        mounted: function () {
            this.getQuestions();
        }
    });
    app.mount("#questionView");
</script>
~~~
</details>