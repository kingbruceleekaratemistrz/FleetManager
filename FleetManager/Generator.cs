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
            /*plate_number      brand       model      prod_year        hp          cc*/
            /*INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	            VALUES ('WA77354', 'Skoda', 'Fabia', '2017', '105', '1422')*/
            string[] lines = System.IO.File.ReadAllLines(datadir + "cars.txt");

            // Inicjalizacja tabeli struktur CarModel
            CarModel[] Cars = new CarModel[lines.Length];
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

                Cars[i].SetAll(brand, model, Int32.Parse(prod_year), Int32.Parse(hp), Int32.Parse(cc));
            }

            // Tworzenie zapytania SQL.
            string str = "";
            str += GenModelInsert(Cars[0], 0, 15);
            str += GenModelInsert(Cars[1], 15, 10);
            str += GenModelInsert(Cars[2], 25, 6);
            str += GenModelInsert(Cars[3], 31, 6);
            str += GenModelInsert(Cars[4], 37, 4);

            str += GenModelInsert(Cars[5], 41, 4);
            str += GenModelInsert(Cars[6], 45, 4);
            str += GenModelInsert(Cars[7], 49, 3);
            str += GenModelInsert(Cars[8], 52, 2);
            str += GenModelInsert(Cars[9], 54, 1);

            str += GenModelInsert(Cars[10], 55, 5);
            str += GenModelInsert(Cars[11], 60, 5);
            str += GenModelInsert(Cars[12], 65, 5);
            str += GenModelInsert(Cars[13], 70, 5);
            str += GenModelInsert(Cars[14], 75, 2);

            str += GenModelInsert(Cars[15], 77, 5);
            str += GenModelInsert(Cars[16], 82, 3);
            str += GenModelInsert(Cars[17], 85, 3);
            str += GenModelInsert(Cars[18], 88, 3);
            str += GenModelInsert(Cars[19], 91, 3);

            str += GenModelInsert(Cars[20], 94, 2);
            str += GenModelInsert(Cars[21], 96, 1);
            str += GenModelInsert(Cars[22], 97, 1);
            str += GenModelInsert(Cars[23], 98, 1);
            str += GenModelInsert(Cars[24], 99, 1);

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
            string[] positions = { "Customer Advisor", "Customer Advisor", "Receptionist", "Accountant", "Salesman", "Manager", "CEO" };
            string[] phones = GenPhoneNumber(100, "70");

            // Składanie zapytania SQL
            string str = "";
            // Dla 14 firm
            for (int i = 0; i < 14; i++)
            {
                // Po siedem użytkowników
                for (int j = 0; j < 7; j++)
                {
                    // Dodanie rekordu do USERS_CRED
                    str += "INSERT INTO USERS_CRED (username, passwd) VALUES ";
                    str += "('" + usernames[i * 7 + j] + "'";
                    str += ", '" + passwords[i * 7 + j] + "')\n";

                    // Dodanie rekordu do USERS_PROFILES
                    str += "INSERT INTO USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_plate) VALUES ";
                    str += "('" + usernames[i * 7 + j] + "'";
                    str += ", '" + firstnames[i * 7 + j] + "'";
                    str += ", '" + lastnames[i * 7 + j] + "'";
                    str += ", '" + companies[i] + "'";
                    str += ", '" + positions[j] + "'";
                    str += ", '" + picdir + "profile_pic_" + j + ".png'";
                    str += ", '" + phones[i * 7 + j] + "'";
                    str += ", '" + usernames[i * 7 + j] + "@" + companies[i].Replace(" ", string.Empty).Replace(",", string.Empty).Replace("-", string.Empty) + ".com'";
                    str += ", 'WX" + (i*7 + j).ToString().PadLeft(5, '0') + "')\n";
                }               
            }

            // Dodanie dwóch użytkowników do ostatniej firmy
            str += "INSERT INTO USERS_CRED (username, passwd) VALUES ('" + usernames[98] + "', '" + passwords[98] + "')\n";
            str += "INSERT INTO USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_plate) VALUES ";
            str += "('" + usernames[98] + "'";
            str += ", '" + firstnames[98] + "'";
            str += ", '" + lastnames[98] + "'";
            str += ", '" + companies[13] + "'";
            str += ", 'Assistant to the Regional Manager'";
            str += ", '" + picdir + "profile_pic_" + 0 + ".png'";
            str += ", '" + phones[98] + "'";
            str += ", '" + usernames[98] + "@" + companies[13].Replace(" ", string.Empty).Replace(",", string.Empty).Replace("-", string.Empty) + ".com'";
            str += ", 'WX" + (98).ToString().PadLeft(5, '0') + "')\n";
            
            str += "INSERT INTO USERS_CRED (username, passwd) VALUES ('" + usernames[99] + "', '" + passwords[99] + "')\n";
            str += "INSERT INTO USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_plate) VALUES ";
            str += "('" + usernames[99] + "'";
            str += ", '" + firstnames[99] + "'";
            str += ", '" + lastnames[99] + "'";
            str += ", '" + companies[13] + "'";
            str += ", 'Money Mull'";
            str += ", '" + picdir + "profile_pic_" + 1 + ".png'";
            str += ", '" + phones[99] + "'";
            str += ", '" + usernames[99] + "@" + companies[13].Replace(" ", string.Empty).Replace(",", string.Empty).Replace("-", string.Empty) + ".com'";
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
        
        string GenModelInsert(CarModel cm, int first, int n)
        {
            string res = "";
            for (int i = 0; i < n; i++)
            {
                res += "INSERT INTO CARS (plate_number, brand, model, prod_year, hp, cc) " +
                    "VALUES(";
                res += "'WX" + first++.ToString().PadLeft(5, '0') + "'";
                res += ", '" + cm.Brand + "'";
                res += ", '" + cm.Model + "'";
                res += ", '" + cm.Year_prod + "'";
                res += ", '" + cm.Hp + "'";
                res += ", '" + cm.Cc + "'";
                res += ")\n";
            }
     
            return res;
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


