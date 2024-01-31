namespace XafCustomComponents
{
    public class TreeItem
    {
        public Guid Oid { get; set; }
        public string Name { get; set; }
        public IEnumerable<TreeItem> Children { get; set; }
    }
}
