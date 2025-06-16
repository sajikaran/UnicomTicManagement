using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Data
{
    public static class DataMigration
    {
        public static void CreateTable()
        {
            using (var connect = DataConnect.GetConnection())
            {
                string createTableCuSuQuery = @"
                                                 IF NOT EXISTS Courses (
                                                 CId INTEGER PRIMARY KEY AUTOINGREEMENT,
                                                    CName TEXT NOT NULL
                );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableCuSuQuery, connect))
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Course Table Created.");
                }

                {
                    string CreateTableSuQuery = @"IF NOT EXISTS Subject(SId INTEGER PRIMARY KEY AUTOINGREEMENT 
                                             SName TEXT NOT NULL, CId INTEGER,
                                             )";
                    SQLiteCommand suco = new SQLiteCommand(CreateTableSuQuery, connect);
                    suco.ExecuteNonQuery();
                    MessageBox.Show("Subject Table Created.");
                }

                { 
                string CreateTableStuQuery = @"IF NOT EXISTS Students(StId INTEGER PRIMARY KEY AUTOINGREEMENT,
                                             StName TEXT NOT NULL,
                                             CId INTEGER,
                                             FOREIGN KEY (CId) REFERENCES Course(CId))";

                using (SQLiteCommand cd = new SQLiteCommand(CreateTableStuQuery, connect))
                {
                    cd.ExecuteNonQuery();
                    MessageBox.Show("StudentTableCreated.");

                }

                }





                
                {
                    string AddExamQuery = @"IF NOT EXISTS Exams(EID INTEGER PRIMARYKEY AUTOINGREEMENT,
                                      ENAME TEXT NOT NULL)";

                    using (SQLiteCommand Conne = new SQLiteCommand(AddExamQuery, connect)) 
                    { 
                        Conne.ExecuteNonQuery();
                        MessageBox.Show("Exam Table Created");
                    }
                    
                }


                string AddMarksQuery = @"IF NOT EXISTS Marks(MID INTEGER PRIMARYKEY AUTOINGREEMENT,
                                          Marks TEXT NOT NULL)";

                using (SQLiteCommand MAR= new SQLiteCommand(AddMarksQuery,connect))
                {
                    MAR.ExecuteNonQuery();
                    MessageBox.Show("Exam Table Created");
                }


            }

            
        }
    }
}




