namespace DocumentApp.API
{
    public interface ISecurity
    {
        bool IsAuthenticated { get; }

        Guid GetUserId();
    }
}
