using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyDrink.Interfaces
{
    public interface IMyAPI
    {
        [Post("/api/user/registry")]
        Task<string> RegisterUser([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
        [Post("/api/user/login")]
        Task<string> LoginUser([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
    }
}
