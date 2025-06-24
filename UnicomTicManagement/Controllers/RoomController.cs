using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Controllers
{
    public class RoomController
    {

        public void AddRoom(Room room)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Room (RName, TId, SuId) VALUES (@RName, @TId, @SuId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RName", room.RName); // ✅ correct param
                    cmd.Parameters.AddWithValue("@TId", room.TId);
                    cmd.Parameters.AddWithValue("@SuId", room.SuId);
                    cmd.ExecuteNonQuery();
                }
            }

        }


        public List<Room> GetAllRooms()
        {

            var rooms = new List<Room>();

            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "SELECT RId, RName, TId, SuId FROM Room";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rooms.Add(new Room
                        {
                            RId = reader.GetInt32(0),
                            RName = reader.GetString(1),
                            TId = reader.GetInt32(2),
                            SuId = reader.GetInt32(3)
                        });
                    }
                }
            }

            return rooms;
        }


        public void DeleteRoom(int roomId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Room WHERE RId = @RId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RId", roomId);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void UpdateRoom(Room room)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Room SET RName = @RName, TId = @TId, SuId = @SuId WHERE RId = @RId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RName", room.RName);
                    cmd.Parameters.AddWithValue("@TId", room.TId);
                    cmd.Parameters.AddWithValue("@SuId", room.SuId);
                    cmd.Parameters.AddWithValue("@RId", room.RId);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }


}