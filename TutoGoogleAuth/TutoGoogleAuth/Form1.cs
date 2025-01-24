using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;

namespace TutoGoogleAuth
{
    public partial class Form1 : Form
    {
        private readonly IConfiguration configuration;
        private readonly string _googleClientId;
        private readonly string _googleClientSecret;

        private bool isconnected = false;

        private Button connectButton;
        private Button disconnectButton;
        private Label statusLabel;
        private void SetupUI()
        {
            // Configuration fenêtre
            this.Text = "Google Auth";
            this.Width = 400;
            this.Height = 300;

            // Label statut
            statusLabel = new Label
            {
                Text = "Non connecté",
                Width = 200,
                Height = 30,
                Top = 20,
                Left = 100
            };
            this.Controls.Add(statusLabel);

            // Bouton connexion 
            connectButton = new Button
            {
                Text = "Connexion Google",
                Width = 200,
                Height = 40,
                Top = 70,
                Left = 100
            };
            connectButton.Click += ConnectButton_Click;
            this.Controls.Add(connectButton);

            // Bouton déconnexion
            disconnectButton = new Button
            {
                Text = "Déconnexion",
                Width = 200,
                Height = 40,
                Top = 130,
                Left = 100,
                Enabled = false
            };
            disconnectButton.Click += DisconnectButton_Click;
            this.Controls.Add(disconnectButton);
        }
        public Form1()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<Form1>();
            
            configuration = builder.Build();
            _googleClientId = configuration["Authentication:Google:ClientId"];
            _googleClientSecret = configuration["Authentication:Google:ClientSecret"];

            InitializeComponent();
            SetupUI();
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isconnected)
                {
                    connectButton.Enabled = false;
                }
                var userInfo = await GetUserInfoAsync();
                if(userInfo.email != null)
                {
                    MessageBox.Show($"Connexion réussie!\nEmail: {userInfo.email}\nID: {userInfo.id}",
    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    statusLabel.Text = "Connecté";
                    isconnected = true;
                    disconnectButton.Enabled = true;
                }

            }
            catch
            {

            }

        }


        private async void DisconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                await SignOutAsync();
                isconnected = false;
                connectButton.Enabled = true;
                disconnectButton.Enabled = false;
                statusLabel.Text = "Non connecté";
            }
            catch
            {
            
            }
        }

        private async Task SignOutAsync()
        {
            var dataStore = new FileDataStore("GoogleAuth.Store");
            await dataStore.ClearAsync();
        }

        private async Task<(string email, string id)> GetUserInfoAsync()
        {
            var secrets = new ClientSecrets
            {
                ClientId = _googleClientId,
                ClientSecret = _googleClientSecret
            };

            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = new[] { "openid", "https://www.googleapis.com/auth/userinfo.email" },
                DataStore = new FileDataStore("GoogleAuth.Store")
            });

            var credential = await new AuthorizationCodeInstalledApp(codeFlow, new LocalServerCodeReceiver()).
                AuthorizeAsync("user", CancellationToken.None);

            if (credential != null) {
                var service = new Google.Apis.Oauth2.v2.Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer{
                    HttpClientInitializer = credential
                });
                var userInfos = await service.Userinfo.Get().ExecuteAsync();
                return (userInfos.Email, userInfos.Id);


            }
            return (null, null);
        }

    }
}
