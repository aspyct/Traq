using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace Traq
{
	public class Target : ContentPage
	{
		Pointer pointer;
		Image arrow;

		public Target ()
		{
			Title = "Target";

			var paris = new Location {
				Latitude = 48.8567,
				Longitude = 2.3508
			};
			pointer = new Pointer (paris);

			var updateButton = new Button {
				Text = "Update"
			};
			updateButton.Clicked += UpdateHeading;

			arrow = new Image {
				Source = ImageSource.FromFile ("Arrow")
			};

			Content = new StackLayout { 
				Children = {
					arrow,
					updateButton
				}
			};
		}

		private void UpdateHeading (object sender, EventArgs e)
		{
			Console.WriteLine ("Updating location");

			IGeolocator locator = DependencyService.Get<IGeolocator>();

			locator.LocationUpdated += (object _, LocationUpdateEvent lue) => {
				pointer.UpdateDeviceLocation (lue.NewLocation);

				locator.StopUpdatingLocation ();
			};

			locator.HeadingUpdated += (object _, HeadingUpdateEvent hue) => {
				pointer.UpdateDeviceHeading (hue.NewHeading);
				arrow.Rotation = pointer.Arrow;
			};

			locator.StartUpdatingLocation ();
			locator.StartUpdatingHeading ();
		}
	}
}
