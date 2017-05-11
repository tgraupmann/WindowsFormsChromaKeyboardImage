namespace WindowsFormsChromaKeyboardImage
{
    partial class Form1
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
            this._mButtonQuit = new System.Windows.Forms.Button();
            this._mButtonLoadImage = new System.Windows.Forms.Button();
            this._mPicture = new System.Windows.Forms.PictureBox();
            this._mComboBoxLayout = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._mPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // _mButtonQuit
            // 
            this._mButtonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._mButtonQuit.Location = new System.Drawing.Point(586, 459);
            this._mButtonQuit.Name = "_mButtonQuit";
            this._mButtonQuit.Size = new System.Drawing.Size(75, 23);
            this._mButtonQuit.TabIndex = 0;
            this._mButtonQuit.Text = "Quit";
            this._mButtonQuit.UseVisualStyleBackColor = true;
            this._mButtonQuit.Click += new System.EventHandler(this._mButtonQuit_Click);
            // 
            // _mButtonLoadImage
            // 
            this._mButtonLoadImage.Location = new System.Drawing.Point(12, 12);
            this._mButtonLoadImage.Name = "_mButtonLoadImage";
            this._mButtonLoadImage.Size = new System.Drawing.Size(75, 23);
            this._mButtonLoadImage.TabIndex = 1;
            this._mButtonLoadImage.Text = "Load Image";
            this._mButtonLoadImage.UseVisualStyleBackColor = true;
            this._mButtonLoadImage.Click += new System.EventHandler(this._mButtonLoadImage_Click);
            // 
            // _mPicture
            // 
            this._mPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mPicture.Location = new System.Drawing.Point(12, 41);
            this._mPicture.Name = "_mPicture";
            this._mPicture.Size = new System.Drawing.Size(649, 412);
            this._mPicture.TabIndex = 2;
            this._mPicture.TabStop = false;
            // 
            // _mComboBoxLayout
            // 
            this._mComboBoxLayout.FormattingEnabled = true;
            this._mComboBoxLayout.Location = new System.Drawing.Point(434, 14);
            this._mComboBoxLayout.Name = "_mComboBoxLayout";
            this._mComboBoxLayout.Size = new System.Drawing.Size(227, 21);
            this._mComboBoxLayout.TabIndex = 3;
            this._mComboBoxLayout.SelectedIndexChanged += new System.EventHandler(this._mComboBoxLayout_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 494);
            this.Controls.Add(this._mComboBoxLayout);
            this.Controls.Add(this._mPicture);
            this.Controls.Add(this._mButtonLoadImage);
            this.Controls.Add(this._mButtonQuit);
            this.Name = "Form1";
            this.Text = "Chroma Image";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this._mPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _mButtonQuit;
        private System.Windows.Forms.Button _mButtonLoadImage;
        private System.Windows.Forms.PictureBox _mPicture;
        private System.Windows.Forms.ComboBox _mComboBoxLayout;
    }
}

