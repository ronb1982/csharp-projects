using System;
using System.Collections.Generic;

namespace DYRMock.Models
{
    public class Filter
    {
        public string Id { get; set; }
        public Dictionary<string, List<string>> FilterOptionsDict { get; set; }
        public const int MAX_COLUMNS_FILTER_TABLE = 5;

        public Filter()
        {
            if (FilterOptionsDict == null)
            {
                FilterOptionsDict = new Dictionary<string, List<string>>();
                SetFilterOptions();
            }
        }

        private void SetFilterOptions()
        {
            if (FilterOptionsDict == null) return;
            FilterOptionsDict.Add("Distance", new List<string>() { "Within 10 miles", "Within 25 miles", "Within 50 miles", "Within 100 miles" });
            FilterOptionsDict.Add("Size", new List<string>() { "Small", "Medium", "Large", "X-Large" });
            FilterOptionsDict.Add("Age", new List<string>() { "Baby", "Young", "Adult", "Senior" });
            FilterOptionsDict.Add("Gender", new List<string>() { "Male", "Female" });
            FilterOptionsDict.Add("Characteristics", new List<string>() { "House Trained", "Spayed / Neutered", "Ok with Dogs", "Ok with Cats", "Ok with Kids" });
        }
    }
}
