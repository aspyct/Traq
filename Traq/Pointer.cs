using System;

namespace Traq
{
	public class Pointer
	{
		public readonly Location Target;

		private double _sun;
		private DateTime lastSunUpdate;
		public double Sun {
			get {
				return _sun + DateTime.Now.Subtract (lastSunUpdate).TotalDays * 360;
			}
			set {
				_sun = value % 360; // 0 - 359 degrees
				lastSunUpdate = DateTime.Now;
			}
		}

		private double bearingToLocation;
		private double lastDeviceBearing;
		public double Arrow {
			get {
				return bearingToLocation - lastDeviceBearing;
			}
		}

		public double Distance { get; private set; }

		public Pointer (Location target)
		{
			Target = target;
			lastSunUpdate = DateTime.Now;
		}

		public void UpdateDeviceLocation (Location deviceLocation)
		{
			bearingToLocation = deviceLocation.BearingTo (Target);
			Distance = deviceLocation.DistanceTo (Target);
		}

		public void UpdateDeviceHeading (Heading deviceHeading)
		{
			lastDeviceBearing = deviceHeading.Degrees;
		}
	}
}

