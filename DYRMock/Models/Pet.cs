using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DYRMock.Models
{
    public enum PetSize { SMALL, MEDIUM, LARGE, XLARGE }
    public enum PetAge { BABY, YOUNG, ADULT, SENIOR }
    public enum PetGender { MALE, FEMALE }
    public enum Characteristics { HOUSE_TRAINED, SPAYED_NEUTERED, OK_WITH_DOGS, OK_WITH_CATS, OK_WITH_KIDS }

    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PetType PetType { get; set; }
        public int LocationPoint { get; set; }
        public bool IsFeatured { get; set; }
        public string MainPhotoUrl { get; set; }
        public List<string> ThumbnailUrls { get; set; }

        // Enums
        public PetSize PetSize { get; set; }
        public PetAge PetAge { get; set; }
        public PetGender PetGender { get; set; }
        public List<Characteristics> Behavior { get; set; }

        // Navigational properties
        public Facility Facility { get; set; }
        public Breed Breed { get; set; }
    }
}
