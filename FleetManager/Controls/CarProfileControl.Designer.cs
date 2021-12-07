
namespace FleetManager.Controls
{
    partial class CarProfileControl
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
            this.CarInfoPanel = new System.Windows.Forms.Panel();
            this.CCLabel = new System.Windows.Forms.Label();
            this.HPLabel = new System.Windows.Forms.Label();
            this.ProductionYearLabel = new System.Windows.Forms.Label();
            this.PlateNumberLabel = new System.Windows.Forms.Label();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.BrandLabel = new System.Windows.Forms.Label();
            this.CarInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CarInfoPanel
            // 
            this.CarInfoPanel.Controls.Add(this.CCLabel);
            this.CarInfoPanel.Controls.Add(this.HPLabel);
            this.CarInfoPanel.Controls.Add(this.ProductionYearLabel);
            this.CarInfoPanel.Controls.Add(this.PlateNumberLabel);
            this.CarInfoPanel.Controls.Add(this.ModelLabel);
            this.CarInfoPanel.Controls.Add(this.BrandLabel);
            this.CarInfoPanel.Location = new System.Drawing.Point(100, 100);
            this.CarInfoPanel.Name = "CarInfoPanel";
            this.CarInfoPanel.Size = new System.Drawing.Size(404, 430);
            this.CarInfoPanel.TabIndex = 0;
            // 
            // CCLabel
            // 
            this.CCLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CCLabel.AutoSize = true;
            this.CCLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CCLabel.Location = new System.Drawing.Point(1, 150);
            this.CCLabel.Name = "CCLabel";
            this.CCLabel.Size = new System.Drawing.Size(30, 24);
            this.CCLabel.TabIndex = 25;
            this.CCLabel.Text = "cc";
            // 
            // HPLabel
            // 
            this.HPLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.HPLabel.AutoSize = true;
            this.HPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HPLabel.Location = new System.Drawing.Point(1, 120);
            this.HPLabel.Name = "HPLabel";
            this.HPLabel.Size = new System.Drawing.Size(111, 24);
            this.HPLabel.TabIndex = 24;
            this.HPLabel.Text = "horsepower";
            // 
            // ProductionYearLabel
            // 
            this.ProductionYearLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProductionYearLabel.AutoSize = true;
            this.ProductionYearLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ProductionYearLabel.Location = new System.Drawing.Point(1, 90);
            this.ProductionYearLabel.Name = "ProductionYearLabel";
            this.ProductionYearLabel.Size = new System.Drawing.Size(161, 24);
            this.ProductionYearLabel.TabIndex = 23;
            this.ProductionYearLabel.Text = "year of production";
            // 
            // PlateNumberLabel
            // 
            this.PlateNumberLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PlateNumberLabel.AutoSize = true;
            this.PlateNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlateNumberLabel.Location = new System.Drawing.Point(1, 60);
            this.PlateNumberLabel.Name = "PlateNumberLabel";
            this.PlateNumberLabel.Size = new System.Drawing.Size(121, 24);
            this.PlateNumberLabel.TabIndex = 22;
            this.PlateNumberLabel.Text = "plate number";
            // 
            // ModelLabel
            // 
            this.ModelLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ModelLabel.AutoSize = true;
            this.ModelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ModelLabel.Location = new System.Drawing.Point(2, 30);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(63, 24);
            this.ModelLabel.TabIndex = 21;
            this.ModelLabel.Text = "model";
            // 
            // BrandLabel
            // 
            this.BrandLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BrandLabel.AutoSize = true;
            this.BrandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BrandLabel.Location = new System.Drawing.Point(3, 0);
            this.BrandLabel.Name = "BrandLabel";
            this.BrandLabel.Size = new System.Drawing.Size(59, 24);
            this.BrandLabel.TabIndex = 20;
            this.BrandLabel.Text = "brand";
            // 
            // CarProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.CarInfoPanel);
            this.MaximumSize = new System.Drawing.Size(920, 680);
            this.MinimumSize = new System.Drawing.Size(920, 680);
            this.Name = "CarProfileControl";
            this.Size = new System.Drawing.Size(920, 680);
            this.CarInfoPanel.ResumeLayout(false);
            this.CarInfoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel CarInfoPanel;
        private System.Windows.Forms.Label CCLabel;
        private System.Windows.Forms.Label HPLabel;
        private System.Windows.Forms.Label ProductionYearLabel;
        private System.Windows.Forms.Label PlateNumberLabel;
        private System.Windows.Forms.Label ModelLabel;
        private System.Windows.Forms.Label BrandLabel;
    }
}
