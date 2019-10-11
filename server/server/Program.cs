using MySql.Data.MySqlClient;
using server.DAL;
using Server.Controller;
using System;
using System.Data;
using System.Data.SqlClient;
using server.Model.Rules;
using server.Model;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Reserves Testrule = new Reserves();
            Lineup Linetest = new Lineup();
            Testrule.Rule(Linetest);
        }
    }
}
