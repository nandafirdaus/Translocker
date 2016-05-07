using System;
using MapKit;
using CoreLocation;

namespace Translocker.iOS
{
	public class ShelterAnnotation : MKAnnotation
	{
		Shelter shelter;
		CLLocationCoordinate2D coord;

		public ShelterAnnotation (string title,
			long latitude, long longitude)
		{
			this.shelter = new Shelter (title, latitude, longitude);

			this.coord = new CLLocationCoordinate2D(this.shelter.Latitude, this.shelter.Longitude);
		}

		public override string Title {
			get {
				return this.shelter.Name;
			}
		}

		public override CLLocationCoordinate2D Coordinate {
			get {
				return coord;
			}
		}
	}
}

