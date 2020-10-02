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
using Android.Media;

namespace pedraPapelTesoura.Resources
{
    [Activity(Label = "tela_inicial", MainLauncher = true)]
    public class tela_inicial : Activity
    {
        DataBase db = new DataBase();

        EditText txtNome;
        Button btnJogar;
        Button btnRanking;
        MediaPlayer intro;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.tela_Inicial);

            CriarBancoDados();

            // Create your application here
            intro = MediaPlayer.Create(this, Resource.Raw.intro);
       
                    intro.Start();
           

            txtNome = FindViewById<EditText>(Resource.Id.txtNome);
            btnJogar = FindViewById<Button>(Resource.Id.btnJogar);
            btnRanking = FindViewById<Button>(Resource.Id.btnRanking);


            btnJogar.Click += delegate
            {
                if (txtNome.Text != "")
                {
                    intro.Stop();
                    Intent novaleta = new Intent(this, typeof(MainActivity));
                    novaleta.PutExtra("nome", txtNome.Text);
                    StartActivity(novaleta);
                }
                else
                {
                    string mensagem = string.Format("Insira um nickname!");
                    Toast.MakeText(this, mensagem, ToastLength.Short).Show();
                }
            };

            btnRanking.Click += BtnRanking_Click;
        
        }

        private void BtnRanking_Click(object sender, EventArgs e)
        {
            intro.Stop();
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