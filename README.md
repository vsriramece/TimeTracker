Technical overview:
1. The Asp.Net WebAPI2 follows Domain Driven Design in conjunction with CQRS (Command Query Responsibility Segregation).
2. Microsoft Unity is used as the dependency injection framework (Inversion of Control) so that the each layer is loosely coupled on the dependencies and also easily unit testable.
3. Entity framework is used as the ORM. In this prototype, code first approach is followed and Azure SQL database is used as the data store.
4. Async/Await is used wherever applicable for the overall performance efficiency of the web application.
5. Followed Asp.Net MVC architecture in the frontend application.
6. Unit test cases sample using Xunit and Fluent Assertions. 
7. Azure Application Insights is configured. The solution is deployable as Azure WebApp (ApiApp).

