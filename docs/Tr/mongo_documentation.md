
# MongoDb 
Tüm MongoDb endpointleri kimlik doğrulama ve yetkilendirme ile korunmaktadır. Tüm isteklerin header parametrelerinde "authorization" parametresinde "JWT Bearer" token gönderilmesi zorunludur.

**Headers**

|parameter           | is required  |description|
|--------------------|--------------|-----------|
| Authorization      | *required    | Bearer Token           |


## Get Databases

### Get Databases


- **Örnek Request**

- Endpoint
```[GET]  ~/api/mongo/db ```


- **Response**

Başarılı: ```200 Ok```

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

Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Create Database


- **Örnek Request**

- Endpoint
```[POST]  ~/api/mongo/db ```

- **Body**

```json
{
    "name":"clan_db",
    "tablename" : "warriors"
}
 ```


- **Response**

Başarılı: ```201 Created```



Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Update Database


- **Örnek Request**

- Endpoint
```[PUT]  ~/api/mongo/db ```

- **Body**
```json
{
    "name": "database_name",
    "newDbName": "database_name_updated"
}
```


- **Response**

Başarılı: ```201 Created```


Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Delete Database


- **Örnek Request**

- Endpoint
```[DELETE]  ~/api/mongo/db?name={{db_name}} ```

- **Query**
```name``` database name


- **Response**

Başarılı: ```201 Created```


Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Get Collections

- **Örnek Request**

- Endpoint
```[GET]  ~/api/mongo/table?dbname={{db_name}} ```

- **Query**
```dbname``` database name


- **Response**

Başarılı: ```200 Ok```

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

Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Create Collection

- **Örnek Request**

- Endpoint
```[POST]  ~/api/mogno/table ```


- **Response**

Başarılı: ```201 Created```

```json 
{
    "dbName": "game_clan_db",
    "name":"attackers"   
}
```
Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Update Collection

- **Örnek Request**

- Endpoint
```[PUT]  ~/api/mongo/table ```


- **Response**

Başarılı: ```201 Created```

```json 
{
    "dbName": "game_clan_db",
    "name":"attackers",
    "name": "attackers_updated"   
}
```
Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Delete Collection


- **Örnek Request**

- Endpoint
```[DELETE]  ~/api/mongo/table?dbname={{db_name}} ```

- **Query**
```dbname``` database name


- **Response**

Başarılı: ```201 Created```


Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Get Collection Items

- **Örnek Request**

- Endpoint
```[GET]  ~/api/mongo/item?dbname={{db_name}}&tablename={{table_name}} ```

- **Query**
 ```dbname``` database name
 
 ```tablename``` table name


- **Response**

Başarılı: ```200 Ok```

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

Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Create Collection Item

- **Örnek Request**

- Endpoint
```[POST]  ~/api/mongo/item```

- **Body**
```json
{
  "dbName": "game_clan_db",
  "tableName": "warriors",
  "doc": {
      "name":"Ironclad",
      "ability":"Iren Shield",
      "power":9
  }
}
```

- **Response**

Başarılı: ```201 Created```

Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Update Collection Item

- **Örnek Request**

- Endpoint
```[PUT]  ~/api/mongo/item```

- **Body**
```json
{
    "dbName": "game_clan_db",
    "tableName": "warriors",
    "doc": {
        "_id": "656b33c5362176ee841bd36c",
        "name": "Stone Sentinel",
        "ability": "Rock Smash",
        "power": 9
    }
}
```

- **Response**

Başarılı: ```204 No Content```

Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
## Delete Collection Item

- **Örnek Request**

- Endpoint
```[DELETE]  ~/api/mongo/item?dbname={{db_name}}&name={{table_name}}&id={{item_id}} ```

- **Query**
 ```dbname``` database name
 
 ```name``` table name

 ```Id``` item Id


- **Response**

Başarılı: ```204 No Content```


Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```