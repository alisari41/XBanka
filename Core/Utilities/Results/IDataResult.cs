namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }//Döndürmek istenen veriyi döndermek için kullanılır
    }
}
