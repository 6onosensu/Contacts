namespace Contacts;

public partial class SendEmailPage : ContentPage
{
    Label emailLabel, nameLabel;
    Button sendButton;
    Entry messageEntry;
	string name, email;
    public SendEmailPage(string _name, string _email)
    {
        name = _name;
        email = _email;

        nameLabel = new Label
        {
            Text = name,
            FontSize = 30,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Blue,
        };
        emailLabel = new Label
        {
            Text = email,
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center,
        };

        messageEntry = new Entry
        {
            Placeholder = "Enter your message...",
            VerticalTextAlignment = TextAlignment.Start,
            Keyboard = Keyboard.Default,
        };

        sendButton = new Button
        {
            Text = "Send email",
            BackgroundColor = Colors.Blue,
            TextColor = Colors.WhiteSmoke,
            FontAttributes = FontAttributes.Bold,
            FontSize = 22,
            WidthRequest = 180,
            HeightRequest = 60,
            VerticalOptions = LayoutOptions.End,
        };
        sendButton.Clicked += SendButton_Clicked; ;

        Content = new VerticalStackLayout
        {
            Children =
            {
                nameLabel,
                emailLabel,
                messageEntry,
                sendButton,
            },
            Spacing = 10,
            Padding = 10,
            BackgroundColor = Colors.WhiteSmoke,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
        };
    }

    private async void SendButton_Clicked(object? sender, EventArgs e)
    {
        var message = messageEntry.Text;
        EmailMessage newEmail = new EmailMessage
        {
            Subject = email,
            Body = message,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string>(new[] { email })
        };
        if (Email.Default.IsComposeSupported) 
        { 
            await Email.Default.ComposeAsync(newEmail);
        }
    }
}