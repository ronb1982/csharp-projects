using System;
using GoogleMaps.LocationServices;

namespace DYRMock.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        public User()
        {
            if (Address == null) Address = new Address();
        }

        // Explicitly passes in the city and state that the user has entered into the search form, and sets these values as the user's city and state.
        public void SetUserLocation(string searchFullLocation, string searchCity, string searchState)
        {
            if (Address != null)
            {
                if (!String.IsNullOrEmpty(searchFullLocation))
                    Address.FullLocation = searchFullLocation;
                
                if (!String.IsNullOrEmpty(searchCity))
                    Address.LocationCity = searchCity;

                if (!String.IsNullOrEmpty(searchState))
                    Address.LocationState = searchState;
            }
        }
    }
}
