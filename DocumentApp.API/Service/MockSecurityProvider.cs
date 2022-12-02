namespace DocumentApp.API
{
    public class MockSecurityProvider : ISecurity
    {
        public bool IsAuthenticated { get => true; }

        public Guid GetUserId() => Guid.Empty;
    }
}
