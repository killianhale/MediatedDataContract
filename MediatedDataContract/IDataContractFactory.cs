namespace MediatedDataContract
{
    public interface IDataContractFactory
    {
        DataContract<TRequest, TResponse> CreateContract<TRequest, TResponse>() where TRequest : class where TResponse : class;
        DataContract<TResponse> CreateContract<TResponse>() where TResponse : class;
    }
}