
namespace FleetManager
{
    partial class RegisterVisitMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.CarServiceLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServiceComboBox = new System.Windows.Forms.ComboBox();
            this.CostLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.ConfirmationButton = new System.Windows.Forms.Button();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.HourComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rejestracja wizyty w warsztacie";
            // 
            // CarServiceLabel
            // 
            this.CarServiceLabel.AutoSize = true;
            this.CarServiceLabel.Location = new System.Drawing.Point(107, 108);
            this.CarServiceLabel.Name = "CarServiceLabel";
            this.CarServiceLabel.Size = new System.Drawing.Size(35, 13);
            this.CarServiceLabel.TabIndex = 1;
            this.CarServiceLabel.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Wybierz powód wizyty";
            // 
            // ServiceComboBox
            // 
            this.ServiceComboBox.FormattingEnabled = true;
            this.ServiceComboBox.Location = new System.Drawing.Point(107, 193);
            this.ServiceComboBox.Name = "ServiceComboBox";
            this.ServiceComboBox.Size = new System.Drawing.Size(121, 21);
            this.ServiceComboBox.TabIndex = 3;
            this.ServiceComboBox.SelectedIndexChanged += new System.EventHandler(this.ServiceComboBox_SelectedIndexChanged);
            // 
            // CostLabel
            // 
            this.CostLabel.AutoSize = true;
            this.CostLabel.Location = new System.Drawing.Point(107, 230);
            this.CostLabel.Name = "CostLabel";
            this.CostLabel.Size = new System.Drawing.Size(39, 13);
            this.CostLabel.TabIndex = 4;
            this.CostLabel.Text = "Koszt: ";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(107, 247);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(36, 13);
            this.TimeLabel.TabIndex = 5;
            this.TimeLabel.Text = "Czas: ";
            // 
            // ConfirmationButton
            // 
            this.ConfirmationButton.Location = new System.Drawing.Point(107, 276);
            this.ConfirmationButton.Name = "ConfirmationButton";
            this.ConfirmationButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmationButton.TabIndex = 6;
            this.ConfirmationButton.Text = "Zarezerwuj";
            this.ConfirmationButton.UseVisualStyleBackColor = true;
            this.ConfirmationButton.Click += new System.EventHandler(this.ConfirmationButton_Click);
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Location = new System.Drawing.Point(107, 336);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.DateTimePicker.TabIndex = 7;
            this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // HourComboBox
            // 
            this.HourComboBox.FormattingEnabled = true;
            this.HourComboBox.Location = new System.Drawing.Point(107, 363);
            this.HourComboBox.Name = "HourComboBox";
            this.HourComboBox.Size = new System.Drawing.Size(121, 21);
            this.HourComboBox.TabIndex = 8;
            // 
            // RegisterVisitMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 561);
            this.Controls.Add(this.HourComboBox);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.ConfirmationButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.CostLabel);
            this.Controls.Add(this.ServiceComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CarServiceLabel);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(480, 600);
            this.MinimumSize = new System.Drawing.Size(480, 600);
            this.Name = "RegisterVisitMenu";
            this.Text = "RegisterVisitMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisterVisitMenu_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CarServiceLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ServiceComboBox;
        private System.Windows.Forms.Label CostLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button ConfirmationButton;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.ComboBox HourComboBox;
    }
}