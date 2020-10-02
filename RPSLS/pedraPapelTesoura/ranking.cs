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
using pedraPapelTesoura.Resources.DataBaseHelper;
using pedraPapelTesoura;
using Android.Support.V7.App;
using Android.Media;

namespace pedraPapelTesoura
{
    [Activity(Label = "ranking")]
    public class ranking : Activity
    {
        MediaPlayer outro;
        ListView lvDados;
        List<Player> rankingPlayers = new List<Player>();
        DataBase db = new DataBase();
        protected override void OnCreate(Bundle savedInstanceState)
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

            CriarBancoDados();
            lvDados = FindViewById<ListView>(Resource.Id.lvDados);
            CarregarDados();
        }
        private void CriarBancoDados()
        {
            db = new DataBase();
            db.CriarBancoDeDados();
        }
        //Obtem todos os alunos da tabela Aluno e exibe no ListView
        private void CarregarDados()
        {
            rankingPlayers = db.GetPlayers();
            var adapter = new listAdapter(this, rankingPlayers);
            lvDados.Adapter = adapter;
        }
    }
}