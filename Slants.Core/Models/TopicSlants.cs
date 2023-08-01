using Slants.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slants.Core.Models
{
    public class TopicSlants
    {
        public string Topic { get; set; } = default!;
        public IEnumerable<Slant> Slants { get; set; } = default!;
    }
}
