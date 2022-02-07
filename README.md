# ALTEN CANADA Test Project - Booking API
**Author: Carlos Abraham Pérez Marrero**
### Application architecture overview:
The solution is composed of 4 projects:
- alten-test.DataAccessLayer (.NET Core Class Library Project)
- alten-test.BusinessLayer (.NET Core Class Library Project)
- alten-test.PresentationLayer (.NET Core WebApi Project)
- alten-test.Core

Each project corresponds to one layer of a N-Tier architecture.
- Data Access Layer: Uses the Unit Of Work design pattern. Implements repositories that abstract the logic required to access the underlying data stores. Configures the application database context. Exposes generic interfaces that services in the business layer consume.
- Business Layer: Implements several services and components that encapsulate the relevant business logic of the application. Exposes interfaces that components in the presentation layer consume. Authorizes user actions on certain resources.
- Presentation Layer: Exposes a Rest API allowing clients to access the application. Implements API routes, controllers, authentication, and data validation.
- Core: Enable sharing multiple data models and data transfer objects across multiple layers.

Authentication was handled in the Presentation Layer using the ASP.NET Core Identity Library. Json Web Token (JWT) was used as default authentication schema.
Authorization was performed in the Business Logic Layer as most of the part of the application authorization rules are business related.
Dependency Injection was achieved in some cases using the Unity package.
\
\
The Relational Database Management System (RDBMS) used for this project was MySQL. Entity Framework Core was used to handle application entities and database migrations. See application database SQL dump. Also note the attached file with the stored procedure used to check Room Availability.

An Angular Single Page Application (SPA) was implemented to consume the REST API. Authentication was implemented using JWT Bearer. Each endpoint user can login/register/logout. (Note that there is a separate endpoint to register admin users for demo purposes. Please see the `/api/auth/register-admin` endpoint on Swagger).
Admin functionalities are bundled in an Angular feature module which is protected by the route guards.\
User Interface was developed using the Angular Material Components. Layout was handled using [FlexLayout](https://github.com/angular/flex-layout/wiki) library. SASS was used as for stylesheet. 
\
\
The main functionalities of the application are:
- Every end-point standard user can check hotel's reservation availability, create a new reservation and manage his list of reservations (modify/cancel reservations).
- Admin users can manage the hotel rooms (list/create/modify/delete). Also Admins can manage all the hotel reservations (list/create/modify/cancel). The application uses a role-based authorization system to grant access to the application resources.
- Supports variable number of rooms.  
- Error handling and client-side data validation.


### How to install and run the project
The solution contains two applications:
- alten-test-booking-api (Asp.Net Core Web API)
- alten-test-booking-spa (Demo Angular Single Page Application to test the API functionality)

1- Change working directory to the Asp.Net Core Web API project in the Presentation Layer:\
\
`cd alten-test-booking-api/alten-test.PresentationLayer/`\
\
Build the project (I used the Asp.Net Core Framework, Version 5.0.1, running on Linux (Ubuntu 20.04))\
\
`dotnet build`\
\
Run the project:\
\
`dotnet run`\
\
It should listen at TCP ports 5000 for HTTP, and 5001 for HTTPS. Swagger is enabled so it can be used to discover/test the API.\
\
2- To run the Angular SPA, first move to the SPA directory:\
\
`cd alten-test-booking-spa`\
\
Then install project dependencies:\
\
`npm install`\
\
And finally run the development server with:\
\
`npm start`\
\


