using ProtoBuf;

namespace Slants.Services
{
    [ProtoContract]
    public class CreateSlantResponse
    {
        [ProtoMember(1)]
        public bool Success { get; set; } = false;

        [ProtoMember(2)]
        public string? ErrorMessage { get; set; }

        [ProtoMember(3)]
        public Slant? Slant { get; set; }
    }
}
