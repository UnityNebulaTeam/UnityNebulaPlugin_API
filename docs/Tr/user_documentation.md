# Kullanıcı İşlemleri

Tüm user endpointleri kimlik doğrulama ve yetkilendirme ile korunmaktadır. Tüm isteklerin header parametrelerinde "authorization" parametresinde "JWT Bearer" token gönderilmesi zorunludur.

**Headers**

|parameter           | is required  |description|
|--------------------|--------------|-----------|
| Authorization      | *required    | Bearer Token |

## Yeni Veritabanı Bağlantısı Ekleme

Yeni bir connection string eklerken ``` keyIdentifier ``` property sadece belirtilen türde verilebilir:

|parameter           | is required  |description|
|--------------------|--------------|-----------|
| keyIdentifier      | *required    | "MongoDb" |

**NOT** : 

Bu sürümde sadece **MongoDb** desteği bulunmaktadır. Sonraki sürümlerde **SQLite**, **MySQL** desteği eklenecektir. 

``` keyIdentifier ``` parametresinde büyük/küçük harf hassasiyeti bulunmamaktadır. ("MONGODB", "MongoDb", "mongodb" geçerli değerlerdir.)

- **Örnek Request**

- Endpoint
```[POST]  ~/api/user/connection ```

- **Body**

```json
{
 "keyIdentifier": "MongoDb",
 "connectionString": "mongodb+srv://secret-mongodb-connectionstring"
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
## Get User Connections

- **Örnek Request**

- Endpoint
```[GET]  ~/api/user/connection ```


- **Response**

Başarılı: ```200 Ok```

```json 
[
    {
        "name": "mongodb",
        "connectionString": "mongodb+srv://secret-mongodb-connectionstring"
    }
]
```



Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```


## User Data

- **Örnek Request**

- Endpoint
```[GET]  ~/api/user/me ```


- **Response**

Başarılı: ```200 Ok```

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



Hata: 
```json
{
    "success": false,
    "message": "An error message"
}
```
