using ProtoBuf.Grpc;
using Slants.Services;

namespace Slants.WebApp.Server.Services
{
    public class SlantsService : ISlantsService
    {
        private readonly List<Slant> _slants = new List<Slant>();

        public SlantsService()
        {
            InitializeSlants();
        }

        public Task<IEnumerable<Slant>> GetFilteredSlantsAsync(GetFilteredSlantsRequest request, CallContext context = default)
        {
            var _filteredSlants = _slants.Where(s => s.Tags.Intersect(request.Tags).Any());
            return Task.FromResult(_filteredSlants);
        }

        public Task<IEnumerable<Slant>> GetSlantsAsync()
        {
            return Task.FromResult(_slants.AsEnumerable());
        }

        private void InitializeSlants()
        {
            _slants.Add(new Slant
            {
                Id = Guid.NewGuid(),
                Text = "Dallas Cowboys are my favorite NFL team.",
                Created = DateTime.Now,
                Tags = new List<string> { "Football", "Sport" }
            });
        }
    }
}
