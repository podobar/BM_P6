using System;

using System.Windows.Forms;
using System.IO;
using MathNet.Numerics.Data.Matlab;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Drawing;

namespace BM_P6
{
    public partial class FaceRecognition : Form
    {
        private readonly int VERTICES = 4;
        private Matrix<double> _faceCoordinates;
        private Bitmap _bitmapSource;
        //Model holistyczny, metoda minimalnoodległościowa
        public FaceRecognition()
        {
            InitializeComponent();
            OpenMatFile();
        }
        private void OpenMatFile()
        {
            //TODO: Change to button reaction or.. as a resource in debug
            using (var reader = new StreamReader(@"C:\Users\Lenovo\Desktop\faces\0ImageData.mat"))
            {
                _faceCoordinates = MatlabReader.Read<double>(reader.BaseStream, "SubDir_Data");
                List<double> list = new List<double>();
                for (int i = 0; i < 8; ++i)
                    list.Add(_faceCoordinates[i, 0]);
            }
        }
        private void LoadImages_Click(object sender, EventArgs e)
        {

        }

        private void LoadImage_Click(object sender, EventArgs e)
        {
            
            using (OpenFileDialog dialog = new OpenFileDialog() {
                Title ="Load image with face to be recognized",
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp" })
            {
                dialog.Title = "Załaduj obraz";
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _bitmapSource?.Dispose();
                    SourcePBox.Image?.Dispose();
                    _bitmapSource = new Bitmap(dialog.FileName);
                    SourcePBox.Image = _bitmapSource;
                    DrawFaceRect(dialog.FileName);
                }
            }

        }

        private void DrawFaceRect(string fileName)
        {
            int index = -1;
            var tokens = fileName.Split('_');
            //Wyjaśnij w sprawozdaniu
            if (int.TryParse(tokens[tokens.Length - 1].Split('.')[0], out index))
            {
                --index;
                var points = new Point[VERTICES];
                for (int i = 0; i < VERTICES; ++i)
                    points[i] = new Point((int)_faceCoordinates[2*i,index], (int)_faceCoordinates[2*i+1,index]);
                CutFace(new Rectangle(
                    points[1].X,
                    points[1].Y,
                    (points[3].X + points[2].X) / 2 - (points[0].X + points[1].X) / 2,
                    (points[0].Y + points[3].Y) / 2 - (points[1].Y + points[2].Y) / 2
                    ));
            }
        }
        private void CutFace(Rectangle face)
        {
            Size s = new Size(450, 550);
            Bitmap tmp_face = _bitmapSource.Clone(face, _bitmapSource.PixelFormat);
            _bitmapSource = new Bitmap(tmp_face, s);
            SourcePBox.Image = _bitmapSource;
            ;
        }
    }
}
