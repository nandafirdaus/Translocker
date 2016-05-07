using System;
		
using UIKit;
using MapKit;
using CoreLocation;
using System.Diagnostics;
using Translocker.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Translocker.iOS
{
	public partial class ViewController : UIViewController
	{
		MKMapView mapView;
		MapDelegate mapDelegate;
		List<ShelterAnnotation> Shelters;
		List<Busway> Busways;
		List<BuswayAnnotation> BuswayPin;

		public ViewController (IntPtr handle) : base (handle)
		{		
			this.Shelters = new List<ShelterAnnotation> ();
			this.BuswayPin = new List<BuswayAnnotation> ();
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			mapView = new MKMapView (View.Bounds);

			mapDelegate = new MapDelegate ();
			mapView.Delegate = mapDelegate;

			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			View.AddSubview(mapView);
			mapView.ShowsUserLocation = false;

			CLLocationCoordinate2D coords = new CLLocationCoordinate2D(-6.196280, 106.823325);
			MKCoordinateSpan span = new MKCoordinateSpan(MapUtils.KilometresToLatitudeDegrees(50), MapUtils.KilometresToLongitudeDegrees(10, coords.Latitude));
			mapView.Region = new MKCoordinateRegion(coords, span);
				
			LoadData ();
			await LoadBuswayData ();

			foreach (ShelterAnnotation item in this.Shelters) {				
				mapView.AddAnnotations(item);
			}

			LoadBuswayPin ();
			runAutoRefreshData ();

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

		async Task LoadBuswayData () 
		{
			RestUtils restUtils = new RestUtils ();
			var rawData = await restUtils.GetDataAsync ("http://apps.roxinlabs.com/transjakarta/get-busway.php");
			List<Busway> parsedData = restUtils.deserializeBuswayJson (rawData);
			this.Busways = parsedData;
		}

		void LoadBuswayPin ()
		{
			mapView.RemoveAnnotations (BuswayPin.ToArray());

			BuswayPin = new List<BuswayAnnotation> ();

			foreach (Busway item in this.Busways) {
				BuswayAnnotation annotation = new BuswayAnnotation (item);
				BuswayPin.Add (annotation);
				mapView.AddAnnotations (annotation);
			}
		}

		async void runAutoRefreshData ()
		{
			while (true) {
				await Task.Delay (5000);
				await LoadBuswayData ();
				LoadBuswayPin ();
			}
		}
	}
}
