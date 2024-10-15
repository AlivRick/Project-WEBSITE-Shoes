namespace Project_WEBSITE.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Product Product { get; set; }
    }
}
