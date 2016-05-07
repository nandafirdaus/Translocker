using System;

namespace Translocker
{
	public class Shelter
	{
		public string Name;
		private double latitude;
		private double longitude;

		public double Latitude {get{ return latitude; } set{ latitude = value / 1E6; }}
		public double Longitude{get{ return longitude; } set{ longitude = value / 1E6; }}

		public Shelter (string name, long latitude, long longitude)
		{
			this.Name = name;
			this.Latitude = latitude;
			this.Longitude = longitude;
		}
	}
}

