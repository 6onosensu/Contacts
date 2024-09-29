using Microsoft.Maui.Layouts;

namespace Contacts
{
    public partial class MainPage : ContentPage
    {
        DbHelper dbHelper;
        Label contactsLabel;
        Button newMessage, addContact;
        ScrollView scrollView;
        VerticalStackLayout verticalSL;
        HorizontalStackLayout horizontalSL;
        public MainPage()
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            AddAndLoadContacts();
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

            LoadContacts();

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
            LoadContacts();
        }
        private async void NewMessage_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMessage());
        }
        private async void AddAndLoadContacts()
        {
            await dbHelper.AddSampleContacts();

            LoadContacts();
        }
        private async void LoadContacts()
        {
            var contacts = await dbHelper.GetContacts();

            foreach (var contact in contacts)
            {
                var contactLayout = new HorizontalStackLayout
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    //FlexAlignContent = HorizontalAlignment.Justified,
                };

                Label contactLabel = new Label
                {
                    Text = contact.Name,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Start,
                };

                int index = contact.Id;
                contactLabel.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => OnContactTapped(index)),
                });

                var settingsIcon = new Image
                {
                    Source = "settings_icon.png",
                    WidthRequest = 30,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.End,
                };

                settingsIcon.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => OpenContactSettings(index)),
                });

                contactLayout.Children.Add(contactLabel);
                contactLayout.Children.Add(settingsIcon);

                verticalSL.Children.Add(contactLayout);
            }
        }
        private async void OnContactTapped(int index)
        {
            var contact = await dbHelper.GetContactById(index);
            await Navigation.PushAsync(new ContactDetailPage(contact));
        }
        private async void OpenContactSettings(int index)
        {
            var contact = await dbHelper.GetContactById(index);
            await Navigation.PushAsync(new ContactSettingsPage(contact.Id));
        }
    }
}
