namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success)// metodun içinde "Success = success;" kullanılması ile aynı. aşşağıdaki kurucu metodun içini getirir(tetikler)
        {
            Message = message;
        }
        public Result(bool success)
        {//Kullanıcı sadece Success'ide geçebilir ikisini birden geçerse message'i de success'ide yukarda set ediyor
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
