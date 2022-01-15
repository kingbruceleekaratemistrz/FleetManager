
namespace FleetManager
{
    partial class AddDataMenu
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
            this.TableSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TableSelectionComboBox
            // 
            this.TableSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TableSelectionComboBox.FormattingEnabled = true;
            this.TableSelectionComboBox.Location = new System.Drawing.Point(120, 30);
            this.TableSelectionComboBox.Name = "TableSelectionComboBox";
            this.TableSelectionComboBox.Size = new System.Drawing.Size(240, 21);
            this.TableSelectionComboBox.TabIndex = 0;
            this.TableSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.TableSelectionComboBox_SelectedIndexChanged);
            // 
            // ControlPanel
            // 
            this.ControlPanel.Location = new System.Drawing.Point(60, 70);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(360, 369);
            this.ControlPanel.TabIndex = 1;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(200, 460);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Zapisz";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AddDataMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 501);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.TableSelectionComboBox);
            this.MaximumSize = new System.Drawing.Size(480, 540);
            this.MinimumSize = new System.Drawing.Size(480, 540);
            this.Name = "AddDataMenu";
            this.Text = "AddDataMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TableSelectionComboBox;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Button SaveButton;
    }
}