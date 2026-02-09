public class TroughClaimManager : ClaimManager
{
    // Singleton magic
    private static TroughClaimManager instance;
    public static TroughClaimManager Instance => instance ??= new();
}