public class NestClaimManager : ClaimManager
{
    // Singleton magic
    private static NestClaimManager instance;
    public static NestClaimManager Instance => instance ??= new();
}