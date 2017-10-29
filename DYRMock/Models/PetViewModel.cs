using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace DYRMock.Models
{
    public class PetViewModel
    {
        private PetBuilder _petBuilder;
        public Pet Pet { get; set; }
        public List<Pet> FeaturedPets { get; set; }
        public List<Pet> SearchResults { get; set; }

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
            SearchResults = new List<Pet>();
            PetTypesSelectItems = new SelectList(PetTypes, "Id", "TypeName");
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

        public void SetPetsByKeyword(string userLocation, int petTypeId, string breedType)
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
                            if (p.Facility.FullLocation.ToLower().Equals(userLocation.ToLower()))
                            {
                                SearchResults.Add(p);
                            }
                            else if (p.Facility.LocationState.ToLower().Equals(userLocation.ToLower()))
                            {
                                petsOutsideLocality.Add(p);
                            }
                        }
                    }

                    // Add a list of pets outside of local area last
                    if (petsOutsideLocality.Count() > 0)
                    {
                        SearchResults.AddRange(petsOutsideLocality);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Error: " + ex.Message);
            }
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
