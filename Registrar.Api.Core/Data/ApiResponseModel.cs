namespace Registrar.Api.Core.Data
{
    public class ApiErrorResponse : ApiResponseModel
    {
        public ApiErrorResponse(
            List<string> errors,
            ResponseState state = ResponseState.Failure, 
            DataFormat dataFormat = DataFormat.ObjectList, 
            string message = "An error occured") : base(state, dataFormat, message)
        {
            Errors = errors;
        }
    }
    public abstract class ApiResponseModel
    {
        public ApiResponseModel(ResponseState state, DataFormat dataFormat, string message)
        {
            Errors= new List<string>();
            State= state;
            DataFormat= dataFormat;
            Message= message;
        }
        public string Message { get; set; }
        public ResponseState State { get; set; }
        public DataFormat DataFormat { get; set; }
        public List<string> Errors { get; set; }
    }

    public class ApiObjResponse<T>: ApiResponseModel
    {
        public ApiObjResponse(ResponseState state = ResponseState.Success,
            DataFormat format = DataFormat.Object, string message = null, T data = default)
             : base(state, format, message)
        {
            Data= data;
        }

        public T Data { get; set; }
    }
    //public class ApiListResponse : ApiResponseModel
    //{
    //    public ApiListResponse(ResponseState state = ResponseState.Success,
    //        DataFormat format = DataFormat.ObjectList, string message = null, int? pageNumber = null, 
    //        int? pageSize = null, List<string> data = null)
    //        : base(state, format, message)
    //    {
    //        PageNumber = pageNumber;
    //        PageSize = pageSize;
    //        Data = data == null ? new List<string>() : data;
    //    }

    //    public int? PageNumber { get; set; }
    //    public int? PageSize { get; set; }
    //    public List<string> Data { get; set; }
    //}
    public class ApiListResponse<T> : ApiResponseModel
    {
        public ApiListResponse(ResponseState state = ResponseState.Success,
            DataFormat format = DataFormat.ObjectList, string message = null, 
            int? pageNumber = null, int? pageSize = null, List<T> data = null)
             : base(state, format, message)
        {
            PageNumber= pageNumber;
            PageSize= pageSize;
            Data = data == null? new List<T>(): data;
        }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public List<T> Data { get; set; }
    }

    public enum ResponseState
    {
        Pending,
        Success,
        Failure
    }
    public enum DataFormat
    {
        Object,
        ObjectList
    }
}
