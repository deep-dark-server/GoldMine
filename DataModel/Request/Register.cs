namespace GoldMine.DataModel.Request
{
    public enum ProtocolType : byte
    {
        Http = 0,
        Binary,
        Both
    }

	public class RequestRegister
	{
		public short userId;
		public ProtocolType protocol;
	}
}