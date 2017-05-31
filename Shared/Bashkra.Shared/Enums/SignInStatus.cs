namespace Bashkra.Shared.Enums
{
    public enum SignInStatus
    {
        InvalidLoginOrPassword = 0,
        Success = 1,
        LockedOut = 2,
        RequiresVerification = 3,
        Failure = 4
    }
}