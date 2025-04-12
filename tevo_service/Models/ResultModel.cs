public class ResultModel<T>
{
    public bool State { get; set; }

    public string Message { get; set; }

    public T? Model { get; set; }

    public ResultModel<T> Success(T? data = default(T?), string message = "Success")
    {
        return new ResultModel<T>
        {
            State = true,
            Model = data,
            Message = message
        };
    }

    public ResultModel<T> Fail(string message)
    {
        return new ResultModel<T>
        {
            State = false,
            Message = message
        };
    }
}