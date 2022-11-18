# TelephoneDirectory
*Uygulama temelde 3 servisten oluşmaktadır. *
**1- Contact.Api**
Kişi ve Kişi iletişim bilgilerinin işlendiği servislerdir. 
- (HttpPost) api/person: Rehberde kişi oluşturma işlemini yapan servistir.
- (HttpDelete) api/person:  Rehberde kişi kaldırma işlemini yapan servistir.
- (HttpGet) api/person: Rehberdeki kişilerin listelenmesi işlemini yapan servistir.
- (HttpGet) api/person/person-detail: Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerinin getirilmesi sağlayan servistir.
- (HttpGet) api/person/location-detail: Rapor servisinin location detayında bilgileri backround servislere ileten servistir.
