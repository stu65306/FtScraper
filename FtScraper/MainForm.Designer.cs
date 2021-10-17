
namespace FtScraper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RunBtn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.logPanel = new System.Windows.Forms.Panel();
            this.logLabel = new System.Windows.Forms.Label();
            this.pageLabel = new System.Windows.Forms.Label();
            this.companyLabel = new System.Windows.Forms.Label();
            this.StopwatchLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.WorkingPanel = new System.Windows.Forms.Panel();
            this.logPanel.SuspendLayout();
            this.WorkingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RunBtn
            // 
            this.RunBtn.Location = new System.Drawing.Point(13, 13);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(776, 23);
            this.RunBtn.TabIndex = 0;
            this.RunBtn.Text = "Run Scraper";
            this.RunBtn.UseVisualStyleBackColor = true;
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 13);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(776, 23);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // logPanel
            // 
            this.logPanel.AutoScroll = true;
            this.logPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logPanel.Controls.Add(this.logLabel);
            this.logPanel.Location = new System.Drawing.Point(3, 29);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(770, 347);
            this.logPanel.TabIndex = 3;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(3, 0);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(0, 15);
            this.logLabel.TabIndex = 6;
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Location = new System.Drawing.Point(0, 4);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(64, 15);
            this.pageLabel.TabIndex = 4;
            this.pageLabel.Text = "0 / 0 Pages";
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(113, 4);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(93, 15);
            this.companyLabel.TabIndex = 5;
            this.companyLabel.Text = "0 / 0 Companies";
            // 
            // StopwatchLabel
            // 
            this.StopwatchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopwatchLabel.Location = new System.Drawing.Point(575, 4);
            this.StopwatchLabel.Name = "StopwatchLabel";
            this.StopwatchLabel.Size = new System.Drawing.Size(200, 15);
            this.StopwatchLabel.TabIndex = 6;
            this.StopwatchLabel.Text = "Working..";
            this.StopwatchLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(341, 0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // WorkingPanel
            // 
            this.WorkingPanel.Controls.Add(this.StopwatchLabel);
            this.WorkingPanel.Controls.Add(this.logPanel);
            this.WorkingPanel.Controls.Add(this.CancelButton);
            this.WorkingPanel.Controls.Add(this.pageLabel);
            this.WorkingPanel.Controls.Add(this.companyLabel);
            this.WorkingPanel.Location = new System.Drawing.Point(13, 43);
            this.WorkingPanel.Name = "WorkingPanel";
            this.WorkingPanel.Size = new System.Drawing.Size(776, 383);
            this.WorkingPanel.TabIndex = 8;
            this.WorkingPanel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 431);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.RunBtn);
            this.Controls.Add(this.WorkingPanel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.logPanel.ResumeLayout(false);
            this.logPanel.PerformLayout();
            this.WorkingPanel.ResumeLayout(false);
            this.WorkingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RunBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel logPanel;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.Label StopwatchLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Panel WorkingPanel;
    }
}

