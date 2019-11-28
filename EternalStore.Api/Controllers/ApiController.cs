using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EternalStore.Api.Extensions
{
    public class ApiController : ControllerBase
    {
        public async void TryCatchDefault<T>(Func<T> func)
        {
            try
            {
                await Task.Run(func);
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
