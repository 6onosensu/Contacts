namespace Contacts;

public class NewMessage : ContentPage
{
    TableView table;
	Button sendButton;
	Entry phoneNumber, messageEntry;
    public NewMessage()
	{
		sendButton = new Button
		{
			Text = "Send SMS",
			BackgroundColor = Colors.White,
			BorderColor = Colors.Black,
			TextColor = Colors.Black,
			BorderWidth = 1,
			WidthRequest = 300,
			HeightRequest = 70,
		};
        sendButton.Clicked += SendButton_Clicked;

		phoneNumber = new Entry
		{
			Text = "Phone Number",
			Keyboard = Keyboard.Telephone,
		};
        messageEntry = new Entry
		{
			Text = "Message..",
			Keyboard = Keyboard.Default,
		};
		
		Content = new VerticalStackLayout
		{
			Children = {
				phoneNumber,
				messageEntry,
				sendButton,
			},
            Spacing = 10,
            Padding = 10,
            BackgroundColor = Colors.WhiteSmoke,
            HorizontalOptions = LayoutOptions.EndAndExpand,
            VerticalOptions = LayoutOptions.EndAndExpand,
        };
	}

    private async void SendButton_Clicked(object? sender, EventArgs e)
    {
        string number = phoneNumber.Text;
		string message = messageEntry.Text;
		SmsMessage sms = new SmsMessage(message, number);
		if (number != null && Sms.Default.IsComposeSupported)
		{
			await Sms.Default.ComposeAsync(sms);
		}
    }
}