using ProtoBuf;

namespace Slants.Services
{
    [ProtoContract]
    public class Author
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; } = string.Empty;
    }
}
