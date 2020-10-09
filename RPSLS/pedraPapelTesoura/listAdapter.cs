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
using static Android.Resource;

namespace pedraPapelTesoura
{
    class listAdapter : BaseAdapter<Player>
    {

        Activity context;
        List<Player> players;

        public listAdapter(Activity _context, List<Player> _players)
        {
            this.context = _context;
            this.players = _players;
        }

        public override Player this[int position]
        {
            get
            {
                return players[position];
            }

        }

        public override int Count
        {
            get
            {
                return players.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = players[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.listViewLayout, null);
            }
             view.FindViewById<TextView>(Resource.Id.txtvNome).Text=item.nome;
             view.FindViewById<TextView>(Resource.Id.txtvVitorias).Text=item.Vitorias.ToString();

            return view;
            //view.FindViewById<TextView>(Resource.Id.txtvVitorias).Text=item.senha;

            //   lvtxtNome.Text = players[position].nome;
            //   lvtxtVitorias.Text = "" + players[position].Vitorias;

            /*   if (holder == null)
               {
                   holder = new listAdapterViewHolder();
                   var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                   //replace with your item and your holder items
                   //comment back in
                   //view = inflater.Inflate(Resource.Layout.item, parent, false);
                   //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
                   view.Tag = holder;
               }


               //fill in your items
               //holder.Title.Text = "new text here";
                    */

        }

        //Fill in cound here, currently 0
     
       
       
    }
/*
    class listAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }*/
}