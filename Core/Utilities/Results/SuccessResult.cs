namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {// Başarılı işlemi belirtmek için
        public SuccessResult(string message) : base(true, message)
        {// Success'leri sildim çünkü burada başarılı olduğunun mesajını vericem

        }
        public SuccessResult() : base(true)
        {

        }
    }
}
