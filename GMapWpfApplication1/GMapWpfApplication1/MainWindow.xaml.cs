using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMapWpfApplication1.CustomMarkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GMapWpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PointLatLng start;
        PointLatLng end;

        // marker
        GMapMarker currentMarker;
        public MainWindow()
        {
            InitializeComponent();
            
            // config map
            gmap1.MapProvider = GMapProviders.OpenStreetMap;
            gmap1.Position = new PointLatLng(54.6961334816182, 25.2985095977783);

            gmap1.MouseMove += new System.Windows.Input.MouseEventHandler(GMap_MouseMove);
            gmap1.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(GMap_MouseLeftButtonDown);

            // set current marker
            currentMarker = new GMapMarker(gmap1.Position);
            {
                currentMarker.Shape = new CustomMarkerRed(this, currentMarker, "custom position marker");
                currentMarker.Offset = new System.Windows.Point(-15, -15);
                currentMarker.ZIndex = int.MaxValue;
                gmap1.Markers.Add(currentMarker);
            }
        }

        // move current marker with left holding
        void GMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point p = e.GetPosition(gmap1);
                currentMarker.Position = gmap1.FromLocalToLatLng((int)p.X, (int)p.Y);
            }
        }

        void GMap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(gmap1);
            currentMarker.Position = gmap1.FromLocalToLatLng((int)p.X, (int)p.Y);
        }

        private void btnRoute_Click(object sender, RoutedEventArgs e)
        {
            RoutingProvider rp = gmap1.MapProvider as RoutingProvider;
            if (rp == null)
            {
                rp = GMapProviders.OpenStreetMap; // use OpenStreetMap if provider does not implement routing
            }

            MapRoute route = rp.GetRoute(start, end, false, false, (int)gmap1.Zoom);

            if (route != null)
            {
                GMapRoute mRoute = new GMapRoute(route.Points);
                {
                    mRoute.ZIndex = -1;
                }
                
                gmap1.Markers.Add(mRoute);

                gmap1.ZoomAndCenterMarkers(null);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("There is no route");
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            start = currentMarker.Position;
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            end = currentMarker.Position;
        }
    }
}
