/**
* @file Form3.cs
* @author Mukrime_Sagiroglu_152120191034
* @author Hazar_Namdar_152120191053
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BoardGame
{
    public partial class Form3 : Form
    {
        /**
         * @brief Baska formlardan erisebilmek icin statik degiskenler tanimlandi.
         */
        public static string userInfoFile;
        public static FileStream fileStream;

        public Form3()
        {
            InitializeComponent();

            /**
             * @brief Okuma modunda bir dosya olusturuldu.
             */
            userInfoFile = Application.StartupPath+ @"../../../userinfo.txt";
            fileStream = new FileStream(userInfoFile, FileMode.OpenOrCreate, FileAccess.Read);

            /**
             * @brief Bir sonraki calistirmada, kaydedilen ayarlarin sistem tarafindan hatirlanmasi saglandi.
             */
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (true) 
                {
                    string satir = reader.ReadLine();

                    /**
                     * @brief Zorluk seviyeleri belirlendi.
                     */
                    if (satir == "")
                    {
                        easy.Checked = true;
                    }
                    if (satir == "easy")
                    {
                        easy.Checked = true;
                    }
                    if (satir == "normal")
                    {
                        normal.Checked = true;
                    }
                    if (satir == "hard")
                    {
                        hard.Checked = true;
                    }
                    if (satir == "custom")
                    {
                        custom.Checked = true;
                        satir = reader.ReadLine();
                        customRow.Text = satir;
                        satir = reader.ReadLine();
                        customCol.Text = satir;
                    }

                    /**
                     * @brief Sekil tipleri belirlendi.
                     */
                    if (satir == "Square") //Square
                    {
                        shapeListBox1.SetItemChecked(0,true);
                    }
                    if (satir == "Triangle") //Triangle
                    {
                        shapeListBox1.SetItemChecked(1, true);
                    }
                    if (satir == "Round") //Round
                    {
                        shapeListBox1.SetItemChecked(2, true);
                    }

                    /**
                     * @brief Sekillerin renkleri belirlendi.
                     */
                    if (satir == "red") //red
                    {
                        colorbox.SetItemChecked(0, true);               
                    }
                    if (satir == "blue") //blue
                    {
                        colorbox.SetItemChecked(1, true);
                    }
                    if (satir == "pink") //pink
                    {
                        colorbox.SetItemChecked(2, true);
                    }

                    if (satir == null) break;
                }
                reader.Close();
            }
            fileStream.Close();
        }

        /*
         * @brief Butona basildiginda yapilan tum islerin kaydedilmesi saglandi.
         */
        private void save_Click(object sender, EventArgs e)//kullanıcının seçimlerini kaydetmek için
        {
            /**
             * @brief Eger isCorrect true ise kaydedilme yapilacak, degilse bir eksiklik var o giderilmelidir.
             */
            bool isCorrect = true;

            if (!File.Exists(userInfoFile))
            {
                File.Create(userInfoFile);
            }

            /**
             * @brief 2 veya daha fazla sekil secilmesi sarti saglandi.
             */
            if (shapeListBox1.CheckedIndices.Count == 0 || shapeListBox1.CheckedIndices.Count == 1)
            {
                isCorrect = false;
                lblMessage2.Text = "Please choose at least two shapes";
            }
            else
            {
                isCorrect = true;
                lblMessage2.Text = "";
            }

            /**
             * @brief En az 1 renk secilmesi saglandi.
             */
            if (colorbox.CheckedIndices.Count == 0)
            {
                isCorrect = false;
                lblMessage2.Text += Environment.NewLine + "You have to choose a color(s)";
            }

            /**
             * @brief Oyunun ozel sartı: 1 renk ve 1 sekil secilemez.
             */
            if (colorbox.CheckedIndices.Count == 1 && shapeListBox1.CheckedIndices.Count == 1)
            {
                isCorrect = false;
                lblMessage2.Text = "You cannot choose 1 color and 1 shape only";
            }

            /**
             * @brief Zorluk seviyesi secilmemisse ekrana hata mesaji verir.
             */
            if (!easy.Checked && !normal.Checked && !hard.Checked && !custom.Checked)
            {
                lblMessage1.Text = "Please choose one of the difficulty levels.\n";
            }

            /**
             * @brief Custom oyun modunda, girilen satir degerinin gecerli olup olmadigini kontrol eder.
             */
            if (customRow.Text != "" && customRow.Visible == true && (Convert.ToInt32(customRow.Text) <= 5 || Convert.ToInt32(customRow.Text) > 15))
            {
                customRow.Text = "";

                lblCustomCheck.Text = "Inputs must be between 6 and 15!";
                isCorrect = false;
            }

            /**
             * @brief Custom oyun modunda, girilen sutun degerinin gecerli olup olmadigini kontrol eder.
             */
            if (customCol.Text != "" && customCol.Visible == true && (Convert.ToInt32(customCol.Text) <= 5 || Convert.ToInt32(customCol.Text) > 15))
            {
                customCol.Text = "";

                lblCustomCheck.Text = "Inputs must be between 6 and 15!";
                isCorrect = false;
            }

            /**
             * @brief Eger isCorrect true ise kaydedilme yapilacak, degilse bir eksiklik var o giderilmelidir.
             */
            if (isCorrect)
            {
                using (StreamWriter writer = new StreamWriter(userInfoFile))
                {
                    if (easy.Checked)
                    {
                        writer.WriteLine(easy.Text);
                    }
                    if (normal.Checked)
                    {
                        writer.WriteLine(normal.Text);
                    }
                    if (hard.Checked)
                    {
                        writer.WriteLine(hard.Text);
                    }
                    if (custom.Checked)
                    {
                        writer.WriteLine(custom.Text);
                        if(customCol.Text != "" && customRow.Text != "")
                        {
                            writer.WriteLine(customRow.Text);
                            writer.WriteLine(customCol.Text);
                            lblMessage1.Text = "";
                        }
                        else if(customCol.Text == "" || customRow.Text == "")
                        {
                            lblMessage1.Text = "Error";
                        }
                    }

                    if (lblMessage1.Text != "Error")
                    {
                        writer.WriteLine("shape");
                        for (int i = 0; i < shapeListBox1.CheckedIndices.Count; i++) //şekil
                        {
                            if (shapeListBox1.CheckedIndices[i] == 0)
                            {
                                writer.WriteLine("Square");
                            }
                            if (shapeListBox1.CheckedIndices[i] == 1)
                            {
                                writer.WriteLine("Triangle");
                            }
                            if (shapeListBox1.CheckedIndices[i] == 2)
                            {
                                writer.WriteLine("Round");
                            }                            
                        }

                        writer.WriteLine("color");
                        for (int i = 0; i < colorbox.CheckedIndices.Count; i++) //renk seçilmezse hata vermiyor
                        {
                            if (colorbox.CheckedIndices[i] == 0)
                            {
                                writer.WriteLine("red");
                            }
                            if (colorbox.CheckedIndices[i] == 1)
                            {
                                writer.WriteLine("blue");
                            }
                            if (colorbox.CheckedIndices[i] == 2)
                            {
                                writer.WriteLine("pink");
                            }
                        }

                        lblMessage1.Text = "Saved";
                    }
                }
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void shapeListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage1.Text = "";
            lblMessage2.Text = "";
        }

        private void easy_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage1.Text = "";
            lblMessage2.Text = "";
        }

        private void normal_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage1.Text = "";
            lblMessage2.Text = "";
        }

        private void hard_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage1.Text = "";
            lblMessage2.Text = "";
        }

        /**
         * @brief Custom modu secilirse, girdi girmek icin 2 adet textBox gorunur hale gelecek.
         */
        private void custom_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage1.Text = "";
            if (custom.Checked)     //custom seçilirse textboxlar görünür oluyor.
            {
                customCol.Visible = true;
                customRow.Visible = true;
            }
            else
            {
                customCol.Visible = false;
                customRow.Visible = false;
            }
        }

        private void customRow_TextChanged(object sender, EventArgs e)
        {

        }

        private void customCol_TextChanged(object sender, EventArgs e)
        {
            
        }

        /**
         * @brief Satir kismina sadece sayi girilmesi icin yapildi.
         */
        private void customRow_KeyPress(object sender, KeyPressEventArgs e) //sadece sayı yazdırmak için
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        /**
         * @brief Sutun kismina sadece sayi girilmesi icin yapildi.
         */
        private void customCol_KeyPress(object sender, KeyPressEventArgs e) //sadece sayı yazdırmak için
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }

        private void colorbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage1.Text = "";
            lblMessage2.Text = "";
        }

        /**
         * @brief Onceki forma geri donulmesi icin yapildi.
         */
        private void btnBack_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
