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

namespace pedraPapelTesoura.Resources
{
    [Activity(Label = "tela_inicial", MainLauncher = true)]
    public class tela_inicial : Activity
    {
        DataBase db = new DataBase();

        EditText txtNome;
        Button btnJogar;
        Button btnRanking;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.tela_Inicial);

            CriarBancoDados();

            // Create your application here

            txtNome = FindViewById<EditText>(Resource.Id.txtNome);
            btnJogar = FindViewById<Button>(Resource.Id.btnJogar);
            btnRanking = FindViewById<Button>(Resource.Id.btnRanking);


            btnJogar.Click += delegate
            {
                Player player = new Player()
                {
                    Nome = txtNome.Text
                };
                db.InserirPlayer(player);

                Intent novaleta = new Intent(this, typeof(MainActivity));
                StartActivity(novaleta);
            };

            btnRanking.Click += BtnRanking_Click;
        
        }

        private void BtnRanking_Click(object sender, EventArgs e)
        {
            Intent novaleta = new Intent(this, typeof(ranking));
            StartActivity(novaleta);
        }

        private void CriarBancoDados()
        {
            db = new DataBase();
            db.CriarBancoDeDados();
        }
    }
}