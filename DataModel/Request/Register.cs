namespace GoldMine.DataModel.Request
{
    public enum ProtocolType
    {
        Http = 0,
        Binary,
        Both
    }

	public class RequestRegister
	{
		public string userId;
		public ProtocolType protocol;
	}
}