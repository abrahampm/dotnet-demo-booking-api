namespace alten_test.Core.Utilities
{
    public abstract class ServiceResult
    {
        public static NoPermissionResult NoPermission()
        {
            return new NoPermissionResult();
        }

        public static SuccessResult Success()
        {
            return new SuccessResult();
        }

        public static SuccessResult<T> Success<T>(T result)
        {
            return new SuccessResult<T>(result);
        }

        protected ServiceResult(ServiceResultType serviceResultType)
        {
            _resultType = serviceResultType;
        }

        private readonly ServiceResultType _resultType;
        public ServiceResultType ResultType
        {
            get { return _resultType; }
        }
    }
    public class SuccessResult<T> : ServiceResult
    {
        public SuccessResult(T result)
            : base(ServiceResultType.Success)
        {
            _result = result;
        }

        private readonly T _result;
        public T Result
        {
            get { return _result; }
        }
    }
    public class SuccessResult : SuccessResult<object>
    {
        public SuccessResult() : this(null) { }
        public SuccessResult(object o) : base(o) { }
    }

    public class NoPermissionResult : ServiceResult
    {
        public NoPermissionResult() : base(ServiceResultType.NoPermission) {}
    }

    public class NotFoundResult : ServiceResult
    {
        public NotFoundResult() : base(ServiceResultType.NotFound) {}
    }

    public class ErrorResult : ServiceResult
    {
        public ErrorResult(string errorMessage) : base(ServiceResultType.Error)
        {
            _errorMessage = errorMessage;
        }
        
        public string Error
        {
            get { return _errorMessage; }
        }

        private readonly string _errorMessage;
    }

    public enum ServiceResultType
    {
        Success,
        NoPermission,
        NotFound,
        Error
    }
}