namespace Contacts;

public class NewMessage : ContentPage
{
    TableView table;
	Button sendButton;
	EntryCell phoneNumber, messageCell;
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

		phoneNumber = new EntryCell
		{
			Label = "Phone Number",
			Keyboard = Keyboard.Telephone,
		};
		messageCell = new EntryCell
		{
			Label = "Message..",
			Keyboard = Keyboard.Default,
		};
        table = new TableView
		{
			Intent = TableIntent.Form,
			Root = new TableRoot
			{
				new TableSection("To: ")
				{
					
				},
				new TableSection("Your message: ")
				{
					
				}
            },
            
        };
		
		Content = new VerticalStackLayout
		{
			Children = {
				table, 
				sendButton,
			},
            BackgroundColor = Colors.WhiteSmoke,
            HorizontalOptions = LayoutOptions.EndAndExpand,
            VerticalOptions = LayoutOptions.EndAndExpand,
        };
	}

    private async void SendButton_Clicked(object? sender, EventArgs e)
    {
        string number = phoneNumber.Text;
		string message = messageCell.Text;
		SmsMessage sms = new SmsMessage(message, number);
		if (number != null && Sms.Default.IsComposeSupported)
		{
			await Sms.Default.ComposeAsync(sms);
		}
    }
}