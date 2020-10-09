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
using Android.Support.V7.App;
using Android.Media;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.ComponentModel;

namespace pedraPapelTesoura
{
    [Activity(Label = "ranking")]
    public class ranking : Activity
    {
        MediaPlayer outro;
        ListView lvDados;
        List<Player> rankingPlayers = new List<Player>();
        List<Player> testeRanking = new List<Player>();
        listAdapter adapter;

        private const string FirebaseURL = "https://rpsls-1d228.firebaseio.com/"; //URL FIREBASE

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ranking);
            // Create your application here
            outro = MediaPlayer.Create(this, Resource.Raw.outro);




            outro.Start();

            if (savedInstanceState!=null)
            {
                outro.Stop();
            }

            lvDados = FindViewById<ListView>(Resource.Id.lvDados);

           await carregaDados();
        }



        private async Task carregaDados()
        {
            //  circular_progress.Visibility = ViewStates.Visible;
            lvDados.Visibility = ViewStates.Invisible;

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


                adapter = new listAdapter(this, rankingPlayers);
                rankingPlayers = rankingPlayers.OrderByDescending(x => player.Vitorias).ToList();
                adapter.NotifyDataSetChanged();
            lvDados.Adapter = adapter;
            lvDados.Visibility = ViewStates.Visible; 
            }
          /*  foreach (var item in itens)
            {
                Player player = new Player();
                player.Id = item.Key;
                player.nome = item.Object.nome;
                player.Vitorias = item.Object.Vitorias;

                testeRanking = rankingPlayers.OrderByDescending(x => player.Vitorias).ToList();

            }*/
            // circular_progress.Visibility = ViewStates.Invisible;
        }
    }
}