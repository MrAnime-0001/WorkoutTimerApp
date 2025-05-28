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
            components = new System.ComponentModel.Container();
            pnlHeader = new Panel();
            lblPresetName = new Label();
            pnlTimerDisplay = new Panel();
            lblTime = new Label();
            progressBar = new ProgressBar();
            pnlControls = new Panel();
            cbPresets = new ComboBox();
            btnStart = new Button();
            btnPause = new Button();
            btnReset = new Button();
            pnlSettings = new Panel();
            lblVolumeText = new Label();
            trackBarVolume = new TrackBar();
            lblVolumeValue = new Label();
            btnSelectAudio = new Button();
            btnToggleNotification = new Button();
            pnlFooter = new Panel();
            btnGoToForm2 = new Button();
            btnTopMost = new Button();
            btnMinimize = new Button();
            btnExit = new Button();
            toolTip = new ToolTip(components);
            pnlHeader.SuspendLayout();
            pnlTimerDisplay.SuspendLayout();
            pnlControls.SuspendLayout();
            pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(25, 25, 25);
            pnlHeader.Controls.Add(lblPresetName);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(15, 10, 15, 10);
            pnlHeader.Size = new Size(480, 50);
            pnlHeader.TabIndex = 0;
            // 
            // lblPresetName
            // 
            lblPresetName.AutoSize = true;
            lblPresetName.Dock = DockStyle.Left;
            lblPresetName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblPresetName.ForeColor = Color.FromArgb(100, 200, 255);
            lblPresetName.Location = new Point(15, 10);
            lblPresetName.Name = "lblPresetName";
            lblPresetName.Size = new Size(140, 21);
            lblPresetName.TabIndex = 0;
            lblPresetName.Text = "Select a Workout";
            lblPresetName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTimerDisplay
            // 
            pnlTimerDisplay.BackColor = Color.FromArgb(18, 18, 18);
            pnlTimerDisplay.Controls.Add(lblTime);
            pnlTimerDisplay.Controls.Add(progressBar);
            pnlTimerDisplay.Dock = DockStyle.Top;
            pnlTimerDisplay.Location = new Point(0, 50);
            pnlTimerDisplay.Name = "pnlTimerDisplay";
            pnlTimerDisplay.Padding = new Padding(20, 25, 20, 25);
            pnlTimerDisplay.Size = new Size(480, 120);
            pnlTimerDisplay.TabIndex = 1;
            // 
            // lblTime
            // 
            lblTime.Anchor = AnchorStyles.Top;
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Consolas", 28F, FontStyle.Bold, GraphicsUnit.Point);
            lblTime.ForeColor = Color.FromArgb(100, 255, 150);
            lblTime.Location = new Point(130, 25);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(188, 45);
            lblTime.TabIndex = 0;
            lblTime.Text = "00:00:00";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.BackColor = Color.FromArgb(35, 35, 35);
            progressBar.ForeColor = Color.FromArgb(100, 200, 255);
            progressBar.Location = new Point(20, 80);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(440, 8);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 1;
            progressBar.Value = 100;
            // 
            // pnlControls
            // 
            pnlControls.BackColor = Color.FromArgb(22, 22, 22);
            pnlControls.Controls.Add(cbPresets);
            pnlControls.Controls.Add(btnStart);
            pnlControls.Controls.Add(btnPause);
            pnlControls.Controls.Add(btnReset);
            pnlControls.Dock = DockStyle.Top;
            pnlControls.Location = new Point(0, 170);
            pnlControls.Name = "pnlControls";
            pnlControls.Padding = new Padding(20, 15, 20, 15);
            pnlControls.Size = new Size(480, 80);
            pnlControls.TabIndex = 2;
            // 
            // cbPresets
            // 
            cbPresets.BackColor = Color.FromArgb(35, 35, 35);
            cbPresets.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPresets.FlatStyle = FlatStyle.Flat;
            cbPresets.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cbPresets.ForeColor = Color.White;
            cbPresets.FormattingEnabled = true;
            cbPresets.Location = new Point(20, 15);
            cbPresets.Name = "cbPresets";
            cbPresets.Size = new Size(200, 25);
            cbPresets.TabIndex = 0;
            toolTip.SetToolTip(cbPresets, "Select a workout preset");
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(50, 150, 50);
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(40, 120, 40);
            btnStart.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 180, 60);
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(240, 15);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(70, 50);
            btnStart.TabIndex = 1;
            btnStart.Text = "▶ Start";
            toolTip.SetToolTip(btnStart, "Start the timer");
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnPause
            // 
            btnPause.BackColor = Color.FromArgb(200, 120, 50);
            btnPause.FlatAppearance.BorderSize = 0;
            btnPause.FlatAppearance.MouseDownBackColor = Color.FromArgb(160, 100, 40);
            btnPause.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 140, 70);
            btnPause.FlatStyle = FlatStyle.Flat;
            btnPause.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnPause.ForeColor = Color.White;
            btnPause.Location = new Point(320, 15);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(70, 50);
            btnPause.TabIndex = 2;
            btnPause.Text = "⏸ Pause";
            toolTip.SetToolTip(btnPause, "Pause/Resume the timer");
            btnPause.UseVisualStyleBackColor = false;
            btnPause.Click += btnPause_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(180, 50, 50);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatAppearance.MouseDownBackColor = Color.FromArgb(140, 40, 40);
            btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 70, 70);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(400, 15);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(70, 50);
            btnReset.TabIndex = 3;
            btnReset.Text = "⟲ Reset";
            toolTip.SetToolTip(btnReset, "Reset the timer");
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // pnlSettings
            // 
            pnlSettings.BackColor = Color.FromArgb(25, 25, 25);
            pnlSettings.Controls.Add(lblVolumeText);
            pnlSettings.Controls.Add(trackBarVolume);
            pnlSettings.Controls.Add(lblVolumeValue);
            pnlSettings.Controls.Add(btnSelectAudio);
            pnlSettings.Controls.Add(btnToggleNotification);
            pnlSettings.Dock = DockStyle.Top;
            pnlSettings.Location = new Point(0, 250);
            pnlSettings.Name = "pnlSettings";
            pnlSettings.Padding = new Padding(20, 15, 20, 15);
            pnlSettings.Size = new Size(480, 90);
            pnlSettings.TabIndex = 3;
            // 
            // lblVolumeText
            // 
            lblVolumeText.AutoSize = true;
            lblVolumeText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblVolumeText.ForeColor = Color.FromArgb(180, 180, 180);
            lblVolumeText.Location = new Point(20, 15);
            lblVolumeText.Name = "lblVolumeText";
            lblVolumeText.Size = new Size(47, 15);
            lblVolumeText.TabIndex = 0;
            lblVolumeText.Text = "Volume";
            // 
            // trackBarVolume
            // 
            trackBarVolume.BackColor = Color.FromArgb(25, 25, 25);
            trackBarVolume.LargeChange = 25;
            trackBarVolume.Location = new Point(20, 35);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Size = new Size(120, 45);
            trackBarVolume.SmallChange = 5;
            trackBarVolume.TabIndex = 1;
            trackBarVolume.TickStyle = TickStyle.None;
            toolTip.SetToolTip(trackBarVolume, "Adjust notification volume");
            trackBarVolume.Value = 100;
            trackBarVolume.Scroll += trackBarVolume_Scroll;
            // 
            // lblVolumeValue
            // 
            lblVolumeValue.AutoSize = true;
            lblVolumeValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblVolumeValue.ForeColor = Color.FromArgb(100, 200, 255);
            lblVolumeValue.Location = new Point(150, 50);
            lblVolumeValue.Name = "lblVolumeValue";
            lblVolumeValue.Size = new Size(28, 15);
            lblVolumeValue.TabIndex = 2;
            lblVolumeValue.Text = "100";
            // 
            // btnSelectAudio
            // 
            btnSelectAudio.BackColor = Color.FromArgb(60, 60, 60);
            btnSelectAudio.FlatAppearance.BorderSize = 0;
            btnSelectAudio.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 50, 50);
            btnSelectAudio.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            btnSelectAudio.FlatStyle = FlatStyle.Flat;
            btnSelectAudio.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSelectAudio.ForeColor = Color.White;
            btnSelectAudio.Location = new Point(200, 25);
            btnSelectAudio.Name = "btnSelectAudio";
            btnSelectAudio.Size = new Size(110, 35);
            btnSelectAudio.TabIndex = 3;
            btnSelectAudio.Text = "🔊 Change Audio";
            toolTip.SetToolTip(btnSelectAudio, "Select custom notification sound");
            btnSelectAudio.UseVisualStyleBackColor = false;
            btnSelectAudio.Click += btnSelectAudio_Click;
            // 
            // btnToggleNotification
            // 
            btnToggleNotification.BackColor = Color.FromArgb(60, 60, 60);
            btnToggleNotification.FlatAppearance.BorderSize = 0;
            btnToggleNotification.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 50, 50);
            btnToggleNotification.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            btnToggleNotification.FlatStyle = FlatStyle.Flat;
            btnToggleNotification.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnToggleNotification.ForeColor = Color.White;
            btnToggleNotification.Location = new Point(320, 25);
            btnToggleNotification.Name = "btnToggleNotification";
            btnToggleNotification.Size = new Size(140, 35);
            btnToggleNotification.TabIndex = 4;
            btnToggleNotification.Text = "💬 Message Box Mode";
            toolTip.SetToolTip(btnToggleNotification, "Toggle notification style");
            btnToggleNotification.UseVisualStyleBackColor = false;
            btnToggleNotification.Click += btnToggleNotification_Click;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(30, 30, 30);
            pnlFooter.Controls.Add(btnGoToForm2);
            pnlFooter.Controls.Add(btnTopMost);
            pnlFooter.Controls.Add(btnMinimize);
            pnlFooter.Controls.Add(btnExit);
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Location = new Point(0, 340);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(20, 10, 20, 15);
            pnlFooter.Size = new Size(480, 55);
            pnlFooter.TabIndex = 4;
            // 
            // btnGoToForm2
            // 
            btnGoToForm2.BackColor = Color.FromArgb(70, 70, 70);
            btnGoToForm2.FlatAppearance.BorderSize = 0;
            btnGoToForm2.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            btnGoToForm2.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 90, 90);
            btnGoToForm2.FlatStyle = FlatStyle.Flat;
            btnGoToForm2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnGoToForm2.ForeColor = Color.White;
            btnGoToForm2.Location = new Point(220, 10);
            btnGoToForm2.Name = "btnGoToForm2";
            btnGoToForm2.Size = new Size(94, 30);
            btnGoToForm2.TabIndex = 3;
            btnGoToForm2.Text = "Change Form";
            toolTip.SetToolTip(btnGoToForm2, "Minimize to system tray");
            btnGoToForm2.UseVisualStyleBackColor = false;
            btnGoToForm2.Click += btnGoToForm2_Click;
            // 
            // btnTopMost
            // 
            btnTopMost.BackColor = Color.FromArgb(70, 70, 70);
            btnTopMost.FlatAppearance.BorderSize = 0;
            btnTopMost.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            btnTopMost.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 90, 90);
            btnTopMost.FlatStyle = FlatStyle.Flat;
            btnTopMost.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnTopMost.ForeColor = Color.White;
            btnTopMost.Location = new Point(20, 10);
            btnTopMost.Name = "btnTopMost";
            btnTopMost.Size = new Size(80, 30);
            btnTopMost.TabIndex = 0;
            btnTopMost.Text = "📌 Pin";
            toolTip.SetToolTip(btnTopMost, "Keep window always on top");
            btnTopMost.UseVisualStyleBackColor = false;
            btnTopMost.Click += btnTopMost_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.BackColor = Color.FromArgb(70, 70, 70);
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 90, 90);
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnMinimize.ForeColor = Color.White;
            btnMinimize.Location = new Point(320, 10);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(70, 30);
            btnMinimize.TabIndex = 1;
            btnMinimize.Text = "➖ Min";
            toolTip.SetToolTip(btnMinimize, "Minimize to system tray");
            btnMinimize.UseVisualStyleBackColor = false;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(150, 50, 50);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(120, 40, 40);
            btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 70, 70);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(400, 10);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(60, 30);
            btnExit.TabIndex = 2;
            btnExit.Text = "✕ Exit";
            toolTip.SetToolTip(btnExit, "Close application");
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(480, 395);
            Controls.Add(pnlFooter);
            Controls.Add(pnlSettings);
            Controls.Add(pnlControls);
            Controls.Add(pnlTimerDisplay);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Workout Timer Pro";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlTimerDisplay.ResumeLayout(false);
            pnlTimerDisplay.PerformLayout();
            pnlControls.ResumeLayout(false);
            pnlSettings.ResumeLayout(false);
            pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // Layout panels
        private Panel pnlHeader;
        private Panel pnlTimerDisplay;
        private Panel pnlControls;
        private Panel pnlSettings;
        private Panel pnlFooter;

        // Timer display
        private Label lblTime;
        private ProgressBar progressBar;
        private Label lblPresetName;

        // Main controls
        private ComboBox cbPresets;
        private Button btnStart;
        private Button btnPause;
        private Button btnReset;

        // Settings
        private Label lblVolumeText;
        private TrackBar trackBarVolume;
        private Label lblVolumeValue;
        private Button btnSelectAudio;
        private Button btnToggleNotification;

        // Footer controls
        private Button btnTopMost;
        private Button btnMinimize;
        private Button btnExit;

        // Additional components
        private ToolTip toolTip;
        private Button btnGoToForm2;
    }
}