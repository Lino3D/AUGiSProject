using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureAuthenticationApp.Helpers;
using Xamarin.Forms;

namespace AzureAuthenticationApp.Models.UI
{
    public class BindablePicker : Picker
    {
        #region Fields

        //Bindable property for the items source
        public new static readonly BindableProperty ItemsSourceProperty =
#pragma warning disable CS0618 // Type or member is obsolete
            BindableProperty.Create<BindablePicker, IEnumerable>(p => p.ItemsSource, null, propertyChanged: OnItemsSourcePropertyChanged);
#pragma warning restore CS0618 // Type or member is obsolete

        //Bindable property for the selected item
        public new static readonly BindableProperty SelectedItemProperty =
#pragma warning disable CS0618 // Type or member is obsolete
            BindableProperty.Create<BindablePicker, object>(p => p.SelectedItem, null, BindingMode.TwoWay, propertyChanged: OnSelectedItemPropertyChanged);
#pragma warning restore CS0618 // Type or member is obsolete

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>
        /// The items source.
        /// </value>
        public new IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public new object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [items source property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="value">The value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemsSourcePropertyChanged(BindableObject bindable, IEnumerable value, IEnumerable newValue)
        {
            var picker = (BindablePicker)bindable;
            var notifyCollection = newValue as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                    {
                        foreach (var newItem in args.NewItems)
                        {
                            picker.Items.Add((newItem ?? "").ToString());
                        }
                    }
                    if (args.OldItems != null)
                    {
                        foreach (var oldItem in args.OldItems)
                        {
                            picker.Items.Remove((oldItem ?? "").ToString());
                        }
                    }
                };
            }

            if (newValue == null)
                return;

            picker.Items.Clear();

            foreach (var item in newValue)
                picker.Items.Add((item ?? "").ToString());
        }

        /// <summary>
        /// Called when [selected item property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="value">The value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object value, object newValue)
        {
            var picker = (BindablePicker)bindable;
            if (picker.ItemsSource != null)
                picker.SelectedIndex = picker.ItemsSource.IndexOf(picker.SelectedItem);

        }


        #endregion
    }
}
