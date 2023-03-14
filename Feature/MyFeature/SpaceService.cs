namespace MyFeature.Feature.MyFeature
{
    public class SpaceService
    {
        private readonly List<Space> _spaces;

        public SpaceService()
        {
            _spaces = new List<Space>();
        }

        public void AddSpace(Space space)
        {
            _spaces.Add(space);
        }

        public bool SpaceExists(Guid spaceId)
        {
            return _spaces.Any(s => s.Id == spaceId);
        }
    }

    public class Space
    {
        public Guid Id { get; set; }
    }
}
