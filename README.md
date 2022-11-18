# TelephoneDirectory
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

#### 3- Uygulama işleyişi aşağıdaki gibidir. 
BackgroundService ile Contact.Api ve  Report.Api arasındaki ilişki httprequest üzerinden yürütülmektedir. 
![](https://raw.githubusercontent.com/CemilGULER/TelephoneDirectory/development/MimariTasar%C4%B1m.png)
