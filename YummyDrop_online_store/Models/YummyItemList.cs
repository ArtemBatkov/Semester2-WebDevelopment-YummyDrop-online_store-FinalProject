namespace YummyDrop_online_store.Models
{
    public class YummyItemList
    {
        public static List<YummyItem> Yummys = new List<YummyItem>
        {
            new YummyItem { Id = 1 , Name = "Apple", DropChance = 60, Cost = 1.99M, Image = "https://s34540.pcdn.co/wp-content/uploads/sites/2/2019/08/apple-food-fruit.jpg.optimal.jpg" },
            new YummyItem { Id = 2, Name = "Orange", DropChance = 15, Cost = 5.5M, Image = "https://thumbs.dreamstime.com/b/orange-fruit-white-background-36051901.jpg" },
            new YummyItem { Id = 3, Name = "Banana", DropChance = 10, Cost = 15.4M, Image = "https://i5.walmartimages.ca/images/Enlarge/580/6_r/875806_R.jpg" },
            new YummyItem { Id = 4, Name = "Strawberry", DropChance = 7.5, Cost = 35M , Image = "https://th-thumbnailer.cdn-si-edu.com/QSKrObWP3MafKopqbKW1H0giKHY=/fit-in/1600x0/https%3A%2F%2Ftf-cmsv2-smithsonianmag-media.s3.amazonaws.com%2Ffiler%2F39%2F3c%2F393c51d9-ce11-49ce-9d41-5ef599dfabea%2Fbn8e34.jpg"},
            new YummyItem { Id = 5, Name = "Graps", DropChance = 4.5, Cost = 78.33M, Image ="https://static.libertyprim.com/files/familles/raisin-large.jpg?1569271841" },
            new YummyItem { Id = 6, Name = "Lemon", DropChance = 3, Cost = 199.99M, Image = "https://www.herbazest.com/imgs/3/f/2/9710/lemon.jpg"},
        };
    }
}
