﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DynamicStackLayoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DynamicStackLayoutExamplePage : ContentPage
    {
        private ObservableCollection<string> _items;

        public ObservableCollection<string> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public DynamicStackLayoutExamplePage()
        {
            InitializeComponent();
            BindingContext = this;
            Items = new ObservableCollection<string>();
            stack.ItemSelected += Stack_ItemSelected;
            for (var i = 0; i < 40; i++)
                Items.Add(i.ToString());
        }

        private async void Stack_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            await DisplayAlert(e.Item.ToString(), null, "Cancel");
        }
    }
}
