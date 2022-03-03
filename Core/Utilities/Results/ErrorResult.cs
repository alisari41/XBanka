namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult( string message) : base(false, message)//başarısız işlem olduğu için success'i almaya gerek yok
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }
}
