namespace YummyDrop_online_store.Models
{
    /// <summary>
    /// The class describes an object of a loot box
    /// </summary>
    public class YummyItem
    {
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public double DropChance { get => _dropChance; set => _dropChance = value; }

        public decimal Cost { get => _cost; set => _cost = value; }

        public string Image { get => _image; set => _image = value; }

        private int _id;
        private string _name;
        private double _dropChance;
        private decimal _cost;
        private string _image;
    }
}
