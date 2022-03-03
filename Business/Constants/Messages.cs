using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {//static yapmamın sebebi her seferinde new'lememek için
     //Message lar sabit olduğu için buradan static kullanıyorum.


        public static string MusteriAdded = "Müşteri başarıyla eklendi.";
        public static string MusteriUpdated = "Müşteri bilgileri başarıyla güncelleştirildi.";
        public static string MusteriDeleted = "Müşteri bilgileri başarıyla silindi.";
        


        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre Hatalı.";
        public static string SuccessfulLogin = "Sisteme Giriş başarılı.";
        public static string UserAlreadyExits = "Bu kullanıcı zaten mevcut.";
        public static string UserRegisterd = "Kullanıcı başarıyla kaydedildi.";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu.";


        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string ProductNameAlreadyExists = "Müşteri numarası zaten mevcut.";
    }
}
