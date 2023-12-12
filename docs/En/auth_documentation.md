# kimlik Doğrulama

### kullanıcı Kaydı

|parameter           | is required  |description|
|--------------------|--------------|-----------|
| Email              | *required    |           |
| Username           |              |Username bilgisi gönderilmezse otomatik username oluşturulur.|
| Password           | *required    |En az 6 karakter. En az bir numerik ve en az bir tane özel karakter|
| Connection String  | *required    |           |
| Key Identifier     | *required    | MONGO, SQLITE, MYSQL, MSSQL, PSGSQL           |

- **Örnek Request**

- Endpoint
```[POST]  ~/api/auth/register ```

- Body:
```json
{
    "email": "test@mail.com",
    "password": "Test1234!",
    "db": {
        "connectionString": "mongodb+srv://Victory:7Aynras2-@gameserver.ptrocqn.mongodb.net/?retryWrites=true&w=majority",
        "keyIdentifier": "MONGO"
    }
}
```
- **Response**

Başarılı: ```201 Created```

Hata: 
```json
{
    "success": false,
    "message": "Email field is required."
}
```

### Kullanıcı Girişi

|parameter           | is required  |description|
|--------------------|--------------|-----------|
| Email/Username     | *required    | Email veya Username ile giriş yapılabilir.          |
| Password           | *required    ||


- **Örnek Request**

- Endpoint
```[POST]  ~/api/auth/login ```

- Body:
```json
{
    "email": "test@mail.com",
    "password": "Test1234!",
}
```

- **Response**

Başarılı: ``` 200 OK ```
```json 
{
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJyYW1hbHRAZ21haWwuY29tIiwiZGJzIjoibnVsbCIsImV4cCI6MTcwMTYzMDgzMSwiaXNzIjoiTmVidWxhUGx1Z2luQVBJIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTM1In0.b8yMKMmyK5foJOAKynDeIoA4ybeCtD-5EsqzW98DqxY",
    "refreshToken": "OCA3TySU6/jmqf14lmHNFSFDwA1Vpqomyuo/6UEc5jk="
}
}

```

Hata: 
```json
{
    "success": false,
    "message": "Email/Username or password is incorrect"
}
```