using System;
using System.Threading.Tasks;

namespace Reviso.Common.HttpAccess
{
    public interface IApiClient
    {
        Task<TOutputType> GetAsync<TOutputType>(string requestUri);
        Task<TOutputType> PostAsync<TInputType, TOutputType>(string requestUri, TInputType input);
        Task<TOutputType> PutAsync<TInputType, TOutputType>(string requestUri, TInputType input);
        Task<TOutputType> DeleteAsync<TOutputType>(string requestUri);
    }
}
