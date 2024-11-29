namespace MovieMicroservice.Models
{
    public class YoutubeSearchResponse
    {
        public List<Item> Items { get; set; } = new();

        public class Item
        {
            public Id Id { get; set; } = new();
        }

        public class Id
        {
            public string VideoId { get; set; }
        }
    }
}
