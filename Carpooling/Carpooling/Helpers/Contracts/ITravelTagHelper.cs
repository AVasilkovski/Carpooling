using Microsoft.AspNetCore.Mvc.Rendering;

namespace Carpooling.Web.Helpers.Contracts
{
    public interface ITravelTagHelper
    {
        SelectList ListTags();
    }
}
