using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseCSharp;

namespace ImageDeal
{
    public partial class ImageDevide : Form
    {
        public ImageDevide()
        {
            InitializeComponent();
            string databaseFileName = System.Windows.Forms.Application.StartupPath + "\\..\\..\\SqliteFile\\imageInfo.db";
            m_database = new SqlliteHelp(databaseFileName);
        }

        private SqlliteHelp m_database = null;
        private Bitmap bitmap = null;
        private void txb_file_name_Clicked(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "jpg|*.png";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                txb_file_name.Text = fileName;
                pictureBox1.ImageLocation = fileName;
                bitmap = (Bitmap)Bitmap.FromFile(fileName, false);
                string sqlTemp = @"
                insert into t_image(
                [f_imgid] ,[f_x]  ,[f_y],[f_alp],[f_r],[f_g],[f_b],[f_wid],[f_hei],[f_num],[f_descript] ) values
                ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');
                ";
                StringBuilder sqlSb = new StringBuilder();
                int index = 1;
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        Color pixelColor = bitmap.GetPixel(i,j);
                        //像素点颜色的 Alpha 值
                        byte alpha = pixelColor.A;
                        //颜色的 RED 分量值
                        byte red = pixelColor.R;
                        //颜色的 GREEN 分量值
                        byte green = pixelColor.G;
                        //颜色的 BLUE 分量值
                        byte blue = pixelColor.B;

                        sqlSb.Append(string.Format(sqlTemp,Guid.NewGuid().ToString()
                            ,i,j,alpha,red,green,blue,bitmap.Width,bitmap.Height,index,DateTime.Now.ToString()));
                        index++; 
                    }
                }
                
                int result = m_database.ExecuteSql(sqlSb.ToString());
                if (result != -1)
                {
                    MessageBox.Show("数据插入成功！");
                    return;
                }


            }
        }


    }
}
