using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Database : Form
    {
        // ... the query string...
        static string query;
        string queryVAL; // For displaying Data
        string value1, value2, value3, value4, value5, value6, Number;

        //query 11 && 15 && 7
        private void button1_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            if(queryVAL == "Query 7")
            {
                string columns = "{0,-15} {1,-20} {2,-15} {3, -15}";
                listBox.Items.Add(String.Format(columns, "Name", "BannerNumber", "RoomNO", "PlaceNO"));
                Number = textBox1.Text;
                Console.WriteLine(Number);
                query = String.Format("SELECT distinct CONCAT(student.firstName, ' ', student.lastName) As Manager, student.bannerNumber, room.PlaceNO, room.RoomNO FROM student, room, lease, hall where student.bannerNumber = lease.Student_bannerNumber AND lease.Room_placeNO = room.placeNO AND room.Hall_hallNO = hall.hallNO AND hall.hallName = '{0}'; ", Number);

            }
            if (queryVAL == "Query 11")
            {
                string columns = "{0,-10} {1,25}";
                listBox.Items.Add(String.Format(columns, "Name", "PhoneNumber"));
                Number = textBox1.Text;
                query = String.Format("SELECT CONCAT(advisor.firstName, ' ', advisor.lastName) As Manager, advisor.phoneNumber FROM Advisor, Student WHERE Advisor.advisorID = Student.Advisor_advisorID AND Student.bannerNumber = {0}; ", Number);
            }
            if (queryVAL == "Query 15")
            {
                string columns = "{0,-10}";
                listBox.Items.Add(String.Format(columns, "numRegistered"));
                Number = textBox1.Text;
                query = String.Format("SELECT COUNT(space-availability) AS numRegistered FROM Parkinglot WHERE Parkinglot.lotNO = {0}; ", Number);
            }
            CoreAddOptions(query);
            Console.WriteLine(Number);
        }

        

        // ... DataBase Connection
        static string connectionstring = Helper.CnnVal("Mydb");
        //MySqlConnection connection = new MySqlConnection(connectionstring);

        public Database()
        {
            InitializeComponent();
            button1.Visible = false;
            textBox1.Visible = false;
            
        }

        // Extract method: do not cram everything into single IndexChanged  
        private void CoreAddOptions(string query1)
        {
            // Do not open a global connection
            // Wrap IDisposable into using 
            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();
                // Wrap IDisposable into using
                using (MySqlCommand cmd = new MySqlCommand(query1, connection))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string caseSwitch = queryVAL;
                        switch (caseSwitch)
                        {
                            case "Query 1":
                                value1 = reader.GetString("Manager");
                                value2 = reader.GetString("phoneNumber");
                                value3 = reader.GetString("hallName");
                                string columns = "{0,-30} {1,-30} {2,-30}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3));
                                break;
                            case "Query 2":
                                value1 = reader.GetString("bannerNumber");
                                value2 = reader.GetString("Manager");
                                value3 = reader.GetString("numOfSemester");
                                value4 = reader.GetString("moveInDate");
                                value5 = reader.GetString("moveOutDate");
                                value6 = reader.GetString("Room_placeNO");
                                columns = "{0,-35} {1,-35} {2,-25} {3,-30} {4,-30} {5,-30}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3, value4,value5,value6));
                                break;
                            case "Query 3":
                                value1 = reader.GetString("NumOfSemester");
                                value2 = reader.GetString("moveOutDate");
                                value3 = reader.GetString("moveInDate");
                                columns = "{0,-15} {1,-25} {2,-15}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3));
                                break;
                            case "Query 4":
                                value1 = reader.GetString("payDue");
                                value2 = reader.GetString("payDate");
                                value3 = reader.GetString("payMethod");
                                value4 = reader.GetString("bannerNumber");
                                columns = "{0,-10} {1,25} {2,25} {3, 25}";
                                listBox.Items.Add(String.Format(columns, value4, value2, value3, value1));
                                break;
                            case "Query 5":
                                value1 = reader.GetString("Manager");
                                value2 = reader.GetString("payDue");
                                value3 = reader.GetString("payMethod");
                                columns = "{0,-10} {1,25} {2,25}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3));
                                break;
                            case "Query 6":
                                value1 = reader.GetString("dateOfInspec");
                                value2 = reader.GetString("comments");
                                value3 = reader.GetString("placeNO");
                                value4 = reader.GetString("roomNO");
                                columns = "{0,-30} {1,-40} {2,-40} {3, -35}";
                                listBox.Items.Add(String.Format(columns, value4, value3, value2, value1));
                                break;
                            case "Query 7":
                                value1 = reader.GetString("Manager");
                                value2 = reader.GetString("bannerNumber");
                                value3 = reader.GetString("roomNO");
                                value4 = reader.GetString("placeNO");
                                columns = "{0,-20} {1,-25} {2,-25} {3, -25}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3, value4));
                                break;
                            case "Query 8":
                                value1 = reader.GetString("Manager");
                                value2 = reader.GetString("email");
                                value3 = reader.GetString("Major_majorID");
                                columns = "{0,-25} {1,-25} {2,0}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3));
                                break;
                            case "Query 9":
                                value1 = reader.GetString("StudentCategory");
                                value2 = reader.GetString("TotalStudents");
                                columns = "{0,-10} {1,20} ";
                                listBox.Items.Add(String.Format(columns, value2, value1));
                                break;
                            case "Query 10":
                                value1 = reader.GetString("Manager");
                                value2 = reader.GetString("bannerNumber");

                                columns = "{0,-15} {1,-25} ";
                                listBox.Items.Add(String.Format(columns, value1, value2));
                                break;
                            case "Query 11":
                                value1 = reader.GetString("Manager");
                                value2 = reader.GetString("phoneNumber");
                                columns = "{0,-10} {1,25} ";
                                listBox.Items.Add(String.Format(columns, value1, value2));
                                break;
                            case "Query 12":
                                value1 = reader.GetString("averageRent");
                                value2 = reader.GetString("minimumRent");
                                value3 = reader.GetString("maximumRent");
                                columns = "{0,-10} {1,25} {2,25}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3));
                                break;
                            case "Query 13":
                                value1 = reader.GetString("myCount");
                                columns = "{0,-15}";
                                listBox.Items.Add(String.Format(columns, value1));
                                break;
                            case "Query 14":
                                value1 = reader.GetString("staffNO");
                                value2 = reader.GetString("Manager");
                                value3 = reader.GetString("location");

                                columns = "{0,-15} {1, -15} {2, -15}";
                                listBox.Items.Add(String.Format(columns, value1, value2, value3));
                                break;
                            case "Query 15":
                                value1 = reader.GetString("NumRegistered");
                                columns = "{0,-10} ";
                                listBox.Items.Add(String.Format(columns, value1, value2));
                                break;

                        }

                    }
                }
            }
        }
   
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //For Query 11 search functionality
            button1.Visible = false;
            textBox1.Visible = false;
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;
            // ... Set SelectedItem as Window Title.
            string value = comboBox.SelectedItem as string;


            // ... Query values.
            string caseSwitch = value;
            queryVAL = caseSwitch;
            switch (caseSwitch)
            {
                case "Query 1":

                    listBox.Items.Clear();
                    string columns = "{0,-30} {1,-30} {2,-30}";
                    listBox.Items.Add(String.Format(columns, "Name", "PhoneNumber", "HallName"));

                    query = "SELECT CONCAT(firstName,' ', lastName) As Manager, staff.phoneNumber, hallName " +
                        "FROM staff, hall " +
                        "WHERE staff.staffNO = hall.Staff_staffNO;";

                    CoreAddOptions(query);
                    Console.Write("Case 1");
                    break;
                case "Query 2":
                    listBox.Items.Clear();
                    columns = "{0,-30} {1,-30} {2,-30} {3,-30} {4,-30} {5,-30}";
                    listBox.Items.Add(String.Format(columns, "BannerNumber", "Name", "Semesters", "MoveInDate", "MoveOutDate", "PlaceNO"));
                    query = "SELECT CONCAT(firstName, ' ', lastName) As Manager, student.bannerNumber,  lease.numOfSemester, lease.moveInDate, lease.moveOutDate, lease.Room_placeNO FROM student, lease where student.bannerNumber = lease.Student_bannerNumber; ";

                    CoreAddOptions(query);
                    Console.WriteLine(query);
                    break;
                case "Query 3":
                    listBox.Items.Clear();
                    columns = "{0,-15} {1,-30} {2,-20}";
                    listBox.Items.Add(String.Format(columns, "Semesters", "MoveOut", "MoveIn"));

                    query = "SELECT NumOfSemester, moveOutDate, moveInDate FROM lease, invoice WHERE invoice.Lease_leaseNo = lease.LeaseNO AND Invoice.Semester = 'Summer'; ";
                    CoreAddOptions(query);

                    Console.WriteLine("Case 3");
                    break;
                case "Query 4":
                    listBox.Items.Clear();
                    columns = "{0,-10} {1,25} {2,25} {3,25}";
                    listBox.Items.Add(String.Format(columns, "BannerNumber", "PayDate", "PayMethod", "PayDue"));

                    query = "SELECT payDue, payDate, payMethod, student.bannerNumber FROM Invoice, Lease, Student WHERE invoice.Lease_leaseNO = lease.LeaseNO AND lease.Student_BannerNumber = student.bannerNumber AND payDate IS NOT NULL;";
                    CoreAddOptions(query);
                    Console.WriteLine("Case 4");
                    break;
                case "Query 5":
                    listBox.Items.Clear();
                    columns = "{0,-10} {1,25} {2,25}";
                    listBox.Items.Add(String.Format(columns, "Name", "PayDue", "PayDate", "payMethod"));

                    query = "SELECT CONCAT(student.firstName, ' ', student.lastName) As Manager, invoice.payDue, invoice.payMethod FROM Invoice, Lease, Student WHERE invoice.Lease_leaseNO = lease.LeaseNO AND lease.Student_BannerNumber = student.bannerNumber AND invoice.payDate IS NULL GROUP BY Manager; ";
                    CoreAddOptions(query);
                    Console.WriteLine("Case 5");
                    break;
                case "Query 6":
                    listBox.Items.Clear();
                    columns = "{0,-20} {1,-40} {2,-35} {3, -35}";
                    listBox.Items.Add(String.Format(columns, "RoomNO", "PlaceNO", "Comments", "Date Of Inspection"));

                    query = "SELECT inspection.dateOfInspec, inspection.comments, room.placeNO, room.roomNO FROM inspection, room WHERE inspection.satisfiedCondition = '0' AND room.placeNO = inspection.Room_placeNO; ";

                    CoreAddOptions(query);
                    Console.WriteLine("Case 6");
                    break;
                case "Query 7":
                    listBox.Items.Clear();
                    button1.Visible = true;
                    textBox1.Visible = true;
                    Console.WriteLine("Case 7");
                    break;
                case "Query 8":
                    listBox.Items.Clear();
                    columns = "{0,-25} {1,-30} {2,-25}";
                    listBox.Items.Add(String.Format(columns, "Name", "Email", "Major"));
                    query = "SELECT  CONCAT(student.firstName, ' ', student.lastName) As Manager, email, Major_majorID FROM Student WHERE STATUS = 'waiting'";

                    CoreAddOptions(query);
                    Console.WriteLine("Case 8");
                    break;
                case "Query 9":
                    listBox.Items.Clear();
                    columns = "{0,-15} {1,-25} ";
                    listBox.Items.Add(String.Format(columns, "Count", "Student Category"));

                    query = "SELECT StudentCategory, COUNT(*) AS TotalStudents FROM Student GROUP BY StudentCategory;";
                    CoreAddOptions(query);
                    Console.WriteLine("Case 9");
                    break;
                case "Query 10":
                    listBox.Items.Clear();
                    columns = "{0,-15} {1,-25} ";
                    listBox.Items.Add(String.Format(columns, "BannerNumber", "Name"));

                    query = "SELECT CONCAT(student.firstName, ' ', student.lastName) As Manager,student.bannerNumber FROM student WHERE student.bannerNumber NOT IN (SELECT student.bannerNumber FROM student, nok WHERE student.bannerNumber=nok.bannerNumber);";
                    CoreAddOptions(query);
                    Console.WriteLine("Case 10");
                    break;
                case "Query 11":
                    listBox.Items.Clear();
                    button1.Visible = true;
                    textBox1.Visible = true;
                    Console.WriteLine("Case 11");
                    break;
                case "Query 12":
                    listBox.Items.Clear();
                    columns = "{0,-10} {1,25} {2,25}";
                    listBox.Items.Add(String.Format(columns, "Average", "Min","Max"));
                    query = "SELECT AVG(rentRate) AS averageRent, MIN(rentRate) AS minimumRent, MAX(rentRate) AS maximumRent FROM Room WHERE Room.Hall_hallNO is not NULL;";

                    CoreAddOptions(query);
                    Console.WriteLine("Case 12");
                    break;

                case "Query 13":
                    listBox.Items.Clear();
                    columns = "{0,-15}";
                    listBox.Items.Add(String.Format(columns, "Number of Places"));

                    query = "SELECT COUNT(placeNO) AS myCount FROM Room WHERE Room.Hall_hallNO is not NULL; ";
                    CoreAddOptions(query);
                    Console.WriteLine("Case 13");
                    break;

                case "Query 14":
                    listBox.Items.Clear();
                    columns = "{0,-15} {1, -15} {2, -15}";
                    listBox.Items.Add(String.Format(columns, "StaffNO","Name", "Location"));
                    query = "SELECT staffNO, CONCAT(firstName, ' ', lastName) As Manager, location FROM Staff WHERE DATEDIFF(curdate(), dateOfBirth) / 365.25 > 60; ";
                    CoreAddOptions(query);
                    Console.WriteLine("Case 14");
                    break;

                case "Query 15":
                    listBox.Items.Clear();
                    button1.Visible = true;
                    textBox1.Visible = true;


                    Console.WriteLine("Case 15");
                    break;
                default:
                    button1.Visible = false;
                    textBox1.Visible = false;
                    listBox.Items.Clear();
                    listBox.Items.Add("No query selected");
                    Console.WriteLine("Default case");
                    break;
            }
        }
    }
}

