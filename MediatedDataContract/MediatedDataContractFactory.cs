using AutoMapper;
using MediatR;

namespace MediatedDataContract
{
    public class MediatedDataContractFactory : IMediatedDataContractFactory
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MediatedDataContractFactory(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public MediatedDataContract<TRequest, TResponse> CreateContract<TRequest, TResponse>() where TRequest : class where TResponse : class
        {
            return new MediatedDataContract<TRequest, TResponse>(_mediator, _mapper);
        }

        public MediatedDataContract<TResponse> CreateContract<TResponse>() where TResponse : class
        {
            return new MediatedDataContract<TResponse>(_mediator, _mapper);
        }
    }
}
