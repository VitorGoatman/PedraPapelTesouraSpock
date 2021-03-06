﻿using System;
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

namespace pedraPapelTesoura
{
    class listAdapter : BaseAdapter<Player>
    {

        Activity context;
        private List<Player> players;

        public listAdapter(Activity _context, List<Player> _players)
        {
            this.context = _context;
            this.players = _players;
        }
        public override int Count
        {
            get
            {
                return players.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return players[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = players[position];
            var view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.listViewLayout, null);
            var lvtxtNome = view.FindViewById<TextView>(Resource.Id.txtvNome);
            var lvtxtVitorias = view.FindViewById<TextView>(Resource.Id.txtvVitorias);

            lvtxtNome.Text = players[position].Nome;
            lvtxtVitorias.Text = "" + players[position].Vitorias;

            return view;
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
     
        public override Player this[int position]
        {
            get
            {
                return players[position];
            }
       
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
    }
/*
    class listAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }*/
}