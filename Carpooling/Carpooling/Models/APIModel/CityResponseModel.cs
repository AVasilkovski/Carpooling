namespace Carpooling.Web.Models.APIModel
{
    public class CityResponseModel
    {
        public CityResponseModel(string city)
        {
            City = city;
        }

        public string City { get; set; }
    }
}
