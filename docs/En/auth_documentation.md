# authentication

### user Registration

|parameter | is required |description|
|--------------------|--------------|----------|
| Email | *required | |
| Username | |If username information is not sent, a username is automatically created.|
| Password | *required |At least 6 characters. At least one numeric and at least one special character
| Connection String | *required | |
| Key Identifier | *required | MONGO, SQLITE, MYSQL, MSSQL, PSGSQL |

- **Sample Request**

- Endpoint
```[POST] ~/api/auth/register ```

-Body:
```json
{
     "email": "test@mail.com",
     "password": "Test1234!",
     "db":{
         "connectionString": "mongodb+srv://Victory:7Aynras2-@gameserver.ptrocqn.mongodb.net/?retryWrites=true&w=majority",
         "keyIdentifier": "MONGO"
     }
}
```
- **Response**

Success: ```201 Created```

Mistake:
```json
{
     "success": false,
     "message": "Email field is required."
}
```

### User login

|parameter | is required |description|
|--------------------|--------------|----------|
| Email/Username | *required | You can log in with Email or Username. |
| Password | *required ||


- **Sample Request**

- Endpoint
```[POST] ~/api/auth/login ```

-Body:
```json
{
     "email": "test@mail.com",
     "password": "Test1234!",
}
```

- **Response**

Success: ``` 200 OK ```
```json
{
   {
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJyYW1h bHRAZ21haWwuY29tIiwiZGJzIjoibnVsbCIsImV4cCI6MTcwMTYzMDgzMSwiaXNzIjoiTmVidWxhUGx1Z2luQVBJIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTM1In0.b8yMKMmyK5foJOAKynDeIoA 4ybeCtD-5EsqzW98DqxY",
     "refreshToken": "OCA3TySU6/jmqf14lmHNFSFDwA1Vpqomyuo/6UEc5jk="
}
}

```

Mistake:
```json
{
     "success": false,
     "message": "Email/Username or password is incorrect"
}
```

### Token Renewal

|parameter | is required |description|
|--------------------|--------------|----------|
| JWT Token | *required ||
| Refresh Token | *required ||


- **Sample Request**

- Endpoint
```[POST] ~/api/auth/refresh ```

-Body:

```json
{
   {
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJyYW1h bHRAZ21haWwuY29tIiwiZGJzIjoibnVsbCIsImV4cCI6MTcwMTYzMDgzMSwiaXNzIjoiTmVidWxhUGx1Z2luQVBJIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTM1In0.b8yMKMmyK5foJOAKynDeIoA 4ybeCtD-5EsqzW98DqxY",
     "refreshToken": "OCA3TySU6/jmqf14lmHNFSFDwA1Vpqomyuo/6UEc5jk="
}
}
```

- **Response**

Success: ``` 200 OK ```
```json
{
   {
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJyYW1h bHRAZ21haWwuY29tIiwiZGJzIjoibnVsbCIsImV4cCI6MTcwMTYzMDgzMSwiaXNzIjoiTmVidWxhUGx1Z2luQVBJIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTM1In0.b8yMKMmyK5foJOAKynDeIoA 4ybeCtD-5EsqzW98DqxY",
     "refreshToken": "OCA3TySU6/jmqf14lmHNFSFDwA1Vpqomyuo/6UEc5jk="
}
}

```

Mistake:
```json
{
     "success": false,
     "message": "Invalid token"
}
```