﻿namespace Demo.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using NativeCode.Mobile.AppCompat.FormsAppCompat;
    using NativeCode.Mobile.AppCompat.Renderers;

    using Xamarin.Forms;

    [Activity(ConfigurationChanges = AppConfig, MainLauncher = true, Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class MainActivity : AppCompatFormsApplicationActivity
    {
        private const ConfigChanges AppConfig = ConfigChanges.Orientation | ConfigChanges.ScreenSize;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.EnableCoordinatorLayout = true;

            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            FormsAppCompat.Init(AppCompatOption.All);

            this.LoadApplication(new App());
        }
    }
}