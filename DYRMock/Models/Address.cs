using System;
using GoogleMaps.LocationServices;

namespace DYRMock.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string FullLocation { get; set; } // holds full location information including city and state
        public string LocAddressLine1 { get; set; }
        public string LocAddressLine2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }
        public MapPoint LatLong { get; set; } // holds latitude and longtitude for this facility
    }
}
