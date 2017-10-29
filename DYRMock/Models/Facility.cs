using System;
using GoogleMaps.LocationServices;

namespace DYRMock.Models
{
    public enum FacilityType { SHELTER, CERTIFIED_BREEDER }

    public class Facility
    {
        public int Id { get; set; }
        public FacilityType FacilityType { get; set; }
        public Address Address { get; set; }

        public Facility()
        {
            if (Address == null) Address = new Address();
        }

        public void SanitizeLocation(string fullLocation)
        {
            if (!String.IsNullOrEmpty(fullLocation))
            {
                if (fullLocation.Contains(","))
                {
                    string[] locationParts = fullLocation.Split(',');
                    if (!String.IsNullOrEmpty(locationParts[0]))
                        Address.LocationCity = locationParts[0];

                    if (!String.IsNullOrEmpty(locationParts[1]))
                        Address.LocationState = locationParts[1];
                }
                else
                {
                    // Just assign the full location to the location state property
                    Address.LocationState = fullLocation;
                }
            }
        }
    }
}
