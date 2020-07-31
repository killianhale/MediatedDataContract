namespace MediatedDataContract
{
    public interface IMediatedDataContractFactory
    {
        MediatedDataContract<TRequest, TResponse> CreateContract<TRequest, TResponse>() where TRequest : class where TResponse : class;
        MediatedDataContract<TResponse> CreateContract<TResponse>() where TResponse : class;
    }
}