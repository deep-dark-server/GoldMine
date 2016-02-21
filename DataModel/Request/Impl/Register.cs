using System;

namespace GoldMine.DataModel.Request.Impl
{
    [Serializable]
    public enum ProtocolType : byte
    {
        Http = 0,
        Binary,
        Both
    }

	public class RequestRegister : IRequest
	{
		public short userId;
		public ProtocolType protocol;
	    public string accesskey;

	    public Guid AccessKey => Guid.Parse(accesskey);
	}
}