using System.Runtime.Serialization;

namespace GoldMine.DataModel.Request
{
    [DataContract]
    public enum ProtocolType
    {
        [EnumMember]
        Http = 0,

        [EnumMember]
        Binary,

        [EnumMember]
        Both
    }

	[DataContract]
	public class RequestRegister
	{
		[DataMember(Order = 1)]
		public string userId;
		[DataMember(Order = 2)]
		public ProtocolType protocol;
	}
}