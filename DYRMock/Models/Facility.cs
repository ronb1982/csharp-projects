using System;
namespace DYRMock.Models
{
    public enum FacilityType { SHELTER, CERTIFIED_BREEDER }

    public class Facility
    {
        public int Id { get; set; }
        public FacilityType FacilityType { get; set; }
        public string FullLocation { get; set; } // holds full location information including city and state
        public string LocationCity { get; set; }
        public string LocationState { get; set; }

        public void SanitizeLocation(string fullLocation)
        {
            if (!String.IsNullOrEmpty(fullLocation))
            {
                if (FullLocation.Contains(","))
                {
                    string[] locationParts = FullLocation.Split(',');
                    if (!String.IsNullOrEmpty(locationParts[0]))
                        LocationCity = locationParts[0];

                    if (!String.IsNullOrEmpty(locationParts[1]))
                        LocationState = locationParts[1];
                }
            }
        }
    }
}
