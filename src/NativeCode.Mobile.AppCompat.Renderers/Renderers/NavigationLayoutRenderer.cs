using Android.Support.V7.Widget;
using Android.Widget;
using Xamarin.Forms;

namespace NativeCode.Mobile.AppCompat.Renderers.Renderers
{
    using Android.Support.Design.Widget;
    using Android.Views;
    using NativeCode.Mobile.AppCompat.Controls;
    using NativeCode.Mobile.AppCompat.Extensions;
    using NativeCode.Mobile.AppCompat.Renderers.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using Xamarin.Forms.Platform.Android;

    public class NavigationLayoutRenderer : ViewRenderer<NavigationLayout, NavigationView>, NavigationView.IOnNavigationItemSelectedListener
    {
        private readonly Dictionary<IMenuItem, NavigationLayoutMenu> mappings = new Dictionary<IMenuItem, NavigationLayoutMenu>();

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            try
            {
                this.mappings[menuItem].ExecuteCommand();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.mappings.Any())
            {
                this.Reset();
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationLayout> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                var context = this.Context;//.GetAppCompatThemedContext();
                var control = new NavigationView(context);
                control.SetFitsSystemWindows(true);
                control.SetNavigationItemSelectedListener(this);

                this.SetNativeControl(control);

                this.UpdateBackgroundColor();
                this.UpdateHeaderView();
                this.UpdateMenuItems();
            }
        }

        private void Reset()
        {
            foreach (var kvp in this.mappings)
            {
                kvp.Key.Dispose();
            }

            this.mappings.Clear();
            this.Control.Menu.Clear();
        }

        private void UpdateHeaderView()
        {
            if (this.Element.HeaderView == null)
            {
                return;
            }
            var androidView = Element.HeaderView.Content.GetNativeView();
            LinearLayout llc = new LinearLayout(Forms.Context)
            {
                LayoutParameters = new LayoutParams(LinearLayoutCompat.LayoutParams.MatchParent, (int)Element.HeightRequest)
            };
            androidView.RemoveFromParent();
            llc.AddView(androidView);
            this.Control.AddHeaderView(llc);
        }

        private void UpdateMenuItems()
        {
            this.Reset();

            for (var index = 0; index < this.Element.Children.Count; index++)
            {
                var menu = this.Element.Children[index];
                var item = this.Control.Menu.Add(menu.Group, index, index, menu.Text);

                if (menu.Icon != null)
                {
                    item.SetIcon(menu.Icon.ToBitmapDrawable());
                }

                this.mappings.Add(item, menu);
            }
        }
    }
}