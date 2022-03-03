namespace Core.Utilities.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)//başarısız işlem olduğu için success'i almaya gerek yok
        {
        }

        public ErrorDataResult(T data) : base(data, false)
        {
        }
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }

        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
