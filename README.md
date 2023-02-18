# TodoApp
CommIT Smart

# TodoApp
CommIT Smart

 1. Implemented a Todo app using .NET6(Core), C#, SQL Server, Entity Framework Core 6, FluentValidation, MediatoR, NUnit, Swagger, etc.
 2. "async" and "await" has been used to execute non-blocking I/O operations while waiting for the results, to create and manage asynchronous tasks, which can help improve the performance and handle a large number of concurrent requests.
 3. Implemented data encryption and decryption functionalities for the sensitive field.
 4. Used ORM(Entity Framework) and SQL Server. We easily replaced it with MongoDB as we are following clean architecture.
 5. I have used Entity framework In-Memory Database, so you don't need to set up ms sql server. Just run the application and it stores all the into memory while the app is running. If you want to connect to a real SQL server it is very easy to configure.
 6. I have written some test cases. Can be improved.
 7. I have followed the Clean Architecture, CQRS and Mediator Design Patterns. Clean architecture is designed to be scalable, allowing for easy integration of new features and modifications. CQRS separates a system's read and writes operations, allowing developers to optimize each operation separately and improve the performance. But in this app, I have used one database but we can use different ones.

