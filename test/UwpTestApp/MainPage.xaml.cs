﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Text;
using Auth0.OidcClient;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Auth0Client _auth0Client;

        public MainPage()
        {
            this.InitializeComponent();

            _auth0Client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "jerrie.auth0.com",
                ClientId = "vV9twaySQzfGesS9Qs6gOgqDsYDdgoKE"
            });
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = "";

            var loginResult = await _auth0Client.LoginAsync();

            // Display error
            if (loginResult.IsError)
            {
                resultTextBox.Text = loginResult.Error;
                return;
            }

            // Display result
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Tokens");
            sb.AppendLine("------");
            sb.AppendLine($"id_token: {loginResult.IdentityToken}");
            sb.AppendLine($"access_token: {loginResult.AccessToken}");
            sb.AppendLine($"refresh_token: {loginResult.RefreshToken}");
            sb.AppendLine();

            sb.AppendLine("Claims");
            sb.AppendLine("------");
            foreach (var claim in loginResult.User.Claims)
            {
                sb.AppendLine($"{claim.Type}: {claim.Value}");
            }

            resultTextBox.Text = sb.ToString();
        }

        private async void LogoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            await _auth0Client.LogoutAsync();
        }
    }
}
