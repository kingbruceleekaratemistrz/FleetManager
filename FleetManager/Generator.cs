using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FleetManager
{
    /// <summary>
    /// Klasa generująca dane do bazy danych.
    /// 100 pełnych profili użytkowników wraz z loginami i hasłami.
    /// 14 profili firm.
    /// X pojazdów.
    /// </summary>
    public class Generator
    {
        public struct CarModel
        {           
            public CarModel(string brand, string model, int year_prod, int hp, int cc)
            {
                Brand = brand;
                Model = model;
                Year_prod = year_prod;              
                Hp = hp;
                Cc = cc;
            }

            public string Brand { get; set; }
            public string Model { get; set; }
            public int Year_prod { get; set; }
            public int Hp { get; set; }
            public int Cc { get; set; }

            public void SetAll(string brand, string model, int year_prod, int hp, int cc)
            {
                Brand = brand;
                Model = model;
                Year_prod = year_prod;
                Hp = hp;
                Cc = cc;
            }

            public override string ToString() => $"{Brand} ({Model}) ({Year_prod}) ({Hp}) ({Cc})";
        }

        const int phone_min = 1000000;
        const int phone_max = 9999999;
        readonly string connectionString = "Server=(LocalDb)\\AdmBD;Database=fleet_db;Trusted_Connection=True;";
        readonly string datadir = "..\\..\\..\\Assets\\Generator_data\\";
        readonly string picdir = "..\\..\\..\\Assets\\Profile_pictures\\";

        public Generator() { }

        public void InitializeDb()
        {
            // wywołanie procedury tworzącej tabele na bazie

            CreateCompanies();
            CreateCars();
            CreateUsers();
            CreateCarServices();
            CreateSerivces();
        }


        /*  
         *  Tabela przechowywująca informacje o profilach firm
         *  name       description      address     phone     mail
         *  ---------- ---------------- ----------- --------- --------
        */
        void CreateCompanies()
        {         
            string[] names = System.IO.File.ReadAllLines(datadir + "companies.txt");
            string[] addresses = System.IO.File.ReadAllLines(datadir + "addresses.txt");
            string[] phones = GenPhoneNumber(14, "99");
            string[] mails = GenMail(names, 14);

            string str = "";
            for (int i = 0; i < 14; i++)
            {
                string tmp = "INSERT INTO COMP_PROFILES ([name], [description], [address], phone, mail) " +
                    "VALUES(";
                tmp += "'" + names[i] + "'";
                tmp += ", '" + "temp description " + (i+1) + "'";
                tmp += ", '" + addresses[i] + "'";
                tmp += ", '" + phones[i] + "'";
                tmp += ", '" + mails[i] + "'";
                tmp += ")";

                str += tmp + "\n";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(str, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Database is created Successfully");
                }
            }
        }

        void CreateCars()
        {
            string[] lines = System.IO.File.ReadAllLines(datadir + "cars.txt");

            string str = "";
            for (int i = 0; i < lines.Length; i++)
            {
                // Wydobycie z wczytanych lini wartości model, brand, prod_year, hp, cc
                string[] subs = lines[i].Split(' ');
                int n = subs.Length;
                string brand = subs[0];
                string cc = subs[n - 1];
                string hp = subs[n - 2];
                string prod_year = subs[n - 3];
                subs[0] = subs[n - 1] = subs[n - 2] = subs[n - 3] = "";
                string model = String.Join(" ", subs).Trim();

                str += "INSERT INTO CARS (brand, model, prod_year, hp, cc, photo_url) " +
                    "VALUES(";
                str += "'" + brand + "'";
                str += ", '" + model + "'";
                str += ", '" + prod_year + "'";
                str += ", '" + hp + "'";
                str += ", '" + cc + "'";
                str += ", '" + "url not found" + "')\n";   
            }

            // Wywołanie zapytania ze zmiennej str na bazie
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(str, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Database is created Successfully");
                }
            }
        }

        void CreateUsers()
        {
            string[] usernames = System.IO.File.ReadAllLines(datadir + "usernames.txt");
            string[] passwords = System.IO.File.ReadAllLines(datadir + "passwords.txt");
            string[] firstnames = System.IO.File.ReadAllLines(datadir + "first_names.txt");
            string[] lastnames = System.IO.File.ReadAllLines(datadir + "last_names.txt");
            string[] companies = System.IO.File.ReadAllLines(datadir + "companies.txt");
            string[] positions = { "Company Administrator", "Customer Advisor", "Receptionist", "Accountant", "Salesman", "Manager", "CEO" };
            string[] phones = GenPhoneNumber(100, "70");

            // Składanie zapytania SQL
            string str = "";
            Random r = new Random();
            // Dla 14 firm
            for (int i = 0; i < 14; i++)
            {
                // Po siedem użytkowników
                for (int j = 0; j < 7; j++)
                {
                    // Dodanie rekordu do USERS_CRED
                    str += "INSERT INTO USERS_CRED (username, passwd, acc) VALUES ";
                    str += "('" + usernames[i * 7 + j] + "'";
                    str += ", '" + passwords[i * 7 + j] + "'";
                    if (j == 0) str += ", 1)\n";
                    else str += ", 0)\n";

                    // Dodanie rekordu do USERS_PROFILES
                    str += "INSERT INTO USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_id, car_plate) VALUES ";
                    str += "('" + usernames[i * 7 + j] + "'";
                    str += ", '" + firstnames[i * 7 + j] + "'";
                    str += ", '" + lastnames[i * 7 + j] + "'";
                    str += ", '" + companies[i] + "'";
                    str += ", '" + positions[j] + "'";
                    str += ", '" + picdir + "profile_pic_" + j + ".png'";
                    str += ", '" + phones[i * 7 + j] + "'";
                    str += ", '" + usernames[i * 7 + j] + "@" + companies[i].Replace(" ", string.Empty).Replace(",", string.Empty).Replace("-", string.Empty) + ".com'";
                    str += ", '" + r.Next(1, 26) + "'";
                    str += ", 'WX" + (i*7 + j).ToString().PadLeft(5, '0') + "')\n";
                }               
            }

            // Dodanie dwóch użytkowników do ostatniej firmy
            str += "INSERT INTO USERS_CRED (username, passwd, acc) VALUES ('" + usernames[98] + "', '" + passwords[98] + "', 0)\n";
            str += "INSERT INTO USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_id, car_plate) VALUES ";
            str += "('" + usernames[98] + "'";
            str += ", '" + firstnames[98] + "'";
            str += ", '" + lastnames[98] + "'";
            str += ", '" + companies[13] + "'";
            str += ", 'Assistant to the Regional Manager'";
            str += ", '" + picdir + "profile_pic_" + 0 + ".png'";
            str += ", '" + phones[98] + "'";
            str += ", '" + usernames[98] + "@" + companies[13].Replace(" ", string.Empty).Replace(",", string.Empty).Replace("-", string.Empty) + ".com'";
            str += ", '" + r.Next(1,26) + "'";
            str += ", 'WX" + (98).ToString().PadLeft(5, '0') + "')\n";
            
            str += "INSERT INTO USERS_CRED (username, passwd, acc) VALUES ('" + usernames[99] + "', '" + passwords[99] + "', 0)\n";
            str += "INSERT INTO USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_id, car_plate) VALUES ";
            str += "('" + usernames[99] + "'";
            str += ", '" + firstnames[99] + "'";
            str += ", '" + lastnames[99] + "'";
            str += ", '" + companies[13] + "'";
            str += ", 'Money Mull'";
            str += ", '" + picdir + "profile_pic_" + 1 + ".png'";
            str += ", '" + phones[99] + "'";
            str += ", '" + usernames[99] + "@" + companies[13].Replace(" ", string.Empty).Replace(",", string.Empty).Replace("-", string.Empty) + ".com'";
            str += ", '" + r.Next(1, 26) + "'";
            str += ", 'WX" + (99).ToString().PadLeft(5, '0') + "')\n";

            // Wywołanie zapytania ze zmiennej str na bazie
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(str, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Database is created Successfully");
                }
            }
        }  
        
        void CreateCarServices()
        {
            string[] names = System.IO.File.ReadAllLines(datadir + "car_services.txt");
            string[] addresses = System.IO.File.ReadAllLines(datadir + "addresses.txt");
            string[] phones = GenPhoneNumber(10, "11");
            string[] mails = GenMail(names, 10);

            string str = "";
            for (int i = 0; i < 10; i++)
            {
                str += "INSERT INTO CAR_SERVICES ([name], [address], phone, mail) VALUES (";
                str += "'" + names[i] + "'";
                str += ", '" + addresses[i] + "'";
                str += ", '" + phones[i] + "'";
                str += ", '" + mails[i] + "')\n";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(str, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Database is created Successfully");
                }
            }
        }

        void CreateSerivces()
        {
            int[] costs = { 100, 40, 100, 150, 200, 70, 50};
            string[] times = { "1h", "1h", "1h", "1h", "3h", "1h", "20min" };
            string[] descriptions = System.IO.File.ReadAllLines(datadir + "services.txt");

            string str = "";
            for (int i = 0; i < 7; i++)
            {
                str += "INSERT INTO [SERVICES] (cost, [time], [description]) VALUES (";
                str += "'" + costs[i] + "'";
                str += ", '" + times[i] + "'";
                str += ", '" + descriptions[i] + "')\n";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(str, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Database is created Successfully");
                }
            }
        }


        string[] GenPhoneNumber(int n, string aa)
        {
            string[] res = new string[n];
            Random r = new Random();
            for (int i = 0; i < n; i++)
                res[i] = aa + r.Next(phone_min, phone_max).ToString();
         
            return res;
        }

        string[] GenMail(string[] names, int n)
        {
            string[] res = new string[n];
            for (int i = 0; i < n; i++)
                res[i] = string.Join("", names[i].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)) + "@gmail.com";
            
            return res;
        }
    }
}


