using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Slants.Services
{
    [ServiceContract]
    public interface ITopicsService
    {
        Task<IEnumerable<string>> GetTopicsAsync();
    }
}
