namespace Contacts
{
    public class ContactSettingsPage : ContentPage
    {
        Contact contact;
        Entry nameEntry, emailEntry, phoneEntry, addressEntry, descriptionEntry;
        DbHelper dbHelper;

        public ContactSettingsPage(Contact contact)
        {
            this.contact = contact;
            dbHelper = new DbHelper();

            Title = "Edit Contact";

            nameEntry = new Entry { Text = contact.Name, Placeholder = "Name" };
            emailEntry = new Entry { Text = contact.Email, Placeholder = "Email", Keyboard = Keyboard.Email };
            phoneEntry = new Entry { Text = contact.PhoneNumber, Placeholder = "Phone", Keyboard = Keyboard.Telephone };
            addressEntry = new Entry { Text = contact.Address, Placeholder = "Address" };
            descriptionEntry = new Entry { Text = contact.Description, Placeholder = "Description" };

            Button saveButton = new Button
            {
                Text = "Save",
                BackgroundColor = Colors.Blue,
                TextColor = Colors.White
            };
            saveButton.Clicked += SaveButton_Clicked;

            Button deleteButton = new Button
            {
                Text = "Delete",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White
            };
            deleteButton.Clicked += DeleteButton_Clicked;

            Content = new StackLayout
            {
                Padding = new Thickness(20),
                Children =
                {
                    new Label { Text = "Edit Contact", FontAttributes = FontAttributes.Bold, FontSize = 20 },
                    nameEntry,
                    emailEntry,
                    phoneEntry,
                    addressEntry,
                    descriptionEntry,
                    saveButton,
                    deleteButton
                }
            };
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            contact.Name = nameEntry.Text;
            contact.Email = emailEntry.Text;
            contact.PhoneNumber = phoneEntry.Text;
            contact.Address = addressEntry.Text;
            contact.Description = descriptionEntry.Text;

            await dbHelper.SaveContact(contact);

            await DisplayAlert("Success", "Contact updated successfully!", "OK");
            await Navigation.PopAsync();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this contact?", "Yes", "No");

            if (confirm)
            {
                await dbHelper.DeleteContact(contact);
                await DisplayAlert("Success", "Contact deleted successfully!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
