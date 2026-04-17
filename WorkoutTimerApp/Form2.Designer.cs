namespace WorkoutTimerApp
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cbPresets2;
        private System.Windows.Forms.Label lblTime2;
        private System.Windows.Forms.Button btnStart2;
        private System.Windows.Forms.Button btnPause2;
        private System.Windows.Forms.Button btnReset2;
        private System.Windows.Forms.Button btnTopMost2;
        private System.Windows.Forms.Button btnBack2;
        private System.Windows.Forms.Button btnExit2;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Panel pnlHeader2;
        private System.Windows.Forms.Label lblTitle2;

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
        /// Method required for Designer support — do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader2 = new System.Windows.Forms.Panel();
            lblTitle2 = new System.Windows.Forms.Label();
            pnlMain = new System.Windows.Forms.Panel();
            cbPresets2 = new System.Windows.Forms.ComboBox();
            lblTime2 = new System.Windows.Forms.Label();
            pnlControls = new System.Windows.Forms.Panel();
            btnStart2 = new System.Windows.Forms.Button();
            btnPause2 = new System.Windows.Forms.Button();
            btnReset2 = new System.Windows.Forms.Button();
            btnTopMost2 = new System.Windows.Forms.Button();
            btnBack2 = new System.Windows.Forms.Button();
            btnExit2 = new System.Windows.Forms.Button();
            pnlHeader2.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlControls.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader2
            // 
            pnlHeader2.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            pnlHeader2.Controls.Add(lblTitle2);
            pnlHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader2.Location = new System.Drawing.Point(0, 0);
            pnlHeader2.Name = "pnlHeader2";
            pnlHeader2.Size = new System.Drawing.Size(260, 30);
            pnlHeader2.TabIndex = 1;
            // 
            // lblTitle2
            // 
            lblTitle2.AutoSize = true;
            lblTitle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitle2.ForeColor = System.Drawing.Color.White;
            lblTitle2.Location = new System.Drawing.Point(10, 7);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new System.Drawing.Size(110, 15);
            lblTitle2.TabIndex = 0;
            lblTitle2.Text = "Workout Timer Lite";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            pnlMain.Controls.Add(cbPresets2);
            pnlMain.Controls.Add(lblTime2);
            pnlMain.Controls.Add(pnlControls);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 30);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new System.Windows.Forms.Padding(10, 5, 10, 8);
            pnlMain.Size = new System.Drawing.Size(260, 100);
            pnlMain.TabIndex = 0;
            // 
            // cbPresets2
            // 
            cbPresets2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cbPresets2.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            cbPresets2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbPresets2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbPresets2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cbPresets2.ForeColor = System.Drawing.Color.White;
            cbPresets2.FormattingEnabled = true;
            cbPresets2.Location = new System.Drawing.Point(10, 5);
            cbPresets2.Name = "cbPresets2";
            cbPresets2.Size = new System.Drawing.Size(240, 23);
            cbPresets2.TabIndex = 0;
            // 
            // lblTime2
            // 
            lblTime2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblTime2.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTime2.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblTime2.Location = new System.Drawing.Point(10, 32);
            lblTime2.Name = "lblTime2";
            lblTime2.Size = new System.Drawing.Size(240, 30);
            lblTime2.TabIndex = 1;
            lblTime2.Text = "00:00:00";
            lblTime2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlControls
            // 
            pnlControls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlControls.Controls.Add(btnStart2);
            pnlControls.Controls.Add(btnPause2);
            pnlControls.Controls.Add(btnReset2);
            pnlControls.Controls.Add(btnTopMost2);
            pnlControls.Controls.Add(btnBack2);
            pnlControls.Controls.Add(btnExit2);
            pnlControls.Location = new System.Drawing.Point(10, 65);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new System.Drawing.Size(240, 25);
            pnlControls.TabIndex = 2;
            // 
            // btnStart2
            // 
            btnStart2.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnStart2.FlatAppearance.BorderSize = 0;
            btnStart2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnStart2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnStart2.ForeColor = System.Drawing.Color.White;
            btnStart2.Location = new System.Drawing.Point(0, 0);
            btnStart2.Name = "btnStart2";
            btnStart2.Size = new System.Drawing.Size(30, 25);
            btnStart2.TabIndex = 0;
            btnStart2.Text = "▶";
            btnStart2.UseVisualStyleBackColor = false;
            btnStart2.Click += btnStart_Click;
            // 
            // btnPause2
            // 
            btnPause2.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            btnPause2.FlatAppearance.BorderSize = 0;
            btnPause2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPause2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnPause2.ForeColor = System.Drawing.Color.White;
            btnPause2.Location = new System.Drawing.Point(35, 0);
            btnPause2.Name = "btnPause2";
            btnPause2.Size = new System.Drawing.Size(30, 25);
            btnPause2.TabIndex = 1;
            btnPause2.Text = "⏸";
            btnPause2.UseVisualStyleBackColor = false;
            btnPause2.Click += btnPause_Click;
            // 
            // btnReset2
            // 
            btnReset2.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            btnReset2.FlatAppearance.BorderSize = 0;
            btnReset2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReset2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnReset2.ForeColor = System.Drawing.Color.White;
            btnReset2.Location = new System.Drawing.Point(70, 0);
            btnReset2.Name = "btnReset2";
            btnReset2.Size = new System.Drawing.Size(30, 25);
            btnReset2.TabIndex = 2;
            btnReset2.Text = "⟲";
            btnReset2.UseVisualStyleBackColor = false;
            btnReset2.Click += btnReset_Click;
            // 
            // btnTopMost2
            // 
            btnTopMost2.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            btnTopMost2.FlatAppearance.BorderSize = 0;
            btnTopMost2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTopMost2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnTopMost2.ForeColor = System.Drawing.Color.White;
            btnTopMost2.Location = new System.Drawing.Point(105, 0);
            btnTopMost2.Name = "btnTopMost2";
            btnTopMost2.Size = new System.Drawing.Size(30, 25);
            btnTopMost2.TabIndex = 3;
            btnTopMost2.Text = "📌";
            btnTopMost2.UseVisualStyleBackColor = false;
            btnTopMost2.Click += btnTopMost_Click;
            // 
            // btnBack2
            // 
            btnBack2.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            btnBack2.FlatAppearance.BorderSize = 0;
            btnBack2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBack2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnBack2.ForeColor = System.Drawing.Color.White;
            btnBack2.Location = new System.Drawing.Point(140, 0);
            btnBack2.Name = "btnBack2";
            btnBack2.Size = new System.Drawing.Size(45, 25);
            btnBack2.TabIndex = 4;
            btnBack2.Text = "LITE";
            btnBack2.UseVisualStyleBackColor = false;
            btnBack2.Click += btnBack_Click;
            // 
            // btnExit2
            // 
            btnExit2.BackColor = System.Drawing.Color.FromArgb(180, 0, 0);
            btnExit2.FlatAppearance.BorderSize = 0;
            btnExit2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExit2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnExit2.ForeColor = System.Drawing.Color.White;
            btnExit2.Location = new System.Drawing.Point(190, 0);
            btnExit2.Name = "btnExit2";
            btnExit2.Size = new System.Drawing.Size(45, 25);
            btnExit2.TabIndex = 5;
            btnExit2.Text = "✕";
            btnExit2.UseVisualStyleBackColor = false;
            btnExit2.Click += btnExit_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            ClientSize = new System.Drawing.Size(260, 130);
            Controls.Add(pnlMain);
            Controls.Add(pnlHeader2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "Form2";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Workout Timer Lite";
            pnlHeader2.ResumeLayout(false);
            pnlHeader2.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
