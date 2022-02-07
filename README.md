# ALTEN CANADA Test Project - Booking API
**Author: Carlos Abraham Pérez Marrero**
### Application architecture overview:
The solution is composed of 4 projects:
- alten-test.DataAccessLayer (.NET Core Class Library Project)
- alten-test.BusinessLayer (.NET Core Class Library Project)
- alten-test.PresentationLayer (.NET Core WebApi Project)
- alten-test.Core

Each project corresponds to one layer of a N-Tier architecture:
- Data Access Layer: Uses the Unit Of Work design pattern. Implements repositories that abstract the logic required to access the underlying data stores. Configures the application database context. Exposes generic interfaces that services in the business layer consume.
- Business Layer: Implements several services and components that encapsulate the relevant business logic of the application. Exposes interfaces that components in the presentation layer consume. Authorizes user actions on certain resources.
- Presentation Layer: Exposes a Rest API allowing clients to access the application. Implements API routes, controllers, authentication, and data validation.
- Core: Enable sharing multiple data models and data transfer objects across multiple layers.

This architecture separates the application into logical layers and physical tiers. Layers are a way to separate responsibilities and manage dependencies. This represents an advantage to the hotel's IT Department at the time of maintaining and scaling up the platform. Access restrictions to the Data Access Layer by allowing requests only from the Business Layer strengths the application security.   

Authentication was handled in the Presentation Layer using the ASP.NET Core Identity Library with Json Web Token (JWT) as authentication schema.
Authorization was performed in the Business Logic Layer as most of the authorization rules are business related.
Dependency Injection was achieved in some cases with the help of a Unity container.
\
\
The Relational Database Management System (RDBMS) used for this project was MySQL Server 8.0.28. Entity Framework Core Version 5.0.5 was used to handle application entities and migrations. Please find the application database SQL dump attached in the `alten-test-bookin-api` directory. The SQL clauses used to create an stored procedure to check hotel room availability is also present in that folder.

An Angular Single Page Application (SPA) was implemented to consume the REST API. Authentication was implemented using the JWT schema. Some endpoint of the REST API are restricted only for authenticated users. Please note that there is a provisional endpoint to register admin users in the application. This can be used for testing. Check out the `/api/auth/register-admin` endpoint on Swagger.
\
\
In the SPA, admin components are bundled in an Angular feature module which is protected by the route guards. UI was developed using the Angular Material Components and page layout was created using the [FlexLayout Library](https://github.com/angular/flex-layout/wiki). SASS was used as for stylesheet. 
\
\
The main functionalities of the system developed are:
- Every end-point standard user can check hotel's reservation availability, create a new reservation and manage his list of reservations (modify/cancel reservations).
- Admin users can manage the hotel rooms (list/create/modify/delete). Also Admins can manage all the hotel reservations (list/create/modify/cancel). The application uses a role-based authorization system to grant access to the application resources.
- Supports unlimited number of rooms. Validation is performed to ensure that every room isn't booked for more than 3 days of duration, and can't be reserved with more than 30 days in advance. The reservations starts at least the next day of bookin.   
- Error handling and client-side data validation.


### How to install and run the project
The solution contains two applications:
- alten-test-booking-api (Asp.Net Core Solution)
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
CORS is configured in the Web API to allow cross-origin request from http://localhost:4200 which is the Angular Development Server default url.


