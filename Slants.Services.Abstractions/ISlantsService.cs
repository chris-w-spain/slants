﻿using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Slants.Services
{
    [ServiceContract]
    public interface ISlantsService
    {
        Task<IEnumerable<Slant>> GetSlantsAsync();
        Task<IEnumerable<Slant>> GetFilteredSlantsAsync(GetFilteredSlantsRequest request, CallContext context = default);
    }
}