using System.Runtime.Serialization;

namespace contests_finder.Models
{
    [DataContract]
    public class ContestDefinition
    {
        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="type")]
        public string Type { get; set; }

        [DataMember(Name="phase")]
        public string Phase { get; set; }

        [DataMember(Name="durationSeconds")]
        public int DurationSeconds { get; set; }

        [DataMember(Name="startTimeSeconds")]
        public int StartTimeSeconds { get; set; }

        [DataMember(Name="relativeTimeSeconds")]
        public int RelativeTimeSeconds { get; set; }
    }
}