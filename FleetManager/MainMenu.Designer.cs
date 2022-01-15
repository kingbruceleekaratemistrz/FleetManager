
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
            this.AddDataButton = new System.Windows.Forms.Button();
            this.CompanyNameLabel = new System.Windows.Forms.Label();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.ProfilePictureBox = new System.Windows.Forms.PictureBox();
            this.LinkTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ShowCompaniesList = new System.Windows.Forms.Label();
            this.ShowCarServicesListLabel = new System.Windows.Forms.Label();
            this.ShowCarListLabel = new System.Windows.Forms.Label();
            this.ShowCoworkersListLabel = new System.Windows.Forms.Label();
            this.ShowCarProfileLabel = new System.Windows.Forms.Label();
            this.ShowCompanyProfileLabel = new System.Windows.Forms.Label();
            this.ShowUserProfileLabel = new System.Windows.Forms.Label();
            this.ShowUsersListLabel = new System.Windows.Forms.Label();
            this.ShowRepairHistoryLabel = new System.Windows.Forms.Label();
            this.SidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).BeginInit();
            this.LinkTableLayout.SuspendLayout();
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
            this.SidePanel.Controls.Add(this.AddDataButton);
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
            // AddDataButton
            // 
            this.AddDataButton.Location = new System.Drawing.Point(70, 544);
            this.AddDataButton.Name = "AddDataButton";
            this.AddDataButton.Size = new System.Drawing.Size(117, 36);
            this.AddDataButton.TabIndex = 6;
            this.AddDataButton.Text = "Dodaj dane";
            this.AddDataButton.UseVisualStyleBackColor = true;
            this.AddDataButton.Click += new System.EventHandler(this.AddDataButton_Click);
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
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Location = new System.Drawing.Point(70, 161);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(53, 13);
            this.LastNameLabel.TabIndex = 4;
            this.LastNameLabel.Text = "Nazwisko";
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
            // ProfilePictureBox
            // 
            this.ProfilePictureBox.Location = new System.Drawing.Point(70, 73);
            this.ProfilePictureBox.Name = "ProfilePictureBox";
            this.ProfilePictureBox.Size = new System.Drawing.Size(64, 64);
            this.ProfilePictureBox.TabIndex = 2;
            this.ProfilePictureBox.TabStop = false;
            // 
            // LinkTableLayout
            // 
            this.LinkTableLayout.ColumnCount = 1;
            this.LinkTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LinkTableLayout.Controls.Add(this.ShowCompaniesList, 0, 7);
            this.LinkTableLayout.Controls.Add(this.ShowCarServicesListLabel, 0, 5);
            this.LinkTableLayout.Controls.Add(this.ShowCarListLabel, 0, 4);
            this.LinkTableLayout.Controls.Add(this.ShowCoworkersListLabel, 0, 3);
            this.LinkTableLayout.Controls.Add(this.ShowCarProfileLabel, 0, 2);
            this.LinkTableLayout.Controls.Add(this.ShowCompanyProfileLabel, 0, 1);
            this.LinkTableLayout.Controls.Add(this.ShowUserProfileLabel, 0, 0);
            this.LinkTableLayout.Controls.Add(this.ShowUsersListLabel, 0, 8);
            this.LinkTableLayout.Controls.Add(this.ShowRepairHistoryLabel, 0, 6);
            this.LinkTableLayout.Location = new System.Drawing.Point(70, 229);
            this.LinkTableLayout.Name = "LinkTableLayout";
            this.LinkTableLayout.RowCount = 9;
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11F));
            this.LinkTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.12F));
            this.LinkTableLayout.Size = new System.Drawing.Size(200, 227);
            this.LinkTableLayout.TabIndex = 1;
            // 
            // ShowCompaniesList
            // 
            this.ShowCompaniesList.AutoSize = true;
            this.ShowCompaniesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCompaniesList.Location = new System.Drawing.Point(3, 175);
            this.ShowCompaniesList.Name = "ShowCompaniesList";
            this.ShowCompaniesList.Size = new System.Drawing.Size(73, 20);
            this.ShowCompaniesList.TabIndex = 7;
            this.ShowCompaniesList.Text = "Lista firm";
            this.ShowCompaniesList.Click += new System.EventHandler(this.ShowCompaniesList_Click);
            // 
            // ShowCarServicesListLabel
            // 
            this.ShowCarServicesListLabel.AutoSize = true;
            this.ShowCarServicesListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCarServicesListLabel.Location = new System.Drawing.Point(3, 125);
            this.ShowCarServicesListLabel.Name = "ShowCarServicesListLabel";
            this.ShowCarServicesListLabel.Size = new System.Drawing.Size(127, 20);
            this.ShowCarServicesListLabel.TabIndex = 5;
            this.ShowCarServicesListLabel.Text = "Lista warsztatów";
            this.ShowCarServicesListLabel.Click += new System.EventHandler(this.ShowCarServicesList);
            // 
            // ShowCarListLabel
            // 
            this.ShowCarListLabel.AutoSize = true;
            this.ShowCarListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCarListLabel.Location = new System.Drawing.Point(3, 100);
            this.ShowCarListLabel.Name = "ShowCarListLabel";
            this.ShowCarListLabel.Size = new System.Drawing.Size(114, 20);
            this.ShowCarListLabel.TabIndex = 4;
            this.ShowCarListLabel.Text = "Lista pojazdów";
            this.ShowCarListLabel.Click += new System.EventHandler(this.ShowCarList_Click);
            // 
            // ShowCoworkersListLabel
            // 
            this.ShowCoworkersListLabel.AutoSize = true;
            this.ShowCoworkersListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCoworkersListLabel.Location = new System.Drawing.Point(3, 75);
            this.ShowCoworkersListLabel.Name = "ShowCoworkersListLabel";
            this.ShowCoworkersListLabel.Size = new System.Drawing.Size(179, 20);
            this.ShowCoworkersListLabel.TabIndex = 3;
            this.ShowCoworkersListLabel.Text = "Lista współpracowników";
            this.ShowCoworkersListLabel.Click += new System.EventHandler(this.ShowCoworkersList_Click);
            // 
            // ShowCarProfileLabel
            // 
            this.ShowCarProfileLabel.AutoSize = true;
            this.ShowCarProfileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCarProfileLabel.Location = new System.Drawing.Point(3, 50);
            this.ShowCarProfileLabel.Name = "ShowCarProfileLabel";
            this.ShowCarProfileLabel.Size = new System.Drawing.Size(85, 20);
            this.ShowCarProfileLabel.TabIndex = 2;
            this.ShowCarProfileLabel.Text = "Mój pojazd";
            this.ShowCarProfileLabel.Click += new System.EventHandler(this.ShowCarProfile_Click);
            // 
            // ShowCompanyProfileLabel
            // 
            this.ShowCompanyProfileLabel.AutoSize = true;
            this.ShowCompanyProfileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowCompanyProfileLabel.Location = new System.Drawing.Point(3, 25);
            this.ShowCompanyProfileLabel.Name = "ShowCompanyProfileLabel";
            this.ShowCompanyProfileLabel.Size = new System.Drawing.Size(81, 20);
            this.ShowCompanyProfileLabel.TabIndex = 1;
            this.ShowCompanyProfileLabel.Text = "Profil firmy";
            this.ShowCompanyProfileLabel.Click += new System.EventHandler(this.ShowCompanyProfile_Click);
            // 
            // ShowUserProfileLabel
            // 
            this.ShowUserProfileLabel.AutoSize = true;
            this.ShowUserProfileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowUserProfileLabel.Location = new System.Drawing.Point(3, 0);
            this.ShowUserProfileLabel.Name = "ShowUserProfileLabel";
            this.ShowUserProfileLabel.Size = new System.Drawing.Size(72, 20);
            this.ShowUserProfileLabel.TabIndex = 0;
            this.ShowUserProfileLabel.Text = "Mój profil";
            this.ShowUserProfileLabel.Click += new System.EventHandler(this.ShowUserProfile_Click);
            // 
            // ShowUsersListLabel
            // 
            this.ShowUsersListLabel.AutoSize = true;
            this.ShowUsersListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowUsersListLabel.Location = new System.Drawing.Point(3, 200);
            this.ShowUsersListLabel.Name = "ShowUsersListLabel";
            this.ShowUsersListLabel.Size = new System.Drawing.Size(144, 20);
            this.ShowUsersListLabel.TabIndex = 6;
            this.ShowUsersListLabel.Text = "Lista użytkowników";
            this.ShowUsersListLabel.Click += new System.EventHandler(this.ShowUsersListLabel_Click);
            // 
            // ShowRepairHistoryLabel
            // 
            this.ShowRepairHistoryLabel.AutoSize = true;
            this.ShowRepairHistoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShowRepairHistoryLabel.Location = new System.Drawing.Point(3, 150);
            this.ShowRepairHistoryLabel.Name = "ShowRepairHistoryLabel";
            this.ShowRepairHistoryLabel.Size = new System.Drawing.Size(119, 20);
            this.ShowRepairHistoryLabel.TabIndex = 8;
            this.ShowRepairHistoryLabel.Text = "Historia napraw";
            this.ShowRepairHistoryLabel.Click += new System.EventHandler(this.ShowRepairHistoryLabel_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePictureBox)).EndInit();
            this.LinkTableLayout.ResumeLayout(false);
            this.LinkTableLayout.PerformLayout();
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
        private System.Windows.Forms.Label ShowCarListLabel;
        private System.Windows.Forms.Label ShowCoworkersListLabel;
        private System.Windows.Forms.Label ShowCarProfileLabel;
        private System.Windows.Forms.Label ShowCompanyProfileLabel;
        private System.Windows.Forms.Label ShowUserProfileLabel;
        private System.Windows.Forms.Label ShowCarServicesListLabel;
        private System.Windows.Forms.Label ShowCompaniesList;
        private System.Windows.Forms.Label ShowUsersListLabel;
        private System.Windows.Forms.Button AddDataButton;
        private System.Windows.Forms.Label ShowRepairHistoryLabel;
    }
}