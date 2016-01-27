namespace NativeCode.Mobile.AppCompat.Renderers
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using NativeCode.Mobile.AppCompat.Controls;
    using NativeCode.Mobile.AppCompat.Helpers;
    using NativeCode.Mobile.AppCompat.Renderers.Renderers;

    using Xamarin.Forms;

    using Switch = Xamarin.Forms.Switch;

    /// <summary>
    /// Enables renderers for various aspects of the library.
    /// </summary>
    public static class FormsAppCompat
    {
        private const string RegistrarType = "Xamarin.Forms.Registrar, Xamarin.Forms.Core";

        private static readonly object RegistrarInstance;

        private static readonly MethodInfo RegisterMethod;

        /// <summary>
        /// Initializes static members of the <see cref="FormsAppCompat" /> class.
        /// </summary>
        static FormsAppCompat()
        {
            var type = Type.GetType(RegistrarType, true);
            var property = type.GetProperty("Registered", ReflectionHelper.NonPublicStatic);
            RegistrarInstance = property.GetValue(null);
            RegisterMethod = property.PropertyType.GetMethod("Register", ReflectionHelper.InstancePublic);
        }

        /// <summary>
        /// Enables registration of all renderers.
        /// </summary>
        [Obsolete("Prefer to use Init.", false)]
        public static void EnableAll()
        {
            Init(AppCompatOption.All);
        }

        /// <summary>
        /// Enables Android-specific renderers.
        /// </summary>
        [Obsolete("Prefer to use Init.", false)]
        public static void EnableAndroidRenderers()
        {
            Init(AppCompatOption.CardViewSupport | AppCompatOption.FloatingActionButtonSupport | AppCompatOption.NavigationLayoutSupport);
        }

        /// <summary>
        /// Enables compatibility renderers.
        /// </summary>
        [Obsolete("Prefer to use Init.", false)]
        public static void EnableAppCompatRenderers()
        {
            Init(AppCompatOption.AppCompatButtonSupport | AppCompatOption.AppCompatEntrySupport | AppCompatOption.AppCompatSpinnerSupport | AppCompatOption.AppCompatSwitchSupport);
        }

        /// <summary>
        /// Enables the <see cref="AppCompatMasterDetailRenderer" />.
        /// </summary>
        [Obsolete("Prefer to use Init.", false)]
        public static void EnableMasterDetailRenderer()
        {
            Init(AppCompatOption.AppCompatMasterDetailSupport);
        }

        public static void Init(AppCompatOption options = AppCompatOption.All)
        {
            Init(AppCompatOption.None, options);
        }

        public static void Init(AppCompatOption exclude = AppCompatOption.None, AppCompatOption options = AppCompatOption.All)
        {
            if (options == AppCompatOption.None)
            {
                return;
            }

            if (!exclude.HasFlag(AppCompatOption.AppCompatButtonSupport) && options.HasFlag(AppCompatOption.AppCompatButtonSupport))
            {
                RegisterType(typeof(Button), typeof(AppCompatButtonRenderer));
            }

            if (!exclude.HasFlag(AppCompatOption.AppCompatEntrySupport) && options.HasFlag(AppCompatOption.AppCompatEntrySupport))
            {
                RegisterType(typeof(Entry), typeof(AppCompatEntryLayoutRenderer));
            }

            if (!exclude.HasFlag(AppCompatOption.AppCompatMasterDetailSupport) && options.HasFlag(AppCompatOption.AppCompatMasterDetailSupport))
            {
                RegisterType(typeof(MasterDetailPage), typeof(AppCompatMasterDetailRenderer));
            }

            if (!exclude.HasFlag(AppCompatOption.AppCompatSpinnerSupport) && options.HasFlag(AppCompatOption.AppCompatSpinnerSupport))
            {
                RegisterType(typeof(Picker), typeof(AppCompatSpinnerRenderer));
            }

            if (!exclude.HasFlag(AppCompatOption.AppCompatSwitchSupport) && options.HasFlag(AppCompatOption.AppCompatSwitchSupport))
            {
                RegisterType(typeof(Switch), typeof(AppCompatSwitchRenderer));
            }

            if (!exclude.HasFlag(AppCompatOption.CardViewSupport) && options.HasFlag(AppCompatOption.CardViewSupport))
            {
                RegisterType(typeof(Card), typeof(CardRenderer));
            }

            if (!exclude.HasFlag(AppCompatOption.FloatingActionButtonSupport) && options.HasFlag(AppCompatOption.FloatingActionButtonSupport))
            {
                RegisterType(typeof(FloatingButton), typeof(FloatingButtonRenderer));
            }

            if (!exclude.HasFlag(AppCompatOption.NavigationLayoutSupport) && options.HasFlag(AppCompatOption.NavigationLayoutSupport))
            {
                RegisterType(typeof(NavigationLayout), typeof(NavigationLayoutRenderer));
            }
        }

        private static void RegisterType(Type handler, Type target)
        {
            RegisterMethod.Invoke(RegistrarInstance, new object[] { handler, target });
            Debug.WriteLine("Registered renderer {0} for {1}.", target.Name, handler.Name);
        }
    }
}