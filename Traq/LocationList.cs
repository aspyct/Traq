using System;

using Xamarin.Forms;

namespace Traq
{
	public class LocationList : ContentPage
	{
		ListView List;

		public LocationList ()
		{
			Title = "Locations";

			List = new ListView ();
			List.ItemsSource = new string[] {
				"one",
				"two",
				"three",
				"four",
				"five"
			};

			List.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem != null) {
					Navigation.PushAsync (new Target ());
				}
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					List
				}
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			List.SelectedItem = null;
		}
	}
}


