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
}