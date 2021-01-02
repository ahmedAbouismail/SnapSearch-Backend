using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snapsearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();

            

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

        private const int margin = 20;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // set the position of all the screen elements

            //Logo Image
            Rectangle logoRect = new Rectangle(
                x: width /2 - LogoImage.Width / 2,
                y: margin,
                width: LogoImage.Width,
                height: LogoImage.Height);
            AbsoluteLayout.SetLayoutBounds(LogoImage, logoRect);

            //Generic Image
            Rectangle genericImageRect = new Rectangle(
                x: width /2  - GenericImage.Width / 2,
                y: 2*margin + logoRect.Height,
                width: GenericImage.Width,
                height: GenericImage.Height);
            AbsoluteLayout.SetLayoutBounds(GenericImage, genericImageRect);

            // Text Label 
            Rectangle textLabelRect = new Rectangle(
                x: width /2 - TextLabel.Width / 2,
                y: 3*margin + logoRect.Height + genericImageRect.Height,
                width: TextLabel.Width,
                height: TextLabel.Height);
            AbsoluteLayout.SetLayoutBounds(TextLabel, textLabelRect);

            // Scroll Container
            Rectangle scrollContainerRect = new Rectangle(
                x: width / 2 - ScrollContainer.Width / 2,
                y: 4 * margin + logoRect.Height + genericImageRect.Height + textLabelRect.Height,
                width: width - (2*margin),
                height: height - (TextLabel.Bounds.Bottom + margin));
            AbsoluteLayout.SetLayoutBounds(ScrollContainer, scrollContainerRect);


        }


        //private void MainPage_SizeChanged(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}