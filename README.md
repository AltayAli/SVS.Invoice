
# SVS.Invoice API

This is .Net Core web API that serves for some kinds of invoices. The project consists of 1 Web API and 3 libraries serving it. Here are the details:  

## SVS.Invoice.API

Actually, when you run the application, you just see the swagger UI. But there are action details.

#### Get all invoices header

```http
  GET /api/InvoiceHeaders
```
Get all invoices headers

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `offset` | `int` | Default value is 0. |
| `limit` | `int` |  Default value is 10. |

#### Get details of invoice headers by given id.

```http
  GET /api/InvoiceHeaders/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of invoice header to fetch |

#### Upload invoice record as json

```http
  POST /api/InvoiceRecords
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `file`      | `string($binary)` | **Required**. Permitted format is json. Other formats will be rejected. |

Here is an example json file.

```bash
  {
    "InvoiceHeader": {
      "InvoiceId": "SVS202300000001",
      "SenderTitle": "Gönderici Firma",
      "ReceiverTitle": "Alıcı Firma",
      "Date": "2023-01-05"
    },
    "InvoiceLine": [
        {
          "Id": 1,
          "Name": "1.Ürün",
          "Quantity": 5,
          "UnitCode": "Adet",
          "UnitPrice": 10
        },
        {
          "Id": 2,
          "Name": "2.Ürün",
          "Quantity": 2,
          "UnitCode": "Litre",
          "UnitPrice": 3
        },
        {
          "Id": 3,
          "Name": "3.Ürün",
          "Quantity": 25,
          "UnitCode": "Kilogram",
          "UnitPrice": 2
        }
    ]
}
```


After you load the file, the system saves it to the database, then the background job will read data and write them to the appropriate tables. Background job runs automatically by every 30 minutes. After the process, detailed information about the process will be sent to the email specified in the EmailSettings>SendMail property in appsettings.json or appsettings.Development.json. Because of that please check the configurations.



## Configuration

You have to config some properties. Open appsettings.json or appsettings.Developer.json file.
- Update ConnectionStrings > DataConnection. Please select the connection string for MS SQL database.
- Update EmailSettings.

Finally you can run the application. When you run the application, database will automatically generate.
 
# SYS.Invoice.BLL

There is some business logic. Actually, I used a repository pattern in the library. You can also find some helper services and models to serve these services.
# SYS.Invoice.Models

You can find a database connection, migrations, some entities, data transfer objects, and AutoMapper profiles.
I used EF Core for a database connection. 
# SVS.Invoice.UnitTests
You can find unit tests for repositories. I used NUnit for tests.
## 🛠 Tecnologies
.Net Core (net6.0), Entity Framework Core, NUnit, AutoMapper, Hangfire, Log4Net

