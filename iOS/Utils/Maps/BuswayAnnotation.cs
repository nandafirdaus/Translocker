using System;
using MapKit;
using CoreLocation;

namespace Translocker.iOS
{
	public class BuswayAnnotation : MKAnnotation
	{
		Busway busway;
		CLLocationCoordinate2D coord;

		public string Buscode { get{ return busway.Buscode; } set { busway.Buscode = value; }}
		public double Latitude { get{ return busway.Latitude; } set { busway.Latitude = value; }}
		public double Longitude{ get{ return busway.Longitude; } set { busway.Longitude = value; }}
		public int Speed { get{ return busway.Speed; } set { busway.Speed = value; }}
		public int Course { get{ return busway.Course; } set { busway.Course = value; }}

		public BuswayAnnotation (string title,
			long latitude, long longitude, int speed, int course)
		{
			this.busway = new Busway (title, latitude, longitude, speed, course);

			this.coord = new CLLocationCoordinate2D(this.busway.Latitude, this.busway.Longitude);
		}

		public BuswayAnnotation (Busway busway)
		{
			this.busway = busway;

			this.coord = new CLLocationCoordinate2D(this.busway.Latitude, this.busway.Longitude);
		}

		public override string Title {
			get {
				return this.busway.Buscode;
			}
		}

		public override  CLLocationCoordinate2D Coordinate {
			get {
				return coord;
			}
		}
	}
}

