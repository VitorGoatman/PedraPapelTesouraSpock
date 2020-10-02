using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using pedraPapelTesoura.Resources.Model;
using SQLite;

namespace pedraPapelTesoura.Resources.DataBaseHelper
{
    class DataBase
    {
        string pasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CriarBancoDeDados()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Players.db")))
                {
                    conexao.CreateTable<Player>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public bool InserirPlayer(Player player)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Players.db")))
                {
                    conexao.Insert(player);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public List<Player> GetPlayers()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Players.db")))
                {
                    return conexao.Table<Player>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public string GetVencedores(string Nome)
        {
            try
            {
                using (var conn = new SQLiteConnection(System.IO.Path.Combine(pasta, "Players.db")))
                {
                    var result = conn.Query<Player>("SELECT * FROM Player ORDER BY Vitorias");
                    foreach (Player player in result)
                    {
                        if (player.Nome.Equals(Nome))
                        {
                            string resp = player.Vitorias.ToString();
                            return resp;
                        }
                    }
                    return string.Empty;
                }
            }
            catch (SQLiteException err)
            {
                Log.Info("SQLiteEx: ", err.Message);
                return null;
            }
        }

        public bool AtualizarPlayer(Player player)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Players.db")))
                {
                    conexao.Query<Player>("UPDATE Player set Nome=?,Vitorias=? Where Id=?", player.Nome, player.Vitorias , player.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public bool GetPlayer(int Id)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Players.db")))
                {
                    conexao.Query<Player>("SELECT * FROM Player Where Id=?", Id);
                    //conexao.Update(aluno);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

    }

}