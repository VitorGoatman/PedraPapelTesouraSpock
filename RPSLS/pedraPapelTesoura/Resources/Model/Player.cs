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
using SQLite;


namespace pedraPapelTesoura.Resources.Model
{
    class Player
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }

        public string senha { get; set; }
        public string nome { get; set; }

        public int Vitorias { get; set; }

        public Player() { }

        public Player(string nome, int Vitorias)
        {
            this.nome = nome;
            this.Vitorias = Vitorias;
        }

        public Player(string nome, string senha)
        {
            this.nome = nome;
            this.senha = senha;
        }
    }
}