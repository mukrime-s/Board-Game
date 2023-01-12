/**
* @file multiplayer.cs
* @author Mukrime_Sagiroglu_152120191034
* @author Hazar_Namdar_152120191053
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Media;
using System.Threading;
using System.Net.Sockets;

namespace BoardGame
{

    public partial class multiplayer : Form
    {
        private bool gotScore = false;
        private int score = 0;
        private ArrayList arrList2 = new ArrayList();
        private int eskiKonum = 0, yeniKonum = 0, sekil, renk;
        private Random rnd = new Random();
        private Button[,] board;

        /**
         * @brief Butondaki şekli kaybetmemek için oluşturulan global değişken
         */
        private Image image;
        

        public multiplayer(bool isHost, string ip = null)
        {
            InitializeComponent();

            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;

            board = new Button[9, 9];
            int top = 0;
            int left = 0;

            /**
             * @brief Tahta olusturuldu.
             */
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = new Button();
                    board[i, j].Width = 35;
                    board[i, j].Height = 35;
                    board[i, j].Left = left;
                    board[i, j].Top = top;
                    this.Controls.Add(board[i, j]);
                    left += 35;
                    board[i, j].BackColor = Color.White;
                }
                top += 35;
                left = 0;
            }

            /**
             * @brief Host kismi
             */
            if (isHost)
            {
                /**
                 * @brief Server Ipsi alinirsa coklu bilgisayar uzerinden oynanir. Burada xammp kullanildi. 
                 */
                server = new TcpListener(System.Net.IPAddress.Any, 8080);
                server.Start();
                sock = server.AcceptSocket();

                /**
                 * @brief Renk, sekil ve konumu byte cinsinden tutmak icin bir buffer olusturuldu. 
                 */
                Byte[] buffer = new Byte[9];

                /**
                 * @brief Verileri 2. kullaniciya aynen aktarmak icin list2 olusturuldu.
                 */
                List<int> list2 = new List<int>(fillBoard());
                for (int i = 0; i < 9; i++)
                {
                    buffer[i] = (byte)list2[i];
                }

                /**
                 * @brief Veri, byte a donusturulerek aktarildi.
                 */
                sock.Send(buffer);
            }
            /**
             * @brief Client kismi
             */
            else
            {
                try
                {
                    /**
                     * @brief Server Ipsi alinirsa coklu bilgisayar uzerinden oynanir. 
                     */
                    client = new TcpClient(ip, 8080);
                    sock = client.Client;
                    Byte[] buffer = new Byte[9];                             // 0,1,2,3,4,5,6,7,8    

                    /**
                     * @brief Aktarilan veri buraya geldi.
                     */
                    sock.Receive(buffer);                                   //sekil 0, 3, 6   // renk 1,4,7  // konum 2,5,8     // 1. için sırasıyla 0-1-2 
                    for (int i = 0; i < 9; i += 3) //sekil 0, 3, 6                                                              // 2. için sırasıyla 3-4-5 
                    {                                                                                                           // 3. için sırasıyla 6-7-8 
                        int number = 0;
                        for (int j = 0; j < 9; j++)   // satır
                        {
                            for (int k = 0; k < 9; k++)  //sutun
                            {
                                if (buffer[i + 2] == number)    // konum 2,5,8
                                {
                                    if (buffer[i] == 0 && buffer[i+1] == 0) // renk 1,4,7 
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmizikare.JPG");
                                    }
                                    if (buffer[i] == 0 && buffer[i + 1] == 1)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/mavikare.JPG");
                                    }
                                    if (buffer[i] == 0 && buffer[i + 1] == 2)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembekare.JPG");
                                    }

                                    if (buffer[i] == 1 && buffer[i + 1] == 0)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmiziucgen.JPG");
                                    }
                                    if (buffer[i] == 1 && buffer[i + 1] == 1)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/maviucgen.JPG");
                                    }
                                    if (buffer[i] == 1 && buffer[i + 1] == 2)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembeucgen.JPG");
                                    }

                                    if (buffer[i] == 2 && buffer[i + 1] == 0)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmizidaire.JPG");
                                    }
                                    if (buffer[i] == 2 && buffer[i + 1] == 1)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/mavidaire.JPG");
                                    }
                                    if (buffer[i] == 2 && buffer[i + 1] == 2)
                                    {
                                        board[j, k].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembedaire.JPG");
                                    }
                                }
                                number++;
                            }
                        }
                    }
                    MessageReceiver.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }

            /**
             * @brief Bos olan butonlar basilamaz hale gelir.
             */
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j].Click += new EventHandler(this.buttonCLick);

                    if (board[i, j].BackgroundImage == null)
                        board[i, j].Enabled = false;
                }
            }
        }

        /**
         * @brief TCP/IP protokolunu kullanmak icin degiskenler olusturuldu.
         */
        private Socket sock;
        private BackgroundWorker MessageReceiver = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;

        /**
         * @brief Kimin sirasi oldugunu belirleyecek fonksiyon olusturuldu.
         */
        private void MessageReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            if (CheckState())
                return;
            FreezeBoard();
            label1.Text = "Opponent's Turn!";
            ReceiveMove();
            label1.Text = "Your Turn!";
            if (!CheckState())
                UnfreezeBoard();
        }

        private bool CheckState()
        {
            return true;
        }

        /**
         * @brief Tahtayı basilamaz hale getirir.
         */
        private void FreezeBoard()
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i,j].Enabled = false;
                }
            }
            
        }

        /**
         * @brief Tahtayı basilabilir hale getirir.
         */
        private void UnfreezeBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j].Enabled = true;
                }
            }
        }


        private void ReceiveMove()
        {
            
        }

        /**
         * @brief Puan aldirma yapar.
         */
        public void getScore()
        {
            score += 3;
        }

        /**
         * @brief Dikeyde puan aldirmayi kontrol eden fonksiyon (newGame dosyasinda detayli anlatildi.)
         */
        public void getPointVertical(int j)
        {
            int pointCondition = 1;

            for (int i = 0; i < 9; i++)
            {
                if (i != 9 - 1 && board[i, j].BackgroundImage != null)
                {
                    for (int k = 1; k <= 9 - 1; k++)
                    {
                        if (board[k, j].BackgroundImage != null && i != k)
                        {
                            if (Convert.ToInt32(board[i, j].BackgroundImage.Tag) == Convert.ToInt32(board[k, j].BackgroundImage.Tag))
                            {
                                pointCondition++;
                            }
                        }
                    }
                }
                if (pointCondition >= 5)
                    break;
                else
                    pointCondition = 1;
            }

            if (pointCondition >= 5)
            {
                gotScore = true;

                for (int i = 0; i < 9; i++)
                {
                    board[i, j].BackgroundImage = null;
                }

                getScore();
            }
        }

        /**
         * @brief Yatayda puan aldirmayi kontrol eden fonksiyon (newGame dosyasinda detayli anlatildi.)
         */
        public void getPointHorizontal(int i)
        {
            int pointCondition = 1;

            for (int j = 0; j < 9; j++)
            {
                if (j != 9 - 1 && board[i, j].BackgroundImage != null)
                {
                    for (int k = 1; k <= 9 - 1; k++)
                    {
                        if (board[i, k].BackgroundImage != null && j != k)
                        {
                            if (Convert.ToInt32(board[i, j].BackgroundImage.Tag) == Convert.ToInt32(board[i, k].BackgroundImage.Tag))
                            {
                                pointCondition++;
                            }
                        }
                    }
                }

                if (pointCondition >= 5)
                {
                    break;
                }
                else
                    pointCondition = 1;
            }

            if (pointCondition >= 5)
            {
                gotScore = true;

                for (int j = 0; j < 9; j++)
                {
                    board[i, j].BackgroundImage = null;
                }

                getScore();

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /**
         * @brief Butona basildiginda oyunu oynatir. (newGame dosyasinda detayli anlatildi.)
         */
        public void buttonCLick(object sender, EventArgs e)
        {
            Button pushed = sender as Button;

            if (pushed.BackgroundImage != null)
            {
                image = pushed.BackgroundImage;

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (pushed.Tag == board[i, j].Tag)
                        {
                            eskiKonum = Convert.ToInt32(pushed.Tag);
                        }
                    }
                }

                pushed.BackgroundImage = null;

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        board[i, j].Enabled = false;

                        if (board[i, j].BackgroundImage == null)
                            board[i, j].Enabled = true;
                    }
                }
            }
            else //Basılan buton boşsa
            {
                pushed.BackgroundImage = image;

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (pushed.Tag == board[i, j].Tag)
                        {
                            yeniKonum = Convert.ToInt32(pushed.Tag);
                        }
                    }
                }

                for (int j = 0; j < 9; j++)
                    this.getPointVertical(j);
                for (int i = 0; i < 9; i++)
                    this.getPointHorizontal(i);

                Thread.Sleep(500);
                fillBoard();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        board[i, j].Enabled = true;

                        if (board[i, j].BackgroundImage == null)
                            board[i, j].Enabled = false;
                    }
                }
                SoundPlayer moveVoice = new SoundPlayer();//hamleden sonra ses eklendi.
                string konum = Application.StartupPath + @"../../../sounds/hamle.wav";

                moveVoice.SoundLocation = konum;

                SoundPlayer pointVoice = new SoundPlayer();
                string location = Application.StartupPath + @"../../../sounds/puan.wav";

                pointVoice.SoundLocation = location;

                //Playing some voice
                if (gotScore)
                {
                    pointVoice.Play();
                    gotScore = false;
                }
                else
                {
                    moveVoice.Play();
                }
                arrList2.Remove(eskiKonum);
                arrList2.Add(yeniKonum);
                arrList2.Sort();
            }
        }

        /**
         * @brief Her hamle sonrasi rastgele 3 yeni sekil atamak icin olusturulan fonksiyon (newGame dosyasinda detayli anlatildi.)
         */
        public List<int> fillBoard()
        {
            List<int> list = new List<int>();//sekil renk ve counterı tutmak icin olusturuldu.  
            int SeklinKonumu_1, SeklinKonumu_2, SeklinKonumu_3;

            while (true) //random hamlenin ust uste binmesi engellendi.
            {
                SeklinKonumu_1 = rnd.Next(0, 81);
                SeklinKonumu_2 = rnd.Next(0, 81);
                SeklinKonumu_3 = rnd.Next(0, 81);

                if ((!arrList2.Contains(SeklinKonumu_1)) && (!arrList2.Contains(SeklinKonumu_2)) && (!arrList2.Contains(SeklinKonumu_3)))
                {
                    arrList2.Add(SeklinKonumu_1);//seklin konum değerlerini tutmak icin.
                    arrList2.Add(SeklinKonumu_2);
                    arrList2.Add(SeklinKonumu_3);
                    break;
                }
            }
            int counter = -1;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    counter++;
                    //bu kısımda random aralıkları belirlendi.

                    sekil = rnd.Next(0, 3);//byte gönderddiğimizde sorun çıkmaması icin sıfırdan baslatildi.
                    renk = rnd.Next(0, 3);


                    if ((counter == SeklinKonumu_1 || counter == SeklinKonumu_2 || counter == SeklinKonumu_3) && (sekil == 0))//birden fazla renk seciminde arralistte aratma yaparken karisiklik çikmamasi icin olusturuldu.
                    {
                        if (renk == 0)    // Square & red
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmizikare.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 5;
                        }
                        if (renk == 1)  // Square & blue
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/mavikare.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 4;
                        }
                        if (renk == 2)  // Square & pink
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembekare.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 6;
                        }
                    }

                    if ((counter == SeklinKonumu_1 || counter == SeklinKonumu_2 || counter == SeklinKonumu_3) && sekil == 1)
                    {
                        if (renk == 0)  // Triangle & red
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmiziucgen.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 8;
                        }
                        if (renk == 1)  // Triangle & blue
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/maviucgen.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 7;
                        }
                        if (renk == 2)  // Triangle & pink
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembeucgen.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 9;
                        }
                    }

                    if ((counter == SeklinKonumu_1 || counter == SeklinKonumu_2 || counter == SeklinKonumu_3) && sekil == 2)
                    {
                        if (renk == 0)  // Round & red
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmizidaire.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 2;//boyutlar 70,70 olarak butonlara uygun şekilde ayarlandı.
                        }
                        if (renk == 1)  // Round & blue
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/mavidaire.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 1;
                        }
                        if (renk == 2)  // Round & pink
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembedaire.JPG");
                            list.Add(sekil);
                            list.Add(renk);
                            list.Add(counter);
                            board[i, j].BackgroundImage.Tag = 3;
                        }
                    }
                }
            }
            return list;
        }

        private void multiplayer_Load(object sender, EventArgs e)
        {
            
        }
    }
}
