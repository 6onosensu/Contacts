namespace Contacts
{
    internal class FileService
    {
        public async Task WriteTextToFile(string text, string targetFileName)
        {
            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
            using (FileStream outputStream = File.OpenWrite(targetFile))
            using (StreamWriter streamWriter = new StreamWriter(outputStream))
            {
                await streamWriter.WriteAsync(text);
            }
        }

        public async Task<string> ReadTextFromFile(string targetFileName)
        {
            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
            using (FileStream inputStream = File.OpenRead(targetFile))
            using (StreamReader reader = new StreamReader(inputStream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private string GetContactFilePath(string contactId)
        {
            return Path.Combine(FileSystem.Current.AppDataDirectory, $"{contactId}.txt");
        }

        public async Task WriteMessageToContactFile(string contactId, string message, bool isReceived)
        {
            string contactFilePath = GetContactFilePath(contactId);
            using (FileStream outputStream = File.Open(contactFilePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter streamWriter = new StreamWriter(outputStream))
            {
                // Prepend an asterisk if the message is received
                string formattedMessage = isReceived ? $"* {message}" : message;
                await streamWriter.WriteLineAsync(formattedMessage);
            }
        }

        public async Task<string> ReadMessagesFromContactFile(string contactId)
        {
            string contactFilePath = GetContactFilePath(contactId);
            if (!File.Exists(contactFilePath))
            {
                return "Start communication with this person!";
            }

            using (FileStream inputStream = File.OpenRead(contactFilePath))
            using (StreamReader reader = new StreamReader(inputStream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
