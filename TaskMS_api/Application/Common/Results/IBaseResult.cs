using Application.Common.Messages;

namespace Application.Common.Results
{
    public interface IBaseResult
    {
        List<ISystemMessage> Messages { get; set; }
    }
}
