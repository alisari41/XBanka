using System.Text;

namespace Core.Utilities.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {//Out Keyword = Değişen nesne biziim beyt array'imize aktarılacak
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;//Salt oluşturma
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Oluşan salt'ı Hash'e çevirme
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))//passwordSalt'ı kullanmayı unuttuğum için şifre hatalı mesajı alıyordum.
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//Kullanıcının gönderdiği şifre ile veritabanındaki aynı mı
                //computedHash yeni oluşturulan şifre girilen hash
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {//passwordHash veri tabanındaki hash
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
