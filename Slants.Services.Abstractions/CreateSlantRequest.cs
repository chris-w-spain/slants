using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slants.Services
{
    [ProtoContract]
    public class CreateSlantRequest
    {
        [ProtoMember(1)]
        public string Text { get; set; } = string.Empty;

        [ProtoMember(2)]
        public List<string> Topics { get; set; } = new List<string>();
    }
}
