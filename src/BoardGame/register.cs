/**
* @file register.cs
* @author Mukrime_Sagiroglu_152120191034
* @author Hazar_Namdar_152120191053
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace BoardGame
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
            txtUserType.ReadOnly = true;
        }

        /**
         * @brief Veri tabaninin kullanilmasi icin degiskenler olusturuldu.
         */
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        /**
         * @brief Asagidaki isimli textBoxlar sadece okunabilir yapildi.
         */
        private void register_Load(object sender, EventArgs e)
        {
            txtUserType.ReadOnly = true;
            txtId.ReadOnly = true;
            txtId.Visible = false;
            baglanti = new SqlConnection("server=.; Initial Catalog=userinfo;Integrated Security=SSPI");
        }

        private String hPassword;

        /**
         * @brief SHA256 kullanilarak parolalar sifrelendi.
         */
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public void submit_Click(object sender, EventArgs e)
        {
            /**
             * @brief Yeni kullanici veri tabanina kaydedildi. 
             */
            if ((txtUsername.Text != "") && (txtPassword.Text != "") && (txtNameSurname.Text != "") //tüm kullanıcı bilgilerini doldurması için
                && (txtPhoneNumber.Text != "") && (txtAddress.Text != "") && (txtCity.Text != "")
                && (txtCountry.Text != "") && (txtEmail.Text != ""))
            {
                string sorgu = "INSERT INTO userinfo (UserType,Username,Passwords,NameSurname,PhoneNumber,Address,City,Country,Mail) values (@UserType,@Username,@Passwords,@NameSurname,@PhoneNumber,@Address,@City,@Country,@Mail)";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@UserType", txtUserType.Text);
                komut.Parameters.AddWithValue("@Username", txtUsername.Text);
                komut.Parameters.AddWithValue("@Passwords", txtPassword.Text);
                komut.Parameters.AddWithValue("@NameSurname", txtNameSurname.Text);
                komut.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                komut.Parameters.AddWithValue("@Address", txtAddress.Text);
                komut.Parameters.AddWithValue("@City", txtCity.Text);
                komut.Parameters.AddWithValue("@Country", txtCountry.Text);
                komut.Parameters.AddWithValue("@Mail", txtEmail.Text);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                logInScreen lg = new logInScreen();
                lg.Show();
                this.Hide();

            }
            else
                error.Text = "Please Fill In All The Blanks";


            //    string file = @"../../../counter.txt";
            //    if (File.Exists(file) == true)
            //    {
            //        string temp;
            //        int count = Convert.ToInt32(File.ReadAllText(file));
            //        count++;
            //        temp = count.ToString();
            //        File.WriteAllText(file, temp);
            //    }
            //    else
            //    {
            //        FileInfo fi = new FileInfo(file);
            //        StreamWriter sw = new StreamWriter(file);
            //        string temp;
            //        int count = 0;
            //        temp = count.ToString();
            //        sw.Close();
            //        File.WriteAllText(file, temp);
            //    }
            //    string counter = File.ReadAllText(file);

            //    if ((txtUsername.Text != "") && (txtPassword.Text != "") && (txtNameSurname.Text != "") //tüm kullanıcı bilgilerini doldurması için
            //        && (txtPhoneNumber.Text != "") && (txtAddress.Text != "") && (txtCity.Text != "")
            //        && (txtCountry.Text != "") && (txtEmail.Text != "")) {

            //        if(!File.Exists(Application.StartupPath + @"../../../userinfo.xml")){ //eğer yoksa "userinfo.xml" oluştursun diye 
            //            XmlTextWriter xmlCreated = new XmlTextWriter(Application.StartupPath + @"../../../userinfo.xml", UTF8Encoding.UTF8);
            //            xmlCreated.WriteStartDocument();
            //            xmlCreated.WriteStartElement("users");
            //            xmlCreated.WriteEndDocument();
            //            xmlCreated.Close();
            //        }
            //        XmlDocument xDoc = new XmlDocument(); //dosya varsa içine yazdırıyor.
            //        xDoc.Load(Application.StartupPath + @"../../../userinfo.xml");//bin klasörünün altından çıkarıp projenin içine oluşturduk.

            //        XmlElement userInformation = xDoc.CreateElement("userInformation");

            //        XmlElement idElement = xDoc.CreateElement("id");
            //        idElement.InnerText = counter;
            //        userInformation.AppendChild(idElement);

            //        XmlElement UserTypeElement = xDoc.CreateElement("UserType"); 
            //        UserTypeElement.InnerText= txtUserType.Text;
            //        userInformation.AppendChild(UserTypeElement); // "AppendChild" anadizinin altına bilgi eklemek için


            //        XmlElement UsernameElement = xDoc.CreateElement("Username");
            //        UsernameElement.InnerText = txtUsername.Text;
            //        userInformation.AppendChild(UsernameElement);

            //        XmlElement PasswordElement = xDoc.CreateElement("Password");

            //        hPassword = sha256_hash(txtPassword.Text);

            //        PasswordElement.InnerText = hPassword;
            //        userInformation.AppendChild(PasswordElement);

            //        XmlElement NameSurnameElement = xDoc.CreateElement("NameSurname");
            //        NameSurnameElement.InnerText = txtNameSurname.Text;
            //        userInformation.AppendChild(NameSurnameElement);

            //        XmlElement PhoneNumberElement = xDoc.CreateElement("PhoneNumber");
            //        PhoneNumberElement.InnerText = txtPhoneNumber.Text;
            //        userInformation.AppendChild(PhoneNumberElement);

            //        XmlElement AddressElement = xDoc.CreateElement("Address");
            //        AddressElement.InnerText = txtAddress.Text;
            //        userInformation.AppendChild(AddressElement);

            //        XmlElement CityElement = xDoc.CreateElement("City");
            //        CityElement.InnerText = txtCity.Text;
            //        userInformation.AppendChild(CityElement);

            //        XmlElement CountryElement = xDoc.CreateElement("Country");
            //        CountryElement.InnerText = txtCountry.Text;
            //        userInformation.AppendChild(CountryElement);

            //        XmlElement MailElement = xDoc.CreateElement("Mail");
            //        MailElement.InnerText = txtEmail.Text;
            //        userInformation.AppendChild(MailElement);

            //        xDoc.DocumentElement.AppendChild(userInformation);

            //        XmlTextWriter xmlSet = new XmlTextWriter(Application.StartupPath + @"../../../userinfo.xml", null);
            //        xmlSet.Formatting = Formatting.Indented;
            //        xDoc.WriteContentTo(xmlSet);
            //        xmlSet.Close();
            //        //xDoc.Save(Application.StartupPath + @"\userinfo.xml");

            //        logInScreen lg = new logInScreen();
            //        lg.Show();
            //        this.Hide();
            //    }
            //    else
            //        error.Text = "Please Fill In All The Blanks";
            //}
        }
    }
}
