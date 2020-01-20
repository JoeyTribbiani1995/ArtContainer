# Source code organization. Architecture of ArtContainer
This document is a guide for developers to the solution structure of art-container. It is a document for a new art-container developer to start learning about the code base. First of all, art-container source code is quite easy to get. The projects and folders are listed in the order they appear in Visual Studio. We recommend that you open the art-container solution in Visual Studio and browse through the projects and files as you read this document.

![image source code](https://github.com/JoeyTribbiani1995/ArtContainer/blob/master/documents/images/soucrecode.PNG)

1. Libraries
- \Libraries\ArtContainer.Core project contains a set of core classes for art-container, such as caching, events, helpers, and business objects (for example, Article entities)
- \Libraries\ArtContainer.Data project contains a set of classes and functions for reading from and writing to a database or other data store. It helps separate data-access logic from your business objects. art-container uses the Entity Framework (EF) Code-First approach. It allows you to define entities in the source code, and then get EF to generate the database from that. That's why it's called Code-First. You can then query your objects using LINQ, which gets translated to SQL behind the scenes and executed against the database. art-container use Fluent API to fully customize the persistence mapping.
- \Libraries\ArtContainer.Services project contains a set of core services, business logic, validations or calculations related with the data, if needed. Some people call it Business Access Layer (BAL).
2. Plugins
3. Presenation
- \Presentation\ArtContainer.API project contains some API to use for front-end
4. Web