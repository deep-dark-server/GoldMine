namespace GoldMine.MainServer.ExceptionMessage
{
    /// <summary>
    /// Contains methods to build exception messages for ServiceLogic
    /// </summary>
    internal static class RegisterExceptionMessage
    {
        public static string ForUnauthorized(string detail) => $"Register host fail: Unauthorized User({detail})";
    }
}