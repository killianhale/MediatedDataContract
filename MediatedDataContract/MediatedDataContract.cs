using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace MediatedDataContract
{
    public class MediatedDataContract<TRequest, TResponse> : DataContract<TRequest, TResponse>
        where TRequest : class where TResponse : class
    {
        private readonly IMediator _mediator;

        public MediatedDataContract(IMediator mediator, IMapper mapper) : base(mapper)
        {
            _mediator = mediator;

            var list = new List<string>().Select(s => s.ToUpper());
        }

        public Func<TRequest, Task<TResponse>> Mediate<TInput, TOutput>(
            Func<TRequest, Task<TRequest>> prep,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
            where TInput : IRequest<TOutput>
        {
            return MediatedWrapCaller.CallWrap<TRequest, TResponse, TInput, TOutput>(this, _mediator);
        }

        public Func<TRequest, Task<TResponse>> Mediate<TInput>()
            where TInput : IRequest<TResponse>
        {
            return MediatedWrapCaller.CallWrap<TRequest, TResponse, TInput>(this, _mediator);
        }
    }

    public class MediatedDataContract<TResponse> : DataContract<TResponse>
        where TResponse : class
    {
        private readonly IMediator _mediator;

        public MediatedDataContract(IMediator mediator, IMapper mapper) : base(mapper)
        {
            _mediator = mediator;
        }

        public Func<Task<TResponse>> Mediate<TInput, TOutput>(
            Func<TInput, Task<TInput>> prep,
            Func<TInput, TOutput, TResponse, Task<TResponse>> convert = null
        )
            where TInput : class, IRequest<TOutput>, new()
        {
            return MediatedWrapCaller.CallWrap<TResponse, TInput, TOutput>(this, _mediator, prep, convert);
        }

        public Func<Task<TResponse>> Mediate<TInput, TOutput>(
            Func<TInput, TInput> prep,
            Func<TInput, TOutput, TResponse, Task<TResponse>> convert = null
        )
            where TInput : class, IRequest<TOutput>, new()
        {
            return MediatedWrapCaller.CallWrap<TResponse, TInput, TOutput>(this, _mediator, prep, convert);
        }

        public Func<Task<TResponse>> Mediate<TInput>(
            Func<TInput, Task<TInput>> prep,
            Func<TInput, TResponse, Task<TResponse>> convert = null
        )
            where TInput : class, IRequest<TResponse>, new()
        {
            return MediatedWrapCaller.CallWrap<TResponse, TInput>(this, _mediator, prep, convert);
        }

        public Func<Task<TResponse>> Mediate<TInput>(
            Func<TInput, TInput> prep,
            Func<TInput, TResponse, Task<TResponse>> convert = null
        )
            where TInput : class, IRequest<TResponse>, new()
        {
            return MediatedWrapCaller.CallWrap<TResponse, TInput>(this, _mediator, prep, convert);
        }
    }

    internal static class MediatedWrapCaller
    {
        public static Func<TRequest, Task<TResponse>> CallWrap<TRequest, TResponse, TInput, TOutput>(
            MediatedDataContract<TRequest, TResponse> contract,
            IMediator mediator,
            Func<TRequest, TInput, Task<TRequest>> prep = null,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
            where TRequest : class
            where TResponse : class
            where TInput : IRequest<TOutput>
        {
            Func<TInput, Task<TOutput>> mediate = async i =>
            {
                TOutput result;

                using (var tokenSource = new CancellationTokenSource())
                {
                    result = await mediator.Send(i, tokenSource.Token);
                }

                return result;
            };

            return prep != null
                ? contract.Wrap(prep, mediate, convert)
                : contract.Wrap(mediate, convert);
        }

        public static Func<TRequest, Task<TResponse>> CallWrap<TRequest, TResponse, TInput>(
            MediatedDataContract<TRequest, TResponse> contract,
            IMediator mediator,
            Func<TRequest, TInput, TRequest> prep = null,
            Func<TRequest, TResponse, Task<TResponse>> convert = null
            )
            where TRequest : class
            where TResponse : class
            where TInput : IRequest<TResponse>
        {
            Func<TInput, Task<TResponse>> mediate = async i =>
            {
                TResponse result;

                using (var tokenSource = new CancellationTokenSource())
                {
                    result = await mediator.Send(i, tokenSource.Token);
                }

                return result;
            };

            return prep != null
                ? contract.Wrap(prep, mediate, convert)
                : contract.Wrap(mediate, convert);
        }

        public static Func<Task<TResponse>> CallWrap<TResponse, TInput, TOutput>
            (MediatedDataContract<TResponse> contract,
            IMediator mediator,
            Func<TInput, Task<TInput>> prep = null,
            Func<TInput, TOutput, TResponse, Task<TResponse>> convert = null
            )
            where TResponse : class
            where TInput : class, IRequest<TOutput>, new()
        {
            Func<TInput, Task<TOutput>> mediate = async i =>
            {
                TOutput result;

                using (var tokenSource = new CancellationTokenSource())
                {
                    result = await mediator.Send(i, tokenSource.Token);
                }

                return result;
            };

            return prep != null
                ? contract.Wrap(prep, mediate, convert)
                : contract.Wrap(prep, mediate, convert);
        }
        
        public static Func<Task<TResponse>> CallWrap<TResponse, TInput, TOutput>
            (MediatedDataContract<TResponse> contract,
            IMediator mediator,
            Func<TInput, TInput> prep = null,
            Func<TInput, TOutput, TResponse, Task<TResponse>> convert = null
            )
            where TResponse : class
            where TInput : class, IRequest<TOutput>, new()
        {
            Func<TInput, Task<TOutput>> mediate = async i =>
            {
                TOutput result;

                using (var tokenSource = new CancellationTokenSource())
                {
                    result = await mediator.Send(i, tokenSource.Token);
                }

                return result;
            };

            return prep != null
                ? contract.Wrap(prep, mediate, convert)
                : contract.Wrap(prep, mediate, convert);
        }

        public static Func<Task<TResponse>> CallWrap<TResponse, TInput>(
            MediatedDataContract<TResponse> contract,
            IMediator mediator,
            Func<TInput, Task<TInput>> prep = null,
            Func<TInput, TResponse, Task<TResponse>> convert = null
            )
            where TResponse : class
            where TInput : class, IRequest<TResponse>, new()
        {
            Func<TInput, Task<TResponse>> mediate = async i =>
           {
               TResponse result;

               using (var tokenSource = new CancellationTokenSource())
               {
                   result = await mediator.Send(i, tokenSource.Token);
               }

               return result;
           };

            return prep != null
                ? contract.Wrap(prep, mediate, convert)
                : contract.Wrap(mediate, convert);
        }

        public static Func<Task<TResponse>> CallWrap<TResponse, TInput>(
            MediatedDataContract<TResponse> contract,
            IMediator mediator,
            Func<TInput, TInput> prep = null,
            Func<TInput, TResponse, Task<TResponse>> convert = null
            )
            where TResponse : class
            where TInput : class, IRequest<TResponse>, new()
        {
            Func<TInput, Task<TResponse>> mediate = async i =>
           {
               TResponse result;

               using (var tokenSource = new CancellationTokenSource())
               {
                   result = await mediator.Send(i, tokenSource.Token);
               }

               return result;
           };

            return prep != null
                ? contract.Wrap(prep, mediate, convert)
                : contract.Wrap(mediate, convert);
        }
    }
}
