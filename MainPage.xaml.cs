namespace Contacts
{
    public partial class MainPage : ContentPage
    {
        Label contactsLabel;
        Button newMessage;
        ScrollView scrollView;
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

            scrollView = new ScrollView
            {
                Padding = 10,
                HorizontalOptions = LayoutOptions.Start,
            };

            newMessage = new Button
            {
                Text = "New Mesage",
                BackgroundColor = Colors.WhiteSmoke,
                BorderColor = Colors.Black,
                TextColor = Colors.Black,
                BorderWidth = 1,
                WidthRequest = 300,
                HeightRequest = 70,
            };

            scrollView.AddLogicalChild(newMessage);

            Content = new VerticalStackLayout
            {
                Children = {
                    contactsLabel, scrollView,
                },
            };
        }


    }
}
