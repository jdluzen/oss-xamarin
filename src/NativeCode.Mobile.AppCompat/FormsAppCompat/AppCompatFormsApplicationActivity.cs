namespace NativeCode.Mobile.AppCompat.FormsAppCompat
{
    using Android.Support.Design.Widget;
    using Android.Views;
    using Android.Widget;
    using Helpers;
    using Xamarin.Forms.Platform.Android;
    /// <summary>
    /// Provides a <see cref="AppCompatDelegate" />-backed activity while maintaining compatibility with $Xamarin.Forms$.
    /// </summary>
    /// <remarks>See <see cref="http://bit.ly/1Lfr30c" /> for information on implementation.</remarks>
    public class AppCompatFormsApplicationActivity : FormsAppCompatActivity, IAppCompatCoordinatorLayoutProvider
    {
        private CoordinatorLayout coordinator;
        private readonly DisposableContainer disposables = new DisposableContainer();
        public static int CoordinatorResource = 0;
        protected bool EnableCoordinatorLayout { get; set; }

        public CoordinatorLayout GetCoordinatorLayout()
        {
            return this.EnableCoordinatorLayout ? this.coordinator : null;
        }

        public override void SetContentView(View view)
        {
            var content = view;

            // We need to create a CoordinatorLayout for Snackbars to find so we get the proper display.
            // This simply wraps the LinearLayout that the FormsApplicationActivity creates.
            // TODO: This relies too much on the implementation detail of the FormsApplicationActivity.
            if (content is RelativeLayout && this.EnableCoordinatorLayout && CoordinatorResource > 0)
            {
                this.coordinator = this.Inflate<CoordinatorLayout>(CoordinatorResource, null);
                var toolbar = this.Inflate<Android.Support.V7.Widget.Toolbar>(FormsAppCompatActivity.ToolbarResource, null);
                this.coordinator.AddView(toolbar);
                this.coordinator.AddView(view);

                this.disposables.Add(this.coordinator);

                content = this.coordinator;
            }

            base.SetContentView(content);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.coordinator = null;
                this.disposables.Dispose();
            }

            base.Dispose(disposing);
        }

        private T Inflate<T>(int id, ViewGroup viewGroup) where T : View
        {
            var inflated = this.LayoutInflater.Inflate(id, viewGroup);
            return inflated.FindViewById<T>(inflated.Id);
        }
    }
}