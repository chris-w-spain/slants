using Slants.Services;

namespace Slants.WebApp.Server.Services
{
    public class TopicsService : ITopicsService
    {
        // add list of 100 popular topics comment on
        private readonly List<string> _topics = new List<string>
        {
            "Politics","Sports","Entertainment","News","Business","Technology","Science","Health","Education","Travel","Food","Art","Music","Movies","Books","Fashion","Fitness","Gaming","History","Home","Humor","Lifestyle","Nature","Parenting","Photography","Relationships","Religion","Shopping","Social Media","Space","Television","Transportation","Weather"
        };

        public Task<IEnumerable<string>> GetTopicsAsync()
        {
            return Task.FromResult(_topics.AsEnumerable());
        }
    }
}
