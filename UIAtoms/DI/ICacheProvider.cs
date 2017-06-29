using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroSpeech.UIAtoms.DI
{
    public interface ICacheProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string key, CancellationToken cancellationToken);


        /// <summary>
        /// Since it will consume the content of the message, it must return message with a copy of content
        /// </summary>
        /// <param name="key"></param>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync(string key, HttpResponseMessage r, CancellationToken cancellationToken);
        
    }

    public interface IAtomCacheProvider {

        Task<TResponse> GetAsync<TResponse>(
            string key, 
            Func<CancellationToken,Task<TResponse>> getMethod,
            CancellationToken cancellationToken,
            Func<TResponse,TimeSpan?> cacheTTL = null)
            where TResponse:class;

    }


    public class AtomCacheProvider : IAtomCacheProvider
    {

        public static AtomCacheProvider Instance = new AtomCacheProvider();

        private Dictionary<string, List<InternalRequest>> requests = 
            new Dictionary<string, List<InternalRequest>>();

        public async Task<TResponse> GetAsync<TResponse>(string key, 
            Func<CancellationToken,Task<TResponse>> getMethod,
            CancellationToken cancellationToken, 
            Func<TResponse, TimeSpan?> cacheTTL = null)
            where TResponse:class
        {
            var itr = new InternalRequest {
                Token = cancellationToken
            };

            List<InternalRequest> request = null;
            lock (requests)
            {
                if (!requests.TryGetValue(key, out request)) {
                    request = new List<InternalRequest>();
                    requests.Add(key, request);
                }
                lock (request)
                {
                    if (request.Count != 0)
                    {
                        var ts = new TaskCompletionSource<TResponse>();

                        itr.TaskSource = ts;
                    }
                    request.Add(itr);
                }
            }

            TResponse r = null;

            if (itr.TaskSource == null)
            {
                r = await getMethod(cancellationToken);

                lock (requests)
                {
                    lock (request)
                    {
                        foreach (var rq in request)
                        {
                            if (rq.TaskSource == null)
                                continue;
                            if (rq.Token.IsCancellationRequested)
                            {
                                rq.AsTaskSource<TResponse>().TrySetCanceled();
                            }
                            else
                            {
                                rq.AsTaskSource<TResponse>().TrySetResult(r);
                            }
                        }
                        request.Clear();
                    }

                    requests.Remove(key);
                }
            }
            else
            {
                r = await itr.AsTaskSource<TResponse>().Task;
            }

            return r;
        }

        public class InternalRequest {
            public CancellationToken Token { get; set; }
            public object TaskSource { get; set; }

            public TaskCompletionSource<TResult> AsTaskSource<TResult>() {
                return (TaskCompletionSource<TResult>)TaskSource;
            }
        }

    }



}
