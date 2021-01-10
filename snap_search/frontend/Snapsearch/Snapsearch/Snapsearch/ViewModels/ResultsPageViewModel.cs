using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using MvvmHelpers;
using Snapsearch.Views;
using Xamarin.Forms;

namespace Snapsearch.ViewModels
{
    public class ResultsPageViewModel : BaseViewModel
    {
        public IList<ImageViewModel> Images { get; set; }


        public static IList<string> CbirLinksList = new List<string>();

        

        public ResultsPageViewModel()
        {


            Images = new ObservableRangeCollection<ImageViewModel>()
            {

               
                new ImageViewModel()
                {
                    ImageName = "Image 1",
                    ImageUrl = CbirLinksList[0],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 2",
                    ImageUrl = CbirLinksList[1],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 3",
                    ImageUrl = CbirLinksList[2],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 4",
                    ImageUrl = CbirLinksList[0],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 5",
                    ImageUrl = CbirLinksList[1],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 6",
                    ImageUrl = CbirLinksList[2],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 7",
                    ImageUrl = CbirLinksList[0],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 8",
                    ImageUrl = CbirLinksList[1],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 9",
                    ImageUrl = CbirLinksList[2],
                    MatchPercentage = 0
                },
                new ImageViewModel()
                {
                    ImageName = "Image 10",
                    ImageUrl = CbirLinksList[0],
                    MatchPercentage = 0
                },
            };

        }

    }
}
