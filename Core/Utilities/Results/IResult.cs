namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }//Yapılan işlem başarılı mı
        string Message { get; }//Yapılan işlem başarılımı mesajı oluşturmak istiyorum.
    }
}
