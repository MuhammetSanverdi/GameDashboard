namespace GameDashboard.Application.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        private T? _data;
        private string _message;
        private bool _isSuccess;
        public DataResult(T data,bool isSuccess, string message)
        {
            _data = data;
            _message = message;
            _isSuccess = isSuccess;
        }
        public DataResult(T data,bool isSuccess)
        {
            _data = data;
            _isSuccess = isSuccess;
        }
        public DataResult(bool isSuccess,string message)
        {
            _isSuccess = isSuccess;
            _message = message;
        }
        public bool IsSuccess
        {
            get
            {
                return _isSuccess;
            }
        }
        public string Message
        {
            get
            {
                return _message;
            }
        }
        public T Data
        {
            get
            {
                return _data;
            }
        }
    }
}
