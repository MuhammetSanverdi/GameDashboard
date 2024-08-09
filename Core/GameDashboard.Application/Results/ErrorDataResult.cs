namespace GameDashboard.Application.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        public ErrorDataResult(string message) : base(false, message)
        {

        }
        public ErrorDataResult(T data) : base(data, false)
        {

        }
    }
}
