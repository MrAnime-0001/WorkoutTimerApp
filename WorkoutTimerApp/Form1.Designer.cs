namespace WorkoutTimerApp
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
            cbPresets = new ComboBox();
            btnStart = new Button();
            btnPause = new Button();
            btnReset = new Button();
            lblTime = new Label();
            btnExit = new Button();
            btnTopMost = new Button();
            trackBarVolume = new TrackBar();
            progressBar = new ProgressBar();
            btnSelectAudio = new Button();
            btnToggleNotification = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            SuspendLayout();
            // 
            // cbPresets
            // 
            cbPresets.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPresets.ForeColor = Color.Black;
            cbPresets.FormattingEnabled = true;
            cbPresets.Location = new Point(12, 86);
            cbPresets.Name = "cbPresets";
            cbPresets.Size = new Size(137, 23);
            cbPresets.TabIndex = 0;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(155, 86);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(56, 45);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(211, 86);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(56, 45);
            btnPause.TabIndex = 2;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(267, 86);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(56, 45);
            btnReset.TabIndex = 3;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 24.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblTime.ForeColor = Color.White;
            lblTime.Location = new Point(74, 9);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(239, 45);
            lblTime.TabIndex = 4;
            lblTime.Text = "Timer: 00:00:00";
            // 
            // btnExit
            // 
            btnExit.Location = new Point(323, 131);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(56, 45);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnTopMost
            // 
            btnTopMost.Location = new Point(323, 86);
            btnTopMost.Name = "btnTopMost";
            btnTopMost.Size = new Size(56, 45);
            btnTopMost.TabIndex = 6;
            btnTopMost.Text = "Top Most";
            btnTopMost.UseVisualStyleBackColor = true;
            btnTopMost.Click += btnTopMost_Click;
            // 
            // trackBarVolume
            // 
            trackBarVolume.LargeChange = 25;
            trackBarVolume.Location = new Point(12, 115);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Size = new Size(137, 45);
            trackBarVolume.SmallChange = 25;
            trackBarVolume.TabIndex = 7;
            trackBarVolume.TickStyle = TickStyle.Both;
            trackBarVolume.Value = 25;
            trackBarVolume.Scroll += trackBarVolume_Scroll;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 57);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(367, 23);
            progressBar.TabIndex = 9;
            progressBar.Value = 100;
            // 
            // btnSelectAudio
            // 
            btnSelectAudio.Location = new Point(155, 131);
            btnSelectAudio.Name = "btnSelectAudio";
            btnSelectAudio.Size = new Size(79, 45);
            btnSelectAudio.TabIndex = 10;
            btnSelectAudio.Text = "Change Audio";
            btnSelectAudio.UseVisualStyleBackColor = true;
            btnSelectAudio.Click += btnSelectAudio_Click;
            // 
            // btnToggleNotification
            // 
            btnToggleNotification.Location = new Point(234, 131);
            btnToggleNotification.Name = "btnToggleNotification";
            btnToggleNotification.Size = new Size(89, 45);
            btnToggleNotification.TabIndex = 11;
            btnToggleNotification.Text = "Switch to Message Box";
            btnToggleNotification.UseVisualStyleBackColor = true;
            btnToggleNotification.Click += btnToggleNotification_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(382, 178);
            Controls.Add(btnToggleNotification);
            Controls.Add(btnSelectAudio);
            Controls.Add(progressBar);
            Controls.Add(trackBarVolume);
            Controls.Add(btnTopMost);
            Controls.Add(btnExit);
            Controls.Add(lblTime);
            Controls.Add(btnReset);
            Controls.Add(btnPause);
            Controls.Add(btnStart);
            Controls.Add(cbPresets);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mr Anime Timer Application";
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbPresets;
        private Button btnStart;
        private Button btnPause;
        private Button btnReset;
        private Label lblTime;
        private Button btnExit;
        private Button btnTopMost;
        private TrackBar trackBarVolume;
        private ProgressBar progressBar;
        private Button btnSelectAudio;
        private Button btnToggleNotification;
    }
}