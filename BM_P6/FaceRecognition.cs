using System;

using System.Windows.Forms;
using System.IO;
using MathNet.Numerics.Data.Matlab;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace BM_P6
{
    public partial class FaceRecognition : Form
    {
        private readonly int SET_SIZE = 100;
        private readonly int VERTICES = 4;
        private readonly Size UNIFIED_IMG_SIZE = new Size(400, 500);

        private Evd<float> _trainedEigenValues;
        List<float[]> _faces;
        private float[] _averageFace;
        private Matrix<double> _faceCoordinates;
        private Bitmap _bitmapSource;
        //Model holistyczny, metoda minimalnoodległościowa
        public FaceRecognition()
        {
            InitializeComponent();
            OpenMatFile();
            _averageFace = new float[UNIFIED_IMG_SIZE.Height*UNIFIED_IMG_SIZE.Width];
            _faces = new List<float[]>();
        }
        private void OpenMatFile()
        {
            //TODO: Change to button reaction or.. as a resource in debug
            using (var reader = new StreamReader(@"C:\Users\Lenovo\Desktop\ImageData.mat"))
            {
                _faceCoordinates = MatlabReader.Read<double>(reader.BaseStream, "SubDir_Data");
                List<double> list = new List<double>();
                for (int i = 0; i < 8; ++i)
                    list.Add(_faceCoordinates[i, 0]);
            }
            ;
        }
        private void Load_TrainingSet_From_Directory_Click(object sender, EventArgs e)
        {
            if(_trainedEigenValues == null)
                using (var fbd = new FolderBrowserDialog())
                {
                    if(fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        _faces.Clear();
                        int count = 0;
                        foreach(var file in Directory.GetFiles(fbd.SelectedPath))
                        {
                            _bitmapSource?.Dispose();
                            _bitmapSource = (Bitmap)Image.FromFile(file);
                            GetFace(fbd.SelectedPath);
                            Convert_Bitmap_To_IntArray();
                            if (++count == SET_SIZE)
                                break;
                                //CutFace
                                //Convert to m x n vector
                                //Substract average vector
                                //Add to average vector (each pixel/100 - training sets)
                        }
                        for (int i = 0; i < SET_SIZE; ++i)
                            for (int j = 0; j < UNIFIED_IMG_SIZE.Height * UNIFIED_IMG_SIZE.Width; ++j)
                                _averageFace[j] += _faces[i][j] / 100;
                        for (int i = 0; i < SET_SIZE; ++i)
                            for (int j = 0; j < UNIFIED_IMG_SIZE.Height * UNIFIED_IMG_SIZE.Width; ++j)
                                _faces[i][j] -= _averageFace[j];
                        Matrix<float> A = CreateMatrix.DenseOfColumnArrays(_faces);
                        var Cov = A.Transpose().Multiply(A);
                        _trainedEigenValues = Cov.Evd();

                    }
                }
        }
        private void Convert_Bitmap_To_IntArray()
        {
            float[] result = new float[UNIFIED_IMG_SIZE.Height * UNIFIED_IMG_SIZE.Width];
            for(int i=0;i<UNIFIED_IMG_SIZE.Width;++i)
            {
                for(int j = 0; j < UNIFIED_IMG_SIZE.Height; ++j)
                {
                    Color c = _bitmapSource.GetPixel(i, j);
                    result[j * UNIFIED_IMG_SIZE.Width + i] = (c.R + c.G + c.B) / 3.0f;
                }
            }
            _faces.Add(result);
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
                }
            }

        }

        private void GetFace(string fileName)
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
                
                Bitmap tmp_face = _bitmapSource.Clone(new Rectangle(
                    points[1].X,
                    points[1].Y,
                    (points[3].X + points[2].X) / 2 - (points[0].X + points[1].X) / 2,
                    (points[0].Y + points[3].Y) / 2 - (points[1].Y + points[2].Y) / 2
                    ), _bitmapSource.PixelFormat);
                _bitmapSource = new Bitmap(tmp_face, UNIFIED_IMG_SIZE);
                SourcePBox.Image = _bitmapSource;
            }
        }
        private void CutFace(Rectangle face)
        {
            
            ;
        }
    }
}
