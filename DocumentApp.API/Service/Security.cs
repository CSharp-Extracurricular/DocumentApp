namespace DocumentApp.API
{
    public class Security : ISecurity
    {
        public bool IsAuthenticated { get => true; }

        public Guid GetUserId() { return Guid.Empty; }
    }
}
