namespace SocialNetwork.Core.Helpers.Result.Abstract
{
    public interface IDataResult<TResult> : IResult
    {
        TResult Data { get; }
    }
}