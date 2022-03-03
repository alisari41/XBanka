namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message)//başarılı işlem olduğu için success'i almaya gerek yok
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(string message) : base(default, true, message)
        {

        }

        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
