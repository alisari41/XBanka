
# Banka Müşteri Takip

## Backend with C#

Kurumsal, Katmanlı Mimari yapısı kullanılarak SOLID kuralları dahilinde oluşturulmuş, C# dili ile yazılmış Banka müşteri takip fikri üzerinden ilerlenmiştir.

> *"Her şey mümkün olduğunca sadeleştirilmeli, fakat basitleştirilmemelidir."* 
> Albert Einstein

### Katmanlar
- **Core:** Projenin çekirdek katmanı, evrensel operasyonlar için kullanılmaktadır. Core katmanını diğer herhangi bir projenizde kullanabiliriz.
- **DataAccess:** Projenin, Veritabanı ile bağını kuran katmandır.
- **Entities:** Veritabanındaki tablolarımızın projemizde nesne olarak kullanılması için oluşturulmuştur. DTO nesnelerinide barındırmaktadır.
- **Business:** Projemizin iş katmanıdır. Türlü iş kuralları; Veri kontrolleri, validasyonlar, IoC Container'lar ve yetki kontrolleri
- **WebAPI:** Prjenin Restful API Katmanıdır.

### Kullanılan Teknolojiler
<ul>
  <li>.Net Core 5</li>
  <li>Docker</li>
  <li>Azure SQL Database</li>
  <li>Result Türleri</li>
  <li>Restful API</li>
  <li>Interceptor</li>
  <li>Autofac
    <ul>
      <li>IoC (Inversion of Control) </li>
      <li>AOP (Aspect Oriented Programing / Yazılım Geliştirme Yaklaşımı) 
        <ul>
            <li>Caching</li>
            <li>Exception</li>
            <li>Logging</li>
            <li>Performance</li>
            <li>Transaction</li>
            <li>Validation</li>
        </ul>      
      </li>
    </ul>
  </li>            
  <li>Fluent Validation</li>
  <li>Cache yönetimi</li>
  <li>JWT Authentication</li>
  <li>Repository Design Pattern</li>
  <li>Cross Cutting Concerns
    <ul>
        <li>Caching</li>
        <li>Logging</li>
        <li>Validation</li>
    </ul>
  </li>
  <li>ORM
    <ul>
        <li>Dapper</li>
        <li>Entity Framework</li>
    </ul>
  </li>
  <li>Extensions
    <ul>
        <li>Claim
            <ul>
                <li>Claim Principal</li>
            </ul>
        </li>
        <li>Exception Middleware</li>
        <li>Service Collection</li>
        <li>Error Handling
            <ul>
                <li>Error Details</li>
                <li>Validation Error Details</li>
            </ul>
        </li>
    </ul>
  </li>
</ul>








## Demo

**Kurumsal Mimariler İçin Sql Server Veri Tabanı Tasarımı**
<br>
*Gereksinim:* XBankası olarak müsterilerimizin tabakibini yapabilmeliyiz.
<ul>
  <li>İki tip müşterimiz mevcut. Gerçek ve Tüzel Müşteriler.</li>
  <li>Gerçek Müşteri; MüşteriNo, Ad, Soyad, TcKimlikNo alanlarına sahiptir.</li>
  <li>Tüzel Müşteri; MüşteriNo, Unvan, VergiNo alanlarına sahiptir.</li>
  <li>Müşterilerin adreslerini takip edebilmeliyiz</li>
  <li>Tüzel Müşterilerin her 6 aylık dönemde bilanço bilgilerini kaydetmeliyiz. Örneğin 3 Yıllık bir tüze müşterinin her 6 aya denk gelen 6 adet bilanço kaydı olmalıdır. Bilanço bilgisi olarak bilanço tutarı ve bilanço tarihi bilgisini tutmak yeterlidir.</li>
  <li>Gerçek müşterilerin bazıları Ticari Faaliyet gerçekleştirmektedir. Bu tip müşterilerimiz için de bilanço takibi yapabilmeliyiz.</li>
  <li>Bir kişinin veya kurumun bankamızın müşterisi olabilmesi için çeşitli kanallar mevcuttur (İnternet, Şube vb.). Müşterinin hangi kanaldan bankamıza ilk kez katıldığı bilgisini de tutmak istiyoruz.</li>
</ul>

  ![image](https://user-images.githubusercontent.com/81421228/157263700-48fad6e9-3fae-4671-b01c-09ad34b6203d.png)

- Diyelim ki Yeni bir müşteri tipi (Sendiklar) eklenmesi gerekiyor yeni kurallarla/yasalarla bu ekleyeceğimiz alan bizim bu zamana kadar yaptığımız sistemde hiçbir şeyin değişmemesi gerekir. O yüzden bu şekilde yaptıkça Solid kurallarına uymuş oluruz.

![image](https://user-images.githubusercontent.com/81421228/157278748-c09bdfe3-7fe7-4b02-8235-d66a41deb555.png)
