using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Slants.Services
{
    [ProtoContract]
    public class Slant
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }

        [ProtoMember(2)]
        public string Text { get; set; } = string.Empty;

        [ProtoMember(3)]
        public DateTime Created { get; set; }

        [ProtoMember(4)]
        public List<string> Tags { get; set; } = new List<string>();
    }
}
