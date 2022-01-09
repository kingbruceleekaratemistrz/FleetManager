
namespace FleetManager.Controls
{
    partial class CarServiceControl
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
            this.MailLabel = new System.Windows.Forms.Label();
            this.PhoneLabel = new System.Windows.Forms.Label();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.RegisterVisitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MailLabel
            // 
            this.MailLabel.AutoSize = true;
            this.MailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MailLabel.Location = new System.Drawing.Point(132, 216);
            this.MailLabel.Name = "MailLabel";
            this.MailLabel.Size = new System.Drawing.Size(44, 24);
            this.MailLabel.TabIndex = 9;
            this.MailLabel.Text = "mail";
            // 
            // PhoneLabel
            // 
            this.PhoneLabel.AutoSize = true;
            this.PhoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PhoneLabel.Location = new System.Drawing.Point(132, 186);
            this.PhoneLabel.Name = "PhoneLabel";
            this.PhoneLabel.Size = new System.Drawing.Size(65, 24);
            this.PhoneLabel.TabIndex = 8;
            this.PhoneLabel.Text = "phone";
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AddressLabel.Location = new System.Drawing.Point(132, 156);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(77, 24);
            this.AddressLabel.TabIndex = 7;
            this.AddressLabel.Text = "address";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NameLabel.Location = new System.Drawing.Point(132, 104);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(58, 24);
            this.NameLabel.TabIndex = 5;
            this.NameLabel.Text = "name";
            // 
            // RegisterVisitButton
            // 
            this.RegisterVisitButton.Location = new System.Drawing.Point(136, 283);
            this.RegisterVisitButton.Name = "RegisterVisitButton";
            this.RegisterVisitButton.Size = new System.Drawing.Size(75, 23);
            this.RegisterVisitButton.TabIndex = 10;
            this.RegisterVisitButton.Text = "button1";
            this.RegisterVisitButton.UseVisualStyleBackColor = true;
            this.RegisterVisitButton.Click += new System.EventHandler(this.RegisterVisitButton_Click);
            // 
            // CarServiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.RegisterVisitButton);
            this.Controls.Add(this.MailLabel);
            this.Controls.Add(this.PhoneLabel);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.NameLabel);
            this.MaximumSize = new System.Drawing.Size(920, 680);
            this.MinimumSize = new System.Drawing.Size(920, 680);
            this.Name = "CarServiceControl";
            this.Size = new System.Drawing.Size(920, 680);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MailLabel;
        private System.Windows.Forms.Label PhoneLabel;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Button RegisterVisitButton;
    }
}
