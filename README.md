# Project Title

Synthentic Financial Manager System For Fraud Transactions
v 1.1 Apr 2018

## Getting Started

The solution contains all de build pipeline necessary to deploy the MVC App, Web API and Database. Follow the instructions in the installin section in order to run the Solution in your dev machine

### Hosted Web App

The app is hosted in the following URL: https://zemogahernan.azurewebsites.net/

The Web App uses the following Azure Services:

* Azure Web App
* Azure SQL Database

### Users

The solution has not yet integrated a way to register new users. Out of the box, the solution will populate the users table with its necessary roles to interact with the solution.

The following credentials ara available to login with role based authentication:

#### Admin User
```
User: administrtor@zemoga.com
Password: AdminPassword3;
```

#### Manager User
```
User: manager@zemoga.com
Password: ManagerPassword2;
```

#### Asistant User
```
User: assistant@zemoga.com
Password: AssistantPassword1;
```

### MVC APP

When you first run the MVC App, a seed method will start populating the customers database for you to start registering new transactions.

#### Constraints

* Only Assistant and Administrator roles can create new transactions.
** In order to save a transaction, the name of a customer must exists, otherwise the operation will fail.
* Only Assistant and Administraror roles can create new customers.
* Only Manager and Administrator roles can modify and update an existig transaction in order to mark them as Fraud or Flagged Fraud
* Only Assistant and Administrator roles can Search for existing transactions 


### Web API

The API is protected by a JWT Token authentication schema, in order to access this API you will first need to Authorize your self.

In order to authenticate, you will need to send an HTTP Request via Postman or any HTTP Request method that you like

#### Authentication
```
For a request to the Hosted Web App:
https://zemogahernan.azurewebsites.net/api/auth/token

For a request in your local machine
https://localhost:{port}/api/auth/token

Method: POST

Body:
{
	"userName": "{User Email}",
	"password": "{User Password}"
}

```

The if the user name and password are correct, the service will respond with a body containing the JWT Token and its expiration date. The token is valid for 30 minutes until you have to renew it.

```
Response example:

{
    "token": "{JWT Token here}",
    "expiration": "2018-04-06T15:53:50Z"
}
```

Save this JWT token in your clipboard, you will need it to authorize youself to use the API

#### Get all transactions

To get all the transactions make a POST request with the following information

```
For a request to the Hosted Web App:
https://zemogahernan.azurewebsites.net/api/transactions

For a request in your local machine:
https://localhost:44370/api/transactions

Method: GET

Headers:

Authorization - Bearer {JWT Token}

```

Response example:
```
[
    {
        "id": 1006,
        "originCustomer": {
            "id": 1,
            "name": "C1231006815",
            "balance": 6000
        },
        "destinationCustomer": {
            "id": 2,
            "name": "C1666544295",
            "balance": 14000
        },
        "oldBalanceOrigin": 10000,
        "newBalanceOrigin": 8000,
        "oldBalanceDestination": 10000,
        "newBalanceDestination": 12000,
        "amount": 2000,
        "transactionType": 0,
        "isFraud": false,
        "isFlaggedFraud": false,
        "date": "2018-01-01T00:00:00"
    },
    {
        "id": 1007,
        "originCustomer": {
            "id": 1,
            "name": "C1231006815",
            "balance": 6000
        },
        "destinationCustomer": {
            "id": 2,
            "name": "C1666544295",
            "balance": 14000
        },
        "oldBalanceOrigin": 10000,
        "newBalanceOrigin": 7000,
        "oldBalanceDestination": 10000,
        "newBalanceDestination": 13000,
        "amount": 3000,
        "transactionType": 0,
        "isFraud": true,
        "isFlaggedFraud": true,
        "date": "2018-02-05T00:00:00"
    }
]
```

#### Register Transaction

```
For a request to the Hosted Web App:
https://zemogahernan.azurewebsites.net/api/transactions

For a request in your local machine:
https://localhost:44370/api/transactions

Method: POST

Headers:

Authorization - Bearer {JWT Token}

Body:

{
	"originCustomer": "{customerName:string}",
	"destinationCustomer": "customerName:string",
	"amount": {amount:int},
	"date": "{date:string}",
	"type": {type:int}
}

for example:

{
	"originCustomer": "C1231006815",
	"destinationCustomer": "C1666544295",
	"amount": "500",
	"date": "12 Jan 2018",
	"type": 3
}

```

Sending the right information will save the trnasaction in the database and the wervice will respond with the created transaction:

```
{
    "id": 3008,
    "originCustomer": {
        "id": 1,
        "name": "C1231006815",
        "balance": 5500
    },
    "destinationCustomer": {
        "id": 2,
        "name": "C1666544295",
        "balance": 14500
    },
    "oldBalanceOrigin": 6000,
    "newBalanceOrigin": 5500,
    "oldBalanceDestination": 14000,
    "newBalanceDestination": 14500,
    "amount": 500,
    "transactionType": 3,
    "isFraud": false,
    "isFlaggedFraud": false,
    "date": "2018-01-12T00:00:00"
}
```

If the service fail to register the transaction, it will inform you with a status code and message.

#### Search Transaction

```
For a request to the Hosted Web App:
https://zemogahernan.azurewebsites.net/api/transactions/search

For a request in your local machine:
https://localhost:44370/api/transactions/search

Method: POST

Headers:

Authorization - Bearer {JWT Token}

Body:

{
	"searchTerm": "{searchTerm:string}"
}

for example:

{
	"searchTerm": "C1666"
}

```

Sending the request in the right way will respond you with the criteria listed in the search term you pass. Example response:
```
[
    {
        "id": 1006,
        "originCustomer": {
            "id": 1,
            "name": "C1231006815",
            "balance": 6000
        },
        "destinationCustomer": {
            "id": 2,
            "name": "C1666544295",
            "balance": 14000
        },
        "oldBalanceOrigin": 10000,
        "newBalanceOrigin": 8000,
        "oldBalanceDestination": 10000,
        "newBalanceDestination": 12000,
        "amount": 2000,
        "transactionType": 0,
        "isFraud": false,
        "isFlaggedFraud": false,
        "date": "2018-01-01T00:00:00"
    },
    {
        "id": 1007,
        "originCustomer": {
            "id": 1,
            "name": "C1231006815",
            "balance": 6000
        },
        "destinationCustomer": {
            "id": 2,
            "name": "C1666544295",
            "balance": 14000
        },
        "oldBalanceOrigin": 10000,
        "newBalanceOrigin": 7000,
        "oldBalanceDestination": 10000,
        "newBalanceDestination": 13000,
        "amount": 3000,
        "transactionType": 0,
        "isFraud": true,
        "isFlaggedFraud": true,
        "date": "2018-02-05T00:00:00"
    },
    {
        "id": 1008,
        "originCustomer": {
            "id": 1,
            "name": "C1231006815",
            "balance": 6000
        },
        "destinationCustomer": {
            "id": 2,
            "name": "C1666544295",
            "balance": 14000
        },
        "oldBalanceOrigin": 7000,
        "newBalanceOrigin": 6000,
        "oldBalanceDestination": 13000,
        "newBalanceDestination": 14000,
        "amount": 1000,
        "transactionType": 0,
        "isFraud": false,
        "isFlaggedFraud": false,
        "date": "2018-01-01T00:00:00"
    },
    {
        "id": 2008,
        "originCustomer": {
            "id": 1,
            "name": "C1231006815",
            "balance": 6000
        },
        "destinationCustomer": {
            "id": 2,
            "name": "C1666544295",
            "balance": 14000
        },
        "oldBalanceOrigin": 6000,
        "newBalanceOrigin": 5000,
        "oldBalanceDestination": 14000,
        "newBalanceDestination": 15000,
        "amount": 1000,
        "transactionType": 0,
        "isFraud": false,
        "isFlaggedFraud": false,
        "date": "2018-01-01T00:00:00"
    }
]
```



### Prerequisites

Install the following software in order to run the proyect

```
* [Visual Studio 2017](https://www.visualstudio.com/vs/whatsnew/)
* [.NET Core](https://www.microsoft.com/net/learn/get-started/windows)
* [NPM](https://www.npmjs.com/)
```

### Installing

The following instructions must be follow in order to deploy the Web App, App Service and Database in you dev machine.

```
Clone this repository in your local machine
```


```
Wait for dependencies to be installed, if the NPM dependencies did not load, open the command line in the root of the project and run "npm install".
```


```
Open, appsettings.Development.json and change the connection string to your desired SQL Server Database Connection. You can use the default one if you want. 
```

```
In the root of the project Run 'dotnet ef database update' to apply migrations in the database.
```

```
Run the project.
```


## Deployment to Azure

To deploy the Web App, Web Service and Database to Azure, create a new JSON configuration file in the root of the folder called appsettings.Production.json
You can copy the same configuration existing in the appsetting.json file.

Next, change the connection string to your selected Azure SQL Database string.

You can use a Continuos Delivery Pipeline in order to deploy the Solution to Azure with VSTS or use the out of the box Visual Studio Wizard to deploy to your selected Azure Web App, you can follow the instructions listed in the following site:
*[Publishing to Azure](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-webapp-using-vs)



## Built With

* [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/getting-started) - The web framework used

## Authors

* **Hernán Cote** - *Initial work* - [HernanCote](https://github.com/HernanCote)


## Acknowledgments

* Zemoga for this awesome project
