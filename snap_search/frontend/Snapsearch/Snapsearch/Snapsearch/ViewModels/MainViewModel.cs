using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

namespace Snapsearch.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IList<ImageViewModel> Images { get; set; }

        public MainViewModel()
        {
            Images = new ObservableRangeCollection<ImageViewModel>()
            {
                new ImageViewModel()
                {
                    ImageName = "Image 1",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 2",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 3",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 4",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 5",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 6",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 7",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 8",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 9",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 10",
                    ImageUrl = "",
                    MatchPercentage = 0
                },
            };

        }
    }
}
