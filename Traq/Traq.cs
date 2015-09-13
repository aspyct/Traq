using System;

using Xamarin.Forms;

namespace Traq
{
	public class App : Application
	{
		public App ()
		{
			var navPage = new NavigationPage (new LocationList ());
			MainPage = navPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

