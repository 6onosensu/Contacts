namespace Contacts;

public class NewContact : ContentPage
{
    ImageCell photo;
    EntryCell nameEntry, emailEntry, phoneEntry, addressEntry, descriptionEntry;
    public NewContact()
	{
        Title = "Add New Contact";
        nameEntry = new EntryCell
        {
            Placeholder = "Enter contact name"
        };

        emailEntry = new EntryCell
        {
            Placeholder = "Enter email address",
            Keyboard = Keyboard.Email
        };

        phoneEntry = new EntryCell
        {
            Placeholder = "Enter phone number",
            Keyboard = Keyboard.Telephone
        };

        addressEntry = new EntryCell
        {
            Placeholder = "Enter address"
        };

        descriptionEntry = new EntryCell
        {
            Placeholder = "Enter description"
        };
        TableView tableView = new TableView
        {
            Intent = TableIntent.Form,
            Root = new TableRoot("New Contact")
                {
                    new TableSection("Contact Information")
                    {
                        nameEntry,
                        emailEntry,
                        phoneEntry,
                        addressEntry,
                        descriptionEntry,
                    }
                }
        };

        Button addButton = new Button
        {
            Text = "Add",
            BackgroundColor = Colors.Blue,
            TextColor = Colors.WhiteSmoke,
            FontAttributes = FontAttributes.Bold,
            FontSize = 22,
            HeightRequest = 60,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
        addButton.Clicked += AddButton_Clicked;

        Content = new StackLayout
        {
            Children =
                {
                    tableView,
                    addButton
                },
            Padding = 10,
            VerticalOptions = LayoutOptions.FillAndExpand
        };
    }

    private async void AddButton_Clicked(object? sender, EventArgs e)
    {
        string name = nameEntry.Text;
        string email = emailEntry.Text;
        string phone = phoneEntry.Text;
        string address = addressEntry.Text;
        string description = descriptionEntry.Text;

        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(phone))
        {
            await DisplayAlert("Error", "Name and phone number are required.", "OK");
            return;
        }

        var newContact = new Contact
        {
            Name = name,
            Email = email,
            PhoneNumber = phone,
            Address = address,
            Description = description,
            Photo = "default_photo.jpg" 
        };

        var dbHelper = new DbHelper();
        await dbHelper.SaveContact(newContact);

        await DisplayAlert("Success", "Contact added successfully!", "OK");
        await Navigation.PopAsync();
    }
}