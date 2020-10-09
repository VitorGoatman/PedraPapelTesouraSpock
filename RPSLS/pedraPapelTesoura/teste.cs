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
using pedraPapelTesoura;
using pedraPapelTesoura.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace pedraPapelTesoura
{
    [Activity(Label = "teste") ]
    public class teste : Activity
    {
        List<Player> rankingPlayers = new List<Player>();
        ListView lvDados;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.teste);


            // Create your application here
            lvDados = FindViewById<ListView>(Resource.Id.lvDados);
            //db.GetVencedores(db.GetPlayer());  


        }

    }
}