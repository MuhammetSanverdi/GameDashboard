namespace GameDashboard.Application.Results
{
    public interface IDataResult<T>:IResult
    {
        public T? Data { get;  }
    }
}
