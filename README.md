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

Çözümde kullanılan ve Docker üzerinden yürütülen servisler ise şöyle.

| **Sistem**     | **Servis**                       |  **Görev**                           | **Adres**  |
|----------------|----------------------------------|--------------------------------------|------------|
| DOCKER COMPOSE | Postgresql | Veritabanı | N/A |
| DOCKER COMPOSE | RabbitMQ | Async Event Queue | localhost:5672 |
| DOCKER COMPOSE | Redis | Distributed Key Value Store | localhost:6379 |
