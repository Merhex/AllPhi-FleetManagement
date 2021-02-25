
using FleetManagement.WinForms.Properties;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace FleetManagement.WinForms.Views
{
    partial class MainForm
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
            this.gradientPanel = new FleetManagement.Blazor.Components.GradientPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.driversGridPanel = new System.Windows.Forms.Panel();
            this.driversGrid = new System.Windows.Forms.DataGridView();
            this.driversLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.driversGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driversGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientPanel
            // 
            this.gradientPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.gradientPanel.FadeFromColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.gradientPanel.FadeToColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(214)))), ((int)(((byte)(255)))));
            this.gradientPanel.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gradientPanel.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.gradientPanel.Name = "gradientPanel";
            this.gradientPanel.Size = new System.Drawing.Size(129, 408);
            this.gradientPanel.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.driversGridPanel);
            this.mainPanel.Controls.Add(this.driversLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(129, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(459, 408);
            this.mainPanel.TabIndex = 1;
            // 
            // driversGridPanel
            // 
            this.driversGridPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driversGridPanel.Controls.Add(this.driversGrid);
            this.driversGridPanel.Location = new System.Drawing.Point(16, 41);
            this.driversGridPanel.Name = "driversGridPanel";
            this.driversGridPanel.Size = new System.Drawing.Size(431, 355);
            this.driversGridPanel.TabIndex = 1;
            // 
            // driversGrid
            // 
            this.driversGrid.AllowUserToDeleteRows = false;
            this.driversGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.driversGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.driversGrid.Location = new System.Drawing.Point(0, 0);
            this.driversGrid.Name = "driversGrid";
            this.driversGrid.RowTemplate.Height = 25;
            this.driversGrid.Size = new System.Drawing.Size(431, 355);
            this.driversGrid.TabIndex = 0;
            // 
            // driversLabel
            // 
            this.driversLabel.AutoSize = true;
            this.driversLabel.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.driversLabel.Location = new System.Drawing.Point(5, 13);
            this.driversLabel.Name = "driversLabel";
            this.driversLabel.Size = new System.Drawing.Size(88, 25);
            this.driversLabel.TabIndex = 0;
            this.driversLabel.Text = "Drivers";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 408);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.gradientPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.driversGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.driversGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Blazor.Components.GradientPanel gradientPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label driversLabel;
        private System.Windows.Forms.Panel driversGridPanel;
        private System.Windows.Forms.DataGridView driversGrid;
    }
}