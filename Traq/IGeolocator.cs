using System;
using System.Threading.Tasks;

namespace Traq
{
	public class LocationUpdateEvent : EventArgs
	{
		public Location NewLocation;
	}

	public class HeadingUpdateEvent : EventArgs
	{
		public Heading NewHeading;
	}

	public interface IGeolocator
	{
		event EventHandler<LocationUpdateEvent> LocationUpdated;
		void StartUpdatingLocation ();
		void StopUpdatingLocation ();

		event EventHandler<HeadingUpdateEvent> HeadingUpdated;
		void StartUpdatingHeading ();
		void StopUpdatingHeading ();
	}
}
