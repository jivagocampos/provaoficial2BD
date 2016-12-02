using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 


namespace TempoMySQL
{

    class Program
    {
        static void Main(string[] args)
        {
           
            string[] linhas = System.IO.File.ReadAllLines(@"C:\001.txt");

           
           
            MySqlConnection conexao = null; 
            MySqlDataReader leituraVariavel = null;
            String bd = @"server=localhost;database=oficial01;userid=root;password=lectra125;";
            
            conexao = new MySqlConnection(bd);
            conexao.Open();

            
            // prepara para contar o tempo
            Stopwatch insert = new Stopwatch();
            Stopwatch select = new Stopwatch();

            insert.Start();
            //loop para percorrer as linhas
            foreach (string line in linhas)
            {
                //função para separar os dados id, data, dados geograficos
                string[] busca = line.Split(','); 
                String inserir = "INSERT INTO gps (idTaxi, data, local) VALUES('" + busca[0] + "', '" + busca[1] 
                    + "','" + busca[2] + busca[3] + "')"; //preparacao das variaveis

                MySqlCommand cm = new MySqlCommand(inserir, conexao);//prepara a insercao
                cm.ExecuteNonQuery();//executa a incersao
            }
            insert.Stop();//para o tempo

            select.Start();
            String trajetoria = "SELECT * FROM gps LIMIT 1000"; 
            MySqlCommand cmd = new MySqlCommand(trajetoria, conexao); 
            leituraVariavel = cmd.ExecuteReader(); 

            //listar resultados
            while (leituraVariavel.Read())
            {

                Console.WriteLine(leituraVariavel.GetString(0) + " | " + leituraVariavel.GetString(1) 
                    + " | " + leituraVariavel.GetString(2));

            }
            select.Stop();
            leituraVariavel.Close();

            Console.WriteLine("Tempo para insercao:" + insert.Elapsed);
            Console.WriteLine("Tempo de busca dos dados:" + select.Elapsed);
            Console.ReadKey();
        
      
        }

    }
}

