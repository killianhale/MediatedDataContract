namespace MediatedDataContract
{
    public interface IDataContractFactory
    {
        DataContract<TRequest, TResponse> CreateContract<TRequest, TResponse>();
        DataContract<TResponse> CreateContract<TResponse>();
    }
}