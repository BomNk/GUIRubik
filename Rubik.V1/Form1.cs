using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using AForge.Video;
using AForge.Video.DirectShow;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.Structure;




namespace Rubik.V1
{
    public partial class Form1 : Form
    {
        public char[][] Rubik = new char[6][] { new char[] {'B','B','B','B','B','B','B','B','B' },
                                                new char[] {'P','P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                new char[] { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'  },
                                                new char[] { 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R' },
                                                new char[] {'G', 'G' , 'G' , 'G', 'G' , 'G' , 'G' , 'G','G' },
                                                new char[] { 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y' } };
        // public char[][] Rubik_copy = new char[6][];

        //Notebook
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        int numTake = 1;
        Image<Hsv, Byte> R_Image;
        Image<Hsv, Byte> My_Image;
        String str = "";
        int numPicture = 1;
        int numList = 1;

        
        // รูปต้นฉบับ
        // Image<Hsv, Byte> gray_image;   // รูปที่โดนเปลี่ยนเป็นแบบ GRY
        Image<Hsv, Byte> My_image_copy; // รูปที่ใช้แก้ใข 

        public Form1()
        {
           
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.Open();
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in webcam)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;

        }


        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)   // Finish
        {

            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            My_Image = new Image<Hsv, byte>(bit);
            if (numTake == 1)
            {
                Box1.Image = bit;
                R_Image = My_Image.Copy();
               // My_Image1 = new Image<Hsv, byte>(bit);
            }
            
            else if (numTake == 2)
            {
                R_Image = My_Image.Copy();
                Box2.Image = bit;
               // My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 3)
            {
                R_Image = My_Image.Copy();
                Box3.Image = bit;
              //  My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 4)
            {
                R_Image = My_Image.Copy();
                Box4.Image = bit;
                //My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 5)
            {
                R_Image = My_Image.Copy();
                Box5.Image = bit;
              //  My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 6)
            {
                R_Image = My_Image.Copy();
                Box6.Image = bit;
                //My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }

        }

        private void Start_Click_1(object sender, EventArgs e) // เปิดกล้องงงงงงงง // Finish
        {
            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            cam.Start();
            label2.Text = "Cam_" + numTake + " Running";


        }

        private void Take_Click_1(object sender, EventArgs e)  // Finish
        {
            // My_Image = new Image<Hsv, Byte>();
            read_color(numTake-1);
            numTake++;
            timer1.Enabled = true;
            //  String str = "";
            
            timer1.Start();

            label2.Text = "Cam_" + numTake + " Running";

            show_color(Rubik);
            //  saveFileDialog1.InitialDirectory = @"d:\picture";
            //saveFileDialog1.FileName = str+i;
            //  if (DialogResult.OK == saveFileDialog1.ShowDialog())
            // {
            //         Box1.Image.Save(saveFileDialog1.FileName);
            //  }
           // int a = numTake - 2;
           // pictureBox1.Image = R_Image.ToBitmap();

            timer1.Stop();
            //numTake++;
            if (numTake == 7)
                label2.Text = " Complete";
            cam.Start();
        }


        private void Stop_Click(object sender, EventArgs e) // Finish
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }

        }













        public void take_pic()// ถ่ายภาพจากลูกรูบิค แล้ว save ภาพ  1 ภาพ ไว้ใช้เรียกหลายครั้ง
        {

        }


        public void read_color(int Face) // อ่านสี ภาพ 1 ภาพ // Finish
        {
            int[,] col = new int[9, 3];
            int i = 0, j = 0;
            int A = 0, B = 0;
            int roll, collum;
            float sumHue = 0, sumSatuation = 0, sumValue = 0;
            //ดึงค่าใส่ในตัวแปร

            for (int y = 110; y < 360; y += 120)    // แกน YYYYYYYYYYY
            {

                for (int x = 220; x < 470; x += 120)   // แกน XXXXXXXXXXXXX
                {
                    collum = y - 5;
                    roll = x - 5;
                    sumHue = 0;
                    sumSatuation = 0;
                    sumValue = 0;
                    for (A = 0; A < 10; A++)
                    {
                        for (B = 0; B < 10; B++)
                        {
                            sumHue += (float)(0.01 * R_Image[collum, roll].Hue);
                            sumSatuation += (float)(0.01 * R_Image[collum, roll].Satuation);
                            sumValue += (float)(0.01 * R_Image[collum, roll].Value);
                        }
                    }
                    // label10.Text = "sumB = " + (int)((sumB / 100) * 100);
                    // label11.Text = "sumG = " + (int)((sumG / 100) * 100);
                    // label12.Text = "sumG = " + (int)((sumR / 100) * 100);



                    for (j = 0; j < 3; j++)
                    {

                        if (j == 0)
                            col[i, j] = (int)((sumHue / 100) * 100);
                        else if (j == 1)
                            col[i, j] = (int)((sumSatuation / 100) * 100);
                        else if (j == 2)
                            col[i, j] = (int)((sumValue / 100) * 100);
                        /*
                        if (j == 0)
                            col[i, j] = My_Image[y, x].Blue;
                        else if (j == 1)
                            col[i, j] = My_Image[y, x].Green;
                        else if (j == 2)
                            col[i, j] = My_Image[y, x].Red;
                           */

                    }


                    i++;
                }
            }
            /*
            label10.Text = "sumB = " + sumB;
            label11.Text = "sumG = " + sumG;
            label12.Text = "sumG = " + sumR;
            */
            //set_label(col);
            double H, g, r;
            for (i = 0; i < 9; i++)
            {
                H = col[i, 0];
                g = col[i, 1];
                r = col[i, 2];
                if (H > 87 && H < 99)    // white
                {
                    Rubik[Face][i] = 'W';
                }

                if (H > 99 && H < 115)   //blue
                {
                    Rubik[Face][i] = 'B';
                }

                if (H > 140 && H < 160)   // ม่วง
                {
                    Rubik[Face][i] = 'P';
                }

                if (H >= 62 && H < 83)   //Green
                {
                    Rubik[Face][i] = 'G';
                }

                if (H > 28 && H < 49) //yellow
                {
                    Rubik[Face][i] = 'Y';
                }

                if ((H > 162 && H < 180) || (H >= 0 && H < 13)) // RED
                {
                    Rubik[Face][i] = 'R';
                }

            }






        }            //



        public void save_color() // บันทึกสีภาพของลูกรูบิค 6 ด้าน
        {

        }

      





        public void rotate_left() //หมุนซ้าย // Finish
        {
           
            rotate_Face(Rubik, 1);

            char[] tmp = new char[3];
            tmp[0] = Rubik[0][0]; tmp[1] = Rubik[0][3]; ; tmp[2] = Rubik[0][6];
            Rubik[0][0] = Rubik[2][0]; Rubik[0][3] = Rubik[2][3]; Rubik[0][6] = Rubik[2][6];
            Rubik[2][0] = Rubik[4][0]; Rubik[2][3] = Rubik[4][3]; Rubik[2][6] = Rubik[4][6];
            Rubik[4][0] = Rubik[5][0]; Rubik[4][3] = Rubik[5][3]; Rubik[4][6] = Rubik[5][6];
            Rubik[5][0] = tmp[0]; Rubik[5][3] = tmp[1]; Rubik[5][6] = tmp[2];





        }

        public void rotate_right() //หมุนขวา // Finish
        {
            rotate_Face(Rubik, 3);

            char[] tmp = new char[3];
            tmp[0] = Rubik[0][2]; tmp[1] = Rubik[0][5]; ; tmp[2] = Rubik[0][8];
            Rubik[0][2] = Rubik[5][2]; Rubik[0][5] = Rubik[5][5]; Rubik[0][8] = Rubik[5][8];
            Rubik[5][2] = Rubik[4][2]; Rubik[5][5] = Rubik[4][5]; Rubik[5][8] = Rubik[4][8];
            Rubik[4][2] = Rubik[2][2]; Rubik[4][5] = Rubik[2][5]; Rubik[4][8] = Rubik[2][8];
            Rubik[2][2] = tmp[0]; Rubik[2][5] = tmp[1]; Rubik[2][8] = tmp[2];

        }

        public void rotate_top() // หมุนบน // Finish
        {
            rotate_Face_right(Rubik,3);
            rotate_Face(Rubik, 1);

            char[] rk = Rubik[0];
            Rubik[0] = Rubik[2];
            Rubik[2] = Rubik[4];
            Rubik[4] = Rubik[5];
            Rubik[5] = rk;


            /*
            rotate_Face(Rubik, 2);

            char[] tmp = new char[3];
            tmp[0] = Rubik[0][6]; tmp[1] = Rubik[0][7]; ; tmp[2] = Rubik[0][8];
            Rubik[0][6] = Rubik[3][0]; Rubik[0][7] = Rubik[3][3]; Rubik[0][8] = Rubik[3][6];
            Rubik[3][0] = Rubik[4][2]; Rubik[3][3] = Rubik[4][1]; Rubik[3][6] = Rubik[4][0];
            Rubik[4][2] = Rubik[1][8]; Rubik[4][1] = Rubik[1][5]; Rubik[4][0] = Rubik[1][2];
            Rubik[1][8] = tmp[0]; Rubik[1][5] = tmp[1]; Rubik[1][2] = tmp[2];
            */
        }

       

        public void rotate_front() // หมุนหน้า // Finish
        {
            rotate_Face(Rubik, 4);

            char[] tmp = new char[3];
            tmp[0] = Rubik[2][6]; tmp[1] = Rubik[2][7]; ; tmp[2] = Rubik[2][8];
            Rubik[2][6] = Rubik[3][6]; Rubik[2][7] = Rubik[3][7]; Rubik[2][8] = Rubik[3][8];
            Rubik[3][6] = Rubik[5][2]; Rubik[3][7] = Rubik[5][1]; Rubik[3][8] = Rubik[5][0];
            Rubik[5][2] = Rubik[1][6]; Rubik[5][1] = Rubik[1][7]; Rubik[5][0] = Rubik[1][8];
            Rubik[1][6] = tmp[0]; Rubik[1][7] = tmp[1]; Rubik[1][8] = tmp[2];

        }

        public void rotate_rear() // หมุนด้านหลัง // Finish
        {
            rotate_Face(Rubik, 0);


            char[] tmp = new char[3];
            tmp[0] = Rubik[5][6]; tmp[1] = Rubik[5][7]; ; tmp[2] = Rubik[5][8];
            Rubik[5][6] = Rubik[3][2]; Rubik[5][7] = Rubik[3][1]; Rubik[5][8] = Rubik[3][0];
            Rubik[3][2] = Rubik[2][2]; Rubik[3][1] = Rubik[2][1]; Rubik[3][0] = Rubik[2][0];
            Rubik[2][2] = Rubik[1][2]; Rubik[2][1] = Rubik[1][1]; Rubik[2][0] = Rubik[1][0];
            Rubik[1][2] = tmp[0]; Rubik[1][1] = tmp[1]; Rubik[1][0] = tmp[2];



        }

        public void rotate_Face(char[][] Rc,int F) //ไว้หมุนหน้าทุกด้าน // Finish
        {
            char M = Rc[F][1];

            Rc[F][1] = Rc[F][5];
            Rc[F][5] = Rc[F][7];
            Rc[F][7] = Rc[F][3];
            Rc[F][3] = M;

            char C = Rc[F][0];

            Rc[F][0] = Rc[F][2];
            Rc[F][2] = Rc[F][8];
            Rc[F][8] = Rc[F][6];
            Rc[F][6] = C;

           
        }


        public void rotate_Face_right(char[][] Rc, int F)
        {
            char M = Rc[F][1];

            Rc[F][1] = Rc[F][3];
            Rc[F][3] = Rc[F][7];
            Rc[F][7] = Rc[F][5];
            Rc[F][5] = M;

            char C = Rc[F][0];

            Rc[F][0] = Rc[F][6];
            Rc[F][6] = Rc[F][8];
            Rc[F][8] = Rc[F][2];
            Rc[F][2] = C;
        }





        public void show_color(char[][] rk) // แสดงค่าสีบน panel // Finish
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (i == 0) // Zone AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                A0.BackColor = Color.Purple;
                            else if (j == 1)
                                A1.BackColor = Color.Purple;
                            else if (j == 2)
                                A2.BackColor = Color.Purple;
                            else if (j == 3)
                                A3.BackColor = Color.Purple;
                            else if (j == 4)
                                A4.BackColor = Color.Purple;
                            else if (j == 5)
                                A5.BackColor = Color.Purple;
                            else if (j == 6)
                                A6.BackColor = Color.Purple;
                            else if (j == 7)
                                A7.BackColor = Color.Purple;
                            else if (j == 8)
                                A8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                A0.BackColor = Color.Blue;
                            else if (j == 1)
                                A1.BackColor = Color.Blue;
                            else if (j == 2)
                                A2.BackColor = Color.Blue;
                            else if (j == 3)
                                A3.BackColor = Color.Blue;
                            else if (j == 4)
                                A4.BackColor = Color.Blue;
                            else if (j == 5)
                                A5.BackColor = Color.Blue;
                            else if (j == 6)
                                A6.BackColor = Color.Blue;
                            else if (j == 7)
                                A7.BackColor = Color.Blue;
                            else if (j == 8)
                                A8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                A0.BackColor = Color.Yellow;
                            else if (j == 1)
                                A1.BackColor = Color.Yellow;
                            else if (j == 2)
                                A2.BackColor = Color.Yellow;
                            else if (j == 3)
                                A3.BackColor = Color.Yellow;
                            else if (j == 4)
                                A4.BackColor = Color.Yellow;
                            else if (j == 5)
                                A5.BackColor = Color.Yellow;
                            else if (j == 6)
                                A6.BackColor = Color.Yellow;
                            else if (j == 7)
                                A7.BackColor = Color.Yellow;
                            else if (j == 8)
                                A8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                A0.BackColor = Color.Green;
                            else if (j == 1)
                                A1.BackColor = Color.Green;
                            else if (j == 2)
                                A2.BackColor = Color.Green;
                            else if (j == 3)
                                A3.BackColor = Color.Green;
                            else if (j == 4)
                                A4.BackColor = Color.Green;
                            else if (j == 5)
                                A5.BackColor = Color.Green;
                            else if (j == 6)
                                A6.BackColor = Color.Green;
                            else if (j == 7)
                                A7.BackColor = Color.Green;
                            else if (j == 8)
                                A8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                A0.BackColor = Color.Red;
                            else if (j == 1)
                                A1.BackColor = Color.Red;
                            else if (j == 2)
                                A2.BackColor = Color.Red;
                            else if (j == 3)
                                A3.BackColor = Color.Red;
                            else if (j == 4)
                                A4.BackColor = Color.Red;
                            else if (j == 5)
                                A5.BackColor = Color.Red;
                            else if (j == 6)
                                A6.BackColor = Color.Red;
                            else if (j == 7)
                                A7.BackColor = Color.Red;
                            else if (j == 8)
                                A8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                A0.BackColor = Color.White;
                            else if (j == 1)
                                A1.BackColor = Color.White;
                            else if (j == 2)
                                A2.BackColor = Color.White;
                            else if (j == 3)
                                A3.BackColor = Color.White;
                            else if (j == 4)
                                A4.BackColor = Color.White;
                            else if (j == 5)
                                A5.BackColor = Color.White;
                            else if (j == 6)
                                A6.BackColor = Color.White;
                            else if (j == 7)
                                A7.BackColor = Color.White;
                            else if (j == 8)
                                A8.BackColor = Color.White;

                        }
                    }


                    if (i == 1) // Zone BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                B0.BackColor = Color.Purple;
                            else if (j == 1)
                                B1.BackColor = Color.Purple;
                            else if (j == 2)
                                B2.BackColor = Color.Purple;
                            else if (j == 3)
                                B3.BackColor = Color.Purple;
                            else if (j == 4)
                                B4.BackColor = Color.Purple;
                            else if (j == 5)
                                B5.BackColor = Color.Purple;
                            else if (j == 6)
                                B6.BackColor = Color.Purple;
                            else if (j == 7)
                                B7.BackColor = Color.Purple;
                            else if (j == 8)
                                B8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                B0.BackColor = Color.Blue;
                            else if (j == 1)
                                B1.BackColor = Color.Blue;
                            else if (j == 2)
                                B2.BackColor = Color.Blue;
                            else if (j == 3)
                                B3.BackColor = Color.Blue;
                            else if (j == 4)
                                B4.BackColor = Color.Blue;
                            else if (j == 5)
                                B5.BackColor = Color.Blue;
                            else if (j == 6)
                                B6.BackColor = Color.Blue;
                            else if (j == 7)
                                B7.BackColor = Color.Blue;
                            else if (j == 8)
                                B8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                B0.BackColor = Color.Yellow;
                            else if (j == 1)
                                B1.BackColor = Color.Yellow;
                            else if (j == 2)
                                B2.BackColor = Color.Yellow;
                            else if (j == 3)
                                B3.BackColor = Color.Yellow;
                            else if (j == 4)
                                B4.BackColor = Color.Yellow;
                            else if (j == 5)
                                B5.BackColor = Color.Yellow;
                            else if (j == 6)
                                B6.BackColor = Color.Yellow;
                            else if (j == 7)
                                B7.BackColor = Color.Yellow;
                            else if (j == 8)
                                B8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                B0.BackColor = Color.Green;
                            else if (j == 1)
                                B1.BackColor = Color.Green;
                            else if (j == 2)
                                B2.BackColor = Color.Green;
                            else if (j == 3)
                                B3.BackColor = Color.Green;
                            else if (j == 4)
                                B4.BackColor = Color.Green;
                            else if (j == 5)
                                B5.BackColor = Color.Green;
                            else if (j == 6)
                                B6.BackColor = Color.Green;
                            else if (j == 7)
                                B7.BackColor = Color.Green;
                            else if (j == 8)
                                B8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                B0.BackColor = Color.Red;
                            else if (j == 1)
                                B1.BackColor = Color.Red;
                            else if (j == 2)
                                B2.BackColor = Color.Red;
                            else if (j == 3)
                                B3.BackColor = Color.Red;
                            else if (j == 4)
                                B4.BackColor = Color.Red;
                            else if (j == 5)
                                B5.BackColor = Color.Red;
                            else if (j == 6)
                                B6.BackColor = Color.Red;
                            else if (j == 7)
                                B7.BackColor = Color.Red;
                            else if (j == 8)
                                B8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                B0.BackColor = Color.White;
                            else if (j == 1)
                                B1.BackColor = Color.White;
                            else if (j == 2)
                                B2.BackColor = Color.White;
                            else if (j == 3)
                                B3.BackColor = Color.White;
                            else if (j == 4)
                                B4.BackColor = Color.White;
                            else if (j == 5)
                                B5.BackColor = Color.White;
                            else if (j == 6)
                                B6.BackColor = Color.White;
                            else if (j == 7)
                                B7.BackColor = Color.White;
                            else if (j == 8)
                                B8.BackColor = Color.White;

                        }
                    }


                    if (i == 2) // Zone CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                C0.BackColor = Color.Purple;
                            else if (j == 1)
                                C1.BackColor = Color.Purple;
                            else if (j == 2)
                                C2.BackColor = Color.Purple;
                            else if (j == 3)
                                C3.BackColor = Color.Purple;
                            else if (j == 4)
                                C4.BackColor = Color.Purple;
                            else if (j == 5)
                                C5.BackColor = Color.Purple;
                            else if (j == 6)
                                C6.BackColor = Color.Purple;
                            else if (j == 7)
                                C7.BackColor = Color.Purple;
                            else if (j == 8)
                                C8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                C0.BackColor = Color.Blue;
                            else if (j == 1)
                                C1.BackColor = Color.Blue;
                            else if (j == 2)
                                C2.BackColor = Color.Blue;
                            else if (j == 3)
                                C3.BackColor = Color.Blue;
                            else if (j == 4)
                                C4.BackColor = Color.Blue;
                            else if (j == 5)
                                C5.BackColor = Color.Blue;
                            else if (j == 6)
                                C6.BackColor = Color.Blue;
                            else if (j == 7)
                                C7.BackColor = Color.Blue;
                            else if (j == 8)
                                C8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                C0.BackColor = Color.Yellow;
                            else if (j == 1)
                                C1.BackColor = Color.Yellow;
                            else if (j == 2)
                                C2.BackColor = Color.Yellow;
                            else if (j == 3)
                                C3.BackColor = Color.Yellow;
                            else if (j == 4)
                                C4.BackColor = Color.Yellow;
                            else if (j == 5)
                                C5.BackColor = Color.Yellow;
                            else if (j == 6)
                                C6.BackColor = Color.Yellow;
                            else if (j == 7)
                                C7.BackColor = Color.Yellow;
                            else if (j == 8)
                                C8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                C0.BackColor = Color.Green;
                            else if (j == 1)
                                C1.BackColor = Color.Green;
                            else if (j == 2)
                                C2.BackColor = Color.Green;
                            else if (j == 3)
                                C3.BackColor = Color.Green;
                            else if (j == 4)
                                C4.BackColor = Color.Green;
                            else if (j == 5)
                                C5.BackColor = Color.Green;
                            else if (j == 6)
                                C6.BackColor = Color.Green;
                            else if (j == 7)
                                C7.BackColor = Color.Green;
                            else if (j == 8)
                                C8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                C0.BackColor = Color.Red;
                            else if (j == 1)
                                C1.BackColor = Color.Red;
                            else if (j == 2)
                                C2.BackColor = Color.Red;
                            else if (j == 3)
                                C3.BackColor = Color.Red;
                            else if (j == 4)
                                C4.BackColor = Color.Red;
                            else if (j == 5)
                                C5.BackColor = Color.Red;
                            else if (j == 6)
                                C6.BackColor = Color.Red;
                            else if (j == 7)
                                C7.BackColor = Color.Red;
                            else if (j == 8)
                                C8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                C0.BackColor = Color.White;
                            else if (j == 1)
                                C1.BackColor = Color.White;
                            else if (j == 2)
                                C2.BackColor = Color.White;
                            else if (j == 3)
                                C3.BackColor = Color.White;
                            else if (j == 4)
                                C4.BackColor = Color.White;
                            else if (j == 5)
                                C5.BackColor = Color.White;
                            else if (j == 6)
                                C6.BackColor = Color.White;
                            else if (j == 7)
                                C7.BackColor = Color.White;
                            else if (j == 8)
                                C8.BackColor = Color.White;

                        }
                    }


                    if (i == 3) // Zone DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                D0.BackColor = Color.Purple;
                            else if (j == 1)
                                D1.BackColor = Color.Purple;
                            else if (j == 2)
                                D2.BackColor = Color.Purple;
                            else if (j == 3)
                                D3.BackColor = Color.Purple;
                            else if (j == 4)
                                D4.BackColor = Color.Purple;
                            else if (j == 5)
                                D5.BackColor = Color.Purple;
                            else if (j == 6)
                                D6.BackColor = Color.Purple;
                            else if (j == 7)
                                D7.BackColor = Color.Purple;
                            else if (j == 8)
                                D8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                D0.BackColor = Color.Blue;
                            else if (j == 1)
                                D1.BackColor = Color.Blue;
                            else if (j == 2)
                                D2.BackColor = Color.Blue;
                            else if (j == 3)
                                D3.BackColor = Color.Blue;
                            else if (j == 4)
                                D4.BackColor = Color.Blue;
                            else if (j == 5)
                                D5.BackColor = Color.Blue;
                            else if (j == 6)
                                D6.BackColor = Color.Blue;
                            else if (j == 7)
                                D7.BackColor = Color.Blue;
                            else if (j == 8)
                                D8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                D0.BackColor = Color.Yellow;
                            else if (j == 1)
                                D1.BackColor = Color.Yellow;
                            else if (j == 2)
                                D2.BackColor = Color.Yellow;
                            else if (j == 3)
                                D3.BackColor = Color.Yellow;
                            else if (j == 4)
                                D4.BackColor = Color.Yellow;
                            else if (j == 5)
                                D5.BackColor = Color.Yellow;
                            else if (j == 6)
                                D6.BackColor = Color.Yellow;
                            else if (j == 7)
                                D7.BackColor = Color.Yellow;
                            else if (j == 8)
                                D8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                D0.BackColor = Color.Green;
                            else if (j == 1)
                                D1.BackColor = Color.Green;
                            else if (j == 2)
                                D2.BackColor = Color.Green;
                            else if (j == 3)
                                D3.BackColor = Color.Green;
                            else if (j == 4)
                                D4.BackColor = Color.Green;
                            else if (j == 5)
                                D5.BackColor = Color.Green;
                            else if (j == 6)
                                D6.BackColor = Color.Green;
                            else if (j == 7)
                                D7.BackColor = Color.Green;
                            else if (j == 8)
                                D8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                D0.BackColor = Color.Red;
                            else if (j == 1)
                                D1.BackColor = Color.Red;
                            else if (j == 2)
                                D2.BackColor = Color.Red;
                            else if (j == 3)
                                D3.BackColor = Color.Red;
                            else if (j == 4)
                                D4.BackColor = Color.Red;
                            else if (j == 5)
                                D5.BackColor = Color.Red;
                            else if (j == 6)
                                D6.BackColor = Color.Red;
                            else if (j == 7)
                                D7.BackColor = Color.Red;
                            else if (j == 8)
                                D8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                D0.BackColor = Color.White;
                            else if (j == 1)
                                D1.BackColor = Color.White;
                            else if (j == 2)
                                D2.BackColor = Color.White;
                            else if (j == 3)
                                D3.BackColor = Color.White;
                            else if (j == 4)
                                D4.BackColor = Color.White;
                            else if (j == 5)
                                D5.BackColor = Color.White;
                            else if (j == 6)
                                D6.BackColor = Color.White;
                            else if (j == 7)
                                D7.BackColor = Color.White;
                            else if (j == 8)
                                D8.BackColor = Color.White;

                        }
                    }

                    if (i == 4) // Zone E
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                E0.BackColor = Color.Purple;
                            else if (j == 1)
                                E1.BackColor = Color.Purple;
                            else if (j == 2)
                                E2.BackColor = Color.Purple;
                            else if (j == 3)
                                E3.BackColor = Color.Purple;
                            else if (j == 4)
                                E4.BackColor = Color.Purple;
                            else if (j == 5)
                                E5.BackColor = Color.Purple;
                            else if (j == 6)
                                E6.BackColor = Color.Purple;
                            else if (j == 7)
                                E7.BackColor = Color.Purple;
                            else if (j == 8)
                                E8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                E0.BackColor = Color.Blue;
                            else if (j == 1)
                                E1.BackColor = Color.Blue;
                            else if (j == 2)
                                E2.BackColor = Color.Blue;
                            else if (j == 3)
                                E3.BackColor = Color.Blue;
                            else if (j == 4)
                                E4.BackColor = Color.Blue;
                            else if (j == 5)
                                E5.BackColor = Color.Blue;
                            else if (j == 6)
                                E6.BackColor = Color.Blue;
                            else if (j == 7)
                                E7.BackColor = Color.Blue;
                            else if (j == 8)
                                E8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                E0.BackColor = Color.Yellow;
                            else if (j == 1)
                                E1.BackColor = Color.Yellow;
                            else if (j == 2)
                                E2.BackColor = Color.Yellow;
                            else if (j == 3)
                                E3.BackColor = Color.Yellow;
                            else if (j == 4)
                                E4.BackColor = Color.Yellow;
                            else if (j == 5)
                                E5.BackColor = Color.Yellow;
                            else if (j == 6)
                                E6.BackColor = Color.Yellow;
                            else if (j == 7)
                                E7.BackColor = Color.Yellow;
                            else if (j == 8)
                                E8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                E0.BackColor = Color.Green;
                            else if (j == 1)
                                E1.BackColor = Color.Green;
                            else if (j == 2)
                                E2.BackColor = Color.Green;
                            else if (j == 3)
                                E3.BackColor = Color.Green;
                            else if (j == 4)
                                E4.BackColor = Color.Green;
                            else if (j == 5)
                                E5.BackColor = Color.Green;
                            else if (j == 6)
                                E6.BackColor = Color.Green;
                            else if (j == 7)
                                E7.BackColor = Color.Green;
                            else if (j == 8)
                                E8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                E0.BackColor = Color.Red;
                            else if (j == 1)
                                E1.BackColor = Color.Red;
                            else if (j == 2)
                                E2.BackColor = Color.Red;
                            else if (j == 3)
                                E3.BackColor = Color.Red;
                            else if (j == 4)
                                E4.BackColor = Color.Red;
                            else if (j == 5)
                                E5.BackColor = Color.Red;
                            else if (j == 6)
                                E6.BackColor = Color.Red;
                            else if (j == 7)
                                E7.BackColor = Color.Red;
                            else if (j == 8)
                                E8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                E0.BackColor = Color.White;
                            else if (j == 1)
                                E1.BackColor = Color.White;
                            else if (j == 2)
                                E2.BackColor = Color.White;
                            else if (j == 3)
                                E3.BackColor = Color.White;
                            else if (j == 4)
                                E4.BackColor = Color.White;
                            else if (j == 5)
                                E5.BackColor = Color.White;
                            else if (j == 6)
                                E6.BackColor = Color.White;
                            else if (j == 7)
                                E7.BackColor = Color.White;
                            else if (j == 8)
                                E8.BackColor = Color.White;

                        }
                    }

                    if (i == 5) // Zone F
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                F0.BackColor = Color.Purple;
                            else if (j == 1)
                                F1.BackColor = Color.Purple;
                            else if (j == 2)
                                F2.BackColor = Color.Purple;
                            else if (j == 3)
                                F3.BackColor = Color.Purple;
                            else if (j == 4)
                                F4.BackColor = Color.Purple;
                            else if (j == 5)
                                F5.BackColor = Color.Purple;
                            else if (j == 6)
                                F6.BackColor = Color.Purple;
                            else if (j == 7)
                                F7.BackColor = Color.Purple;
                            else if (j == 8)
                                F8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                F0.BackColor = Color.Blue;
                            else if (j == 1)
                                F1.BackColor = Color.Blue;
                            else if (j == 2)
                                F2.BackColor = Color.Blue;
                            else if (j == 3)
                                F3.BackColor = Color.Blue;
                            else if (j == 4)
                                F4.BackColor = Color.Blue;
                            else if (j == 5)
                                F5.BackColor = Color.Blue;
                            else if (j == 6)
                                F6.BackColor = Color.Blue;
                            else if (j == 7)
                                F7.BackColor = Color.Blue;
                            else if (j == 8)
                                F8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                F0.BackColor = Color.Yellow;
                            else if (j == 1)
                                F1.BackColor = Color.Yellow;
                            else if (j == 2)
                                F2.BackColor = Color.Yellow;
                            else if (j == 3)
                                F3.BackColor = Color.Yellow;
                            else if (j == 4)
                                F4.BackColor = Color.Yellow;
                            else if (j == 5)
                                F5.BackColor = Color.Yellow;
                            else if (j == 6)
                                F6.BackColor = Color.Yellow;
                            else if (j == 7)
                                F7.BackColor = Color.Yellow;
                            else if (j == 8)
                                F8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                F0.BackColor = Color.Green;
                            else if (j == 1)
                                F1.BackColor = Color.Green;
                            else if (j == 2)
                                F2.BackColor = Color.Green;
                            else if (j == 3)
                                F3.BackColor = Color.Green;
                            else if (j == 4)
                                F4.BackColor = Color.Green;
                            else if (j == 5)
                                F5.BackColor = Color.Green;
                            else if (j == 6)
                                F6.BackColor = Color.Green;
                            else if (j == 7)
                                F7.BackColor = Color.Green;
                            else if (j == 8)
                                F8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                F0.BackColor = Color.Red;
                            else if (j == 1)
                                F1.BackColor = Color.Red;
                            else if (j == 2)
                                F2.BackColor = Color.Red;
                            else if (j == 3)
                                F3.BackColor = Color.Red;
                            else if (j == 4)
                                F4.BackColor = Color.Red;
                            else if (j == 5)
                                F5.BackColor = Color.Red;
                            else if (j == 6)
                                F6.BackColor = Color.Red;
                            else if (j == 7)
                                F7.BackColor = Color.Red;
                            else if (j == 8)
                                F8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                F0.BackColor = Color.White;
                            else if (j == 1)
                                F1.BackColor = Color.White;
                            else if (j == 2)
                                F2.BackColor = Color.White;
                            else if (j == 3)
                                F3.BackColor = Color.White;
                            else if (j == 4)
                                F4.BackColor = Color.White;
                            else if (j == 5)
                                F5.BackColor = Color.White;
                            else if (j == 6)
                                F6.BackColor = Color.White;
                            else if (j == 7)
                                F7.BackColor = Color.White;
                            else if (j == 8)
                                F8.BackColor = Color.White;

                        }
                    } // end Zone F


                } // End for j
            } // end for i
        } // end Function show Panel




     
        private void Test_Click(object sender, EventArgs e) // Reset //Finish
        {
            char[][] RK = new char[6][] {  new char[] {'B','B','B','B','B','B','B','B','B' },
                                                new char[] {'P','P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                new char[] { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'  },
                                                new char[] { 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R' },
                                                new char[] {'G', 'G' , 'G' , 'G', 'G' , 'G' , 'G' , 'G','G' },
                                                new char[] { 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y' } };

            listBox1.Items.Clear();
            numList = 0;
            Rubik = RK ;
            show_color(Rubik);

        } 



        //ส่วนควบคุมการหมุน

       



        // ทิศในการหมุน
        







      
        private void Ck_Color_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.Show();
        }

        private void button2_Click(object sender, EventArgs e) // Test  TakePicture
        {
            
            if (numPicture == 1)
                str = "C:/Users/BOM/Desktop/New folder/2.jpg";
            else if (numPicture == 2)
                str = "C:/Users/BOM/Desktop/New folder/1.jpg";
            else if (numPicture == 3)
                str = "C:/Users/BOM/Desktop/New folder/6.jpg";
            else if(numPicture == 4)
                str = "C:/Users/BOM/Desktop/New folder/5.jpg";
            else if(numPicture == 5)
                str = "C:/Users/BOM/Desktop/New folder/4.jpg";
            else if(numPicture == 6)

                str = "C:/Users/BOM/Desktop/New folder/3.jpg";
            R_Image = new Image<Hsv, Byte>(str);

            if (numPicture == 1)
                Box1.Image = R_Image.ToBitmap();
            if (numPicture == 2)
                Box2.Image = R_Image.ToBitmap();
            if (numPicture == 3)
                Box3.Image = R_Image.ToBitmap();
            if (numPicture == 4)
                Box4.Image = R_Image.ToBitmap();
            if (numPicture == 5)
                Box5.Image = R_Image.ToBitmap();
            if (numPicture == 6)
                Box6.Image = R_Image.ToBitmap();

            read_color(numPicture - 1);

            show_color(Rubik);
            numPicture++;





        }

        private void RotateRight_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RotateLeft_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Left_Click(object sender, EventArgs e)    // Click L
        {
            rotate_left();
            show_color(Rubik);
            listBox1.Items.Add(numList + ": Left");
            numList++;
            serialPort1.Write("L");
        }

        private void Right_Click_1(object sender, EventArgs e)   // Click R
        {
            rotate_right();
            show_color(Rubik);
            listBox1.Items.Add(numList + ": Right");
            numList++;
            serialPort1.Write("R");
        }

        private void Top_Click(object sender, EventArgs e)   // Chang Face
        {
            rotate_top();
            show_color(Rubik);
            listBox1.Items.Add(numList + ": Change_Face");
            numList++;
        }

        private void Front_Click(object sender, EventArgs e) //Click F
        {
            rotate_front();
            show_color(Rubik);
            listBox1.Items.Add(numList + ": Front");
            numList++;
            serialPort1.Write("F");// Tanapon Ninket
        }

        private void Rear_Click(object sender, EventArgs e)  // Click B
        {
            rotate_rear();
            show_color(Rubik);
            listBox1.Items.Add(numList + ": Rear");
            numList++;
            serialPort1.Write("B");
        }


        private void U_Click(object sender, EventArgs e)  
        {
            

        }

        private void D_Click(object sender, EventArgs e)
        {

        }

        private void Bdet_Click(object sender, EventArgs e)
        {
            serialPort1.Write("b");
        }

        private void Ldet_Click(object sender, EventArgs e)
        {
            serialPort1.Write("l");
        }

        private void Fdet_Click(object sender, EventArgs e)
        {
            serialPort1.Write("f");
        }

        private void Rdet_Click(object sender, EventArgs e)
        {
            serialPort1.Write("r");
        }

        private void Udet_Click(object sender, EventArgs e)
        {

        }

        private void Ddet_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
        }


        private void open_left_right_Click(object sender, EventArgs e) // เลื่อนออก ซ้าย---ขวา
        {
            serialPort1.Write("K");
            //ส่งค่าควบคุม Hardware  
        }

        private void close_left_right_Click(object sender, EventArgs e) // เลื่อนเข้า ซ้าย---ขวา
        {
            serialPort1.Write("k");
            // ส่งค่าควบคุม Hardware
        }

        private void open_front_back_Click(object sender, EventArgs e)  // เลื่อนเข้า หน้า---หลัง
        {
            serialPort1.Write("J");
            // ส่งค่าควบคุม Hardware
        }

        private void close_front_back_Click(object sender, EventArgs e) // เลื่อนออก หน้า---หลัง
        {
            serialPort1.Write("j");
            // ส่งค่าควบคุม Hardware
        }
    } // end class 
}
