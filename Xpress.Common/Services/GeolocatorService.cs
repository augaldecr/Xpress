using Plugin.Geolocator;
using System;
using System.Threading.Tasks;

namespace Xpress.Common.Services
{
    public class GeolocatorService : IGeolocatorService
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public async Task GetLocationAsync()
        {
            try
            {
                Plugin.Geolocator.Abstractions.IGeolocator locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                Plugin.Geolocator.Abstractions.Position location = await locator.GetPositionAsync();
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}