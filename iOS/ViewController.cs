using System;
		
using UIKit;
using MapKit;
using CoreLocation;
using System.Diagnostics;
using Translocker.Utils;
using System.Collections;

namespace Translocker.iOS
{
	public partial class ViewController : UIViewController
	{
		MKMapView mapView;
		MapDelegate mapDelegate;
		ArrayList Shelters;

		public ViewController (IntPtr handle) : base (handle)
		{		
			this.Shelters = new ArrayList ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			mapView = new MKMapView (View.Bounds);
			mapView.ShowsUserLocation =true;
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			View.AddSubview(mapView);

			CLLocationCoordinate2D coords = new CLLocationCoordinate2D(-6.196280, 106.823325);
			MKCoordinateSpan span = new MKCoordinateSpan(MapUtils.KilometresToLatitudeDegrees(50), MapUtils.KilometresToLongitudeDegrees(10, coords.Latitude));
			mapView.Region = new MKCoordinateRegion(coords, span);

			mapDelegate = new MapDelegate ();
			mapView.Delegate = mapDelegate;

			LoadData ();

			foreach (ShelterAnnotation item in this.Shelters) {
				
				mapView.AddAnnotations(item);
			}

//			coords = new CLLocationCoordinate2D(-6.229728,106.6894312);
//			span = new MKCoordinateSpan(MapUtils.KilometresToLatitudeDegrees(2), MapUtils.KilometresToLongitudeDegrees(2, coords.Latitude));
//			mapView.Region = new MKCoordinateRegion(coords, span);
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}

		void LoadData ()
		{
			IOUtils ioUtils = new IOUtils ();
			string rawShelters = ioUtils.readFileFromSharedProject ("Assets.Shelters.txt");

			string[] sheltersData = rawShelters.Split ('\n');

			foreach (string datum in sheltersData) {
				if (!string.IsNullOrEmpty(datum)) {
					string[] temp = datum.Split (',');
					ShelterAnnotation shelter = new ShelterAnnotation (temp[0], long.Parse(temp[2]), long.Parse(temp[1]));
					this.Shelters.Add (shelter);
				}
			}
		}

		class BasicMapAnnotation : MKAnnotation{
			public override CLLocationCoordinate2D Coordinate {get;}
			string title, subtitle;
			public override string Title { get{ return title; }}
			public override string Subtitle { get{ return subtitle; }}
			public BasicMapAnnotation (CLLocationCoordinate2D coordinate, string title, string subtitle) {
				this.Coordinate = coordinate;
				this.title = title;
				this.subtitle = subtitle;
			}
		}
	}
}
