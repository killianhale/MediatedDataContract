using System;
using System.Threading.Tasks;
using AutoMapper;

namespace MediatedDataContract
{
    public class DataContract<TRequest, TResponse>
        where TRequest : class where TResponse : class
    {
        private readonly IMapper _mapper;

        public DataContract(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region TInput and TOutput

        public Func<TRequest, Task<TResponse>> Wrap<TInput, TOutput>(
            Func<TRequest, TInput, Task<TRequest>> prep,
            Func<TInput, Task<TOutput>> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            async Task<TResponse> result(TRequest request)
            {
                var domainObj = _mapper.Map<TInput>(request);
                await prep?.Invoke(request, domainObj);

                var output = await action?.Invoke(domainObj);

                var response = _mapper.Map<TResponse>(output);
                response = await convert?.Invoke(request, output, response);

                return response;
            }

            return result;
        }

        public Func<TRequest, Task<TResponse>> Wrap<TInput, TOutput>(
            Func<TInput, Task<TOutput>> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            return Wrap(action, convert);
        }


        public Func<TRequest, Task<TResponse>> Wrap<TInput, TOutput>(
            Func<TRequest, TInput, TRequest> prep,
            Func<TInput, Task<TOutput>> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            async Task<TResponse> result(TRequest request)
            {
                var domainObj = _mapper.Map<TInput>(request);
                prep?.Invoke(request, domainObj);

                var output = await action?.Invoke(domainObj);

                var response = _mapper.Map<TResponse>(output);
                response = await convert?.Invoke(request, output, response);

                return response;
            }

            return result;
        }

        public Func<TRequest, Task<TResponse>> Wrap<TInput, TOutput>(
            Func<TInput, TOutput> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            return Wrap(action, convert);
        }

        #endregion

        #region TInput only

        public Func<TRequest, Task<TResponse>> Wrap<TInput>(
            Func<TRequest,TInput, Task<TRequest>> prep,
            Func<TInput, Task<TResponse>> action,
            Func<TRequest, TResponse, Task<TResponse>> convert = null
            )
        {
            async Task<TResponse> result(TRequest request)
            {
                var domainObj = _mapper.Map<TInput>(request);
                await prep?.Invoke(request, domainObj);

                var output = await action?.Invoke(domainObj);

                var response = await convert?.Invoke(request, output);

                return output;
            }

            return result;
        }

        public Func<TRequest, Task<TResponse>> Wrap<TInput>(
            Func<TInput, Task<TResponse>> action,
            Func<TRequest, TResponse, Task<TResponse>> convert = null
            )
        {
            return Wrap(action, convert);
        }

        public Func<TRequest, Task<TResponse>> Wrap<TInput>(
            Func<TRequest,TInput, TRequest> prep,
            Func<TInput, Task<TResponse>> action,
            Func<TRequest, TResponse, Task<TResponse>> convert = null
            )
        {
            async Task<TResponse> result(TRequest request)
            {
                var domainObj = _mapper.Map<TInput>(request);
                prep?.Invoke(request, domainObj);

                var output = await action?.Invoke(domainObj);

                var response = await convert?.Invoke(request, output);

                return output;
            }

            return result;
        }

        public Func<TRequest, Task<TResponse>> Wrap<TInput>(
            Func<TInput, TResponse> action,
            Func<TRequest, TResponse, Task<TResponse>> convert = null
            )
        {
            return Wrap(action, convert);
        }

        #endregion

        #region

        public Func<TRequest, Task<TResponse>> Wrap<TOutput>(
            Func<TRequest, Task<TRequest>> prep,
            Func<TRequest, Task<TOutput>> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            async Task<TResponse> result(TRequest request)
            {
                request = await prep?.Invoke(request);

                var output = await action?.Invoke(request);

                var response = _mapper.Map<TResponse>(output);

                response = await convert?.Invoke(request, output, response);

                return response;
            }

            return result;
        }

        public Func<TRequest, Task<TResponse>> Wrap<TOutput>(
            Func<TRequest, Task<TOutput>> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            return Wrap<TOutput>(action, convert);
        }

        public Func<TRequest, Task<TResponse>> Wrap<TOutput>(
            Func<TRequest, TRequest> prep,
            Func<TRequest, Task<TOutput>> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            async Task<TResponse> result(TRequest request)
            {
                request = prep?.Invoke(request);

                var output = await action?.Invoke(request);

                var response = _mapper.Map<TResponse>(output);

                response = await convert?.Invoke(request, output, response);

                return response;
            }

            return result;
        }

        public Func<TRequest, Task<TResponse>> Wrap<TOutput>(
            Func<TRequest, TOutput> action,
            Func<TRequest, TOutput, TResponse, Task<TResponse>> convert = null
            )
        {
            return Wrap<TOutput>(action, convert);
        }

        #endregion
    }

    public class DataContract<TResponse>
        where TResponse : class
    {
        private readonly IMapper _mapper;

        public DataContract(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region TInput and TOutput

        public Func<Task<TResponse>> Wrap<TInput, TOutput>(
            Func<TInput, Task<TInput>> prep,
            Func<TInput, Task<TOutput>> action,
            Func<TInput, TOutput, TResponse, Task<TResponse>> convert = null
            )
            where TInput : class, new()
        {
            async Task<TResponse> result()
            {
                var domainObj = new TInput();
                domainObj = await prep?.Invoke(domainObj);

                var output = await action?.Invoke(domainObj);

                var response = _mapper.Map<TResponse>(output);
                response = await convert?.Invoke(domainObj, output, response);

                return response;
            }

            return result;
        }

        public Func<Task<TResponse>> Wrap<TInput, TOutput>(
            Func<TInput, Task<TOutput>> action,
            Func<TInput, TOutput, TResponse, Task<TResponse>> convert = null
            )
            where TInput : class, new()
        {
            return Wrap(action, convert);
        }

        public Func<Task<TResponse>> Wrap<TInput, TOutput>(
            Func<TInput, TInput> prep,
            Func<TInput, Task<TOutput>> action,
            Func<TInput, TOutput, TResponse, Task<TResponse>> convert = null
            )
            where TInput : class, new()
        {
            async Task<TResponse> result()
            {
                var domainObj = new TInput();
                domainObj = prep?.Invoke(domainObj);

                var output = await action?.Invoke(domainObj);

                var response = _mapper.Map<TResponse>(output);
                response = await convert?.Invoke(domainObj, output, response);

                return response;
            }

            return result;
        }

        #endregion

        #region TInput only

        public Func<Task<TResponse>> Wrap<TInput>(
            Func<TInput, Task<TInput>> prep,
            Func<TInput, Task<TResponse>> action,
            Func<TInput, TResponse, Task<TResponse>> convert = null
            )
            where TInput : class, new()
        {
            async Task<TResponse> result()
            {
                var domainObj = new TInput();
                domainObj = await prep?.Invoke(domainObj);

                var output = await action?.Invoke(domainObj);

                var response = await convert?.Invoke(domainObj, output);

                return response;
            }

            return result;
        }

        public Func<Task<TResponse>> Wrap<TInput>(
            Func<TInput, Task<TResponse>> action,
            Func<TInput, TResponse, Task<TResponse>> convert = null
            )
            where TInput : class, new()
        {
            return Wrap(action, convert);
        }

        public Func<Task<TResponse>> Wrap<TInput>(
            Func<TInput, TInput> prep,
            Func<TInput, Task<TResponse>> action,
            Func<TInput, TResponse, Task<TResponse>> convert = null
            )
            where TInput : class, new()
        {
            async Task<TResponse> result()
            {
                var domainObj = new TInput();
                domainObj = prep?.Invoke(domainObj);

                var output = await action?.Invoke(domainObj);

                var response = await convert?.Invoke(domainObj, output);

                return response;
            }

            return result;
        }

        #endregion
    }
}
