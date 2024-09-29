namespace Contacts
{
    public partial class MainPage : ContentPage
    {
        Label contactsLabel;
        Button newMessage, addContact;
        ScrollView scrollView;
        VerticalStackLayout verticalSL;
        HorizontalStackLayout horizontalSL;
        public MainPage()
        {
            contactsLabel = new Label
            {
                Text = "Contacts",
                FontAttributes = FontAttributes.Bold,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
            };

            verticalSL = new VerticalStackLayout
            {
                Padding = 10,
                Spacing = 40,
            };

            horizontalSL = new HorizontalStackLayout
            {
                Spacing = 10,
            };
            verticalSL.Children.Add(horizontalSL);

            newMessage = new Button
            {
                Text = "New Message",
                BackgroundColor = Colors.Blue,
                TextColor = Colors.WhiteSmoke,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                WidthRequest = 180,
                HeightRequest = 60,
            };
            newMessage.Clicked += NewMessage_Clicked;
            addContact = new Button
            {
                Text = "Add Contact",
                BackgroundColor = Colors.Blue,
                TextColor = Colors.WhiteSmoke,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                WidthRequest = 180,
                HeightRequest = 60,
            };
            addContact.Clicked += AddContact_Clicked;

            horizontalSL.Children.Add(addContact);
            horizontalSL.Children.Add(newMessage);

            for (int i = 1; i <= 10; i++) 
            {
                var contactLayout = new HorizontalStackLayout
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.Center,
                };

                Label contact = new Label
                {
                    Text = "Contact " + i,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Start,
                };

                int index = i;
                contact.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => OnContactTapped(index)),
                });

                var settingsIcon = new Image
                {
                    Source = "settings_icon.png",
                    WidthRequest = 30,
                    HeightRequest = 30,
                    VerticalOptions = LayoutOptions.Center,
                };

                settingsIcon.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => OpenContactSettings(index)),
                });

                contactLayout.Children.Add(contact);
                contactLayout.Children.Add(settingsIcon);

                verticalSL.Children.Add(contactLayout);
            };

            scrollView = new ScrollView
            {
                Content = verticalSL,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Content = new VerticalStackLayout
            {
                Children = {
                    contactsLabel, scrollView,
                },
            };
        }

        private async void AddContact_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewContact());
        }

        private async void NewMessage_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMessage());
        }

        private async void OnContactTapped(int index)
        {
            await Navigation.PushAsync(new ContactDetailPage(index));
        }
        private async void OpenContactSettings(int index)
        {
            await Navigation.PushAsync(new ContactSettingsPage(index));
        }
    }
}
