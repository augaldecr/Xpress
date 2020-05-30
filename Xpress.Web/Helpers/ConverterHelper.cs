using Xpress.Common.Models;
using Xpress.Web.Data;

namespace Xpress.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public UserResponse ToUserResponse(User user)
        {
            if (user == null)
            {
                return null;
            }
            return new UserResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                //UserType = user.UserType
            };
        }
    }
}
