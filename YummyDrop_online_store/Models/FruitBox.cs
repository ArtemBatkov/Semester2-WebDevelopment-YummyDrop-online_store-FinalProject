namespace YummyDrop_online_store.Models
{
    public class FruitBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<YummyItem> BoxContent1 { get; set; }
    }
}
