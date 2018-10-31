using System.Collections.Generic;
using MyMessagePortal.Models;

namespace MyMessagePortal.ViewModels
{
    public class ObservedChannelsViewModel
    {
        public IEnumerable<ObservedChannelsModel> ObservedChannels { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int NumberOfPages { get; set; }
    }
}
