﻿namespace NativeCode.Mobile.AppCompat.Controls
{
    using System.Windows.Input;

    using Xamarin.Forms;

    /// <summary>
    /// Provides a floating action button.
    /// </summary>
    public class FloatingButton : View, ICommandProvider
    {
        public static readonly BindableProperty ButtonSizeProperty = BindableProperty.Create<FloatingButton, FloatingButtonSize>(
            x => x.ButtonSize,
            default(FloatingButtonSize),
            BindingMode.OneWayToSource);

        public static readonly BindableProperty ColorProperty = BindableProperty.Create<FloatingButton, Color>(x => x.Color, Color.Accent);

        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create<FloatingButton, Color>(x => x.ColorPressed, Color.Accent);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create<FloatingButton, ICommand>(x => x.Command, default(ICommand));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<FloatingButton, object>(
            x => x.CommandParameter,
            default(object));

        public static readonly BindableProperty IconProperty = BindableProperty.Create<FloatingButton, object>(x => x.Icon, default(object));

        /// <summary>
        /// Gets or sets the size of the button.
        /// </summary>
        public FloatingButtonSize ButtonSize
        {
            get { return (FloatingButtonSize)this.GetValue(ButtonSizeProperty); }
            set { this.SetValue(ButtonSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the button.
        /// </summary>
        public Color Color
        {
            get { return (Color)this.GetValue(ColorProperty); }
            set { this.SetValue(ColorProperty, value); }
        }

        public Color ColorPressed
        {
            get { return (Color)this.GetValue(ColorPressedProperty); }
            set { this.SetValue(ColorPressedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public object Icon
        {
            get { return this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }
    }
}