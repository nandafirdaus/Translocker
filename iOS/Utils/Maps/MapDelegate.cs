using System;
using MapKit;
using UIKit;

namespace Translocker.iOS
{
	public class MapDelegate : MKMapViewDelegate
	{
		static string shelterAnnotationId = "ShelterAnnotation";
		static string BusAnnotationId = "BusAnnotation";

		public MapDelegate ()
		{
		}

		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;

//			if (annotation is MKUserLocation)
//				return null; 

			if (annotation is ShelterAnnotation) {

				// show shelter annotation
				annotationView = mapView.DequeueReusableAnnotation (shelterAnnotationId);

				if (annotationView == null)
					annotationView = new MKAnnotationView (annotation, shelterAnnotationId);

				UIImage image = UIImage.FromFile ("Images/shelter.png");
				annotationView.Image = image;
				annotationView.CanShowCallout = true;
			} else if (annotation is BuswayAnnotation) {
				// show busway annotation
				annotationView = mapView.DequeueReusableAnnotation (BusAnnotationId);

				if (annotationView == null) {
					annotationView = new MKAnnotationView (annotation, BusAnnotationId);
				}

				UIImage image = UIImage.FromFile ("Images/busway.png");
				annotationView.Image = image;
				annotationView.CanShowCallout = false;
			}

			return annotationView;
		}
	}
}

