using Microsoft.Extensions.Caching.Memory;
using ProtoBuf.Grpc;
using Slants.Services;

namespace Slants.WebApp.Server.Services
{
    public class SlantsService : ISlantsService
    {
        // if we deploy it in a load balanced scenario, we will need to use a distributed cache
        private readonly DateTime _appStart = DateTime.Now.Subtract(TimeSpan.FromDays(3));
        private readonly List<Author> _authors;
        private readonly List<Slant> _slants;
        private readonly ICurrentUserContext _currentUserContext;

        public SlantsService(IMemoryCache memoryCache)
        {
            _slants = memoryCache.GetOrCreate("slants", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return _initializeSlants();
            }) ?? new List<Slant>();

            _authors = memoryCache.GetOrCreate("authors", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return _initializeAuthors();
            }) ?? new List<Author>();

            _currentUserContext = new MockedUserContext(_slants.First().Author ?? new()); 
            // this currently has no way of being null, but the requirements of a slant have a nullable author
        }

        public Task<CreateSlantResponse> CreateSlantAsync(CreateSlantRequest request, CallContext context = default)
        {
            try
            {
                // make sure author exists, and create it if it doesn't
                var author = _authors.Where(a => a.Id == _currentUserContext.UserId).FirstOrDefault();
                if(author == null)
                {
                    author = new Author { Id = _currentUserContext.UserId, Name = _currentUserContext.UserName };
                    _authors.Add(author);
                }

                var slant = new Slant
                {
                    Id = Guid.NewGuid(),
                    Text = request.Text,
                    Topics = request.Topics,
                    Created = DateTime.Now,
                    Author = author
                };

                _slants.Add(slant);
                return Task.FromResult(new CreateSlantResponse { Success = true, Slant = slant });
            }catch(Exception x)
            {
                return Task.FromResult(new CreateSlantResponse { Success = false, ErrorMessage = x.Message });
            }
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
                Topics = new List<string> { "Sports" },
                Author = fanBoy
            });

            slants.Add(new Slant
            {
                Id = Guid.NewGuid(),
                Text = "Dallas Mavericks are the best NBA team.",
                Created = _appStart.AddHours(2),
                Topics = new List<string> { "Sports" },
                Author = fanBoy
            });

            var fanGirl = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Fan Girl"
            };

            slants.Add(new Slant
            {
                Id = Guid.NewGuid(),
                Text = "Kansas City Chief are the best NFL team.",
                Created = _appStart.AddHours(2),
                Topics = new List<string> { "Sports" },
                Author = fanGirl
            });

            return slants;
        }
    
        private List<Author> _initializeAuthors()
        {
            // initialize _slants first
            if(_slants == null)
                throw new InvalidOperationException("Slants is empty and it should already be initialzed.");

            // get all unique authors from _slants
            return _slants.Select(s => s.Author).Distinct().ToList();
        }
    }
}
