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
            pnlMain = new Panel();
            cbPresets2 = new ComboBox();
            lblTime2 = new Label();
            pnlControls = new Panel();
            btnStart2 = new Button();
            btnPause2 = new Button();
            btnReset2 = new Button();
            btnTopMost2 = new Button();
            btnBack2 = new Button();
            btnExit2 = new Button();
            pnlMain.SuspendLayout();
            pnlControls.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(45, 45, 48);
            pnlMain.Controls.Add(cbPresets2);
            pnlMain.Controls.Add(lblTime2);
            pnlMain.Controls.Add(pnlControls);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10, 8, 10, 8);
            pnlMain.Size = new Size(274, 109);
            pnlMain.TabIndex = 0;
            // 
            // cbPresets2
            // 
            cbPresets2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbPresets2.BackColor = Color.FromArgb(60, 60, 64);
            cbPresets2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPresets2.FlatStyle = FlatStyle.Flat;
            cbPresets2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cbPresets2.ForeColor = Color.White;
            cbPresets2.FormattingEnabled = true;
            cbPresets2.Location = new Point(10, 8);
            cbPresets2.Name = "cbPresets2";
            cbPresets2.Size = new Size(255, 23);
            cbPresets2.TabIndex = 0;
            // 
            // lblTime2
            // 
            lblTime2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTime2.Font = new Font("Consolas", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblTime2.ForeColor = Color.FromArgb(0, 150, 136);
            lblTime2.Location = new Point(10, 36);
            lblTime2.Name = "lblTime2";
            lblTime2.Size = new Size(255, 32);
            lblTime2.TabIndex = 1;
            lblTime2.Text = "00:00:00";
            lblTime2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlControls
            // 
            pnlControls.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlControls.Controls.Add(btnStart2);
            pnlControls.Controls.Add(btnPause2);
            pnlControls.Controls.Add(btnReset2);
            pnlControls.Controls.Add(btnTopMost2);
            pnlControls.Controls.Add(btnBack2);
            pnlControls.Controls.Add(btnExit2);
            pnlControls.Location = new Point(10, 73);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new Size(255, 28);
            pnlControls.TabIndex = 2;
            // 
            // btnStart2
            // 
            btnStart2.BackColor = Color.FromArgb(76, 175, 80);
            btnStart2.FlatAppearance.BorderSize = 0;
            btnStart2.FlatAppearance.MouseDownBackColor = Color.FromArgb(56, 142, 60);
            btnStart2.FlatAppearance.MouseOverBackColor = Color.FromArgb(102, 187, 106);
            btnStart2.FlatStyle = FlatStyle.Flat;
            btnStart2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart2.ForeColor = Color.White;
            btnStart2.Location = new Point(0, 0);
            btnStart2.Name = "btnStart2";
            btnStart2.Size = new Size(35, 28);
            btnStart2.TabIndex = 0;
            btnStart2.Text = "▶";
            btnStart2.UseVisualStyleBackColor = false;
            btnStart2.Click += btnStart_Click;
            // 
            // btnPause2
            // 
            btnPause2.BackColor = Color.FromArgb(255, 193, 7);
            btnPause2.FlatAppearance.BorderSize = 0;
            btnPause2.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 160, 0);
            btnPause2.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 213, 79);
            btnPause2.FlatStyle = FlatStyle.Flat;
            btnPause2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnPause2.ForeColor = Color.Black;
            btnPause2.Location = new Point(38, 0);
            btnPause2.Name = "btnPause2";
            btnPause2.Size = new Size(35, 28);
            btnPause2.TabIndex = 1;
            btnPause2.Text = "⏸";
            btnPause2.UseVisualStyleBackColor = false;
            btnPause2.Click += btnPause_Click;
            // 
            // btnReset2
            // 
            btnReset2.BackColor = Color.FromArgb(244, 67, 54);
            btnReset2.FlatAppearance.BorderSize = 0;
            btnReset2.FlatAppearance.MouseDownBackColor = Color.FromArgb(198, 40, 40);
            btnReset2.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 108, 0);
            btnReset2.FlatStyle = FlatStyle.Flat;
            btnReset2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnReset2.ForeColor = Color.White;
            btnReset2.Location = new Point(76, 0);
            btnReset2.Name = "btnReset2";
            btnReset2.Size = new Size(35, 28);
            btnReset2.TabIndex = 2;
            btnReset2.Text = "⟲";
            btnReset2.UseVisualStyleBackColor = false;
            btnReset2.Click += btnReset_Click;
            // 
            // btnTopMost2
            // 
            btnTopMost2.BackColor = Color.FromArgb(103, 58, 183);
            btnTopMost2.FlatAppearance.BorderSize = 0;
            btnTopMost2.FlatAppearance.MouseDownBackColor = Color.FromArgb(81, 45, 168);
            btnTopMost2.FlatAppearance.MouseOverBackColor = Color.FromArgb(123, 31, 162);
            btnTopMost2.FlatStyle = FlatStyle.Flat;
            btnTopMost2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnTopMost2.ForeColor = Color.White;
            btnTopMost2.Location = new Point(114, 0);
            btnTopMost2.Name = "btnTopMost2";
            btnTopMost2.Size = new Size(35, 28);
            btnTopMost2.TabIndex = 3;
            btnTopMost2.Text = "📌";
            btnTopMost2.UseVisualStyleBackColor = false;
            btnTopMost2.Click += btnTopMost_Click;
            // 
            // btnBack2
            // 
            btnBack2.BackColor = Color.FromArgb(96, 125, 139);
            btnBack2.FlatAppearance.BorderSize = 0;
            btnBack2.FlatAppearance.MouseDownBackColor = Color.FromArgb(69, 90, 100);
            btnBack2.FlatAppearance.MouseOverBackColor = Color.FromArgb(120, 144, 156);
            btnBack2.FlatStyle = FlatStyle.Flat;
            btnBack2.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnBack2.ForeColor = Color.White;
            btnBack2.Location = new Point(152, 0);
            btnBack2.Name = "btnBack2";
            btnBack2.Size = new Size(50, 28);
            btnBack2.TabIndex = 4;
            btnBack2.Text = "◀ Back";
            btnBack2.UseVisualStyleBackColor = false;
            btnBack2.Click += btnBack_Click;
            // 
            // btnExit2
            // 
            btnExit2.BackColor = Color.FromArgb(158, 158, 158);
            btnExit2.FlatAppearance.BorderSize = 0;
            btnExit2.FlatAppearance.MouseDownBackColor = Color.FromArgb(117, 117, 117);
            btnExit2.FlatAppearance.MouseOverBackColor = Color.FromArgb(189, 189, 189);
            btnExit2.FlatStyle = FlatStyle.Flat;
            btnExit2.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnExit2.ForeColor = Color.Black;
            btnExit2.Location = new Point(205, 0);
            btnExit2.Name = "btnExit2";
            btnExit2.Size = new Size(50, 28);
            btnExit2.TabIndex = 5;
            btnExit2.Text = "✕ Exit";
            btnExit2.UseVisualStyleBackColor = false;
            btnExit2.Click += btnExit_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(274, 109);
            Controls.Add(pnlMain);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Workout Timer";
            pnlMain.ResumeLayout(false);
            pnlControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}