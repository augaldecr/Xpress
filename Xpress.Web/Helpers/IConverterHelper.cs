using Xpress.Common.Models;
using Xpress.Web.Data;

namespace Xpress.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(User user);
    }
}