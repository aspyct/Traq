using System;
using System.Threading.Tasks;
using Android.Hardware;
using Xamarin.Forms;
using Android.Content;
using Traq.Droid;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.OS;

[assembly: Xamarin.Forms.Dependency (typeof (Geolocator_Android))]

namespace Traq.Droid
{
	public class Geolocator_Android : Java.Lang.Object, IGeolocator, ISensorEventListener, IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener, ILocationListener
	{ 
		private SensorManager _manager;
		private Sensor _compass;
		private IGoogleApiClient _googleApiClient;

		public event EventHandler<LocationUpdateEvent> LocationUpdated;
		public event EventHandler<HeadingUpdateEvent> HeadingUpdated;

		public Geolocator_Android ()
		{
		}

		protected override void JavaFinalize ()
		{
			if (_googleApiClient != null) {
				_googleApiClient.Disconnect ();
			}

			base.JavaFinalize ();
		}

		public void StartUpdatingLocation ()
		{
			if (_googleApiClient == null) {
				_googleApiClient = new GoogleApiClientBuilder (Forms.Context)
					.AddApi (LocationServices.API)
					.AddConnectionCallbacks (this)
					.AddOnConnectionFailedListener (this)
					.Build ();

				_googleApiClient.Connect ();
			}
		}

		public void StopUpdatingLocation ()
		{
		}

		public void StartUpdatingHeading ()
		{
			if (_manager == null) {
				_manager = (SensorManager) Forms.Context.GetSystemService (Context.SensorService);
			}

			if (_compass == null) {
				_compass = _manager.GetDefaultSensor (SensorType.Orientation);
			}

			_manager.RegisterListener (this, _compass, SensorDelay.Normal);
		}

		public void StopUpdatingHeading ()
		{
			_manager.UnregisterListener (this);
		}

		public void OnAccuracyChanged (Sensor sensor, SensorStatus status)
		{
			if (sensor == _compass) {

			}
		}

		public void OnSensorChanged (SensorEvent e)
		{
			if (e.Sensor == _compass) {
				double degrees = e.Values [0];
				var newHeading = new Traq.Heading {
					Degrees = degrees
				};

				HeadingUpdated (this, new HeadingUpdateEvent {
					NewHeading = newHeading
				});
			}
		}

		public void OnConnected (Bundle connectionHint)
		{
			Console.WriteLine ("Connected to Google services");

			var locationRequest = new LocationRequest ();
			locationRequest.SetPriority (100);
			locationRequest.SetFastestInterval (2000);
			locationRequest.SetInterval (5000);

			LocationServices.FusedLocationApi.RequestLocationUpdates (_googleApiClient, locationRequest, this);
		}

		public void OnConnectionSuspended (int cause)
		{
			throw new NotImplementedException ();
		}

		public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
		{
			throw new NotImplementedException ();
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			var newLocation = new Location {
				Latitude = location.Latitude,
				Longitude = location.Longitude
			};

			LocationUpdated (this, new LocationUpdateEvent {
				NewLocation = newLocation
			});
		}
	}
}
