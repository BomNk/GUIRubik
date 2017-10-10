using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;


namespace Rubik.V1
{
    public partial class Form2 : Form
    {
        Image<Hsv, Byte> My_Image;   // รูปต้นฉบับ
        Image<Hsv, Byte> gray_image;   // รูปที่โดนเปลี่ยนเป็นแบบ GRY
        Image<Hsv, Byte> My_image_copy; // รูปที่ใช้แก้ใข 
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (true)   // เมื่อเลือกเสร็จแล้ว กดปุ่ม OK
            {
                //Load the Image
                String str = "C:/Users/BOM/Desktop/New folder/6.jpg";
                My_Image = new Image<Hsv, Byte>(str);    // นำภาพที่เลือกใส่ในตัวแปร Image
                                                         //  Console.WriteLine(Openfile.FileName);
                                                         //Display the Image
                image_PCBX.Image = My_Image.ToBitmap();   // นำภาพแสดงออกทาง 

                My_image_copy = My_Image.Copy();


            }
        }

      

        public void set_label(int[,] rr)
        {

            label1.Text = "1: value = " + rr[0, 0] + "," + rr[0, 1] + "," + rr[0, 2];
            label2.Text = "2: value = " + rr[1, 0] + "," + rr[1, 1] + "," + rr[1, 2];
            label3.Text = "3: value = " + rr[2, 0] + "," + rr[2, 1] + "," + rr[2, 2];
            label4.Text = "4: value = " + rr[3, 0] + "," + rr[3, 1] + "," + rr[3, 2];
            label5.Text = "5: value = " + rr[4, 0] + "," + rr[4, 1] + "," + rr[4, 2];
            label6.Text = "6: value = " + rr[5, 0] + "," + rr[5, 1] + "," + rr[5, 2];
            label7.Text = "7: value = " + rr[6, 0] + "," + rr[6, 1] + "," + rr[6, 2];
            label8.Text = "8: value = " + rr[7, 0] + "," + rr[7, 1] + "," + rr[7, 2];
            label9.Text = "9: value = " + rr[8, 0] + "," + rr[8, 1] + "," + rr[8, 2];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[,] col = new int[9, 3];
            int i = 0, j = 0;
            int A = 0, B = 0;
            int roll, collum;
            float sumB = 0, sumG = 0, sumR = 0;
            //ดึงค่าใส่ในตัวแปร

            for (int y = 110; y < 360; y += 120)
            {

                for (int x = 220; x < 470; x += 120)
                {
                    collum = y - 5;
                    roll = x - 5;
                    sumB = 0;
                    sumG = 0;
                    sumR = 0;
                    for (A = 0; A < 10; A++)
                    {
                        for (B = 0; B < 10; B++)
                        {
                            sumB += (float)(0.01 * My_Image[collum, roll].Hue);
                            sumG += (float)(0.01 * My_Image[collum, roll].Satuation);
                            sumR += (float)(0.01 * My_Image[collum, roll].Value);
                        }
                    }
                    // label10.Text = "sumB = " + (int)((sumB / 100) * 100);
                    // label11.Text = "sumG = " + (int)((sumG / 100) * 100);
                    // label12.Text = "sumG = " + (int)((sumR / 100) * 100);



                    for (j = 0; j < 3; j++)
                    {

                        if (j == 0)
                            col[i, j] = (int)((sumB / 100) * 100);
                        else if (j == 1)
                            col[i, j] = (int)((sumG / 100) * 100);
                        else if (j == 2)
                            col[i, j] = (int)((sumR / 100) * 100);
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
            set_label(col);

        }

        private void image_PCBX_Click(object sender, EventArgs e)
        {

        }

        private void image_PCBX_MouseMove(object sender, MouseEventArgs e)
        {
            if (image_PCBX.Image != null)  //  ถ้ามีภาพเข้ามา 
            {
                X_pos_LBL.Text = "X: " + e.X.ToString();     // คำสั้งแสดงตำแหน่งแกน X
                Y_pos_LBL.Text = "Y: " + e.Y.ToString();     // คำสั้งแสดงตำแหน่งแกน Y

               
                    Val_LBL.Text = "Value: " + My_Image[e.Y, e.X].ToString();
             
                //It is much more stable with large images to access the image.Data propert directley than use code like bellow
                //Bitmap tmp_img = new Bitmap(image_PCBX.Image);
                //Val_LBL.Text = "Value: " + tmp_img.GetPixel(e.X, e.Y).ToString();
            }
        }
    }
}
