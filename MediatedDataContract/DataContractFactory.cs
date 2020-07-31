using AutoMapper;

namespace MediatedDataContract
{
    public class DataContractFactory : IDataContractFactory
    {
        private readonly IMapper _mapper;

        public DataContractFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DataContract<TRequest, TResponse> CreateContract<TRequest, TResponse>() where TRequest : class where TResponse : class
        {
            return new DataContract<TRequest, TResponse>(_mapper);
        }

        public DataContract<TResponse> CreateContract<TResponse>() where TResponse : class
        {
            return new DataContract<TResponse>(_mapper);
        }
    }
}
