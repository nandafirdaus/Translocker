using System;
		
using UIKit;
using MapKit;
using CoreLocation;

namespace Translocker.iOS
{
	public partial class ViewController : UIViewController
	{
		MKMapView mapView;

		public ViewController (IntPtr handle) : base (handle)
		{		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			mapView = new MKMapView (View.Bounds);
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			View.AddSubview(mapView);

			CLLocationCoordinate2D coords = new CLLocationCoordinate2D(48.857, 2.351);
			MKCoordinateSpan span = new MKCoordinateSpan(MapUtils.KilometresToLatitudeDegrees(20), MapUtils.KilometresToLongitudeDegrees(20, coords.Latitude));
			mapView.Region = new MKCoordinateRegion(coords, span);
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
