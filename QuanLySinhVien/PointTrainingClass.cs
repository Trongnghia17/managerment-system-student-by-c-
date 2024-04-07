using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    internal class PointTrainingClass
    {
        DBconnect connect = new DBconnect();
        //create a function add score
        public bool insertPointTraining(int stdid, double pt, string desc)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `pointtraining`(`StudentId`, `PointTraining`, `Description`) VALUES (@stid,@pt,@desc)", connect.getconnection);
            //@stid,@pt,@desc
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@pt", MySqlDbType.Double).Value = pt;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //create a functon to get list
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool checkPointTraining(int stdId)
        {
            DataTable table = getList(new MySqlCommand("SELECT * FROM `pointtraining` WHERE `StudentId`= '" + stdId  + "'"));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
        // Create A function to edit score data
        public bool updatePointTraining(int stdid, double pt, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `pointtraining` SET `PointTraining`=@pt,`Description`=@desc WHERE `StudentId`=@stid ", connect.getconnection);
            //@stid,@sco,@desc
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@pt", MySqlDbType.Double).Value = pt;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //Create a function to delete a score data
        public bool deletePointTraining(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `pointtraining` WHERE `StudentId`=@id", connect.getconnection);

            //@id
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
    }
}
