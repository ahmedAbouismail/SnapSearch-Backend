using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Snapsearch.Views;

namespace Snapsearch.ViewModels
{
    public class ResultsPageViewModel : BaseViewModel
    {
        public IList<ImageViewModel> Images { get; set; }
        

        public ResultsPageViewModel()
        {
            
            Images = new ObservableRangeCollection<ImageViewModel>()
            {
                new ImageViewModel()
                {
                    ImageName = "Image 1",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 2",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 3",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 4",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 5",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 6",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 7",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 8",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 9",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 10",
                    ImageUrl = "snapsearch_homepageimage",
                    MatchPercentage = 0
                },
            };

        }

    }
}
