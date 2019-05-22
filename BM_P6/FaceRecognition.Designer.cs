namespace BM_P6
{
    partial class FaceRecognition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SourcePBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SourcePBox
            // 
            this.SourcePBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourcePBox.Location = new System.Drawing.Point(0, 0);
            this.SourcePBox.Margin = new System.Windows.Forms.Padding(0);
            this.SourcePBox.MaximumSize = new System.Drawing.Size(450, 550);
            this.SourcePBox.MinimumSize = new System.Drawing.Size(450, 550);
            this.SourcePBox.Name = "SourcePBox";
            this.SourcePBox.Size = new System.Drawing.Size(450, 550);
            this.SourcePBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.SourcePBox.TabIndex = 0;
            this.SourcePBox.TabStop = false;
            this.SourcePBox.Click += new System.EventHandler(this.Load_TrainingSet_From_Directory_Click);
            // 
            // FaceRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 552);
            this.Controls.Add(this.SourcePBox);
            this.Name = "FaceRecognition";
            this.Text = "Face recognition";
            ((System.ComponentModel.ISupportInitialize)(this.SourcePBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox SourcePBox;
    }
}

