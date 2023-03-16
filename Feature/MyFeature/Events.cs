namespace MyFeature.Feature.MyFeature
{
    public class Events
    {
        public Guid idEvent { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Imgid { get; set; }
        public Guid Spaceid { get; set; }
        public List<Tiket> TiketList { get; set; }
        public bool IsOpen { get; set; }
    }
}
