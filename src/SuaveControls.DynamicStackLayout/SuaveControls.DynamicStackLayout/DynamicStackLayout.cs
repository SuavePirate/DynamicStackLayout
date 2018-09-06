using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
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

        public event EventHandler<ItemTappedEventArgs> ItemSelected;

        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(DynamicStackLayout), null);

        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public static readonly BindableProperty SelectedCommandParameterProperty = BindableProperty.Create("SelectedCommandParameter", typeof(object), typeof(DynamicStackLayout), null);

        public object SelectedCommandParameter
        {
            get { return GetValue(SelectedCommandParameterProperty); }
            set { SetValue(SelectedCommandParameterProperty, value); }
        }


        #endregion

        // ------------------------------------------------------------

        #region Private Methods

        private void CreateStack()
        {
            Children.Clear();
            // Check for data
            if (ItemTemplate == null || ItemsSource == null || ItemsSource.Count() == 0 || ItemsSource.First() == null)
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
            var command = SelectedCommand ?? new Command((obj) =>
            {
                var args = new ItemTappedEventArgs(ItemsSource, item);
                ItemSelected?.Invoke(this, args);
            });
            var commandParameter = SelectedCommandParameter ?? item;

            var view = (View)ItemTemplate.CreateContent();
            var bindableObject = (BindableObject)view;
            if (bindableObject != null)
            {
                bindableObject.BindingContext = item;
            }
            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = command,
                CommandParameter = commandParameter,
                NumberOfTapsRequired = 1
            });

            return view;
        }

        #endregion
    }
}
