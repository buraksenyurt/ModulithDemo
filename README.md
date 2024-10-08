# Modulith Demo (Modüler Monolitik Sistem Çözümü)

Değerli Mehmet Özkaya'nın [Modüler Monolitik](https://www.udemy.com/share/10bAjh3@TzRn0z5EmmTiGTezg3R7UGoGGzqQy7DRdMVOhq9nzqrVxooF7SIIXZ7QMiEZLXHuHg==/) sistemler üzerine hazırlamış olduğu harika Udemy kursundan yararlanarak inşa etmeye çalıştığım çözümün yer aldığı repodur.

## Geliştirme Ortamı

Çalışmayı aşağıdaki özelliklere sahip platformda icra etmekteyim.

| Özellik   | Açıklama                      |
|-----------|-------------------------------|
| OS | Ubuntu 22.04 LTS |
| CPU | Intel® Core™ i7 2.80GHz × 8 |
| RAM | 32 Gb |
| IDE | Visual Studio Code |
| Framework | .Net 8.0 |

## Çözümde Kullanılan Platformlar

Çözümde kullanılan ve Docker üzerinden yürütülen servisler ise şöyle. Makinede başka bir çözüm içinde benzer servisleri kullandığımdan standart port numaralarını container dışından erişim için 1 artırarak kullandım. Örneğin Postgresql veritabanı için standart port 5432 ancak bir başka çözümde bunu kullandığım için 5433 olarak ayarladım. Elbette docker container içerisinde kendi network ortamında 5432 üzerinden erişiliyor.

| **Sistem**     | **Servis**                       |  **Görev**                           | **Adres**  |
|----------------|----------------------------------|--------------------------------------|------------|
| DOCKER COMPOSE | Postgresql | Veritabanı | localhost:5433 |
| DOCKER COMPOSE | PgAdmin | Veritabanı UI | localhost:5051 |
| DOCKER COMPOSE | RabbitMQ | Async Event Queue | localhost:5673 |
| DOCKER COMPOSE | Redis | Distributed Key Value Store | localhost:6380 |

Pg-admin ayarları ise şöyle.

- **Hostname :** postgres
- **Port     :** 5432
- **Username :** johndoe
- **Password :** somew0rds
- **Database :** RentAGameDb

## Entity Framework Migration

EF migration işlemleri için aşağıdaki komutlar kullanılabilir. Tabii bu işlemleri yapmadan önce Postgresql bağlantı bilgisini içeren Api projesinin çalışır olduğundan emin olmalıyız. Komutumuzu RentAGame.Catalog klasöründe çalıştırabiliriz.

```shell
# Eğer yoksa gerekli ef aracı yüklenir
dotnet tool install --global dotnet-ef

# Örneğin InitialCreate isimli migration paketinin oluşturulması
dotnet ef migrations add InitialCreate --startup-project ../../../Gateway/RentAGame.Api

# Bu son pakete göre gerekli db operasyonlarının işletilmesi
dotnet ef database update --startup-project ../../../Gateway/RentAGame.Api
```

## Web Api Endpoint

Rest Api tarafında kullanılacak endpoint adresleri aşağıdaki gibidir.

| HTTP Metot | URI | Açıklama |
|-----------|-----------------|------------------|
| GET | /games | Tüm oyunlar listesi |
| GET | /games/{id} | Belli bir Id değerine sahip oyun bilgisi |
| POST | /games/genre | Belli türlere ait oyunların listesi (genre listesi request body ile yollanır) |
| GET | /games/price | Belli bir fiyat aralığındaki oyunları çekmek için (Fiyat listesi request body ile yollanır) |
| POST | /games | Yeni bir oyun eklemek için |
| PUT | /games/{id} | Oyun bilgilerini güncellemek için |
| DELETE | /games/{id} | Belli bir oyunu silmek için |

Örnek isteklere ait Postman Collection için [bu dosyayı](./Rent%20A%20Game%20Modulith%20Adventure.postman_collection.json) kullanabilirsiniz.
