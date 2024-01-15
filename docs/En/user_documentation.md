# User Actions

All user endpoints are protected by authentication and authorization. It is mandatory to send the "JWT Bearer" token in the "authorization" parameter in the header parameters of all requests.

**Headers**

|parameter | is required |description|
|--------------------|--------------|----------|
| Authorization | *required | Bearer Token |

## Adding New Database Connection

When adding a new connection string, the ``` keyIdentifier ``` property can only be given of the specified type:

|parameter | is required |description|
|--------------------|--------------|----------|
| keyIdentifier | *required | "MongoDb" |

**NOTE** :

This version only supports **MongoDb**. **SQLite**, **MySQL** support will be added in later versions.

The ``` keyIdentifier ``` parameter is not case sensitive. ("MONGODB", "MongoDb", "mongodb" are valid values.)

- **Sample Request**

- Endpoint
```[POST] ~/api/user/connection ```

- **Body**

```json
{
  "keyIdentifier": "MongoDb",
  "connectionString": "mongodb+srv://secret-mongodb-connectionstring"
}
  ```


- **Response**

Success: ```201 Created```



Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Get User Connections

- **Sample Request**

- Endpoint
```[GET] ~/api/user/connection ```


- **Response**

Success: ```200 Arrows```

```json
[
     {
         "name": "mongodb",
         "connectionString": "mongodb+srv://secret-mongodb-connectionstring"
     }
]
```



Error:
```json
{
     "success": false,
     "message": "An error message"
}
```


## User Data

- **Sample Request**

- Endpoint
```[GET] ~/api/user/me ```


- **Response**

Success: ```200 Arrows```

```json

"username": "ramalt_1234",
"email": "info.ramazanalltuntepe@gmail.com",
"Connections": [
     {
         "name": "mongodb",
         "connectionString": "mongodb+srv://secret-mongodb-connectionstring"
     }
]

```



Error:
```json
{
     "success": false,
     "message": "An error message"
}
```