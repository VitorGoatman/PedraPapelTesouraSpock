using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android;
using System.Drawing;
using Android.Views;
using Android.Media;
using Android.Graphics.Drawables;
using AlertDialog = Android.App.AlertDialog;
using pedraPapelTesoura.Resources.Model;
using pedraPapelTesoura.Resources.DataBaseHelper;
using pedraPapelTesoura;
using System.Collections.Generic;
using Android.Content;
using pedraPapelTesoura.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace pedraPapelTesoura
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = false   )]
    public class MainActivity : AppCompatActivity
    {
        DataBase db = new DataBase();
        List<Player> rankingPlayers = new List<Player>();

        const int pedra = 1;
        const int tesoura = 2;
        const int papel = 3;
        const int lagarto = 4;
        const int spock = 5;
        int vitorias = 0;
        int pontosPlayer = 0;
        int pontosComputer = 0;
        int empates = 0;
        string placar;

        MediaPlayer vitoria;
        MediaPlayer derrota;
        ImageButton Pedra;
        ImageButton Tesoura;
        ImageButton Papel;
        ImageButton Lagarto;
        ImageButton Spock;
        Random rnd = new Random();
        TextView Placar;
        Button Regras;
        ImageView ImagePlayer;
        ImageView ImageCPU;
        ToggleButton Audio;
        Button Teste;
        EditText txtNome;
        TextView nome;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            CriarBancoDados();
   

            vitoria = MediaPlayer.Create(this, Resource.Raw.victory);
            derrota = MediaPlayer.Create(this, Resource.Raw.defeat);

            Pedra = FindViewById<ImageButton>(Resource.Id.btnPedra);
            Tesoura = FindViewById<ImageButton>(Resource.Id.btnTesoura);
            Papel = FindViewById<ImageButton>(Resource.Id.btnPapel);
            Lagarto = FindViewById<ImageButton>(Resource.Id.btnLagarto);
            Spock = FindViewById<ImageButton>(Resource.Id.btnSpock);
            Placar = FindViewById<TextView>(Resource.Id.txtvPlacar);
            ImagePlayer = FindViewById<ImageView>(Resource.Id.imageViewPlayer);
            ImageCPU = FindViewById<ImageView>(Resource.Id.imageViewCPU);
            Regras = FindViewById<Button>(Resource.Id.btnRegras);
            Audio = FindViewById<ToggleButton>(Resource.Id.toggleBtn);
            Teste = FindViewById<Button>(Resource.Id.txtvT);

            nome = FindViewById<TextView>(Resource.Id.nome);
            if (Intent.GetStringExtra("nome") != null)
                nome.Text = Intent.GetStringExtra("nome").ToString();

            txtNome = FindViewById<EditText>(Resource.Id.txtNome);

            CarregarDados();
            Pedra.Click += Pedra_Click;
            Tesoura.Click += Tesoura_Click;
            Papel.Click += Papel_Click;
            Lagarto.Click += Lagarto_Click;
            Spock.Click += Spock_Click;
            Regras.Click += Regras_Click;
            Audio.Click += Musica_Click;
            Teste.Click += Teste_Click;

            Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
            placar = Placar.Text;

            if (savedInstanceState != null) {
                placar = savedInstanceState.GetString("savePlacar");
                Placar.Text = placar;
                pontosPlayer = savedInstanceState.GetInt("saveScoreP");
                pontosComputer = savedInstanceState.GetInt("saveScoreC");
                empates = savedInstanceState.GetInt("saveScoreE");
                int playerId = savedInstanceState.GetInt("Player");
                int computerId = savedInstanceState.GetInt("CPU");
                // ImagePlayer.SetImageResource(playerId);
                // ImageCPU.SetImageResource(computerId);
                //Teste.Text = "Player: " + playerId.ToString() + "CPU : " + computerId.ToString();
                
            }
           
        }

        private void Teste_Click(object sender, EventArgs e)
        {
            Player player = new Player()
            {
                Nome = nome.Text,
               // Id = int.Parse(txtNome.Tag.ToString()),
                Vitorias = vitorias
            };
            db.InserirPlayer(player);
            CarregarDados();

            AlertDialog.Builder caixa = new AlertDialog.Builder(this);
            caixa.SetTitle("Obrigado por jogar!");
            caixa.SetMessage("Confira sua posição no ranking!");
            caixa.Show();
            StartTimer();

    //        Intent telainicial = new Intent(this, typeof(tela_inicial));
      //      StartActivity(telainicial);
        }

        public async void StartTimer()
        {
            await Task.Delay(5000); //60 minutes
            StartActivity(typeof(tela_inicial));
        }

        private void CarregarDados()
        {
            rankingPlayers = db.GetPlayers();
            var adapter = new listAdapter(this, rankingPlayers);
           // lvDados.Adapter = adapter;
        }

        //rotina para criar o banco de dados
        private void CriarBancoDados()
        {
            db = new DataBase();
            db.CriarBancoDeDados();
        }

        private void Musica_Click(object sender, EventArgs e)
        {
            if (Audio.Checked)
            {
                Audio.SetBackgroundResource(Resource.Drawable.audioOn);
                string mensagem = string.Format("Efeitos sonoros ligados");
                Toast.MakeText(this, mensagem, ToastLength.Short).Show();
                
            }
            else
            {
                Audio.SetBackgroundResource(Resource.Drawable.audioOff);

                string mensagem = string.Format("Efeitos sonoros desligados");
                Toast.MakeText(this, mensagem, ToastLength.Short).Show();
            }
        }

        private void Regras_Click(object sender, EventArgs e)
        {
           AlertDialog.Builder caixa = new AlertDialog.Builder(this);
            caixa.SetTitle("Regras");
            caixa.SetMessage("Pedra, papel, tesoura, lagarto, Spock. É muito simples. Olhe – "
                + "tesoura corta papel, papel cobre pedra, pedra esmaga lagarto, "
                + "lagarto envenena Spock, Spock esmaga tesoura, tesoura decapita lagarto,"
                + "lagarto come papel, papel refuta Spock,"
                + " Spock vaporiza pedra e como sempre, pedra quebra tesoura.");
            caixa.Show();
        }

        private void Spock_Click(object sender, EventArgs e)
        {
            int teste = rnd.Next(1, 6);

            ImagePlayer.SetImageResource(Resource.Drawable.spock);
            ImagePlayer.Visibility = ViewStates.Visible;
            if (teste == tesoura)
            {
                pontosPlayer++;

                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.tesoura);
            }
            else if (teste == pedra)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.pedra);
            }
            else if (teste == spock)
            {
                empates++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.spock);
            }
            else if (teste == lagarto)
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.lagarto);
            }
            else
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.papel);
            }
            Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
            placar = Placar.Text;
            if (pontosComputer == 5)
            {
                if (Audio.Checked)
                {
                    derrota.Start();
                }

                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("O CPU marcou 5 pontos! Você perdeu!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;
               
            }
            if (pontosPlayer == 5)
            {
                if (Audio.Checked)
                {
                    vitoria.Start();
                }

                vitorias++;
                Player player = new Player()
                {
                    //Id = int.Parse(txtNome.Tag.ToString()),
                    Vitorias = vitorias
                };
                db.AtualizarPlayer(player);
                CarregarDados();

                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("Você marcou 5 pontos! Você ganhou!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;
            }

        }

        private void Lagarto_Click(object sender, EventArgs e)
        {
            int teste = rnd.Next(1, 6);
            ImagePlayer.SetImageResource(Resource.Drawable.lagarto);
            ImagePlayer.Visibility = ViewStates.Visible;
            if (teste == spock)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.spock);
            }
            else if (teste == papel)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.papel);
            }
            else if (teste == lagarto)
            {
                empates++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.lagarto);
            }
            else if (teste == pedra)
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.pedra);
            }
            else
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.tesoura);
            }
            Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
            placar = Placar.Text;
            if (pontosComputer == 5)
            {
                if (Audio.Checked)
                {
                    derrota.Start();
                }

                AlertDialog.Builder caixa = new AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("O CPU marcou 5 pontos! Você perdeu!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;
            }
            if (pontosPlayer == 5)
            {
                if (Audio.Checked)
                {
                    vitoria.Start();
                }

                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("Você marcou 5 pontos! Você ganhou!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;

                vitorias++;
               
            }
        }

        private void Pedra_Click(object sender, EventArgs e)
        {
            int teste = rnd.Next(1, 6);
            ImagePlayer.SetImageResource(Resource.Drawable.pedra);
            ImagePlayer.Visibility = ViewStates.Visible;
            if (teste == tesoura)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.tesoura);
            }
            else if (teste == lagarto)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.lagarto);
            }
            else if (teste == pedra)
            {
                empates++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.pedra);
            }
            else if (teste == spock)
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.spock);
            }
            else
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.papel);
            }
            Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
            if (pontosComputer == 5)
            {
                if (Audio.Checked)
                {
                    derrota.Start();
                }

               AlertDialog.Builder caixa = new AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("O CPU marcou 5 pontos! Você perdeu!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;
            }
            if (pontosPlayer == 5)
            {
                if (Audio.Checked)
                {
                    vitoria.Start();
                }

               AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("Você marcou 5 pontos! Você ganhou!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;

                vitorias++;
                Player player = new Player()
                {
                 //   Id = int.Parse(txtNome.Tag.ToString()),
                    Vitorias = vitorias
                };
                db.AtualizarPlayer(player);
                CarregarDados();
            }
        }
        private void Papel_Click(object sender, EventArgs e)
        {

            ImagePlayer.SetImageResource(Resource.Drawable.papel);
            ImagePlayer.Visibility = ViewStates.Visible;
            int teste = rnd.Next(1, 6);
            if (teste == pedra)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.pedra);
            }
            else if (teste == spock)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.spock);
            }
            else if (teste == papel)
            {
                empates++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.papel);
            }
            else if (teste == lagarto)
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.lagarto);
            }
            else
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.tesoura);
            }
            Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
            placar = Placar.Text;
            if (pontosComputer == 5)
            {
                if (Audio.Checked)
                {
                    derrota.Start();
                }

                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("O CPU marcou 5 pontos! Você perdeu!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;
            }
            if (pontosPlayer == 5)
            {
                if (Audio.Checked)
                {
                    vitoria.Start();
                }

                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("Você marcou 5 pontos! Você ganhou!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;

                vitorias++;
                Player player = new Player()
                {
                   // Id = int.Parse(txtNome.Tag.ToString()),
                    Vitorias = vitorias
                };
                db.AtualizarPlayer(player);
                CarregarDados();
            }
        }

        private void Tesoura_Click(object sender, EventArgs e)
        {
            int teste = rnd.Next(1, 6);
            ImagePlayer.SetImageResource(Resource.Drawable.tesoura);
            ImagePlayer.Visibility = ViewStates.Visible;
            if (teste == papel)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.papel);
            }
            else if (teste == lagarto)
            {
                pontosPlayer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.lagarto);
            }
            else if (teste == tesoura)
            {
                empates++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.tesoura);
            }
            else if (teste == spock)
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.spock);
            }
            else
            {
                pontosComputer++;
                ImageCPU.Visibility = ViewStates.Visible;
                ImageCPU.SetImageResource(Resource.Drawable.pedra);
            }
            Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
            placar = Placar.Text;
            if (pontosComputer == 5)
            {
                if (Audio.Checked)
                {
                    derrota.Start();
                }

                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("O CPU marcou 5 pontos! Você perdeu!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;
            }
            if (pontosPlayer == 5)
            {
                if (Audio.Checked)
                {
                    vitoria.Start();
                }

                Android.App.AlertDialog.Builder caixa = new Android.App.AlertDialog.Builder(this);
                caixa.SetTitle("Fim de jogo!");
                caixa.SetMessage("Você marcou 5 pontos! Você ganhou!");
                caixa.Show();
                pontosComputer = 0;
                pontosPlayer = 0;
                empates = 0;
                Placar.Text = "Jogador: " + pontosPlayer.ToString() + "     CPU: " + pontosComputer.ToString() + "     Empates: " + empates.ToString();
                placar = Placar.Text;

                vitorias++;
                Player player = new Player()
                {
                  //  Id = int.Parse(txtNome.Tag.ToString()),
                    Vitorias = vitorias
                };
                db.AtualizarPlayer(player);
                CarregarDados();
            }

        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("savePlacar",placar);
            outState.PutInt("saveScoreP",pontosPlayer);
            outState.PutInt("saveScoreC", pontosComputer);
            outState.PutInt("saveScoreE", empates);
            int playerId = ImagePlayer.Id;
            int computerId = ImageCPU.Id;
            outState.PutInt("Player", playerId);
            outState.PutInt("CPU", computerId);
 
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}