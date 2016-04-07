using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EntranceAnimation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Compositor _compositor;
        public MainPage()
        {
            this.InitializeComponent();

            //We need the compositor in order to use the Visual Layer (Windows.UI.Composition)
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            //Load data to the GridView
            LoadData();
        }

        private void LoadData()
        {
            myGridView.ItemsSource = new List<string>
            {
                "http://static.trackseries.tv/banners/posters/121361-49.jpg",
                "http://static.trackseries.tv/banners/posters/257655-25.jpg",
                "http://static.trackseries.tv/banners/posters/153021-36.jpg",
                "http://static.trackseries.tv/banners/posters/80379-34.jpg",
                "http://static.trackseries.tv/banners/posters/176941-10.jpg",
                "http://static.trackseries.tv/banners/posters/81189-10.jpg",
                "http://static.trackseries.tv/banners/posters/247808-24.jpg",
                "http://static.trackseries.tv/banners/posters/274431-18.jpg",
                "http://static.trackseries.tv/banners/posters/301824-5.jpg",
                "http://static.trackseries.tv/banners/posters/281714-2.jpg",
                "http://static.trackseries.tv/banners/posters/273181-17.jpg"
            };
        }

        private void GridView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            //We need the visual element of the XAML control to access to the visual properties and methods
            var visual = ElementCompositionPreview.GetElementVisual(args.ItemContainer);

            //Animation to move from the bottom to its position
            var animation = _compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.00f, 600);
            animation.InsertKeyFrame(1.00f, 0);
            animation.Duration = TimeSpan.FromSeconds(1);
            animation.DelayTime = TimeSpan.FromMilliseconds(args.ItemIndex * 100);

            //Animation fade in
            var animation2 = _compositor.CreateScalarKeyFrameAnimation();
            animation2.InsertKeyFrame(0.00f, 0);
            animation2.InsertKeyFrame(1.00f, 1);
            animation2.Duration = TimeSpan.FromSeconds(1);
            animation2.DelayTime = TimeSpan.FromMilliseconds(200 + args.ItemIndex * 100);

            //We start each animation. We need to specify what property we want to animate
            visual.StartAnimation("Offset.Y", animation);
            visual.StartAnimation("Opacity", animation2);
        }
    }
}
