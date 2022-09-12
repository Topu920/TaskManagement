using Application.Common.Messages;


namespace Application.Common.Results
{
    public class ManagerResult<T> : BaseResult
    {
        public ManagerResult()
        {
            this.Messages = new List<ISystemMessage>();
        }

        public T Data { get; set; } = default!;
    }
}
