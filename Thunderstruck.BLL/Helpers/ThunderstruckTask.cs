using System;
using System.Threading.Tasks;

namespace Thunderstruck.BLL.Helpers
{
    public class ThunderstruckTask<T>
    {
        public static async Task<T> Try(Func<Task<T>> operation)
        {
            try
            {
                var result = await operation.Invoke();
                return await Task.FromResult<T>(result);
            }
            catch (Exception e)
            {
                return await Task.FromException<T>(e);
            }

        }
    }
}