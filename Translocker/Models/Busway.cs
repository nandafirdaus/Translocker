using System;

namespace Translocker
{
	public class Busway
	{
		private string buscode;
		private double latitude;
		private double longitude;
		private int speed;
		private int course;

		public string Buscode { get; set;}
		public double Latitude {get{ return latitude; } set{ latitude = value; }}
		public double Longitude{get{ return longitude; } set{ longitude = value; }}
		public int Speed { get; set;}
		public int Course { get; set;}

		public Busway (string name, double latitude, double longitude, int speed, int course)
		{
			this.Buscode = buscode;
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.Speed = speed;
			this.Course = course;
		}

	}
}

