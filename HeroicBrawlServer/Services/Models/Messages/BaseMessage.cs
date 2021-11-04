using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HeroicBrawlServer.Services.Models.Messages
{
    /// <summary>
    ///     Base class for realtime messages
    /// </summary>
    public class BaseMessage
    {

        /// <summary>
        ///     Returns serialized message
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() } });
        }
    }
}
