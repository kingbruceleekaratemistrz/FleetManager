namespace FleetManager
{
    partial class UserProfileMenu
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
            this.FirstnameLabel = new System.Windows.Forms.Label();
            this.LastnameLabel = new System.Windows.Forms.Label();
            this.CompanyLabel = new System.Windows.Forms.Label();
            this.ProfilePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FirstnameLabel
            // 
            this.FirstnameLabel.AutoSize = true;
            this.FirstnameLabel.Location = new System.Drawing.Point(618, 251);
            this.FirstnameLabel.Name = "FirstnameLabel";
            this.FirstnameLabel.Size = new System.Drawing.Size(35, 13);
            this.FirstnameLabel.TabIndex = 0;
            this.FirstnameLabel.Text = "label1";
            // 
            // LastnameLabel
            // 
            this.LastnameLabel.AutoSize = true;
            this.LastnameLabel.Location = new System.Drawing.Point(618, 273);
            this.LastnameLabel.Name = "LastnameLabel";
            this.LastnameLabel.Size = new System.Drawing.Size(35, 13);
            this.LastnameLabel.TabIndex = 1;
            this.LastnameLabel.Text = "label2";
            // 
            // CompanyLabel
            // 
            this.CompanyLabel.AutoSize = true;
            this.CompanyLabel.Location = new System.Drawing.Point(618, 297);
            this.CompanyLabel.Name = "CompanyLabel";
            this.CompanyLabel.Size = new System.Drawing.Size(35, 13);
            this.CompanyLabel.TabIndex = 2;
            this.CompanyLabel.Text = "label3";
            // 
            // ProfilePictureBox
            // 
            this.ProfilePictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProfilePictureBox.Location = new System.Drawing.Point(576, 120);
            this.ProfilePictureBox.Name = "ProfilePictureBox";
            this.ProfilePictureBox.Size = new System.Drawing.Size(128, 128);
            this.ProfilePictureBox.TabIndex = 3;
            this.ProfilePictureBox.TabStop = false;
            // 
            // UserProfileMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.ProfilePictureBox);
            this.Controls.Add(this.CompanyLabel);
            this.Controls.Add(this.LastnameLabel);
            this.Controls.Add(this.FirstnameLabel);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "UserProfileMenu";
            this.Text = "UserProfile";
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FirstnameLabel;
        private System.Windows.Forms.Label LastnameLabel;
        private System.Windows.Forms.Label CompanyLabel;
        private System.Windows.Forms.PictureBox ProfilePictureBox;
    }
}