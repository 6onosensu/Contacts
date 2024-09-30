

namespace Contacts;
public class SendSmsPage : ContentPage
{
    Button sendButton;
    Entry messageEntry;
    Label nameLabel, phoneLabel;
    public SendSmsPage(string name, string phone)
	{
        nameLabel = new Label
        {
            Text = name,
            FontSize = 30,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Blue,
        };
        phoneLabel = new Label
        {
            Text = phone,
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
            Text = "Send SMS",
            BackgroundColor = Colors.Blue,
            TextColor = Colors.WhiteSmoke,
            FontAttributes = FontAttributes.Bold,
            FontSize = 22,
            WidthRequest = 180,
            HeightRequest = 60,
            VerticalOptions = LayoutOptions.FillAndExpand,
        };
        sendButton.Clicked += async (sender, args) =>
        {
            string message = messageEntry.Text;
            if (phone != null && Sms.Default.IsComposeSupported)
            {
                var sms = new SmsMessage(message, phone);
                await Sms.Default.ComposeAsync(sms);
            }
        };

        Content = new StackLayout
        {
            Children =
            {
                nameLabel,
                phoneLabel,
                messageEntry,
                sendButton,
            },
            Spacing = 10,
            Padding = 10,
            BackgroundColor = Colors.WhiteSmoke,
            VerticalOptions = LayoutOptions.FillAndExpand,
        };
    }
}