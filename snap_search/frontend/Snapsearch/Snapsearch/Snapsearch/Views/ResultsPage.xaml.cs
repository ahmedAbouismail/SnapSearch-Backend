using Snapsearch.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace Snapsearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();

            PickedImage.Source = ResultsPageViewModel.PhotoPath;

        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    SizeChanged += MainPage_SizeChanged;
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();

        //    SizeChanged -= MainPage_SizeChanged;

       
        //}

        private const int Margin = 10;

        protected override void OnSizeAllocated(double width, double height)
        {
            
            base.OnSizeAllocated(width, height);

            //Rectangle gradientRect = new Rectangle(
            //    x: 1,
            //    y: 1,
            //    width: width,
            //    height: height);
            //AbsoluteLayout.SetLayoutBounds(BottomGradient, gradientRect);

            BottomGradient.HeightRequest = height;
            BottomGradient.WidthRequest = width;

            // set the position of all the screen elements
            //Logo Image
            Rectangle logoRect = new Rectangle(
                x: width / 2 - LogoImage.Width / 2,
                y: 1,
                width: 0,
                height: 0);
            AbsoluteLayout.SetLayoutBounds(LogoImage, logoRect);

            // Generic Image
            Rectangle genericImageRect = new Rectangle(
                x: width / 2 - GenericImage.Width / 2,
                y: 2 * Margin + logoRect.Height,
                width: 160,
                height: 160);
            AbsoluteLayout.SetLayoutBounds(GenericImage, genericImageRect);

            // Picked Image
            AbsoluteLayout.SetLayoutBounds(PickedImage, genericImageRect);

            // Generic Image
            Rectangle genericImageFrameRect = new Rectangle(
                x: width / 2 - 170 / 2,
                y: 2 * Margin + logoRect.Height - 10,
                width: 170,
                height: 170);
            AbsoluteLayout.SetLayoutBounds(PickedImageFrame, genericImageFrameRect);

            // Text Label 
            Rectangle textLabelRect = new Rectangle(
                x: width / 2 - TextLabel.Width / 2,
                y: 3 * Margin + logoRect.Height + genericImageRect.Height,
                width: TextLabel.Width,
                height: TextLabel.Height);
            AbsoluteLayout.SetLayoutBounds(TextLabel, textLabelRect);

            // Scroll Container
            Rectangle scrollContainerRect = new Rectangle(
                x: width / 2 - ScrollContainer.Width / 2,
                y: 4 * Margin + logoRect.Height + genericImageRect.Height + textLabelRect.Height,
                width: width - (2 * Margin),
                height: height - (TextLabel.Bounds.Bottom + Margin));
            AbsoluteLayout.SetLayoutBounds(ScrollContainer, scrollContainerRect);

            // Scroll Container
            Rectangle flexLayoutContainerRect = new Rectangle(
                x: width / 2 - ScrollContainer.Width / 2,
                y: 4 * Margin + logoRect.Height + genericImageRect.Height + textLabelRect.Height,
                width: width - (2 * Margin),
                height: height - (TextLabel.Bounds.Bottom + Margin));
            AbsoluteLayout.SetLayoutBounds(ResultFlexLayout, scrollContainerRect);


        }


        //private void MainPage_SizeChanged(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}