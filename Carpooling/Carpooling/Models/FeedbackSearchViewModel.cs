namespace Carpooling.Web.Models
{
    public class FeedbackSearchViewModel : FeedbackViewModel 
    {
        public int UserId { get; set; }
         
        public string Comment { get; set; }

        public double Rating { get; set; }
    }
}
