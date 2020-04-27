using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ContestFinder.Models
{
    [DataContract]
    public class CFResponse<T>
    {
        [DataMember(Name="status")]
        public string Status { get; set; }

        [DataMember(Name="comment")]
        public string Comment { get; set; }

        [DataMember(Name="result")]
        public T Result { get; set; }
    }
}