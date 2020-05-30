using Xpress.Common.Models;

namespace Xpress.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}