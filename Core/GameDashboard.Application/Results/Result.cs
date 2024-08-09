namespace GameDashboard.Application.Results
{
    public class Result : IResult
    {
        private string _message;
        private bool _isSuccess;
        public Result(bool isSuccess, string message)
        {
            _message = message;
            _isSuccess = isSuccess;
        }
        public Result(bool isSuccess)
        {
            _isSuccess = isSuccess;
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
    }
}
