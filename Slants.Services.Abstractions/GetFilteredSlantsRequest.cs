using System.Runtime.Serialization;

namespace Slants.Services
{
    [DataContract]
    public class GetFilteredSlantsRequest
    {
        [DataMember(Order = 1)]
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
    }
}
