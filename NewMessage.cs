namespace Contacts;

public class NewMessage : ContentPage
{
    TableView table;
	Button sendButton;
	EntryCell phoneNumber, messageEntry;
    public NewMessage()
	{
		phoneNumber = new EntryCell
		{
            Placeholder = "Enter phone number",
            Keyboard = Keyboard.Telephone,
		};
        messageEntry = new EntryCell
		{
            Placeholder = "Enter your message...",
            VerticalTextAlignment = TextAlignment.Start,
            Keyboard = Keyboard.Default,
		};

        sendButton = new Button
        {
            Text = "Send SMS",
            BackgroundColor = Colors.White,
            BorderColor = Colors.Black,
            TextColor = Colors.Black,
            BorderWidth = 1,
            WidthRequest = 300,
            HeightRequest = 70,
            VerticalOptions = LayoutOptions.End,
        };
        sendButton.Clicked += SendButton_Clicked;

        table = new TableView
        {
            Root = new TableRoot
            {
                new TableSection
                {
                    phoneNumber,
                    messageEntry
                }
            },
            Intent = TableIntent.Form
        };

        Content = new VerticalStackLayout
        {
            Children =
            {
                table,
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
        string number = phoneNumber.Text;
		string message = messageEntry.Text;
		if (number != null && Sms.Default.IsComposeSupported)
		{
            var sms = new SmsMessage(message, number);
            await Sms.Default.ComposeAsync(sms);
		}
    }
}