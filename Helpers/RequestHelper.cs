using CoolGuys.UseCases._contracts;
using Flurl.Http;
using Newtonsoft.Json;

namespace CoolGuys.Helpers;

public class RequestHelper
{
    public static async Task<T> HandleRequest<T>(Func<Task<T>> action, Func<Exception, T> unexpectedError)
    {
        try
        {
            return await action();
        }
        catch (Exception e)
        {
            if (!(e is FlurlHttpException ex))
            {
                return unexpectedError(e);
            }

            if (ex.StatusCode == null)
            {
                return unexpectedError(e);
            }

            if (ex.InnerException is JsonSerializationException)
            {
                return unexpectedError(e);
            }
            var error = await ex.GetResponseJsonAsync<ErrorDto>();
            throw new Exception(error.detail);
        }
    }
        
    public static async Task HandleRequest(Func<Task> action, Action<Exception> unexpectedError)
    {
        try
        {
            await action();
        }
        catch (Exception e)
        {
            if (!(e is FlurlHttpException ex))
            {
                unexpectedError(e);
                return;
            }

            if (ex.StatusCode == null)
            {
                unexpectedError(e);
                return;
            }
                
            if (ex.InnerException is JsonSerializationException)
            {
                unexpectedError(e);
                return;
            }

            var error = await ex.GetResponseJsonAsync<ErrorDto>();
            throw new Exception(error.detail);
        }
    }
}