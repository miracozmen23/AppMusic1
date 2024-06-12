namespace AppMusic1.RequestFeatures
{
    public class SongParameters:RequestParameters
    {
        public uint MinDuration { get; set; }
        public uint MaxDuration { get; set; } = 600;
        public bool ValidDurationRange => MaxDuration > MinDuration;

    }
}
