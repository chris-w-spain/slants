namespace Slants.WebApp.Server.Services
{
    public class MockedUserContext : ICurrentUserContext
    {
        /// <summary>
        /// uses author to create a mocked user context because we don't currently have users in the system, 
        /// that will come with security
        /// </summary>
        /// <param name="author"></param>
        public MockedUserContext(Slants.Services.Author author)
        {
            UserId = author.Id;
            UserName = author.Name;
        }

        public Guid UserId { get; private set; }

        public string UserName { get; private set; }
    }
}
