using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using pedraPapelTesoura.Resources.Model;
using pedraPapelTesoura;
using Android.Media;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Auth;


namespace pedraPapelTesoura.Resources
{
    [Activity(Label = "tela_inicial", MainLauncher = true)]
    public class tela_inicial : Activity
    {

        EditText txtNome;
        EditText txtSenha;
        Button btnJogar;
        Button btnRanking;
        MediaPlayer intro;
        List<Player> rankingPlayers = new List<Player>();
        listAdapter adapter;
        Player player;
        Button btnAdicionar;

        int i = 0;

        private const string FirebaseURL = "https://rpsls-1d228.firebaseio.com/"; //URL FIREBASE

        protected override async void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.tela_Inicial);
            btnAdicionar = FindViewById<Button>(Resource.Id.btnAdicionar);
            btnAdicionar.Click += BtnAdicionar_Click;

            // Create your application here
            intro = MediaPlayer.Create(this, Resource.Raw.intro);
       
                    intro.Start();
           

            txtNome = FindViewById<EditText>(Resource.Id.txtNome);
            txtSenha = FindViewById<EditText>(Resource.Id.txtSenha);
            btnJogar = FindViewById<Button>(Resource.Id.btnJogar);
            btnRanking = FindViewById<Button>(Resource.Id.btnRanking);


            btnJogar.Click += async delegate
            {
                if (txtNome.Text != "" || txtSenha.Text != "")
                {
                    var firebase = new FirebaseClient(FirebaseURL);

                    var itens = await firebase
                .Child("Players")
                .OnceAsync<Player>();

                    foreach (var item in itens)
                    {

                        Player player = new Player();
                        player.Id = item.Key;
                        player.nome = item.Object.nome;
                        player.Vitorias = item.Object.Vitorias;

                        if (txtNome.Text.Equals(item.Object.nome) && txtSenha.Text.Equals(item.Object.senha))
                        {
                            intro.Stop();
                            Intent novatela = new Intent(this, typeof(MainActivity));
                            novatela.PutExtra("Id", item.Key);
                            Console.WriteLine("placeholderfodase");
                            StartActivity(novatela);
                            return;
                        }
                        else
                        {
                            string mensagem = string.Format("Usuário não cadastrado!");
                            Toast.MakeText(this, mensagem, ToastLength.Short).Show();
                            
                            return;
                        }
                    }

                   
                   
                }
                else
                {
                    string mensagem = string.Format("Usuário não cadastrado!");
                    Toast.MakeText(this, mensagem, ToastLength.Short).Show();
                }
            };

            btnRanking.Click += BtnRanking_Click;

            await carregaDados();

        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            criarUsuario();
        }

        private async void criarUsuario()
        {
            Player player = new Player();
            player.Id = String.Empty;
            player.nome = txtNome.Text;
            player.senha = txtSenha.Text;

            var firebase = new FirebaseClient(FirebaseURL);
            //Add Item
            var item = await firebase.Child("Players").PostAsync<Player>(player);
            await carregaDados();
            txtNome.Text = "";
            txtSenha.Text = "";
        }

        private void BtnRanking_Click(object sender, EventArgs e)
        {
            intro.Stop();
            Intent novaleta = new Intent(this, typeof(ranking));
            StartActivity(novaleta);
        }

        private async Task carregaDados()
        {
          //  circular_progress.Visibility = ViewStates.Visible;
         //   lstUsuarios.Visibility = ViewStates.Invisible;

            var firebase = new FirebaseClient(FirebaseURL);

            var itens = await firebase
                .Child("Players")
                .OnceAsync<Player>();

            rankingPlayers.Clear();
            adapter = null;
            foreach (var item in itens)
            {
                Player player = new Player();
                player.Id = item.Key;
                player.nome = item.Object.nome;
                player.Vitorias = item.Object.Vitorias;

                rankingPlayers.Add(player);
                    /*
                adapter = new listAdapter(this, rankingPlayers);
                adapter.NotifyDataSetChanged();
                lstUsuarios.Adapter = adapter;
                lstUsuarios.Visibility = ViewStates.Visible; */
            }
           // circular_progress.Visibility = ViewStates.Invisible;
        }
    }
}