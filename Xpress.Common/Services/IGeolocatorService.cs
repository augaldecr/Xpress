using System.Threading.Tasks;

namespace Xpress.Common.Services
{
    public interface IGeolocatorService
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
        Task GetLocationAsync();
    }
}