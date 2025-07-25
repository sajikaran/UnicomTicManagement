﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Controllers
{
    public class TimetableController
    {
        public void AddTimeSlot(TimeTable timeTable)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO TimeTable (TimeSlot) VALUES (@TimeSlot)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TimeSlot", timeTable.TimeSlot);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<TimeTable> GetAllTimeSlots()
        {
            List<TimeTable> timeTables = new List<TimeTable>();
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM TimeTable";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        timeTables.Add(new TimeTable
                        {
                            TId = reader.GetInt32(0),
                            TimeSlot = reader.GetString(1)
                        });
                    }
                }
            }
            return timeTables;
        }





        public void DeleteTimeSlot(int id)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM TimeTable WHERE TId = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }




        public void UpdateTimeSlot(TimeTable timeTable)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "UPDATE TimeTable SET TimeSlot = @TimeSlot WHERE TId = @TId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TimeSlot", timeTable.TimeSlot);
                    cmd.Parameters.AddWithValue("@TId", timeTable.TId);
                    cmd.ExecuteNonQuery();
                }
            }

        }

    }


}

