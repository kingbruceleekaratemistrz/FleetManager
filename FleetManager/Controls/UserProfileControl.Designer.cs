
namespace FleetManager.Controls
{
    partial class UserProfileControl
    {
        /// <summary> 
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.CarLabel = new System.Windows.Forms.Label();
            this.MailLabel = new System.Windows.Forms.Label();
            this.PhoneLabel = new System.Windows.Forms.Label();
            this.PositionLabel = new System.Windows.Forms.Label();
            this.ProfilePictureBox = new System.Windows.Forms.PictureBox();
            this.CompanyLabel = new System.Windows.Forms.Label();
            this.LastnameLabel = new System.Windows.Forms.Label();
            this.FirstnameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CarLabel
            // 
            this.CarLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CarLabel.AutoSize = true;
            this.CarLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CarLabel.Location = new System.Drawing.Point(98, 440);
            this.CarLabel.Name = "CarLabel";
            this.CarLabel.Size = new System.Drawing.Size(36, 24);
            this.CarLabel.TabIndex = 19;
            this.CarLabel.Text = "car";
            this.CarLabel.Click += new System.EventHandler(this.CarLabel_Click);
            // 
            // MailLabel
            // 
            this.MailLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MailLabel.AutoSize = true;
            this.MailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MailLabel.Location = new System.Drawing.Point(98, 410);
            this.MailLabel.Name = "MailLabel";
            this.MailLabel.Size = new System.Drawing.Size(44, 24);
            this.MailLabel.TabIndex = 18;
            this.MailLabel.Text = "mail";
            // 
            // PhoneLabel
            // 
            this.PhoneLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PhoneLabel.AutoSize = true;
            this.PhoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PhoneLabel.Location = new System.Drawing.Point(98, 380);
            this.PhoneLabel.Name = "PhoneLabel";
            this.PhoneLabel.Size = new System.Drawing.Size(65, 24);
            this.PhoneLabel.TabIndex = 17;
            this.PhoneLabel.Text = "phone";
            // 
            // PositionLabel
            // 
            this.PositionLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PositionLabel.AutoSize = true;
            this.PositionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PositionLabel.Location = new System.Drawing.Point(98, 350);
            this.PositionLabel.Name = "PositionLabel";
            this.PositionLabel.Size = new System.Drawing.Size(75, 24);
            this.PositionLabel.TabIndex = 16;
            this.PositionLabel.Text = "position";
            // 
            // ProfilePictureBox
            // 
            this.ProfilePictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProfilePictureBox.Location = new System.Drawing.Point(100, 100);
            this.ProfilePictureBox.Name = "ProfilePictureBox";
            this.ProfilePictureBox.Size = new System.Drawing.Size(128, 128);
            this.ProfilePictureBox.TabIndex = 15;
            this.ProfilePictureBox.TabStop = false;
            // 
            // CompanyLabel
            // 
            this.CompanyLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CompanyLabel.AutoSize = true;
            this.CompanyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CompanyLabel.Location = new System.Drawing.Point(98, 320);
            this.CompanyLabel.Name = "CompanyLabel";
            this.CompanyLabel.Size = new System.Drawing.Size(88, 24);
            this.CompanyLabel.TabIndex = 14;
            this.CompanyLabel.Text = "company";
            // 
            // LastnameLabel
            // 
            this.LastnameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LastnameLabel.AutoSize = true;
            this.LastnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LastnameLabel.Location = new System.Drawing.Point(99, 290);
            this.LastnameLabel.Name = "LastnameLabel";
            this.LastnameLabel.Size = new System.Drawing.Size(85, 24);
            this.LastnameLabel.TabIndex = 13;
            this.LastnameLabel.Text = "lastname";
            // 
            // FirstnameLabel
            // 
            this.FirstnameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.FirstnameLabel.AutoSize = true;
            this.FirstnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FirstnameLabel.Location = new System.Drawing.Point(100, 260);
            this.FirstnameLabel.Name = "FirstnameLabel";
            this.FirstnameLabel.Size = new System.Drawing.Size(85, 24);
            this.FirstnameLabel.TabIndex = 12;
            this.FirstnameLabel.Text = "firstname";
            // 
            // UserProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.CarLabel);
            this.Controls.Add(this.MailLabel);
            this.Controls.Add(this.PhoneLabel);
            this.Controls.Add(this.PositionLabel);
            this.Controls.Add(this.ProfilePictureBox);
            this.Controls.Add(this.CompanyLabel);
            this.Controls.Add(this.LastnameLabel);
            this.Controls.Add(this.FirstnameLabel);
            this.MaximumSize = new System.Drawing.Size(920, 680);
            this.MinimumSize = new System.Drawing.Size(920, 680);
            this.Name = "UserProfileControl";
            this.Size = new System.Drawing.Size(920, 680);
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CarLabel;
        private System.Windows.Forms.Label MailLabel;
        private System.Windows.Forms.Label PhoneLabel;
        private System.Windows.Forms.Label PositionLabel;
        private System.Windows.Forms.PictureBox ProfilePictureBox;
        private System.Windows.Forms.Label CompanyLabel;
        private System.Windows.Forms.Label LastnameLabel;
        private System.Windows.Forms.Label FirstnameLabel;
    }
}
