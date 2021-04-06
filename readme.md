# DotNetCore31_WebExample

This is an example project that shows step by step, how to build a simple web application with asp.net core 3.1. 

## Result

The example is a simplified version of www.sli.do. That is a website for audiences to ask questions to the speaker.

It shows a list of questions. The users can add new questions to the list and vote for questions. 

## Used server side frameworks

* .Net 5
* Asp.net core 5
* XUnit 2.4.1
* FluentAssertions 5.10.3
* Swashbuckle 6.1.1
* Entity framework core 5
* Asp.net SignalR core 5

## Used client side frameworks
* Twitter Bootstrap 4.6.0
* Vue.js 3.0.11
* Axios 0.21.1
 
## Steps

To recreate this project, these four steps should be followed. For a detailed description of each step, click on the corresponding header. To see the state of the project after each step, you can switch to the corresponding branch in this repository.

### [Step 1 - Setup the project](Step1.md)

Create new projects for the WebApi and the Unit Tests.

### [Step 2 - Web Api implementation](Step2.md)

Add the implementation for the database access, commands, queries, unit tests and add the Swagger UI.

### [Step 3 - Add website](Step3.md)

Add the implementation of the website.

### [Step 4 - Client Refresh](Step4.md)

Add the implementation for the server-to-client communication for the page refresh.
