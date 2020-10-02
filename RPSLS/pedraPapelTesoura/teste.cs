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
using Android.Support.V7.App;
using Android;
using System.Drawing;
using Android.Media;
using Android.Graphics.Drawables;
using AlertDialog = Android.App.AlertDialog;
using pedraPapelTesoura.Resources.Model;
using pedraPapelTesoura.Resources.DataBaseHelper;
using pedraPapelTesoura;
using pedraPapelTesoura.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace pedraPapelTesoura
{
    [Activity(Label = "teste") ]
    public class teste : Activity
    {
        DataBase db = new DataBase();
        List<Player> rankingPlayers = new List<Player>();
        ListView lvDados;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.teste);


            CriarBancoDados();

            // Create your application here
            lvDados = FindViewById<ListView>(Resource.Id.lvDados);
            //db.GetVencedores(db.GetPlayer());

            CarregarDados();


        }

        private void CarregarDados()
        {
            rankingPlayers = db.GetPlayers();
            var adapter = new listAdapter(this, rankingPlayers);
            lvDados.Adapter = adapter;
        }

        //rotina para criar o banco de dados
        private void CriarBancoDados()
        {
            db = new DataBase();
            db.CriarBancoDeDados();
        }
    }
}