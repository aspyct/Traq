using System;

namespace Traq
{
	public class Location
	{
		public double Latitude;
		public double Longitude;
		public double Accuracy;

		public double DistanceTo (Location destination)
		{
			// Using "haversine" formula
			double R = 6371000.0; // meters, earth's radius
			double p1 = ToRadians (this.Latitude);
			double p2 = ToRadians (destination.Latitude);
			double dp = ToRadians (destination.Latitude - this.Latitude);
			double dl = ToRadians (destination.Longitude - this.Longitude);

			/*
			 * var a = Math.sin(Δφ/2) * Math.sin(Δφ/2) +
        	 * Math.cos(φ1) * Math.cos(φ2) *
        	 * Math.sin(Δλ/2) * Math.sin(Δλ/2);
			 * var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
			 * var d = R * c;
			 */

			double a = Math.Sin (dp / 2) * Math.Sin (dp / 2)
					+ Math.Cos (p1) * Math.Cos (p2)
			        * Math.Sin (dl / 2) * Math.Sin (dl / 2);
			double c = 2 * Math.Atan2 (Math.Sqrt (a), Math.Sqrt (1 - a));

			return R * c;
		}

		public double BearingTo (Location destination)
		{
			double p1 = ToRadians (this.Latitude);
			double p2 = ToRadians (destination.Latitude);
			double l1 = ToRadians (this.Longitude);
			double l2 = ToRadians (destination.Longitude);

			double y = Math.Sin (l2 - l1) * Math.Cos (p2);
			double x = Math.Cos (p1) * Math.Sin (p2)
			        - Math.Sin (p1) * Math.Cos (p2) * Math.Cos (l2 - l1);

			return ToDegrees (Math.Atan2 (y, x));
		}

		private static double ToRadians (double degrees)
		{
			return Math.PI / 180 * degrees;
		}

		private static double ToDegrees (double radians)
		{
			return 180 * radians / Math.PI;
		}
	}
}

