using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Carpooling.Web.Helpers
{
    public class TravelTagHelper : ITravelTagHelper
    {
        private readonly ITravelTagService travelTagService;

        public TravelTagHelper(ITravelTagService travelTagService)
        {
            this.travelTagService = travelTagService;
        }

        public SelectList ListTags()
        {
            var tags = this.travelTagService.GetAll();
            var list = new SelectList(tags, "Tag", "Tag");
            return list;
        }
    }
}
