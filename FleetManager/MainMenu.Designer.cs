
namespace FleetManager
{
    partial class MainMenu
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.SidePanel = new System.Windows.Forms.Panel();
            this.LinkTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ProfilePictureBox = new System.Windows.Forms.PictureBox();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.CompanyNameLabel = new System.Windows.Forms.Label();
            this.ShowUserProfile = new System.Windows.Forms.Label();
            this.ShowCompanyProfile = new System.Windows.Forms.Label();
            this.ShowCarProfile = new System.Windows.Forms.Label();
            this.ShowCoworkersList = new System.Windows.Forms.Label();
            this.ShowCarList = new System.Windows.Forms.Label();
            this.SidePanel.SuspendLayout();
            this.LinkTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(340, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(920, 680);
            this.MainPanel.TabIndex = 0;
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.SidePanel.Controls.Add(this.CompanyNameLabel);
            this.SidePanel.Controls.Add(this.LastNameLabel);
            this.SidePanel.Controls.Add(this.FirstNameLabel);
            this.SidePanel.Controls.Add(this.ProfilePictureBox);
            this.SidePanel.Controls.Add(this.LinkTableLayout);
            this.SidePanel.Location = new System.Drawing.Point(0, 0);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(340, 680);
            this.SidePanel.TabIndex = 0;
            // 
            // LinkTableLayout
            // 
            this.LinkTableLayout.ColumnCount = 1;
            this.LinkTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LinkTableLayout.Controls.Add(this.ShowCarList, 0, 4);
            this.LinkTableLayout.Controls.Add(this.ShowCoworkersList, 0, 3);
            this.LinkTableLayout.Controls.Add(this.ShowCarProfile, 0, 2);
            this.LinkTableLayout.Controls.Add(this.ShowCompanyProfile, 0, 1);
            this.LinkTableLayout.Controls.Add(this.ShowUserProfile, 0, 0);
            this.LinkTableLayout.Location = new System.Drawing.Point(70, 229);
            this.LinkTableLayout.Name = "LinkTableLayout";
            this.LinkTableLayout.RowCount = 5;
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LinkTableLayout.Size = new System.Drawing.Size(200, 150);
            this.LinkTableLayout.TabIndex = 1;
            // 
            // ProfilePictureBox
            // 
            this.ProfilePictureBox.Location = new System.Drawing.Point(70, 73);
            this.ProfilePictureBox.Name = "ProfilePictureBox";
            this.ProfilePictureBox.Size = new System.Drawing.Size(64, 64);
            this.ProfilePictureBox.TabIndex = 2;
            this.ProfilePictureBox.TabStop = false;
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.Location = new System.Drawing.Point(70, 144);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(26, 13);
            this.FirstNameLabel.TabIndex = 3;
            this.FirstNameLabel.Text = "Imię";
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Location = new System.Drawing.Point(70, 161);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(53, 13);
            this.LastNameLabel.TabIndex = 4;
            this.LastNameLabel.Text = "Nazwisko";
            // 
            // CompanyNameLabel
            // 
            this.CompanyNameLabel.AutoSize = true;
            this.CompanyNameLabel.Location = new System.Drawing.Point(70, 178);
            this.CompanyNameLabel.Name = "CompanyNameLabel";
            this.CompanyNameLabel.Size = new System.Drawing.Size(64, 13);
            this.CompanyNameLabel.TabIndex = 5;
            this.CompanyNameLabel.Text = "Nazwa firmy";
            // 
            // ShowUserProfile
            // 
            this.ShowUserProfile.AutoSize = true;
            this.ShowUserProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowUserProfile.Location = new System.Drawing.Point(3, 0);
            this.ShowUserProfile.Name = "ShowUserProfile";
            this.ShowUserProfile.Size = new System.Drawing.Size(72, 20);
            this.ShowUserProfile.TabIndex = 0;
            this.ShowUserProfile.Text = "Mój profil";
            this.ShowUserProfile.Click += new System.EventHandler(this.ShowUserProfile_Click);
            // 
            // ShowCompanyProfile
            // 
            this.ShowCompanyProfile.AutoSize = true;
            this.ShowCompanyProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCompanyProfile.Location = new System.Drawing.Point(3, 30);
            this.ShowCompanyProfile.Name = "ShowCompanyProfile";
            this.ShowCompanyProfile.Size = new System.Drawing.Size(81, 20);
            this.ShowCompanyProfile.TabIndex = 1;
            this.ShowCompanyProfile.Text = "Profil firmy";
            this.ShowCompanyProfile.Click += new System.EventHandler(this.ShowCompanyProfile_Click);
            // 
            // ShowCarProfile
            // 
            this.ShowCarProfile.AutoSize = true;
            this.ShowCarProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCarProfile.Location = new System.Drawing.Point(3, 60);
            this.ShowCarProfile.Name = "ShowCarProfile";
            this.ShowCarProfile.Size = new System.Drawing.Size(85, 20);
            this.ShowCarProfile.TabIndex = 2;
            this.ShowCarProfile.Text = "Mój pojazd";
            this.ShowCarProfile.Click += new System.EventHandler(this.ShowCarProfile_Click);
            // 
            // ShowCoworkersList
            // 
            this.ShowCoworkersList.AutoSize = true;
            this.ShowCoworkersList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCoworkersList.Location = new System.Drawing.Point(3, 90);
            this.ShowCoworkersList.Name = "ShowCoworkersList";
            this.ShowCoworkersList.Size = new System.Drawing.Size(179, 20);
            this.ShowCoworkersList.TabIndex = 3;
            this.ShowCoworkersList.Text = "Lista współpracowników";
            this.ShowCoworkersList.Click += new System.EventHandler(this.ShowCoworkersList_Click);
            // 
            // ShowCarList
            // 
            this.ShowCarList.AutoSize = true;
            this.ShowCarList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCarList.Location = new System.Drawing.Point(3, 120);
            this.ShowCarList.Name = "ShowCarList";
            this.ShowCarList.Size = new System.Drawing.Size(114, 20);
            this.ShowCarList.TabIndex = 4;
            this.ShowCarList.Text = "Lista pojazdów";
            this.ShowCarList.Click += new System.EventHandler(this.ShowCarList_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.SidePanel);
            this.Controls.Add(this.MainPanel);
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainMenu";
            this.Text = "Fleet Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.SidePanel.ResumeLayout(false);
            this.SidePanel.PerformLayout();
            this.LinkTableLayout.ResumeLayout(false);
            this.LinkTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.PictureBox ProfilePictureBox;
        private System.Windows.Forms.TableLayoutPanel LinkTableLayout;
        private System.Windows.Forms.Label CompanyNameLabel;
        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.Label FirstNameLabel;
        private System.Windows.Forms.Label ShowCarList;
        private System.Windows.Forms.Label ShowCoworkersList;
        private System.Windows.Forms.Label ShowCarProfile;
        private System.Windows.Forms.Label ShowCompanyProfile;
        private System.Windows.Forms.Label ShowUserProfile;
    }
}