﻿using System;
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
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;

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
        public char[][] Rubik_tmp = new char[6][];
        //public char[][] Rubik_real = new char[6][];
       
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
       
        Image<Hsv, Byte> R_Image;
        Image<Hsv, Byte> My_Image;
        String str = "";
       
        int Time,Delay=0;
        Stopwatch stopwatch = new Stopwatch();
        //time counter = new time();
        int NumTimer = 0;   // Dela
        int numStep = 0;
        int numTake = 1;
        int numPicture = 1;
        int numList = 1;


        int UD = 0;
        



        String Result ="", Face ="",FF="" ;
        //private SerialPort SerialPort1;

        // Test Commit
        // รูปต้นฉบับ
        // Image<Hsv, Byte> gray_image;   // รูปที่โดนเปลี่ยนเป็นแบบ GRY
        Image<Hsv, Byte> My_image_copy; // รูปที่ใช้แก้ใข 

        public Form1()
        {
            
            InitializeComponent();
            getAvailableComponent();
            //Text_Time.Text = min + " : " + sec + " : " + ssec;
            numStep = 0;

           

        }
        void getAvailableComponent()
        {
            String[] Ports = SerialPort.GetPortNames();
            comboBox2_connect.Items.AddRange(Ports);
        }


        private void Form1_Load(object sender, EventArgs e)
        {


            //opwatch_timer.Start();


            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in webcam)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            //comboBox1.SelectedIndex = 0;

        }

        private void convert_Face_Rubik()
        {
            Rubik_tmp[0] = Rubik[2] ;
            Rubik_tmp[1] =  Rubik[3];
            Rubik_tmp[2] =  Rubik[4];
            Rubik_tmp[3] =  Rubik[5];
            Rubik_tmp[4] =  Rubik[1];
            Rubik_tmp[5] =  Rubik[0];
        }  // Finish



        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)   // Finish
        {

            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            My_Image = new Image<Hsv, byte>(bit);
            if (numTake == 1)
            {
                Box3.Image = bit;
                R_Image = My_Image.Copy();
               // My_Image1 = new Image<Hsv, byte>(bit);
            }
            
            else if (numTake == 2)
            {
                R_Image = My_Image.Copy();
                Box4.Image = bit;
               // My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 3)
            {
                R_Image = My_Image.Copy();
                Box5.Image = bit;
              //  My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 4)
            {
                R_Image = My_Image.Copy();
                Box6.Image = bit;
                //My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 5)
            {
                R_Image = My_Image.Copy();
                Box2.Image = bit;
              //  My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 6)
            {
                R_Image = My_Image.Copy();
                Box1.Image = bit;
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
            //read_color(numTake-1);
            if (numTake == 1)
            {

                //Box1.Image = R_Image.ToBitmap();
               
                read_color(3 - 1); //U
            }
            if (numTake == 2)
            {

                //Box5.Image = R_Image.ToBitmap();v

                
               
                read_color(4 - 1); //R
            }
            if (numTake == 3)
            {

                //Box2.Image = R_Image.ToBitmap();
                read_color(5 - 1); //F
              
            }
            if (numTake == 4)
            {
               
                                   //Box4.Image = R_Image.ToBitmap();
                read_color(6 - 1); //D
            }
            if (numTake == 5)
            {
                read_color(2 - 1); //L
                                   //Box3.Image = R_Image.ToBitmap();

            }
            if (numTake == 6)
            {
                read_color(1 - 1); //B
                                   //Box6.Image = R_Image.ToBitmap();

            }


            numTake++;
           
            timer1.Enabled = true;
            //  String str = "";
            
            timer1.Start();

            label2.Text = "Cam_" + numTake + " Running";

            show_color1(Rubik);


            
            Text_face.Text = showRubik(Rubik);
            Face = showRubik(Rubik);
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
            {
                label2.Text = " Complete";
               
                // timer2.Stop();

            }

            cam.Start();
        }


        private void Stop_Click(object sender, EventArgs e) // Finish
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }

        }













     


        public void read_color(int Face) // อ่านสี ภาพ 1 ภาพ // Finish
        {
            int[,] col = new int[9, 3];
            int i = 0, j = 0;
            int A = 0, B = 0;
            int roll, collum;
            float sumHue = 0, sumSatuation = 0, sumValue = 0;
            //ดึงค่าใส่ในตัวแปร

            for (int y = 125; y < 450; y += 135)    // แกน YYYYYYYYYYY
            {

                for (int x = 156; x < 450; x += 135)   // แกน XXXXXXXXXXXXX
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
                      

                    }


                    i++;
                }
            }
      
            double H, g, r;
            for (i = 0; i < 9; i++)
            {
                H = col[i, 0];
                g = col[i, 1];
                r = col[i, 2];
                if (g < 20)    // white
                {
                    Rubik[Face][i] = 'W';
                }
                else if ((H >= 0 && H < 11) || (H > 160 && H <= 180)) // RED
                {
                    Rubik[Face][i] = 'R';
                }

                else if (H > 25 && H < 50) //yellow
                {
                    Rubik[Face][i] = 'Y';
                }
                else if (H > 10 && H < 26)   // ม่วง
                {
                    Rubik[Face][i] = 'P';
                }
                else if (H >= 50 && H < 99)   //Green
                {
                    Rubik[Face][i] = 'G';
                }

                else if (H >= 99 && H <= 160)   //blue
                {
                    Rubik[Face][i] = 'B';
                }
                else
                    Rubik[Face][i] = 'W';

            }






        }            //



       

      


        ///////////////////////////////// คำสั่งที่ใช้หมุนลูกรูบิค/////////////////////////////////////


        public void rotate_left(int arrow) //Left // Finish
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 1, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[0][0]; tmp[1] = Rubik[0][3]; ; tmp[2] = Rubik[0][6];
                Rubik[0][0] = Rubik[2][0]; Rubik[0][3] = Rubik[2][3]; Rubik[0][6] = Rubik[2][6];
                Rubik[2][0] = Rubik[4][0]; Rubik[2][3] = Rubik[4][3]; Rubik[2][6] = Rubik[4][6];
                Rubik[4][0] = Rubik[5][0]; Rubik[4][3] = Rubik[5][3]; Rubik[4][6] = Rubik[5][6];
                Rubik[5][0] = tmp[0]; Rubik[5][3] = tmp[1]; Rubik[5][6] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 1, 1);

                char[] tmp = new char[3];

                tmp[0] = Rubik[5][0]; tmp[1] = Rubik[5][3]; tmp[2] = Rubik[5][6];

                 Rubik[5][0]= Rubik[4][0] ;  Rubik[5][3] = Rubik[4][3];  Rubik[5][6] = Rubik[4][6];
              
                 Rubik[4][0] = Rubik[2][0] ;  Rubik[4][3] = Rubik[2][3]; Rubik[4][6] = Rubik[2][6];
                 Rubik[2][0] = Rubik[0][0] ; Rubik[2][3] = Rubik[0][3];  Rubik[2][6] = Rubik[0][6] ;

                Rubik[0][0] = tmp[0]; Rubik[0][3] = tmp[1]; Rubik[0][6] = tmp[2];
               

            }

        }

        public void rotate_right(int arrow) // Right // Finish
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 3, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[0][2]; tmp[1] = Rubik[0][5]; ; tmp[2] = Rubik[0][8];
                Rubik[0][2] = Rubik[5][2]; Rubik[0][5] = Rubik[5][5]; Rubik[0][8] = Rubik[5][8];
                Rubik[5][2] = Rubik[4][2]; Rubik[5][5] = Rubik[4][5]; Rubik[5][8] = Rubik[4][8];
                Rubik[4][2] = Rubik[2][2]; Rubik[4][5] = Rubik[2][5]; Rubik[4][8] = Rubik[2][8];
                Rubik[2][2] = tmp[0]; Rubik[2][5] = tmp[1]; Rubik[2][8] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 3, 1);

                char[] tmp = new char[3];
                tmp[0] = Rubik[2][2]; tmp[1] = Rubik[2][5]; tmp[2] = Rubik[2][8];

                Rubik[2][2] = Rubik[4][2]; Rubik[2][5] = Rubik[4][5]; Rubik[2][8] = Rubik[4][8];
                 Rubik[4][2] = Rubik[5][2]; Rubik[4][5] = Rubik[5][5];  Rubik[4][8] = Rubik[5][8];
                Rubik[5][2] = Rubik[0][2]; Rubik[5][5] = Rubik[0][5]; Rubik[5][8] = Rubik[0][8];

                Rubik[0][2] = tmp[0]; Rubik[0][5] = tmp[1]; ; Rubik[0][8] = tmp[2];
                

            }
        }

        public void rotate_top(int arrow) // Top or UP // //
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 2, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[0][6]; tmp[1] = Rubik[0][7]; ; tmp[2] = Rubik[0][8];
                Rubik[0][6] = Rubik[3][0]; Rubik[0][7] = Rubik[3][3]; Rubik[0][8] = Rubik[3][6];
                Rubik[3][0] = Rubik[4][2]; Rubik[3][3] = Rubik[4][1]; Rubik[3][6] = Rubik[4][0];
                Rubik[4][2] = Rubik[1][8]; Rubik[4][1] = Rubik[1][5]; Rubik[4][0] = Rubik[1][2];
                Rubik[1][8] = tmp[0]; Rubik[1][5] = tmp[1]; Rubik[1][2] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 2, 1);

                char[] tmp = new char[3];
                tmp[0] = Rubik[1][8]; tmp[1] = Rubik[1][5]; tmp[2] = Rubik[1][2];
               
                Rubik[1][8] = Rubik[4][2]; Rubik[1][5] = Rubik[4][1]; Rubik[1][2] = Rubik[4][0];
               
                 Rubik[4][2] = Rubik[3][0];  Rubik[4][1] = Rubik[3][3];  Rubik[4][0] = Rubik[3][6];
                Rubik[3][0] = Rubik[0][6]; Rubik[3][3] = Rubik[0][7]; Rubik[3][6] = Rubik[0][8];

                Rubik[0][6] = tmp[0]; Rubik[0][7] = tmp[1]; ; Rubik[0][8] = tmp[2];

            }

        }

        public void rotate_front(int arrow) // Front // Finish
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 4, 0);
                char[] tmp = new char[3];
                tmp[0] = Rubik[2][6]; tmp[1] = Rubik[2][7]; ; tmp[2] = Rubik[2][8];

                Rubik[2][6] = Rubik[3][6]; Rubik[2][7] = Rubik[3][7]; Rubik[2][8] = Rubik[3][8];
                Rubik[3][6] = Rubik[5][2]; Rubik[3][7] = Rubik[5][1]; Rubik[3][8] = Rubik[5][0];
                Rubik[5][2] = Rubik[1][6]; Rubik[5][1] = Rubik[1][7]; Rubik[5][0] = Rubik[1][8];

                Rubik[1][6] = tmp[0]; Rubik[1][7] = tmp[1]; Rubik[1][8] = tmp[2];
            }
            else if( arrow ==1)

            {
                rotate_Face(Rubik, 4, 1);
                char[] tmp = new char[3];
                tmp[0] = Rubik[1][6]; tmp[1] = Rubik[1][7]; tmp[2] = Rubik[1][8];
                Rubik[1][6] = Rubik[5][2]; Rubik[1][7] = Rubik[5][1]; Rubik[1][8] = Rubik[5][0];
                Rubik[5][2] = Rubik[3][6]; Rubik[5][1] = Rubik[3][7]; Rubik[5][0] = Rubik[3][8];
                Rubik[3][6] = Rubik[2][6]; Rubik[3][7] = Rubik[2][7]; Rubik[3][8] = Rubik[2][8];
                Rubik[2][6] = tmp[0] ; Rubik[2][7] = tmp[1] ; ; Rubik[2][8] = tmp[2] ;
               
                
                 
                
                
            }


        }

        public void rotate_rear(int arrow) // Back // Finish
        {
            if (arrow == 0){
            rotate_Face(Rubik, 0, 0);

            char[] tmp = new char[3];

            tmp[0] = Rubik[5][6]; tmp[1] = Rubik[5][7]; ; tmp[2] = Rubik[5][8];

            Rubik[5][6] = Rubik[3][2]; Rubik[5][7] = Rubik[3][1]; Rubik[5][8] = Rubik[3][0];
            Rubik[3][2] = Rubik[2][2]; Rubik[3][1] = Rubik[2][1]; Rubik[3][0] = Rubik[2][0];
            Rubik[2][2] = Rubik[1][2]; Rubik[2][1] = Rubik[1][1]; Rubik[2][0] = Rubik[1][0];

            Rubik[1][2] = tmp[0]; Rubik[1][1] = tmp[1]; Rubik[1][0] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 0, 1);

                char[] tmp = new char[3];
                tmp[0] = Rubik[1][2]; tmp[1] = Rubik[1][1]; tmp[2] = Rubik[1][0];
                Rubik[1][2] = Rubik[2][2];  Rubik[1][1]= Rubik[2][1] ; Rubik[1][0] = Rubik[2][0] ;
                Rubik[2][2] = Rubik[3][2];  Rubik[2][1] = Rubik[3][1] ;  Rubik[2][0] = Rubik[3][0] ;
                Rubik[3][2] = Rubik[5][6] ;  Rubik[3][1] = Rubik[5][7] ;  Rubik[3][0] = Rubik[5][8] ;
               
              
                Rubik[5][6] = tmp[0]; Rubik[5][7] = tmp[1]; ; Rubik[5][8] = tmp[2];
            }

        }

        public void rotate_down(int arrow)
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 5, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[4][6]; tmp[1] = Rubik[4][7]; ; tmp[2] = Rubik[4][8];
                Rubik[4][6] = Rubik[3][8]; Rubik[4][7] = Rubik[3][5]; Rubik[4][8] = Rubik[3][2];
                Rubik[3][2] = Rubik[0][0]; Rubik[3][5] = Rubik[0][1]; Rubik[3][8] = Rubik[0][2];
                Rubik[0][0] = Rubik[1][6]; Rubik[0][1] = Rubik[1][3]; Rubik[0][2] = Rubik[1][0];
                Rubik[1][0] = tmp[0]; Rubik[1][3] = tmp[1]; Rubik[1][6] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 5,1);

                char[] tmp = new char[3];

                tmp[0] = Rubik[1][0]; tmp[1] = Rubik[1][3]; tmp[2] = Rubik[1][6];

                Rubik[1][6] = Rubik[0][0];  Rubik[1][3] = Rubik[0][1]; Rubik[1][0] = Rubik[0][2];
                Rubik[0][0] = Rubik[3][2]; Rubik[0][1] = Rubik[3][5];  Rubik[0][2] = Rubik[3][8];
                Rubik[3][8] = Rubik[4][6];  Rubik[3][5] = Rubik[4][7];  Rubik[3][2] = Rubik[4][8];

                Rubik[4][6] = tmp[0]; Rubik[4][7] = tmp[1]; Rubik[4][8] = tmp[2];
               
            }

        }

        


        public void rotate_Face(char[][] Rc,int F,int arrow) //ไว้หมุนหน้าทุกด้าน // Finish
        {
            if (arrow == 0)  // หมุนทวน
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
            else if(arrow == 1)  // หมุนตามเข็ม
            {
                char M = Rc[F][3];

                Rc[F][3] = Rc[F][7];
                Rc[F][7] = Rc[F][5];
                Rc[F][5] = Rc[F][1];
                Rc[F][1] = M;

                char C = Rc[F][6];

                Rc[F][6] = Rc[F][8];
                Rc[F][8] = Rc[F][2];
                Rc[F][2] = Rc[F][0];
                Rc[F][0] = C;
            }

           
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



        
        //  Original  Show Rubik

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
                                A0.BackColor = Color.Orange;
                            else if (j == 1)
                                A1.BackColor = Color.Orange;
                            else if (j == 2)
                                A2.BackColor = Color.Orange;
                            else if (j == 3)
                                A3.BackColor = Color.Orange;
                            else if (j == 4)
                                A4.BackColor = Color.Orange;
                            else if (j == 5)
                                A5.BackColor = Color.Orange;
                            else if (j == 6)
                                A6.BackColor = Color.Orange;
                            else if (j == 7)
                                A7.BackColor = Color.Orange;
                            else if (j == 8)
                                A8.BackColor = Color.Orange;
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
                                B0.BackColor = Color.Orange;
                            else if (j == 1)
                                B1.BackColor = Color.Orange;
                            else if (j == 2)
                                B2.BackColor = Color.Orange;
                            else if (j == 3)
                                B3.BackColor = Color.Orange;
                            else if (j == 4)
                                B4.BackColor = Color.Orange;
                            else if (j == 5)
                                B5.BackColor = Color.Orange;
                            else if (j == 6)
                                B6.BackColor = Color.Orange;
                            else if (j == 7)
                                B7.BackColor = Color.Orange;
                            else if (j == 8)
                                B8.BackColor = Color.Orange;
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
                                C0.BackColor = Color.Orange;
                            else if (j == 1)
                                C1.BackColor = Color.Orange;
                            else if (j == 2)
                                C2.BackColor = Color.Orange;
                            else if (j == 3)
                                C3.BackColor = Color.Orange;
                            else if (j == 4)
                                C4.BackColor = Color.Orange;
                            else if (j == 5)
                                C5.BackColor = Color.Orange;
                            else if (j == 6)
                                C6.BackColor = Color.Orange;
                            else if (j == 7)
                                C7.BackColor = Color.Orange;
                            else if (j == 8)
                                C8.BackColor = Color.Orange;
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
                                D0.BackColor = Color.Orange;
                            else if (j == 1)
                                D1.BackColor = Color.Orange;
                            else if (j == 2)
                                D2.BackColor = Color.Orange;
                            else if (j == 3)
                                D3.BackColor = Color.Orange;
                            else if (j == 4)
                                D4.BackColor = Color.Orange;
                            else if (j == 5)
                                D5.BackColor = Color.Orange;
                            else if (j == 6)
                                D6.BackColor = Color.Orange;
                            else if (j == 7)
                                D7.BackColor = Color.Orange;
                            else if (j == 8)
                                D8.BackColor = Color.Orange;
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
                                E0.BackColor = Color.Orange;
                            else if (j == 1)
                                E1.BackColor = Color.Orange;
                            else if (j == 2)
                                E2.BackColor = Color.Orange;
                            else if (j == 3)
                                E3.BackColor = Color.Orange;
                            else if (j == 4)
                                E4.BackColor = Color.Orange;
                            else if (j == 5)
                                E5.BackColor = Color.Orange;
                            else if (j == 6)
                                E6.BackColor = Color.Orange;
                            else if (j == 7)
                                E7.BackColor = Color.Orange;
                            else if (j == 8)
                                E8.BackColor = Color.Orange;
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
                                F0.BackColor = Color.Orange;
                            else if (j == 1)
                                F1.BackColor = Color.Orange;
                            else if (j == 2)
                                F2.BackColor = Color.Orange;
                            else if (j == 3)
                                F3.BackColor = Color.Orange;
                            else if (j == 4)
                                F4.BackColor = Color.Orange;
                            else if (j == 5)
                                F5.BackColor = Color.Orange;
                            else if (j == 6)
                                F6.BackColor = Color.Orange;
                            else if (j == 7)
                                F7.BackColor = Color.Orange;
                            else if (j == 8)
                                F8.BackColor = Color.Orange;
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



        // END Original

             // Show_Rubik() Original


        //  End Edit

        public void show_color1(char[][] rk) // แสดงค่าสีบน panel // Finish
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (i == 0) // Zone AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 8)
                                A0.BackColor = Color.Orange;
                            else if (j == 7)
                                A1.BackColor = Color.Orange;
                            else if (j == 6)
                                A2.BackColor = Color.Orange;
                            else if (j == 5)
                                A3.BackColor = Color.Orange;
                            else if (j == 4)
                                A4.BackColor = Color.Orange;
                            else if (j == 3)
                                A5.BackColor = Color.Orange;
                            else if (j == 2)
                                A6.BackColor = Color.Orange;
                            else if (j == 1)
                                A7.BackColor = Color.Orange;
                            else if (j == 0)
                                A8.BackColor = Color.Orange;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 8)
                                A0.BackColor = Color.Blue;
                            else if (j == 7)
                                A1.BackColor = Color.Blue;
                            else if (j == 6)
                                A2.BackColor = Color.Blue;
                            else if (j == 5)
                                A3.BackColor = Color.Blue;
                            else if (j == 4)
                                A4.BackColor = Color.Blue;
                            else if (j == 3)
                                A5.BackColor = Color.Blue;
                            else if (j == 2)
                                A6.BackColor = Color.Blue;
                            else if (j == 1)
                                A7.BackColor = Color.Blue;
                            else if (j == 0)
                                A8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 8)
                                A0.BackColor = Color.Yellow;
                            else if (j == 7)
                                A1.BackColor = Color.Yellow;
                            else if (j == 6)
                                A2.BackColor = Color.Yellow;
                            else if (j == 5)
                                A3.BackColor = Color.Yellow;
                            else if (j == 4)
                                A4.BackColor = Color.Yellow;
                            else if (j == 3)
                                A5.BackColor = Color.Yellow;
                            else if (j == 2)
                                A6.BackColor = Color.Yellow;
                            else if (j == 1)
                                A7.BackColor = Color.Yellow;
                            else if (j == 0)
                                A8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 8)
                                A0.BackColor = Color.Green;
                            else if (j == 7)
                                A1.BackColor = Color.Green;
                            else if (j == 6)
                                A2.BackColor = Color.Green;
                            else if (j == 5)
                                A3.BackColor = Color.Green;
                            else if (j == 4)
                                A4.BackColor = Color.Green;
                            else if (j == 3)
                                A5.BackColor = Color.Green;
                            else if (j == 2)
                                A6.BackColor = Color.Green;
                            else if (j == 1)
                                A7.BackColor = Color.Green;
                            else if (j == 0)
                                A8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 8)
                                A0.BackColor = Color.Red;
                            else if (j == 7)
                                A1.BackColor = Color.Red;
                            else if (j == 6)
                                A2.BackColor = Color.Red;
                            else if (j == 5)
                                A3.BackColor = Color.Red;
                            else if (j == 4)
                                A4.BackColor = Color.Red;
                            else if (j == 3)
                                A5.BackColor = Color.Red;
                            else if (j == 2)
                                A6.BackColor = Color.Red;
                            else if (j == 1)
                                A7.BackColor = Color.Red;
                            else if (j == 0)
                                A8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 8)
                                A0.BackColor = Color.White;
                            else if (j == 7)
                                A1.BackColor = Color.White;
                            else if (j == 6)
                                A2.BackColor = Color.White;
                            else if (j == 5)
                                A3.BackColor = Color.White;
                            else if (j == 4)
                                A4.BackColor = Color.White;
                            else if (j == 3)
                                A5.BackColor = Color.White;
                            else if (j == 2)
                                A6.BackColor = Color.White;
                            else if (j == 1)
                                A7.BackColor = Color.White;
                            else if (j == 0)
                                A8.BackColor = Color.White;

                        }
                    }


                    if (i == 1) // Zone BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 6)
                                B0.BackColor = Color.Orange;
                            else if (j == 3)
                                B1.BackColor = Color.Orange;
                            else if (j == 0)
                                B2.BackColor = Color.Orange;
                            else if (j == 7)
                                B3.BackColor = Color.Orange;
                            else if (j == 4)
                                B4.BackColor = Color.Orange;
                            else if (j == 1)
                                B5.BackColor = Color.Orange;
                            else if (j == 8)
                                B6.BackColor = Color.Orange;
                            else if (j == 5)
                                B7.BackColor = Color.Orange;
                            else if (j == 2)
                                B8.BackColor = Color.Orange;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 6)
                                B0.BackColor = Color.Blue;
                            else if (j == 3)
                                B1.BackColor = Color.Blue;
                            else if (j == 0)
                                B2.BackColor = Color.Blue;
                            else if (j == 7)
                                B3.BackColor = Color.Blue;
                            else if (j == 4)
                                B4.BackColor = Color.Blue;
                            else if (j == 1)
                                B5.BackColor = Color.Blue;
                            else if (j == 8)
                                B6.BackColor = Color.Blue;
                            else if (j == 5)
                                B7.BackColor = Color.Blue;
                            else if (j == 2)
                                B8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 6)
                                B0.BackColor = Color.Yellow;
                            else if (j == 3)
                                B1.BackColor = Color.Yellow;
                            else if (j == 0)
                                B2.BackColor = Color.Yellow;
                            else if (j == 7)
                                B3.BackColor = Color.Yellow;
                            else if (j == 4)
                                B4.BackColor = Color.Yellow;
                            else if (j == 1)
                                B5.BackColor = Color.Yellow;
                            else if (j == 8)
                                B6.BackColor = Color.Yellow;
                            else if (j == 5)
                                B7.BackColor = Color.Yellow;
                            else if (j == 2)
                                B8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 6)
                                B0.BackColor = Color.Green;
                            else if (j == 3)
                                B1.BackColor = Color.Green;
                            else if (j == 0)
                                B2.BackColor = Color.Green;
                            else if (j == 7)
                                B3.BackColor = Color.Green;
                            else if (j == 4)
                                B4.BackColor = Color.Green;
                            else if (j == 1)
                                B5.BackColor = Color.Green;
                            else if (j == 8)
                                B6.BackColor = Color.Green;
                            else if (j == 5)
                                B7.BackColor = Color.Green;
                            else if (j == 2)
                                B8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 6)
                                B0.BackColor = Color.Red;
                            else if (j == 3)
                                B1.BackColor = Color.Red;
                            else if (j == 0)
                                B2.BackColor = Color.Red;
                            else if (j == 7)
                                B3.BackColor = Color.Red;
                            else if (j == 4)
                                B4.BackColor = Color.Red;
                            else if (j == 1)
                                B5.BackColor = Color.Red;
                            else if (j == 8)
                                B6.BackColor = Color.Red;
                            else if (j == 5)
                                B7.BackColor = Color.Red;
                            else if (j == 2)
                                B8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 6)
                                B0.BackColor = Color.White;
                            else if (j == 3)
                                B1.BackColor = Color.White;
                            else if (j == 0)
                                B2.BackColor = Color.White;
                            else if (j == 7)
                                B3.BackColor = Color.White;
                            else if (j == 4)
                                B4.BackColor = Color.White;
                            else if (j == 1)
                                B5.BackColor = Color.White;
                            else if (j == 8)
                                B6.BackColor = Color.White;
                            else if (j == 5)
                                B7.BackColor = Color.White;
                            else if (j == 2)
                                B8.BackColor = Color.White;

                        }
                    }


                    if (i == 2) // Zone CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                C0.BackColor = Color.Orange;
                            else if (j == 1)
                                C1.BackColor = Color.Orange;
                            else if (j == 2)
                                C2.BackColor = Color.Orange;
                            else if (j == 3)
                                C3.BackColor = Color.Orange;
                            else if (j == 4)
                                C4.BackColor = Color.Orange;
                            else if (j == 5)
                                C5.BackColor = Color.Orange;
                            else if (j == 6)
                                C6.BackColor = Color.Orange;
                            else if (j == 7)
                                C7.BackColor = Color.Orange;
                            else if (j == 8)
                                C8.BackColor = Color.Orange;
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
                            if (j == 2)
                                D0.BackColor = Color.Orange;
                            else if (j == 5)
                                D1.BackColor = Color.Orange;
                            else if (j == 8)
                                D2.BackColor = Color.Orange;
                            else if (j == 1)
                                D3.BackColor = Color.Orange;
                            else if (j == 4)
                                D4.BackColor = Color.Orange;
                            else if (j == 7)
                                D5.BackColor = Color.Orange;
                            else if (j == 0)
                                D6.BackColor = Color.Orange;
                            else if (j == 3)
                                D7.BackColor = Color.Orange;
                            else if (j == 6)
                                D8.BackColor = Color.Orange;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 2)
                                D0.BackColor = Color.Blue;
                            else if (j == 5)
                                D1.BackColor = Color.Blue;
                            else if (j == 8)
                                D2.BackColor = Color.Blue;
                            else if (j == 1)
                                D3.BackColor = Color.Blue;
                            else if (j == 4)
                                D4.BackColor = Color.Blue;
                            else if (j == 7)
                                D5.BackColor = Color.Blue;
                            else if (j == 0)
                                D6.BackColor = Color.Blue;
                            else if (j == 3)
                                D7.BackColor = Color.Blue;
                            else if (j == 6)
                                D8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 2)
                                D0.BackColor = Color.Yellow;
                            else if (j == 5)
                                D1.BackColor = Color.Yellow;
                            else if (j == 8)
                                D2.BackColor = Color.Yellow;
                            else if (j == 1)
                                D3.BackColor = Color.Yellow;
                            else if (j == 4)
                                D4.BackColor = Color.Yellow;
                            else if (j == 7)
                                D5.BackColor = Color.Yellow;
                            else if (j == 0)
                                D6.BackColor = Color.Yellow;
                            else if (j == 3)
                                D7.BackColor = Color.Yellow;
                            else if (j == 6)
                                D8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 2)
                                D0.BackColor = Color.Green;
                            else if (j == 5)
                                D1.BackColor = Color.Green;
                            else if (j == 8)
                                D2.BackColor = Color.Green;
                            else if (j == 1)
                                D3.BackColor = Color.Green;
                            else if (j == 4)
                                D4.BackColor = Color.Green;
                            else if (j == 7)
                                D5.BackColor = Color.Green;
                            else if (j == 0)
                                D6.BackColor = Color.Green;
                            else if (j == 3)
                                D7.BackColor = Color.Green;
                            else if (j == 6)
                                D8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 2)
                                D0.BackColor = Color.Red;
                            else if (j == 5)
                                D1.BackColor = Color.Red;
                            else if (j == 8)
                                D2.BackColor = Color.Red;
                            else if (j == 1)
                                D3.BackColor = Color.Red;
                            else if (j == 4)
                                D4.BackColor = Color.Red;
                            else if (j == 7)
                                D5.BackColor = Color.Red;
                            else if (j == 0)
                                D6.BackColor = Color.Red;
                            else if (j == 3)
                                D7.BackColor = Color.Red;
                            else if (j == 6)
                                D8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 2)
                                D0.BackColor = Color.White;
                            else if (j == 5)
                                D1.BackColor = Color.White;
                            else if (j == 8)
                                D2.BackColor = Color.White;
                            else if (j == 1)
                                D3.BackColor = Color.White;
                            else if (j == 4)
                                D4.BackColor = Color.White;
                            else if (j == 7)
                                D5.BackColor = Color.White;
                            else if (j == 0)
                                D6.BackColor = Color.White;
                            else if (j == 3)
                                D7.BackColor = Color.White;
                            else if (j == 6)
                                D8.BackColor = Color.White;

                        }
                    }

                    if (i == 4) // Zone E
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                E0.BackColor = Color.Orange;
                            else if (j == 1)
                                E1.BackColor = Color.Orange;
                            else if (j == 2)
                                E2.BackColor = Color.Orange;
                            else if (j == 3)
                                E3.BackColor = Color.Orange;
                            else if (j == 4)
                                E4.BackColor = Color.Orange;
                            else if (j == 5)
                                E5.BackColor = Color.Orange;
                            else if (j == 6)
                                E6.BackColor = Color.Orange;
                            else if (j == 7)
                                E7.BackColor = Color.Orange;
                            else if (j == 8)
                                E8.BackColor = Color.Orange;
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
                                F0.BackColor = Color.Orange;
                            else if (j == 1)
                                F1.BackColor = Color.Orange;
                            else if (j == 2)
                                F2.BackColor = Color.Orange;
                            else if (j == 3)
                                F3.BackColor = Color.Orange;
                            else if (j == 4)
                                F4.BackColor = Color.Orange;
                            else if (j == 5)
                                F5.BackColor = Color.Orange;
                            else if (j == 6)
                                F6.BackColor = Color.Orange;
                            else if (j == 7)
                                F7.BackColor = Color.Orange;
                            else if (j == 8)
                                F8.BackColor = Color.Orange;
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

        // END Edit






        //ส่วนควบคุมการหมุน










        /*
                private void button2_Click(object sender, EventArgs e) // Test  TakePicture
                {

                    if (numPicture == 1)
                        str = "C:/Users/BOM/Desktop/New folder/2.jpg"; // back
                    else if (numPicture == 2)
                        str = "C:/Users/BOM/Desktop/New folder/4.jpg"; // Front
                    else if (numPicture == 3)
                        str = "C:/Users/BOM/Desktop/New folder/1.jpg"; // left
                    else if(numPicture == 4)
                        str = "C:/Users/BOM/Desktop/New folder/5.jpg"; // Right
                    else if(numPicture == 5)
                        str = "C:/Users/BOM/Desktop/New folder/6.jpg"; // up
                    else if(numPicture == 6)
                        str = "C:/Users/BOM/Desktop/New folder/3.jpg"; // Dow


                    R_Image = new Image<Hsv, Byte>(str);

                    if (numPicture == 1)
                    {

                        Box1.Image = R_Image.ToBitmap();
                        read_color(1 - 1);
                    }
                    if (numPicture == 2)
                    {

                        Box5.Image = R_Image.ToBitmap();
                        read_color(5 - 1);
                    }
                    if (numPicture == 3)
                    {

                        Box2.Image = R_Image.ToBitmap();
                        read_color(2 - 1);
                    }
                    if (numPicture == 4)
                    {

                        Box4.Image = R_Image.ToBitmap();
                        read_color(4 - 1);
                    }
                    if (numPicture == 5)
                    {

                        Box3.Image = R_Image.ToBitmap();
                        read_color(3 - 1);
                    }
                    if (numPicture == 6)
                    {

                        Box6.Image = R_Image.ToBitmap();
                        read_color(6 - 1);
                    }



                   // read_color()

                    show_color(Rubik);
                    numPicture++;

                    Text_face.Text = showRubik(Rubik);




                }
                */
        public string showRubik(char[][] Rubik)    // Show data in Rubik
        {
            String str = "";
            for (int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    //if(Rubik[i][j] == 'B')
                    str = str + Convert_RGB_to_Face(Rubik[i][j]);
                }
            }
            return str;
        }


        public string Convert_RGB_to_Face(char Rubik)
        {
            String str = "";
            if (Rubik == 'B')
            {
                str = "B";
            }
            else if (Rubik == 'P')
            {
                str = "L";
            }
            else if (Rubik == 'W')
            {
                str = "U";
            }
            else if (Rubik == 'R')
            {
                str = "R";
            }
            else if (Rubik == 'G')
            {
                str = "F";
            }
            else if (Rubik == 'Y')
            {
                str = "D";
            }
            return str;
        }






      



        ///////////////////////// ทิศในการหมุน    ปุ่มควบคุม  //////////////////////////////////////////////////
    



        private void Left_Click(object sender, EventArgs e)    //  L
        {
            //rotate_left(1);
            //show_color(Rubik);
            //listBox1.Items.Add(numList + ":L");
            //numList++;
            // serialPort1.Write("L");
            serialPort1.Write("1");
            L();
            serialPort1.Write("0");
        }

        private void Right_Click_1(object sender, EventArgs e)   //  R
        {
            //rotate_right(1);
            //show_color(Rubik);
            //listBox1.Items.Add(numList + ": R");
            //numList++;
            //  serialPort1.Write("R");
            serialPort1.Write("1");
            R();
            serialPort1.Write("0");
        }



        private void Front_Click(object sender, EventArgs e) // F
        {
            //rotate_front(1);
            //show_color(Rubik);
            //listBox1.Items.Add(numList + ": F");
            //numList++;
            /// Step of Work ///
            serialPort1.Write("1");
            F();
            serialPort1.Write("0");

            ///
            //serialPort1.Write("F");// Tanapon Ninket
        }

        private void Rear_Click(object sender, EventArgs e)  //  B
        {
            //rotate_rear(1);
            //show_color(Rubik);
            //listBox1.Items.Add(numList + ":B");
            //numList++;
            serialPort1.Write("1");
            // serialPort1.Write("B");

           
            B();
            serialPort1.Write("0");
        }

        private void U_Click(object sender, EventArgs e)  //  Top
        {
            //rotate_top(1);
            //show_color(Rubik);
            //listBox1.Items.Add(numList + ": U'");
            //numList++;
            // serialPort1.Write("b");
            serialPort1.Write("1");
            UU();
            
            serialPort1.Write("0");

        }

        private void D_Click(object sender, EventArgs e)   // Down
        {
            //rotate_down(1);
           // show_color(Rubik);
            //listBox1.Items.Add(numList + ":D");
            //numList++;
            // serialPort1.Write("b");
            serialPort1.Write("1");
            DD();
          
            serialPort1.Write("0");
        }

        private void Bdet_Click(object sender, EventArgs e) //B'
        {
            //rotate_rear(0);
            //show_color(Rubik);
           // listBox1.Items.Add(numList + ": B'");
            //numList++;
            // serialPort1.Write("b");
            serialPort1.Write("1");
            B_det();
           
            serialPort1.Write("0");
        }

        private void Ldet_Click(object sender, EventArgs e)  // L'
        {
            //rotate_left(0);
           // show_color(Rubik);
            //listBox1.Items.Add(numList + ": L'");
            //textBox1.Text = "L";
           // numList++;
            // serialPort1.Write("l");
            serialPort1.Write("1");
            L_det();
           
            serialPort1.Write("0");
        }

        private void Fdet_Click(object sender, EventArgs e)  // F'
        {
           // rotate_front(0);
            //show_color(Rubik);
            //listBox1.Items.Add(numList + ": F'");
            //Text_Result.Text = "FFFFFFFFFFFFFFFFFFFFFFFF";  //
            //numList++;
            serialPort1.Write("1");
            F_det();
            
            serialPort1.Write("0");
            //serialPort1.Write("f");// Tanapon Ninket
        }

        private void Rdet_Click(object sender, EventArgs e) // R'
        {
            //rotate_right(0);
           // show_color(Rubik);
            //listBox1.Items.Add(numList + ": R'");  
            //numList++;
            serialPort1.Write("1");
            R_det();
            serialPort1.Write("0");
            //serialPort1.Write("r");
        }

        private void Udet_Click(object sender, EventArgs e) // U' 
        {
            //rotate_top(0);
           // show_color(Rubik);
           // listBox1.Items.Add(numList + ": U'");
            //numList++;
            serialPort1.Write("1");
            U_det();
           
            serialPort1.Write("0");
            //serialPort1.Write("r");
        }

        private void Ddet_Click(object sender, EventArgs e)
        {
           //rotate_down(0);
           // show_color(Rubik);
           // listBox1.Items.Add(numList + ":D'");
            //numList++;
            serialPort1.Write("1");
            D_det();
           
            serialPort1.Write("0");
            //serialPort1.Write("r");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
        }



        private void open_left_right_Click(object sender, EventArgs e) // เลื่อนออก ซ้าย---ขวา  <<LR>>
        {
            listBox1.Items.Add(numList + ": " +"Left-Right  Open");

            serialPort1.Write("0j");
        }

        private void close_left_right_Click(object sender, EventArgs e) // เลื่อนเข้า ซ้าย---ขวา  >>LR<<
        {
            listBox1.Items.Add(numList + ": " + "Left-Right  Close");
            serialPort1.Write("0J");
        }

        private void open_front_back_Click(object sender, EventArgs e)  // เลื่อนเข้า หน้า---หลัง   <<FB>>
        {
            listBox1.Items.Add(numList + ": " + "Front-Back  Open");
            serialPort1.Write("0K");
        }

        private void close_front_back_Click(object sender, EventArgs e) // เลื่อนออก หน้า---หลัง   >>FB<<
        {
            listBox1.Items.Add(numList + ": " + "Front-Back  Close");
            serialPort1.Write("0k");
        }

        private void Start_Auto_Click(object sender, EventArgs e)
        {

           

            //Robot_Start();

            //timer_read_result.Enabled = true;
            //timer_read_result.Start();
            //time_init();


            //esult = Text_Result.Text;
            //Result_to_function(Result);

            //if (NumTimer == 6)
            //{


            // }



            //STOP_AUTO_Click(sender, e);

            //Start_Auto_Click(sender, e);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

        }

     

       


        private void timer2_Tick(object sender, EventArgs e)   //////// Timer for TakePicture  ////////////
        {
            serialPort1.Write("0");

            if (NumTimer == 0)
            {
                serialPort1.Write("Kdkj");
                Thread.Sleep(4000);
                Take_Click_1(sender, e);
                Thread.Sleep(1000);

            }
            else if(NumTimer == 1)
            {
                serialPort1.Write("JKd");
                Thread.Sleep(2000);
                serialPort1.Write("akjad");
                Thread.Sleep(7000);
                Take_Click_1(sender, e);
                //serialPort1.Write("JKdk");
                //Thread.Sleep(3700);
            }
            else if(NumTimer == 2)
            {
                serialPort1.Write("DJKdkj");
                Thread.Sleep(5000);
                Take_Click_1(sender, e);
            }
            else if(NumTimer == 3)
            {
                serialPort1.Write("JKda");
                Thread.Sleep(4500);
                Take_Click_1(sender, e);
                //serialPort1.Write("DJKA");
                //Thread.Sleep(2000);
            }
            else if(NumTimer == 4)
            {
                //serialPort1.Write("aa");
                 serialPort1.Write("AkjD");
                Thread.Sleep(5000);
                //Thread.Sleep(3700);
                Take_Click_1(sender, e);

            }
            else if(NumTimer == 5)
            {
                serialPort1.Write("JKdkjD");
                Thread.Sleep(5000);
                Take_Click_1(sender, e);
                Thread.Sleep(1000);
                serialPort1.Write("DDJKdAkjAJ");
                Thread.Sleep(4000);

            }
           
           
            
            if (NumTimer == 6)
            {
               
                Text_face.Text = Face;
                Write_Face();
                //
                
                //Timer Rubik Start
                Thread.Sleep(1000);
                serialPort1.Write("1");

                
                rotate_Face(Rubik, 0, 1);
                rotate_Face(Rubik, 0, 1);

                rotate_Face(Rubik, 1, 1);
                rotate_Face(Rubik, 3, 0);

                show_color(Rubik);
                // ene change zone
                timer_read_result.Enabled = true;
                timer_read_result.Start();


               
                
                timer2.Stop();
                // Thread.Sleep(3000);
               

            }
            NumTimer++;
            //Delay++;
        }
        
        public void Robot_Start()
        {
            
            Result = Text_Result.Text;
            Result_to_function(Result);
        }

        private void timer_read_result_Tick(object sender, EventArgs e)
        {

            Read_Result();
            ///////////////// for wite Read File /////////////////
            if(Result.Length != null)
            {
                //Thread.Sleep(1000);
               // Robot_Start();

                timer_read_result.Stop();
               
            }



        }



        // ////////////////////  Function Control  Rubik Cube Robot   Begin EiEi  ///////////////////////////////


        void F()
        {
            serialPort1.Write("F");
            /*
            serialPort1.Write("F");
            serialPort1.Write("K");
            serialPort1.Write("f");
            serialPort1.Write("k");
            */
        }
        void F_det()
        {
            serialPort1.Write("f");
            /*
            serialPort1.Write("f");
            serialPort1.Write("K");
            serialPort1.Write("F");
            serialPort1.Write("k");
            */
        }
        void F_2()
        {
            serialPort1.Write("I");
        }
     
        

        void B()
        {
            serialPort1.Write("B");
            /*
            serialPort1.Write("B");
            serialPort1.Write("K");
            serialPort1.Write("b");
            serialPort1.Write("k");
            */
        }
        void B_det()
        {
            serialPort1.Write("b");
            /*
            serialPort1.Write("b");
            serialPort1.Write("K");
            serialPort1.Write("B");
            serialPort1.Write("k");
            */
        }
        void B_2()
        {
            serialPort1.Write("K");
            //Thread.Sleep(100);
        }

        void L()
        {
            serialPort1.Write("L");
            /*
            serialPort1.Write("L");
            serialPort1.Write("j");
            serialPort1.Write("l");
            serialPort1.Write("J");
            */
        }  //เสร็จ
        void L_det()
        {
            serialPort1.Write("l");
            /*
            serialPort1.Write("l");
            serialPort1.Write("j");
            serialPort1.Write("L");
            serialPort1.Write("J");
            */
        }   /// <summary>
        /// </summary>
        void L_2()
        {
            serialPort1.Write("J");
        }
      


        void R()
        {
            serialPort1.Write("R");
            /*
            serialPort1.Write("R");
            serialPort1.Write("j");
            serialPort1.Write("r");
            serialPort1.Write("J");
            */
        }  //เสร็จ
        void R_det()
        {
            serialPort1.Write("r");
            /*
            serialPort1.Write("r");
            serialPort1.Write("j");
            serialPort1.Write("R");
            serialPort1.Write("J");
            */
        }  //เสร็จ
        void R_2()
        {
            serialPort1.Write("M");
        }   //เสร็จ
      

    
        void UU()//เสร็จ   // U
        {
            serialPort1.Write("U");
            /*
             serialPort1.Write("j");
             serialPort1.Write("D");
             serialPort1.Write("J");
             serialPort1.Write("KdkRjrdJKDk");
             Thread.Sleep(UD);*/

        }
        void U_det() // U'  //เสร็จ
        {
            serialPort1.Write("u");
            /*
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkrjRdJKDk");
            Thread.Sleep(UD);
            */
        }
        void U_2() // เสร็จ
        {
            serialPort1.Write("O");
           /* serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkRRjdJKDk");
            Thread.Sleep(UD);*/
        }       
        void DD()  //เสร็จ
        {
            serialPort1.Write("D");
            /*
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkLjldJKDk");
            Thread.Sleep(UD);*/
        }
        void D_det()  // เสร็จ
        {
            serialPort1.Write("d");
            /*
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkljLdJKDk");
            Thread.Sleep(UD);
            */
        }
        void D_2()   // เสร็จ
        {
            serialPort1.Write("P");
            /*
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkLLjdJKDk");
            Thread.Sleep(UD);*/
        }
       



        private void Box1_Click(object sender, EventArgs e)
        {

        }

        private void Stopwatch_timer_Tick(object sender, EventArgs e) ////// Stopwatch _Timer////
        {

            Text_Time.Text = stopwatch.Elapsed.ToString().Substring(3, 8);
        }

       


       

     




        // ////////////////////  Function Control  Rubik Cube Robot   Finish   ///////////////////////////////



        //////////////////////   Generate  Rusult //////////////////////
 
        public void Result_to_function(string Result)
        {
            //Face = Face.Substring(3);  // เราต้องใส่ Index เพื่อระบุส่วนที่จะเอา(ตัดส่วนอื่นทิ้ง) (X,Y)   x คือ ตำแหน่ง   Y คือ ความยาวที่เราจะเอา
            //Fcee = Face.Trim(new char[]{ '' , '*' ,'.' })  // ใน new char คือส่วนที่เราจะตัดว่ามีตัวอักษรใหนบ้าง
            //first_string.Remove(first_string.IndexOf(" test"), " test".Length);
            StringBuilder sb = new StringBuilder(Result);
            sb.Remove(Result.IndexOf("."), 3);
            Result = sb.ToString();


            //Console.WriteLine(" Result = {0}", Result);
            //Console.WriteLine(" {0}", Result.Length);
            System.Diagnostics.Debug.WriteLine("Result length = " + (Result.Length -5) );
            System.Diagnostics.Debug.WriteLine("Result = " + Result);
            int x = 1;
            
            for (int i = 0; i < Result.Length-5 ; i += 3)
            {
                serialPort1.Write("1");

               

                if (Result.Substring(i, 2).Equals("L "))
                {
                    //listBox1.Items.Add(x + ":L");
                    //rotate_left(1);
                    //show_color(Rubik);
                    L();
                    
                    //x++;
                }
                else if (Result.Substring(i, 2).Equals("L'"))
                {
                    //listBox1.Items.Add(x + ":L'");
                    //rotate_left(0);
                    //show_color(Rubik);
                    L_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("L2"))
                {
                    //listBox1.Items.Add(x + ":L2");
                    //rotate_left(1);
                    //rotate_left(1);
                    //show_color(Rubik);
                    L_2();
                    x++;
                }


                 else if (Result.Substring(i, 2).Equals("B "))
                {
                    //listBox1.Items.Add(x + ":B ");
                    //rotate_rear(1);
                    //show_color(Rubik);
                    B();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("B'"))
                {
                    // listBox1.Items.Add(x + ":B'");
                   // rotate_rear(0);
                   // show_color(Rubik);
                    B_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("B2"))
                {
                    //listBox1.Items.Add(x + ":B2");
                    //rotate_rear(1);
                    //rotate_rear(1);
                    //show_color(Rubik);
                    B_2();
                    x++;
                }

                 else if (Result.Substring(i, 2).Equals("R "))
                {
                    //listBox1.Items.Add(x + ":R ");
                   // rotate_right(1);
                   // show_color(Rubik);
                    R();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("R'"))
                {
                    // listBox1.Items.Add(x + ":R'");
                    //rotate_right(0);
                   // show_color(Rubik);
                    R_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("R2"))
                {
                    // listBox1.Items.Add(x + ":R2");
                   // rotate_right(1);
                   // rotate_right(1);
                    //show_color(Rubik);
                    R_2();
                    x++;
                }

                 else if (Result.Substring(i, 2).Equals("U "))
                {
                    // listBox1.Items.Add(x + ":U ");
                    ///rotate_top(1);
                    //show_color(Rubik);
                    UU();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("U'"))
                {
                    //listBox1.Items.Add(x + ":U'");
                   // rotate_top(0);
                   // show_color(Rubik);
                    U_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("U2"))
                {
                    // listBox1.Items.Add(x + ":U2");
                    //rotate_top(0);
                    //rotate_top(0);
                    //show_color(Rubik);
                    U_2();
                    x++;
                }

                 else if (Result.Substring(i, 2).Equals("D "))
                {
                    // listBox1.Items.Add(x + ":D ");
                    //rotate_down(1);
                   // show_color(Rubik);
                    DD();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("D'"))
                {
                    //listBox1.Items.Add(x + ":D'");
                    //rotate_down(0);
                    //show_color(Rubik);
                    D_det();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("D2"))
                {
                    // listBox1.Items.Add(x + ":D2");
                    //rotate_down(1);
                    //rotate_down(1);
                    //show_color(Rubik);
                    D_2();       
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("F "))
                {

                    //listBox1.Items.Add(x + ":F ");
                    //rotate_front(1);
                    //show_color(Rubik);
                    F();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("F'"))
                {
                    //listBox1.Items.Add(x + ":F'");
                    //rotate_front(0);
                    //show_color(Rubik);
                    F_det();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("F2"))
                {
                    //listBox1.Items.Add(x + ":F2");
                   // rotate_front(1);
                   // rotate_front(1);
                    //show_color(Rubik);
                    F_2();
                    x++;
                }
                numStep++;
                serialPort1.Write("0");
            }

            listBox1.Items.Add("Result is " + numStep + " Step");


           // System.Diagnostics.Debug.WriteLine("x = " + x);



        }

        ////////////////////// Generate Result Finish ////////////////

       
        ////////////////////////////////Read / Write File Face and Result  /////////////////////////////////////
        
        public void Read_Result()
        {
            using (StreamReader readtext = new StreamReader(@"D:\\Result.txt"))
            {
                string readMeText = readtext.ReadLine();
                Text_Result.Text = readMeText;
                //Console.WriteLine(Result);
            }
        }




        public void Write_Face()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\\Face.txt"))
            {
                //Face= "LRUUURFULDFLURDFLBRLBLFRBRRDBUFDBBDLFBDLLFUURFDDBBDUFR";
                //Form1 test = new Form1();
                convert_Face_Rubik();
                FF = showRubik(Rubik_tmp);


                Text_face.Text = FF;
                //Text_face.Text = Face;


                file.Write(FF);
                //ReadFile Naja//
                
            }

            
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        ////////////////////////////////  END  File IO  ////////////////////////////////////////////////////////


        //=======
        ///////////////////   สิ้นสุดการทำงานของปุ่ม

        ////////Button Control ////////////
        private void button3_Click(object sender, EventArgs e)   ////// HW  Start /////////
        {
            try
            {
                Stopwatch_timer.Enabled = true;
                stopwatch.Start();
                Stopwatch_timer.Start();
                Robot_Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Check Step Result", "Error");
            }
        }

        private void DisConnect_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            connect.Enabled = true;

            Rear.Enabled = false;
            Left.Enabled = false;
            Front.Enabled = false;
            Right.Enabled = false;
            U.Enabled = false;
            D.Enabled = false;

            Bdet.Enabled = false;
            Ldet.Enabled = false;
            Fdet.Enabled = false;
            Rdet.Enabled = false;
            Udet.Enabled = false;
            Ddet.Enabled = false;

            open_front_back.Enabled = false;
            open_left_right.Enabled = false;
            close_front_back.Enabled = false;
            close_left_right.Enabled = false;

            button3.Enabled = false;
            button4.Enabled = false;
            STOP_AUTO.Enabled = false;

        }

        private void button4_Click_1(object sender, EventArgs e) // Start image
        {
            timer2.Enabled = true;
            timer2.Start();
        }
        private void STOP_AUTO_Click(object sender, EventArgs e)   /////  Button  STOP  for Auto CLick //////////////
        {
            timer2.Enabled = false;
            timer2.Stop();
            Stopwatch_timer.Enabled = false;
            Stopwatch_timer.Stop();
        }
        private void Test_Click(object sender, EventArgs e) // Reset Click125 //Finish
        {
            char[][] RK = new char[6][] {  new char[] {'B','B','B','B','B','B','B','B','B' },  // น้ำเงิน
                                                new char[] {'P','P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},  // ส้ม
                                                new char[] { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'  }, //ขาว
                                                new char[] { 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R' },  // แดง
                                                new char[] {'G', 'G' , 'G' , 'G', 'G' , 'G' , 'G' , 'G','G' },  // เขียว
                                                new char[] { 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y' } };  // เหลือง

            listBox1.Items.Clear();
            //numList = 1;
            //Box1.Image = Image.FromFile("../load.jpg");
            NumTimer = 0;   // Dela
            numStep = 0;
            numTake = 1;
            numPicture = 1;
            numList = 1;

            Rubik = RK;
            show_color(Rubik);
           
            







            
            Text_Time.Text = "00:00:00";

          

        }
        private void button4_Click(object sender, EventArgs e)  /////// CONNECT  ////////////////
        {
            try
            {
                serialPort1.PortName = comboBox2_connect.Text;
                serialPort1.Open();
                serialPort1.DataReceived += SerialPort1_DataReceived;
                connect.Enabled = false;

                Rear.Enabled = true;
                Left.Enabled = true;
                Front.Enabled = true;
                Right.Enabled = true;
                U.Enabled = true;
                D.Enabled = true;

                Bdet.Enabled = true;
                Ldet.Enabled = true;
                Fdet.Enabled = true;
                Rdet.Enabled = true;
                Udet.Enabled = true;
                Ddet.Enabled = true;

                open_front_back.Enabled = true;
                open_left_right.Enabled = true;
                close_front_back.Enabled = true;
                close_left_right.Enabled = true;

                button3.Enabled = true;
                button4.Enabled = true;
                STOP_AUTO.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                string str = serialPort1.ReadLine();


                // data_serialport.Enabled = true;
                //data_serialport.Start();
                this.BeginInvoke(new LineReceivedEvent(LineReceived), str);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error");
            }
        }
        private delegate void LineReceivedEvent(string line);
        private void LineReceived(string str)
        {
            try
            {


                if (str.Substring(0, 2).Equals("F'"))
                    rotate_front(0);
                else if (str.Substring(0, 2).Equals("F2"))
                {
                    rotate_front(1);
                    rotate_front(1);
                }
                else if (str.Substring(0, 1).Equals("F"))
                    rotate_front(1);

                else if (str.Substring(0, 2).Equals("L'"))
                    rotate_left(0);
                else if (str.Substring(0, 2).Equals("L2"))
                {
                    rotate_left(1);
                    rotate_left(1);
                }
                else if (str.Substring(0, 1).Equals("L"))
                    rotate_left(1);

                else if (str.Substring(0, 2).Equals("B'"))
                    rotate_rear(0);
                else if (str.Substring(0, 2).Equals("B2"))
                {
                    rotate_rear(1);
                    rotate_rear(1);
                }
                else if (str.Substring(0, 1).Equals("B"))
                    rotate_rear(1);

                else if (str.Substring(0, 2).Equals("R'"))
                    rotate_right(0);
                else if (str.Substring(0, 2).Equals("R2"))
                {
                    rotate_right(1);
                    rotate_right(1);
                }
                else if (str.Substring(0, 1).Equals("R"))
                    rotate_right(1);

                else if (str.Substring(0, 2).Equals("U'"))
                    rotate_top(0);
                else if (str.Substring(0, 2).Equals("U2"))
                {
                    rotate_top(1);
                    rotate_top(1);
                }
                else if (str.Substring(0, 1).Equals("U"))
                    rotate_top(1);

                else if (str.Substring(0, 2).Equals("D'"))
                    rotate_down(0);
                else if (str.Substring(0, 2).Equals("D2"))
                {
                    rotate_down(1);
                    rotate_down(1);
                }
                else if (str.Substring(0, 1).Equals("D"))
                    rotate_down(1);

                show_color(Rubik);
                //tanapon  ninket


                listBox1.Items.Add(numList + ":" + str);

                if(numList == numStep)
                {
                    listBox1.Items.Add("Time STOP ");
                    timer2.Enabled = false;
                    timer2.Stop();
                    Stopwatch_timer.Enabled = false;
                    Stopwatch_timer.Stop();
                }

                numList++;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error");
            }

            //circularProgressBar1.Update();


        }
    } // end class 
}
