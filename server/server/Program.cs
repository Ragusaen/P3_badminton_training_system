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
            AgeRule Testrule2 = new AgeRule();
            SexRule Testrule3 = new SexRule();
            CorrectLineup Testrule4 = new CorrectLineup();
            ExtraPlayer Testrule5 = new ExtraPlayer();
            Lineup Linetest = new Lineup();
            Testrule.Rule(Linetest);
            Testrule2.Rule(Linetest);
            Testrule3.Rule(Linetest);
            Testrule4.Rule(Linetest);
            Testrule5.Rule(Linetest);
        }
    }
}
