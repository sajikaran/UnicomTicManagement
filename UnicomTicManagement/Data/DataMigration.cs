using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Models;
using static System.Net.Mime.MediaTypeNames;

namespace UnicomTicManagement.Data
{
    public static class DataMigration
    {
        public static void createTable()
        {
            using (var connection = DataConnect.GetConnection())
            {
                connection.Open();
                // Course table creation
                string createCourseTable = @"
                                            CREATE TABLE IF NOT EXISTS Course (
                                            CId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            CName TEXT NOT NULL 
                                             );
                                             ";
                SQLiteCommand createCourseCmd = new SQLiteCommand(createCourseTable, connection);
                createCourseCmd.ExecuteNonQuery();

                // Student table creation
                string createStudentTable = @"
                                                CREATE TABLE IF NOT EXISTS Student (
                                                 StId INTEGER PRIMARY KEY AUTOINCREMENT,
                                                     StName TEXT NOT NULL,
                                                     CId INTEGER,
                                                        FOREIGN KEY (CId) REFERENCES Course(CId)
                                                          );
                                                             ";
                SQLiteCommand createStudentCmd = new SQLiteCommand(createStudentTable, connection);
                createStudentCmd.ExecuteNonQuery();

                // Create Subject Table
                string createSubjectTable = @"
                                              CREATE TABLE IF NOT EXISTS Subject (
                                               SuId INTEGER PRIMARY KEY AUTOINCREMENT,
                                                SuName TEXT NOT NULL,
                                                CourseId INTEGER,
                                                 FOREIGN KEY (CourseId) REFERENCES Course(CId)
                                                   );";

                SQLiteCommand createSubjectCmd = new SQLiteCommand(createSubjectTable, connection);
                createSubjectCmd.ExecuteNonQuery();



                // Create ExamTable/////
                string ExamTable = @"
                                       CREATE TABLE IF NOT EXISTS Exam (
                                       ExId INTEGER PRIMARY KEY AUTOINCREMENT,
                                       ExName NOT NULL,
                                       SuId INTEGER,
                                       FOREIGN KEY (SuId) REFERENCES Subject(SuId)
                                        );";


                SQLiteCommand createExamCmd = new SQLiteCommand(ExamTable, connection);
                createExamCmd.ExecuteNonQuery();


                ///Crete TimeTable ///////
                string createTimeTable = @"
                                            CREATE TABLE IF NOT EXISTS TimeTable (
                                            TId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            TimeSlot TEXT NOT NULL
                                             );";
                SQLiteCommand createTimeCmd = new SQLiteCommand(createTimeTable, connection);
                createTimeCmd.ExecuteNonQuery();


                /////////CreateRoomTable//////////////////////////////////////////////////////////////////////////////
                string createRoomTable = @"CREATE TABLE IF NOT EXISTS Room (
                                            RId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            RName TEXT NOT NULL,
                                            TId INTEGER,
                                            SuId INTEGER,
                                            FOREIGN KEY (TId) REFERENCES TimeTable(TId),
                                            FOREIGN KEY (SuId) REFERENCES Subject(SuId))";
                SQLiteCommand createRoomCmd = new SQLiteCommand(createRoomTable, connection);
                createRoomCmd.ExecuteNonQuery();


                //////////////////////CreteMArksTable?////////////////////////////////////////////////////////////


                string createMarkTable = @"
                                            CREATE TABLE IF NOT EXISTS Mark (
                                            MId INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Marks REAL NOT NULL,
                                            StId INTEGER NOT NULL,
                                            SuId INTEGER NOT NULL,
                                            ExId INTEGER NOT NULL,
                                            FOREIGN KEY (StId) REFERENCES Student(StId),
                                            FOREIGN KEY (SuId) REFERENCES Subject(SuId),
                                            FOREIGN KEY (ExId) REFERENCES Exam(ExId)
                                            );";


                SQLiteCommand createMarkCmd = new SQLiteCommand(createMarkTable, connection);
                createMarkCmd.ExecuteNonQuery();



                string UsersTable = @"
                                         CREATE TABLE IF NOT EXISTS Users (
                                         UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Username TEXT NOT NULL UNIQUE,
                                          Password TEXT NOT NULL,
                                          Role TEXT NOT NULL,
                                          StId INTEGER
                                            )";
                SQLiteCommand createuserCmd = new SQLiteCommand(UsersTable, connection);
                createuserCmd.ExecuteNonQuery();


                string insertDefaultUsers = @"
                                            INSERT OR IGNORE INTO Users (Username, Password, Role, StId) VALUES 
                                            ('admin1', 'admin@123', 'Admin', NULL),
                                            ('lect1', 'lect@123', 'Lecturer', NULL),
                                            ('staff1', 'staff@123', 'Staff', NULL);";

                SQLiteCommand InsertDefaultuserCmd= new SQLiteCommand(insertDefaultUsers, connection);
                InsertDefaultuserCmd.ExecuteNonQuery();


            }

        }


    }
}

















