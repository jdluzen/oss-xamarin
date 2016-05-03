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
                var context = this.Context.GetAppCompatThemedContext();
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
            // TODO: It's adding it, but it never shows up in the XML in monitor.
            var header = this.Element.HeaderView.GetNativeView();
            //var h2 = this.Element.HeaderView.ConvertFormsToNative();
            var temp = Element.HeaderView.Content.GetNativeView();
            //var hchild = temp as Android.Widget.Button;
            //Android.Widget.Button b = header as Android.Widget.Button;
            //var c0 = header.GetChildAt(0);
            //LinearLayoutCompat.LayoutParams lp = new LinearLayoutCompat.LayoutParams(LinearLayoutCompat.LayoutParams.MatchParent, 400);
            //header.LayoutParameters = new LayoutParams(lp);
            /*if (Control.ChildCount > 0)
                Control.GetChildAt(0).Invalidate();
            else*/
            LinearLayout llc = new LinearLayout(Forms.Context) { LayoutParameters = new LayoutParams(LinearLayoutCompat.LayoutParams.MatchParent, 400) };
            //var llc0 = llc.GetChildAt(0);
            //ImageView iv = new ImageView(Forms.Context);
            //iv.SetImageResource(AppCompat.Resource.Drawable.abc_btn_check_material);
            //AppCompatButton acb = new AppCompatButton(Forms.Context) { Text = "hi" };
            //llc.AddView(acb);
            //llc.AddView(header);
            //llc.AddView(iv);
            //View root = header.RootView;
            temp.RemoveFromParent();//keep
            llc.AddView(temp);//keep
            //root.ForceLayout();
            //root.LayoutParameters = new LinearLayout.LayoutParams(LinearLayoutCompat.LayoutParams.MatchParent, LinearLayoutCompat.LayoutParams.WrapContent);
            //root.SetMinimumHeight(400);
            //root.SetMinimumWidth(400);
            //Element.ForceLayout();
            //header.SetMinimumHeight(800);
            this.Control.AddHeaderView(/*header);//root);//llc);// header);/*/llc);
            //((IVisualElementController)Element).NativeSizeChanged();
            //header.Layout(header.Parent);
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