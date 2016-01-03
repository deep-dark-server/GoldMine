namespace GoldMine.MainServer
{
    public partial class ServiceLogic
    {
        private class RegisterException
        {
            public static string GetUnauthorizedMessage(string detail)
            {
                return $"Register host fail: Unauthorized User({detail})";
            }
        }
    }
}