# securrency
Transaction Discovery Service (TDS)



I was able to come up with 2 Mincro services;

1. Identity Service which handles the authentication of users of the API
2. WalletManager.API -> This API handles uploading of wallet, tranasction discovery and retrieving of transactions.

For both documentation and testing of the API, I have configured SWagger documentation. You can also make use of Postman.


Clean Architecture
Reposiotry Pattern
CQRS - MediatR
JWT - Authorization
Swagger - Documentation
NLog - Logging





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





