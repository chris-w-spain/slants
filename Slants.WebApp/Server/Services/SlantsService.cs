using Microsoft.Extensions.Caching.Memory;
using ProtoBuf.Grpc;
using Slants.Services;

namespace Slants.WebApp.Server.Services
{
    public class SlantsService : ISlantsService
    {
        // if we deploy it in a load balanced scenario, we will need to use a distributed cache
        private readonly DateTime _appStart = DateTime.Now.Subtract(TimeSpan.FromDays(3));
        private readonly List<Slant> _slants;
        private readonly ICurrentUserContext _currentUserContext;

        public SlantsService(IMemoryCache memoryCache)
        {
            _slants = memoryCache.GetOrCreate("slants", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return _initializeSlants();
            }) ?? new List<Slant>();

            _currentUserContext = new MockedUserContext(_slants.First().Author ?? new()); 
            // this currently has no way of being null, but the requirements of a slant have a nullable author
        }

        public Task<IEnumerable<Slant>> GetCurrentUserSlantsAsync()
        {
            var userSlants = _slants.Where(s => s.Author?.Id == _currentUserContext.UserId);
            return Task.FromResult(userSlants.AsEnumerable());
        }

        public Task<IEnumerable<Slant>> GetFilteredSlantsAsync(GetFilteredSlantsRequest request, CallContext context = default)
        {
            var filteredSlants = _slants.Where(s => s.Topics.Intersect(request.Tags).Any());
            return Task.FromResult(filteredSlants);
        }

        public Task<IEnumerable<Slant>> GetSlantsAsync()
        {
            return Task.FromResult(_slants.AsEnumerable());
        }

        private List<Slant> _initializeSlants()
        {
            List<Slant> slants = new();
            var fanBoy = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Fan Boy"
            };

            slants.Add(new Slant
            {
                Id = Guid.NewGuid(),
                Text = "Dallas Cowboys are my favorite NFL team.",
                Created = _appStart.AddHours(1),
                Topics = new List<string> { "Football", "Sport" },
                Author = fanBoy
            });

            slants.Add(new Slant
            {
                Id = Guid.NewGuid(),
                Text = "Dallas Mavericks are the best NBA team.",
                Created = _appStart.AddHours(2),
                Topics = new List<string> { "Basketball", "Sport" },
                Author = fanBoy
            });

            var fanGirl = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Fant Girl"
            };

            slants.Add(new Slant
            {
                Id = Guid.NewGuid(),
                Text = "Kansas City Chief are the best NFL team.",
                Created = _appStart.AddHours(2),
                Topics = new List<string> { "Football", "Sport" },
                Author = fanGirl
            });

            return slants;
        }
    }
}
