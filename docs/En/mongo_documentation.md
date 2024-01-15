#MongoDb
All MongoDb endpoints are protected by authentication and authorization. It is mandatory to send the "JWT Bearer" token in the "authorization" parameter in the header parameters of all requests.

**Headers**

|parameter | is required |description|
|--------------------|--------------|----------|
| Authorization | *required | Bearer Token |


## Get Databases

### Get Databases


- **Sample Request**

- Endpoint
```[GET] ~/api/mogno/db ```


- **Response**

Success: ```200 Arrows```

```json
[
     {
         "name": "database_1"
     },
     {
         "name": "database_test"
     },
     {
         "name": "another_database"
     },
]
```

Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Create Database


- **Sample Request**

- Endpoint
```[POST] ~/api/mogno/db ```

- **Body**

```json
{
     "name":"clan_db",
     "tablename" : "warriors"
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
## Update Database


- **Sample Request**

- Endpoint
```[PUT] ~/api/mogno/db ```

- **Body**
```json
{
     "name": "database_name",
     "newDbName": "database_name_updated"
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
## Delete Database


- **Sample Request**

- Endpoint
```[DELETE] ~/api/mongo/db?name={{db_name}} ```

- **Query**
```name``` database name


- **Response**

Success: ```201 Created```


Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Get Collections

- **Sample Request**

- Endpoint
```[GET] ~/api/mongo/table?dbname={{db_name}} ```

- **Query**
```dbname``` database name


- **Response**

Success: ```200 Arrows```

```json
[
     {
         "name": "collection_1"
     },
     {
         "name": "collection_test"
     },
     {
         "name": "another_collection"
     },
]
```

Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Create Collection

- **Sample Request**

- Endpoint
```[POST] ~/api/mogno/table ```


- **Response**

Success: ```201 Created```

```json
{
     "dbName": "game_clan_db",
     "name":"attackers"
}
```
Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Update Collection

- **Sample Request**

- Endpoint
```[PUT] ~/api/mogno/table ```


- **Response**

Success: ```201 Created```

```json
{
     "dbName": "game_clan_db",
     "name":"attackers",
     "name": "attackers_updated"
}
```
Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Delete Collection


- **Sample Request**

- Endpoint
```[DELETE] ~/api/mongo/table?dbname={{db_name}} ```

- **Query**
```dbname``` database name


- **Response**

Success: ```201 Created```


Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Get Collection Items

- **Sample Request**

- Endpoint
```[GET] ~/api/mongo/item?dbname={{db_name}}&tablename={{table_name}} ```

- **Query**
  ```dbname``` database name
 
  ```tablename``` table name


- **Response**

Success: ```200 Arrows```

```json
[
     {
         "_id": 123,
         "field": "field value 123",
         "otherField": 3,
         "anotherField": "Value"

     },
     {
         "_id": 124,
         "field": "field value 123",
         "otherField": 4,
         "anotherField": "Value"

     },

]
```

Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Create Collection Item

- **Sample Request**

- Endpoint
```[POST] ~/api/mongo/item```

- **Body**
```json
{
   "dbName": "game_clan_db",
   "tableName": "warriors",
   "doc":{
       "name":"Ironclad",
       "ability":"Iren Shield",
       "power":9
   }
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
## Update Collection Item

- **Sample Request**

- Endpoint
```[PUT] ~/api/mongo/item```

- **Body**
```json
{
     "dbName": "game_clan_db",
     "tableName": "warriors",
     "doc":{
         "_id": "656b33c5362176ee841bd36c",
         "name": "Stone Sentinel",
         "ability": "Rock Smash",
         "power": 9
     }
}
```

- **Response**

Success: ```204 No Content```

Error:
```json
{
     "success": false,
     "message": "An error message"
}
```
## Delete Collection Item

- **Sample Request**

- Endpoint
```[DELETE] ~/api/mongo/item?dbname={{db_name}}&name={{table_name}}&id={{item_id}} ```

- **Query**
  ```dbname``` database name
 
  ```name``` table name

  ```Id``` item Id


- **Response**

Success: ```204 No Content```


Error:
```json
{
     "success": false,
     "message": "An error message"
}
```