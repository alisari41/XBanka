

// ReSharper disable CommentTypo

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }//Kullanıcını girdiği şifreyi kuvvetlendirmek için
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }

    }
}
