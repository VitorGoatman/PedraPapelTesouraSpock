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
using SQLite;


namespace pedraPapelTesoura.Resources.Model
{
    class Player
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public string Nome { get; set; }

        public int Vitorias { get; set; }

        public Player() { }

    }
}