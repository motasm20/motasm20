using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace Beast.Classes
{
    class DbHandler
    {
        //Hier maken we een MySqlConnection met o.a. Server;Database;Uid;Pwd;
        MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=beast2;Uid=root;Pwd=;");
        public DataTable beastnummer(string beastnummer)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                MySqlCommand myCommand = _connection.CreateCommand();
                
                myCommand.CommandText = $"SELECT * FROM `demo` WHERE `beastId` = '{beastnummer}'";
                
                
                
                MySqlDataReader reader = myCommand.ExecuteReader();
                
                dt.Load(reader);

            }
            catch (Exception e)
            {
                MessageBox.Show("your kkr database is broken fix it!!!\n" + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public DataTable GetNumber()
        {
            //Maak een DataTable
            DataTable mytable = new DataTable();
            try
            {
                //Open de verbinding
                _connection.Open();
                //Maak een Commando via de connection
                MySqlCommand myCommand = _connection.CreateCommand();
                //Zet de CommandText
                myCommand.CommandText = "SELECT * FROM `demo`";
                //Voer de MySqlDataReader uit
                MySqlDataReader reader = myCommand.ExecuteReader();
                //Laad het resultaat in de DataTable
                mytable.Load(reader);
            }
            catch (Exception e)
            {
                MessageBox.Show("your kkr database is broken fix it!!!\n" + e.Message);
            }
            finally
            {
                //Sluit de verbinding
                _connection.Close();
            }


            //Return de DataTable
            return mytable;
        }

        public int UpdateNumber(string waarde, string rijnummer, string kleur)
        {
            int amountChanged = 0;
            try
            {
                // Open de verbinding
                _connection.Open();

                // Maak een Commando via de connection
                MySqlCommand myCommand = _connection.CreateCommand();

                // Zet de CommandText met de nieuwe tekst en voeg een WHERE-clausule toe

                myCommand.CommandText = $"UPDATE `demo` SET `nummer`={waarde}, `kleur`='{kleur}' WHERE `beastId`= {rijnummer}";


                // ExecuteNonQuery en zet het resultaat in de int
                amountChanged = myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fout opgetreden bij het bijwerken van de database: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }

            return amountChanged;
        }

        public int toevoegen(string waarde, string kleur)
        {
            int amountChanged = 0;
            try
            {
                _connection.Open();
                MySqlCommand myCommand = _connection.CreateCommand();

                myCommand.CommandText = $"INSERT INTO `demo`(`nummer`, `kleur`) VALUES ('{waarde}','{kleur}')";

                amountChanged = myCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Fout opgetreden bij het bijwerken van de database: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return amountChanged;
        }
        public int Verwijderen(string rijnummer)
        {
            int amountChanged = 0;
            try
            {
                _connection.Open();
                MySqlCommand myCommand = _connection.CreateCommand();

                myCommand.CommandText = $"DELETE FROM `demo` WHERE `beastId`= {rijnummer}";

                amountChanged = myCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Fout opgetreden bij het bijwerken van de database: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return amountChanged;
        }
    }
}
