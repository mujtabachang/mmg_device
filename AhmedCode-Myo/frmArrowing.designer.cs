namespace Testing_ResizingForm
{
    partial class frmArrowing
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
            this.picMiddle = new System.Windows.Forms.PictureBox();
            this.picRight = new System.Windows.Forms.PictureBox();
            this.picLeft = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // picMiddle
            // 
            this.picMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMiddle.Image = global::AhmedCode_Myo.Properties.Resources.Button_Stop_icon;
            this.picMiddle.Location = new System.Drawing.Point(320, 0);
            this.picMiddle.Name = "picMiddle";
            this.picMiddle.Size = new System.Drawing.Size(266, 329);
            this.picMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMiddle.TabIndex = 4;
            this.picMiddle.TabStop = false;
            this.picMiddle.Visible = false;
            // 
            // picRight
            // 
            this.picRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.picRight.Image = global::AhmedCode_Myo.Properties.Resources.Actions_go_next_icon;
            this.picRight.Location = new System.Drawing.Point(586, 0);
            this.picRight.Name = "picRight";
            this.picRight.Size = new System.Drawing.Size(320, 329);
            this.picRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRight.TabIndex = 3;
            this.picRight.TabStop = false;
            this.picRight.Visible = false;
            // 
            // picLeft
            // 
            this.picLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.picLeft.Image = global::AhmedCode_Myo.Properties.Resources.Actions_go_previous_icon;
            this.picLeft.Location = new System.Drawing.Point(0, 0);
            this.picLeft.Name = "picLeft";
            this.picLeft.Size = new System.Drawing.Size(320, 329);
            this.picLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLeft.TabIndex = 0;
            this.picLeft.TabStop = false;
            this.picLeft.Visible = false;
            // 
            // frmArrowing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 329);
            this.Controls.Add(this.picMiddle);
            this.Controls.Add(this.picRight);
            this.Controls.Add(this.picLeft);
            this.Name = "frmArrowing";
            this.Text = "Arrow Display";
            ((System.ComponentModel.ISupportInitialize)(this.picMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picLeft;
        private System.Windows.Forms.PictureBox picRight;
        private System.Windows.Forms.PictureBox picMiddle;
    }
}

