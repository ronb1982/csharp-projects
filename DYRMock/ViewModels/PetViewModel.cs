using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using DYRMock.Models;
using GoogleMaps.LocationServices;

namespace DYRMock.Models
{
    public class PetViewModel
    {
        private PetBuilder _petBuilder;
        private GoogleLocationService LocationService { get; set; }

        // Defines the maximum distance in miles that a user's search will show if no specific facilities/pets were found in the local area.
        public int MAX_DISTANCE_IN_MILES { get; private set; } = 100;
        private const double RADIUS_EARTH_IN_MILES = 3963.0;

        public Pet Pet { get; set; }
        public User User { get; set; }
        public Filter Filter { get; set; }
        public List<Pet> FeaturedPets { get; set; }
        public List<Pet> SearchResults { get; set; }
        public string MsgNoResultsFound { get; private set; }

        public List<PetType> PetTypes = new List<PetType>()
        {
            new PetType() { Id = 1, TypeName = "Dog" },
            new PetType() { Id = 2, TypeName = "Cat" },
            new PetType() { Id = 3, TypeName = "Other" }
        };

        public int SelectedPetId { get; set; }
        public string SelectedPetType { get; set; }
        public SelectList PetTypesSelectItems { get; set; }

        public PetViewModel()
        {
            MsgNoResultsFound = String.Empty;

            if (SearchResults == null)
                SearchResults = new List<Pet>();

            if (User == null)
                User = new User();

            if (PetTypesSelectItems == null)
                PetTypesSelectItems = new SelectList(PetTypes, "Id", "TypeName");

            if (Filter == null)
                Filter = new Filter();

            _petBuilder = new PetBuilder(this);
            if (_petBuilder != null) SetFeaturedPets();
        }

        private void SetFeaturedPets()
        {
            FeaturedPets = new List<Pet>();

            if (_petBuilder.Pets != null && _petBuilder.Pets.Count > 0)
            {
                foreach (var p in _petBuilder.Pets)
                {
                    if (p != null && p.IsFeatured)
                    {
                        FeaturedPets.Add(p);
                    }
                }
            }
        }

        public void SetPetsByKeyword(int petTypeId, string breedType)
        {
            try
            {
                if (_petBuilder.Pets != null && _petBuilder.Pets.Count > 0)
                {
                    SelectedPetType = PetTypes.FirstOrDefault(pt => pt.Id == petTypeId).TypeName;
                    List<Pet> petsOutsideLocality = new List<Pet>();

                    foreach (var p in _petBuilder.Pets)
                    {
                        if (p != null && IsValidPetSearch(p, SelectedPetType, breedType))
                        {
                            if (p.Facility.Address.FullLocation.ToLower().Equals(User.Address.FullLocation.ToLower()))
                            {
                                SearchResults.Add(p);
                            }
                            else if (User.Address.FullLocation.ToLower().Contains(p.Facility.Address.LocationState.ToLower()) ||
                                     IsWithinGivenMileRadius(p))
                            {
                                petsOutsideLocality.Add(p);
                            }
                        }
                    }

                    // If no local search results were found, display a message.
                    if (SearchResults.Count() == 0)
                    {
                        MsgNoResultsFound = String.Format("Sorry, we couldn't find any {0} in your city.", SelectedPetType);
                    }

                    // Add pets outside local area to main SearchResults list, and display a special message.
                    if (petsOutsideLocality.Count() > 0)
                    {
                        SearchResults.AddRange(petsOutsideLocality);
                        MsgNoResultsFound += String.Format(" However, we've found {0} within {1} of your area.", SelectedPetType, MAX_DISTANCE_IN_MILES);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error: " + ex.Message);
            }
        }

        private bool IsWithinGivenMileRadius(Pet p)
        {
            bool isWithinRange = false;

            if (p != null)
            {                
                try
                {
                    LocationService = new GoogleLocationService();
                    var distance = GetDistanceInMiles(p);

                    if (distance >= 0 && distance <= MAX_DISTANCE_IN_MILES)
                    {
                        isWithinRange = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write("Error: " + ex.Message);
                }
            }

            return isWithinRange;
        }

        private double GetDistanceInMiles(Pet p)
        {
            double d = 0.0; // distance
            if (p != null && User != null)
            {
                // Get latitudes and longitudes of pet and user
                var petLatLong = LocationService.GetLatLongFromAddress(p.Facility.Address.FullLocation);
                var userLatLong = LocationService.GetLatLongFromAddress(User.Address.FullLocation);

                var sinLats = Math.Sin(Radians(petLatLong.Latitude)) * Math.Sin(Radians(userLatLong.Latitude));
                var cosLats = Math.Cos(Radians(petLatLong.Latitude)) * Math.Cos(Radians(userLatLong.Latitude));
                var cosLong = Math.Cos(Radians(petLatLong.Longitude) - Radians(userLatLong.Longitude));
                var cosD = sinLats + (cosLats * cosLong);
                d = RADIUS_EARTH_IN_MILES * Math.Acos(cosD);
            }

            return d;
        }

        // Convert degrees to radians
        private static double Radians(double degrees)
        {
            return (degrees * Math.PI) / 180;
        }


        // Returns true if the user's search contains a valid location, and a valid breed type (or no breed type at all)
        private bool IsValidPetSearch(Pet p, string selectedPetType, string breedType)
        {
            bool isValidSearch = false;

            if (p.PetType.TypeName.ToLower().Equals(SelectedPetType.ToLower()) &&
                (String.IsNullOrEmpty(breedType) || breedType.ToLower().Contains(p.Breed.BreedType.ToLower()) ||
                 p.Breed.BreedType.ToLower().Contains(breedType.ToLower())))
            {
                isValidSearch = true;
            }
            return isValidSearch;
        }
    }
}
