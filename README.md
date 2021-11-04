# securrency
Transaction Discovery Service (TDS)




Microservices
-----------------------------------------------
I was able to come up with 2 Micro services;

1. Identity Service which handles the authentication of users of the API
2.  WalletManager.API -> This API handles uploading of wallet, tranasction discovery and retrieving of transactions.

For both documentation and testing of the API, I have configured Swagger documentation. You can also make use of Postman.

Two users were created (admin1 and admin2 with password TDSAdmin12345) for test purposes.
Authorization: Bearer {token}



Find below architecture, design patterns and tools used in the development;
 
1. Clean Architecture
2. Reposiotry Pattern
3. CQRS - MediatR
4. JWT - Authorization
5. Swagger - Documentation
6. NLog - Logging
7. MSSSQL Server Database
8. xUnit for Unit Testing
9. Moq for Mocking unit test data




Application Layer
----------------------------------------
1. Backrgound service(TransactionDiscoveryHostedService) was developed using dotnet core hosted sesrvice. To avoid multiple execution when there is an ongoing execution, this was combined with a time 
2. Application logic using CQRS(Commands for the upload of wallet and query for the fetching of data). CQRS was used to ensure Single Responsibilty principle where one class is meant to have one responsibilty.

I tested with the following WalletAddress, as the test wallet were not working.
"GC2BKLYOOYPDEFJKLKY6FNNRQMGFLVHJKQRGNSSRRGSMPGF32LHCQVGF"

Domain Layer
--------------------------------------------
This is where all the 
1. Global Error Handling
2. Logging
3. JWT Setup
4. Extensions





Infrastructure Layer;
-----------------------------------------------
This handles all interactions to third parties.
1. Stellar
For understanding of the API, I went through the following documents
https://github.com/stellar/go/blob/master/services/horizon/internal/docs/reference/endpoints/payments-for-account.md


Running Migrations on the database
--------------------------------------
You may want to change the DBConnection string.

dotnet ef migrations add Initial 
dotnet ef database update




Running Test
--------------------------------------
dotnet test 





