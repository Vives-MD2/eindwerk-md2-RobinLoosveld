using System.Collections.Generic;

namespace Thunderstruck.UI.Api
{
    public class ApiMultiResponse<T>
    {
        public IEnumerable<T> Value { get; set; }
    }
}