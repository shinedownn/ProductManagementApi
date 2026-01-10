# ProductManagementAPI
 
_Özet_
----------------------
&emsp; &emsp; Bu proje, mimarisi Db First yaklaşımı ile geliştirilmiş bir Web API projesidir. Proje içerisinde Dapper kullanılarak MSSQL veritabanı ile etkileşim sağlanmıştır. Katmanlı mimari yapısı ile projenin sürdürülebilirliği ve genişletilebilirliği hedeflenmiştir

_Mimari Yapı_
----------------------
- ${\color{purple}{DataAccess}}$ katmanında Generic Repository deseni kullanılarak veri erişim işlemleri soyutlanmıştır. Bu sayede farklı veri kaynaklarına kolayca adapte olunabilir  
- ${\color{purple}{Entities}}$   katmanında veritabanı tablolarına karşılık gelen sınıflar yer almaktadır. AutoMapper kullanılarak veri transferi sırasında nesneler arasında kolayca dönüşüm sağlanmıştır  
- ${\color{purple}{Services}}$   katmanında https://fakestoreapi.com/products üzerinden ürün verileri çekilerek ${\color{red}{Products}}$ tablosuna eklenmesi ve veritabanındaki ürünlerle dış kaynaktan çekilen ürün listesi karşılaştırma işlemleri gerçekleştirilmiştir  
- ${\color{purple}{Mapping}}$    katmanında AutoMapper profilleri tanımlanmıştır  
- ${\color{purple}{Filters}}$    katmanında isteklerin doğrulanması için Fluent Validation kullanılmıştır  
- ${\color{purple}{Sql \space Script}}$ klasöründe veritabanı tabloları ve prosedürlerin oluşturulması için gerekli SQL script dosyası bulunmaktadır  
- ${\color{purple}{Utility}}$    katmanında JWT Token oluşturma işlemi ve şifre hash değerinin hesaplanması gerçekleştirilmiştir  
- ${\color{purple}{Response}}$   katmanında API yanıt modeli tanımlanmıştır  

_Nasıl çalıştırılır?_
-----------------------
1- ${\color{orange}{MSSQL}}$'de veritabanı yaratılır  
2- ${\color{olive}{Connection \space string}}$ düzenlenir  
3- ${\color{green}{Swagger}}$ ile ${\color{blue} \text{/api/Database}}$ tetiklenerek ${\color{blue}{Sql \space {Script \backslash Init.sql}}}$ script dosyası çalıştırılır. Veritabanı tabloları ve prosedürler örnek veriler ile birlikte kurulur. Örnek veriler https://fakestoreapi.com/products 'den çekilir ve fiyat bilgileri değiştirilerek veritabanına yazılır    
4- ${\color{green}{Swagger}}$ ile ${\color{blue} \text{/api/Token}}$ demo kullanıcı ile tetiklenir   
5- ${\color{green}{Swagger}}$ ile ${\color{blue} \text{/api/ExternalProduct/GetDifferentProducts}}$ ile veritabanındaki ürünlerle dış kaynaktan çekilen ürün listesi karşılaştırma raporu alınır

_Token almak için_
-----------------------
*Kullanıcı adı*  : demo  
*Şifre*          : demo  

_Kullanılan Teknolojiler_
-----------------------
C#  
Dapper  
MSSQL  

_Kullanılan Teknikler_
-----------------------
JWT Token  
Generic Repository  
Mapping (AutoMapper)  
Dependency Injection  
Fluent Validation  
Linq  
Sql Script çalıştırma ile veritabanı kurulumu
 



