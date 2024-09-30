using Microsoft.Maui.ApplicationModel.Communication;

namespace Contacts;

public class ContactDetailPage : ContentPage
{
	Image photo;
	Label name, phoneNumber, email, address, description;
	public ContactDetailPage(Contact contact)
	{
        Grid contactGrid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Auto },
            },
            RowSpacing = 10,
            Padding = 20,
            Margin = 10,
        };

        name = new Label
        {
            Text = contact.Name,
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
        };
        contactGrid.Add(name, 0, 0);

        photo = new Image
        {
            Source = contact.Photo,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        contactGrid.Add(photo, 0, 1);

        email = new Label
        {
            Text = contact.Email,
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Start
        };
        contactGrid.Add(email, 0, 2);

        phoneNumber = new Label
        {
            Text = contact.PhoneNumber,
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Start
        };
        contactGrid.Add(phoneNumber, 0, 3);

        address = new Label
        {
            Text = "Address: " + contact.Address,
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Start
        };
        contactGrid.Add(address, 0, 4);

        description = new Label
        {
            Text = "Description: " + contact.Description,
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Start
        };
        contactGrid.Add(description, 0, 5);

        Content = new ScrollView
        {
            Content = contactGrid
        };

        var buttonLayout = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 20
        };

        Button emailButton = new Button
        {
            Text = "Email",
            BackgroundColor = Colors.Blue,
            TextColor = Colors.White,
            WidthRequest = 100,
            FontAttributes = FontAttributes.Bold
        };
        emailButton.Clicked += EmailButton_Clicked;

        Button smsButton = new Button
        {
            Text = "SMS",
            BackgroundColor = Colors.Blue,
            TextColor = Colors.White,
            WidthRequest = 100,
            FontAttributes = FontAttributes.Bold
        };
        smsButton.Clicked += SmsButton_Clicked;

        Button callButton = new Button
        {
            Text = "Call",
            BackgroundColor = Colors.Blue,
            TextColor = Colors.White,
            WidthRequest = 100,
            FontAttributes = FontAttributes.Bold
        };
        callButton.Clicked += CallButton_Clicked;

        buttonLayout.Children.Add(emailButton);
        buttonLayout.Children.Add(smsButton);
        buttonLayout.Children.Add(callButton);

        contactGrid.Add(buttonLayout, 0, 6);
    }

    private void CallButton_Clicked(object? sender, EventArgs e)
    {
        
    }

    private async void SmsButton_Clicked(object? sender, EventArgs e)
    {
        var _name = name.Text;
        var phone = phoneNumber.Text;
        await Navigation.PushAsync(new SendSmsPage(_name, phone));
    }

    private async void EmailButton_Clicked(object? sender, EventArgs e)
    {
        var _name = name.Text;
        var _email = email.Text;
        await Navigation.PushAsync(new SendEmailPage(_name, _email));
    }
}