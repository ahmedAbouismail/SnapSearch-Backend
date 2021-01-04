using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Snapsearch.Views;

namespace Snapsearch.ViewModels
{
    // this class updates the images in the ui
    public class ImageViewModel : ObservableObject
    {
        private string image;
        // Do the images contain names in the database??!
        public string ImageName { get; set; }
        // Image url from the API
        public string ImageUrl { get; set; }


        // Does the API also send match percentages for images??!
        public decimal MatchPercentage { get; set; }

        //todo - extend with additional properties later
    }

}
