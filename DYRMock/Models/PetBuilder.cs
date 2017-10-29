using System;
using System.Collections.Generic;
using System.Linq;

namespace DYRMock.Models
{
    public class PetBuilder
    {
        public List<Pet> Pets { get; set; }

        public PetBuilder(PetViewModel vm)
        {
            if (vm == null) vm = new PetViewModel();

            Pets = new List<Pet>()
                {
                    new Pet()
                    {
                        Id = 1, Name = "Leo", IsFeatured = true, PetType = vm.PetTypes.FirstOrDefault(pt => pt.Id == 1), PetSize = PetSize.LARGE,
                        PetAge = PetAge.YOUNG, PetGender = PetGender.FEMALE,
                        Breed = new Breed() { Id = 1, BreedType = "American Foxhound" },
                        Facility = new Facility() { Id = 1, FacilityType = FacilityType.SHELTER, Address = new Address() { LocationCity = "Manhattan", LocationState = "NY" }},
                        Behavior = new List<Characteristics>() { Characteristics.HOUSE_TRAINED, Characteristics.OK_WITH_DOGS },
                        MainPhotoUrl = "https://vetstreet.brightspotcdn.com/dims4/default/de9e04e/2147483647/thumbnail/645x380/quality/90/?url=https%3A%2F%2Fvetstreet-brightspot.s3.amazonaws.com%2F19%2Ff231d0a41b11e087a80050568d634f%2Ffile%2FAmerican-Foxhound-3-645mk062311.jpg",
                        ThumbnailUrls = new List<string>() { "http://cdn3-www.dogtime.com/assets/uploads/2011/01/file_23006_american-foxhound.jpg", "http://cdn.akc.org/akcdoglovers/AmericanFoxhound_hero.jpg" }
                    },
                    new Pet()
                    {
                        Id = 2, Name = "Cali", IsFeatured = true, PetType = vm.PetTypes.FirstOrDefault(pt => pt.Id == 1), PetSize = PetSize.SMALL,
                        PetAge = PetAge.YOUNG, PetGender = PetGender.MALE,
                        Breed = new Breed() { Id = 2, BreedType = "West Highland Terrier" },
                        Facility = new Facility() { Id = 2, FacilityType = FacilityType.CERTIFIED_BREEDER, Address = new Address() { LocationCity = "Manhattan", LocationState = "NY" }},
                        Behavior = new List<Characteristics>() { Characteristics.HOUSE_TRAINED, Characteristics.OK_WITH_CATS, Characteristics.OK_WITH_KIDS },
                        MainPhotoUrl = "http://cdn2-www.dogtime.com/assets/uploads/gallery/west-highland-white-terrier-dogs-and-puppies/west-highland-white-terrier-dogs-puppies-4.jpg",
                        ThumbnailUrls = new List<string>() { "https://upload.wikimedia.org/wikipedia/commons/2/2c/West_Highland_White_Terrier_Krakow.jpg", "https://fthmb.tqn.com/60ZeMI-7nIRlEuq16RshXHVeBYA=/960x0/filters:no_upscale()/West-Highland-White-Terrier-168553049-resized-56a26aa23df78cf77275606f.jpg" }
                    },
                    new Pet()
                    {
                        Id = 3, Name = "Melika", IsFeatured = true, PetType = vm.PetTypes.FirstOrDefault(pt => pt.Id == 2), PetSize = PetSize.LARGE,
                        PetAge = PetAge.ADULT, PetGender = PetGender.FEMALE,
                        Breed = new Breed() { Id = 1, BreedType = "Domestic Short Hair" },
                        Facility = new Facility() { Id = 3, FacilityType = FacilityType.SHELTER, Address = new Address() { LocationCity = "Indiana", LocationState = "PA" }},
                        Behavior = new List<Characteristics>() { Characteristics.SPAYED_NEUTERED, Characteristics.OK_WITH_DOGS },
                        MainPhotoUrl = "https://www.aspcapetinsurance.com/media/2242/8-tips-to-prevent-kidney-disease-in-cats.jpg",
                        ThumbnailUrls = new List<string>() { "https://www.aspcapetinsurance.com/media/2242/8-tips-to-prevent-kidney-disease-in-cats.jpg", "http://img-aws.ehowcdn.com/600x600p/photos.demandstudios.com/144/36/fotolia_12819661_XS.jpg" }
                    }
                };

            BuildFullLocation();
        }

        private void BuildFullLocation()
        {
            foreach (var p in Pets)
            {
                if (!String.IsNullOrEmpty(p.Facility.Address.LocationCity) && !String.IsNullOrEmpty(p.Facility.Address.LocationState))
                {
                    p.Facility.Address.FullLocation = p.Facility.Address.LocationCity + ", " + p.Facility.Address.LocationState;
                }
            }
        }
    }
}
