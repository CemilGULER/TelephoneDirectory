# TelephoneDirectory
Uygulama .net 6.0'da geliştirilmiştir. 
### Uygulama temelde 3 servisten oluşmaktadır.
#### 1- Contact.Api
Kişi ve Kişi iletişim bilgilerinin işlendiği servislerdir. 
- (HttpPost) api/person: Rehberde kişi oluşturma işlemini yapan servistir.
- (HttpDelete) api/person:  Rehberde kişi kaldırma işlemini yapan servistir.
- (HttpGet) api/person: Rehberdeki kişilerin listelenmesi işlemini yapan servistir.
- (HttpGet) api/person/person-detail: Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerinin getirilmesi sağlayan servistir.
- (HttpGet) api/person/location-detail: Rapor servisinin location detayında bilgileri backround servislere ileten servistir.
- (HttpPost) api/personcontact: Rehberdeki kişiye iletişim bilgisi ekleme işlemini yapan servistir.
- (HttpDelete) api/personcontact:  Rehberdeki kişiden iletişim bilgisi kaldırma işlemini yapan servistir.
#### 2- Report.Api
Rapor talep işlemlerinin yapıldığı servislerdir.
- (HttpPost) api/report/report-request: Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi yapan servistir.
- (HttpGet) api/report/requested-report-list: Sistemin oluşturduğu raporların listelenmesi işlemini yapan servistir.
- (HttpGet) api/report/report-detail: Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi işlemini yapan servistir.
- (HttpPost) api/report/completed-report: Backround servislerinde rapor oluşturulduktan saporun adı ve rapor yolunun setlendiği servistir.
#### 3- BackgroundService
Rapor talep edilme işlemi yapıldıktan sonra raporu oluşturan arka plan servisleridir. Öncelikler kuyruk üzerinden talep okunur. Sonra httpclient ile  Contact.Api üzerinden rapora ait data alınır sonrasında rapor oluşturulak(Dosya işlemi yapılır) rapora ait bilgiler httpclient ile Report.Api  üzerinde set edilir. 

#### Uygulama işleyişi aşağıdaki gibidir. 
BackgroundService ile Contact.Api ve  Report.Api arasındaki ilişki httprequest üzerinden yürütülmektedir. 
![](https://raw.githubusercontent.com/CemilGULER/TelephoneDirectory/development/MimariTasar%C4%B1m.png)

#### Veritabanı. 
Veritabanı olarak postgresql kullanılmıştır.  
Code First yaklaşımı tercih edilmiştir.  
**docker run -e POSTGRES_PASSWORD="1q2w3e4R!" -p 5432:5432 --name local-postgres postgres** komutu ile local ortama postgre kurulumu gerçekleştirilmiştir.  
**Update-Database -P TelephoneDirectory.Data.Access -Context TelephoneDirectoryDbContext -S TelephoneDirectory.Contact.Api** komutu ile oluşturulan migrationların veritabanına yansıması sağlanmalıdır.
### Kuyruk
Kuyruk olarak RabbitMQ tercih edilmiştir.
### Test
Uygulamayı uçtan uca test edecek toplamda 14 adet test geliştirilmiştir.  
Bunların 4 tanesi Unit Test geri kalan 10 tanesi  Integration Test'dir.  
![](https://raw.githubusercontent.com/CemilGULER/TelephoneDirectory/development/Test.PNG)
### Git
Developer branch'inde her committe versiyon tag'leme yapılmış olup, Geliştirme tamamlandıktan sonra pull request oluşturulurak kontroller sonrasında ana branch olan main'e birleştirilmiştir. 
  
