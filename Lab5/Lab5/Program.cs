using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            //заповнення баз даними
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=mydb;Integrated Security=True";
            string sqlExpression1 = "INSERT INTO Student VALUES ('Student1',1,'Software Engineering')";
            string sqlExpression2 = "INSERT INTO Student VALUES ('Student2',2,'Computer Engineering')";
            string sqlExpression3 = "INSERT INTO Student VALUES ('Student3',3,'Computer Science')";
            string sqlExpression4 = "INSERT INTO Student VALUES ('Student4',4,'Systems Engineering')";
            string sqlExpression5 = "INSERT INTO Student VALUES ('Student5',5,'Software Engineering')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Пiдключення вiдкрито...");
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                int number = command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression2, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression3, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression4, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression5, connection);
                number += command.ExecuteNonQuery();
                Console.WriteLine("Додано об'єктiв до таблицi Student: {0}", number);
            }

            sqlExpression1 = "INSERT INTO Speciality VALUES ('121','Software Engineering','FICT', 'blabla')";
            sqlExpression2 = "INSERT INTO Speciality VALUES ('123','Computer Engineering', 'FICT', 'blabla')";
            sqlExpression3 = "INSERT INTO Speciality VALUES ('122','Computer Science','FICT', 'blabla')";
            sqlExpression4 = "INSERT INTO Speciality VALUES ('126','Systems Engineering','FICT', 'blabla')";
            sqlExpression5 = "INSERT INTO Speciality VALUES ('121','Software Engineering','FICT', 'blabla')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Пiдключення вiдкрито...");
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                int number = command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression2, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression3, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression4, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression5, connection);
                number += command.ExecuteNonQuery();
                Console.WriteLine("Додано об'єктiв до таблицi Speciality: {0}", number);
            }
            sqlExpression1 = "INSERT INTO Subject VALUES ('blabla',1,100,300,100,100,50,'exam','OT')";
            sqlExpression2 = "INSERT INTO Subject VALUES ('blabla',2,100,300,100,100,50,'test','OT')";
            sqlExpression3 = "INSERT INTO Subject VALUES ('blabla',3,100,300,100,100,50,'exam','ACOIY')";
            sqlExpression4 = "INSERT INTO Subject VALUES ('blabla',4,100,300,100,100,50,'coursework','TK')";
            sqlExpression5 = "INSERT INTO Subject VALUES ('blabla',5,100,300,100,100,50,'test','AYTC')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Пiдключення вiдкрито...");
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                int number = command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression2, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression3, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression4, connection);
                number += command.ExecuteNonQuery();
                command = new SqlCommand(sqlExpression5, connection);
                number += command.ExecuteNonQuery();
                Console.WriteLine("Додано об'єктiв до таблицi Discipline: {0}", number);
            }

            //читання з баз
            sqlExpression1 = "SELECT * FROM Student";
            sqlExpression2 = "SELECT * FROM Speciality";
            sqlExpression3 = "SELECT * FROM Subject";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("===== Student =====");
                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));

                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object semester = reader.GetValue(2);
                        object speciality = reader.GetValue(3);

                        Console.WriteLine("{0} \t{1} \t{2} \t{3}", id, name, semester, speciality);
                    }
                }
                reader.Close();

                command = new SqlCommand(sqlExpression2, connection);
                reader = command.ExecuteReader();
                Console.WriteLine("===== Speciality =====");
                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4));

                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object code = reader.GetValue(1);
                        object name = reader.GetValue(2);
                        object faculty = reader.GetValue(3);
                        object disciplines = reader.GetValue(4);

                        Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4}", id, code, name, faculty, disciplines);
                    }
                }
                reader.Close();

                command = new SqlCommand(sqlExpression3, connection);
                reader = command.ExecuteReader();
                Console.WriteLine("===== Subject =====");
                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4), reader.GetName(5), reader.GetName(6), reader.GetName(7), reader.GetName(8), reader.GetName(9));

                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object semester = reader.GetValue(2);
                        object n_hour = reader.GetValue(3);
                        object credit = reader.GetValue(4);
                        object n_lectures = reader.GetValue(5);
                        object n_practical = reader.GetValue(6);
                        object n_lab = reader.GetValue(7);
                        object type_control = reader.GetValue(8);
                        object chair = reader.GetValue(9);


                        Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6} \t{7} \t{8} \t{9}", id, name, semester, n_hour, credit, n_lectures, n_practical, n_lab, type_control, chair);
                    }
                }
                reader.Close();
            }

            //агрегатні функції
            string sqlExpression = "SELECT COUNT(*) FROM Student";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                object count = command.ExecuteScalar();

                command.CommandText = "SELECT MIN(n_hour) FROM Subject";
                object minAge = command.ExecuteScalar();

                Console.WriteLine("У таблицi Student {0} об'єктiв", count);
                Console.WriteLine("Мiнiмальна кiлькiсть годин для предмету: {0}", minAge);
            }


            Console.WriteLine("Пiдключення закрите...");
            Console.Read();
        }
    }
}
