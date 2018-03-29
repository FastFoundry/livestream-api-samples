### Example of .net Application making Livestream Rest API calls

![Flow](flow.png)

### Prerequisite

You need to have .Net 4.6.1 on your system.

### Run your server

1. Edit '[YOUR_API_KEY]' with your secret key in 'Program.cs'
2. Edit '[YOUR_CLIENT_ID]' with your client id in 'Program.cs'
3. Edit 'SCOPE' with your desired scope ('readonly','playback',or 'all') in 'Program.cs'
4. Build project
5. Run
6. Open `http://localhost:8080/token` and you should see a JSON response of your token, timestamp, and client id to be made in a client request.

For more information please visit the documentation - https://livestream.com/developers/docs/api