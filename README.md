# DynamicStackLayout
A Xamarin.Forms layout for creating dynamically wrapped views. Inspired by the `WrapLayout` example: https://developer.xamarin.com/samples/xamarin-forms/UserInterface/CustomLayout/WrapLayout/

## Installation

It's on NuGet! https://www.nuget.org/packages/DynamicStackLayout/
```
Install-Package DynamicStackLayout
```

Be sure to install in all projects that use it.

## Usage

There are two key properties that make this control useful - the `ItemsSource` (like a `ListView`) and the `ItemTemplate` (although, you can also just add children to the view - it does both!)
Be sure to wrap it in a `ScrollView` though

**XAML**

Add the `xmlns`:

```
xmlns:suave="clr-namespace:SuaveControls.DynamicStackLayout;assembly=SuaveControls.DynamicStackLayout"
```

Use it in your View:
```
<ScrollView>
    <suave:DynamicStackLayout ItemsSource="{Binding Items}" HorizontalOptions="Fill">
        <suave:DynamicStackLayout.ItemTemplate>
            <DataTemplate>
                <StackLayout BackgroundColor="Gray" WidthRequest="120" HeightRequest="180">
                    <Label Text="{Binding .}" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" VerticalTextAlignment="Center" HorizontalTextAlignment="Center" />
                </StackLayout>
            </DataTemplate>
        </suave:DynamicStackLayout.ItemTemplate>
    </suave:DynamicStackLayout>
</ScrollView>
```

Don't like data-binding and want to just use child views? You can do that too!

```
<ScrollView>
    <suave:DynamicStackLayout HorizontalOptions="Fill">
      <StackLayout BackgroundColor="Gray" WidthRequest="120" HeightRequest="180">
          <Label Text="0" TextColor="White" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" VerticalTextAlignment="Center" HorizontalTextAlignment="Center" />
      </StackLayout>
      <StackLayout BackgroundColor="Gray" WidthRequest="120" HeightRequest="180">
          <Label Text="1" TextColor="White" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" VerticalTextAlignment="Center" HorizontalTextAlignment="Center" />
      </StackLayout>
      <StackLayout BackgroundColor="Gray" WidthRequest="120" HeightRequest="180">
          <Label Text="2" TextColor="White" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" VerticalTextAlignment="Center" HorizontalTextAlignment="Center" />
      </StackLayout>
      <StackLayout BackgroundColor="Gray" WidthRequest="120" HeightRequest="180">
          <Label Text="3" TextColor="White" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" VerticalTextAlignment="Center" HorizontalTextAlignment="Center" />
      </StackLayout>
      <StackLayout BackgroundColor="Gray" WidthRequest="120" HeightRequest="180">
          <Label Text="4" TextColor="White" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" VerticalTextAlignment="Center" HorizontalTextAlignment="Center" />
      </StackLayout>
    </suave:DynamicStackLayout>
</ScrollView>
```

Check out this blog post for more details! https://alexdunn.org/2017/12/12/xamarin-nuget-xamarin-forms-dynamic-bindable-stacklayout/

## Features

- Bindable child views
- Bindable to collections
- Handles layout changing well (try rotating the device)
- Doesn't require custom renderers (All Xamarin.Forms baby!)

## What does this thing look like?

![horizontal scrolling stacklayout](https://alexdunndev.files.wordpress.com/2017/12/screen-shot-2017-12-12-at-1-16-43-pm.png?w=327&h=576)
![horizontal scrolling stacklayout](https://alexdunndev.files.wordpress.com/2017/12/screen-shot-2017-12-12-at-1-16-54-pm.png?w=325&h=576)
![horizontal scrolling stacklayout](https://alexdunndev.files.wordpress.com/2017/12/screen-shot-2017-12-12-at-1-17-08-pm.png?w=656&h=369)

## Notes
This does not use any native view virtualization, which means performance does not scale well with extremely large data sets.

## Coming soon

- `ItemSelected` event and `SelectedItem` bindable property (for now, you can add custom gestures and commands to your `DataTemplate` and handle the events yourself)
- Better Collection Updating
