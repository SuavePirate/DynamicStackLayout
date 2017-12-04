using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SuaveControls.DynamicStackLayout
{
    /// <summary>
    /// Dynamic grid view. Just like the Xamarin.Forms list view but lays out items in a grid.
    /// Note: this is not ideal for large collections since it does not use virtualization
    /// </summary>
    public class DynamicStackLayout : StackLayout
    {
        #region Overrides

        /// <summary>
        /// Called when the binding context changes
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            CreateStack();
        }

        /// <summary>
        /// Called when the item source property changes
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                CreateStack();
            }

            base.OnPropertyChanged(propertyName);
        }

        #endregion

        // ------------------------------------------------------------

        #region Public Properties

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(DynamicStackLayout), null);
        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(DynamicStackLayout), null);
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }


        #endregion

        // ------------------------------------------------------------

        #region Private Methods

        private void CreateStack()
        {
            Children.Clear();
            // Check for data
            if (ItemsSource == null || ItemsSource.Count() == 0 || ItemsSource.First() == null)
            {
                return;
            }

            CreateCells();
        }


        /// <summary>
        /// Creates the cells.
        /// </summary>
        private void CreateCells()
        {
            foreach (var item in ItemsSource)
            {
                Children.Add(CreateCellView(item));
            }
        }

        /// <summary>
        /// Creates the cell view.
        /// </summary>
        /// <returns>The cell view.</returns>
        /// <param name="item">Item.</param>
        private View CreateCellView(object item)
        {
            var view = (View)ItemTemplate.CreateContent();
            var bindableObject = (BindableObject)view;

            if (bindableObject != null)
            {
                bindableObject.BindingContext = item;
            }

            return view;
        }

        #endregion
    }
}
