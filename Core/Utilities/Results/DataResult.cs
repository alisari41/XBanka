﻿namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {//Resulttan farkı Datasının olması
        public DataResult(T data,bool success,string message):base(success,message)
        {
            Data = data;
        }
        public DataResult(T data,bool success) : base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
