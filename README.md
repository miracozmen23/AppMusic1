# ApiMusic

## Proje Açıklaması
Bu proje, kullanıcıların dinledikleri müzikler hakkında bilgi işlemlerini gerçekleştirmeyi amaçlar. Kullanıcılar albümlere, şarkıcılara ve şarkılara göre isteklerini getirebilir. Veri tabanında kalıcı olarak saklanan bu bilgilere; filtreleme, sıralama ve sayfalama özellikleri sayesinde kolaylıkla erişebilirler.

## Proje Hedefleri
1. Proje için modellerin ve veri transfer objelerinin belirlenmesi.
2. Entity Framework üzerinden veritabanı oluşturulması, proje konusu ile ilgili gerekli tabloların eklenmesi.
3. Temel veri tabanı işlemlerinin (ekleme, silme, güncelleme, sorgulama) gerçekleştirilmesi.
4. Veri Tabanı tabloları arasında EF Core ilişkilerinin (şarkıcı-albüm arasında one to one, şarkıcı-şarkı arasında one to many, vb.) uygulanması.
5. Uygun hata mesajlarıyla hataların yönetiminin sağlanması.
6. Yönetici tarafından şarkılar, şarkıcılar ve albümler için; sorgulama, ekleme, düzenleme ve silme gibi yönetim işlevselliği sağlamak.
7. Standart kullanıcılar ve anonimler için sorgulama işlevselliği sağlamak.
8. Swagger ve Postman ile bu aşamaları test etmek.

## Proje Geliştirme Aşamaları ve Süreçleri

### 1. Analiz ve Tasarım Aşaması
- Model analizi yapılır ve kullanıcı senaryoları belirlenir.
- Ardından veritabanı modeli oluşturulur ve testler için Postman tasarımı gerçekleştirilir.

### 2. Geliştirme Aşaması
- ASP.NET framework'ü kullanılarak RESTful prensiplerine uygun proje geliştirilir.
- Veritabanı için EF Core tercih edilir.
- Geliştirme sürecinde kodlama standartlarına ve güvenlik prensiplerine uyulur.

### 3. Test Aşaması
- Geliştirilen modüller birim testlerden geçirilir ve sistem bütün olarak test edilir.
- Özel hata mesajları ile ve logger’la hata ayıklama testleri yapılır.

