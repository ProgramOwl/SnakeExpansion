namespace Snake
{
    partial class SnakeForm
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
            this.components = new System.ComponentModel.Container();
            this.GameCanvas = new System.Windows.Forms.PictureBox();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.Start_Btn = new System.Windows.Forms.Button();
            this.DareBtn = new System.Windows.Forms.Button();
            this.ScoreTxtBox = new System.Windows.Forms.TextBox();
            this.ScoreLbl = new System.Windows.Forms.Label();
            this.Score2Label = new System.Windows.Forms.Label();
            this.Score2TxtBox = new System.Windows.Forms.TextBox();
            this.Player2Label = new System.Windows.Forms.Label();
            this.ToggleGrid = new System.Windows.Forms.CheckBox();
            this.Ctrl_Toggle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.skin2label = new System.Windows.Forms.Label();
            this.skin2comboBox = new System.Windows.Forms.ComboBox();
            this.skin1comboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // GameCanvas
            // 
            this.GameCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GameCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameCanvas.Location = new System.Drawing.Point(7, 6);
            this.GameCanvas.Margin = new System.Windows.Forms.Padding(6);
            this.GameCanvas.Name = "GameCanvas";
            this.GameCanvas.Size = new System.Drawing.Size(994, 737);
            this.GameCanvas.TabIndex = 0;
            this.GameCanvas.TabStop = false;
            this.GameCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.GameCanvas_Paint);
            // 
            // GameTimer
            // 
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(1016, 24);
            this.Start_Btn.Margin = new System.Windows.Forms.Padding(6);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(376, 42);
            this.Start_Btn.TabIndex = 0;
            this.Start_Btn.TabStop = false;
            this.Start_Btn.Text = "Start/Pause";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // DareBtn
            // 
            this.DareBtn.Location = new System.Drawing.Point(1014, 700);
            this.DareBtn.Margin = new System.Windows.Forms.Padding(6);
            this.DareBtn.Name = "DareBtn";
            this.DareBtn.Size = new System.Drawing.Size(378, 42);
            this.DareBtn.TabIndex = 0;
            this.DareBtn.TabStop = false;
            this.DareBtn.Text = "I Dare You To Press Me";
            this.DareBtn.UseVisualStyleBackColor = true;
            this.DareBtn.Click += new System.EventHandler(this.DareBtn_Click);
            // 
            // ScoreTxtBox
            // 
            this.ScoreTxtBox.Enabled = false;
            this.ScoreTxtBox.Location = new System.Drawing.Point(1096, 292);
            this.ScoreTxtBox.Margin = new System.Windows.Forms.Padding(6);
            this.ScoreTxtBox.Name = "ScoreTxtBox";
            this.ScoreTxtBox.ReadOnly = true;
            this.ScoreTxtBox.Size = new System.Drawing.Size(292, 29);
            this.ScoreTxtBox.TabIndex = 3;
            // 
            // ScoreLbl
            // 
            this.ScoreLbl.AutoSize = true;
            this.ScoreLbl.Location = new System.Drawing.Point(1016, 303);
            this.ScoreLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ScoreLbl.Name = "ScoreLbl";
            this.ScoreLbl.Size = new System.Drawing.Size(70, 25);
            this.ScoreLbl.TabIndex = 4;
            this.ScoreLbl.Text = "Score:";
            // 
            // Score2Label
            // 
            this.Score2Label.AutoSize = true;
            this.Score2Label.Location = new System.Drawing.Point(1016, 496);
            this.Score2Label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Score2Label.Name = "Score2Label";
            this.Score2Label.Size = new System.Drawing.Size(70, 25);
            this.Score2Label.TabIndex = 6;
            this.Score2Label.Text = "Score:";
            // 
            // Score2TxtBox
            // 
            this.Score2TxtBox.Enabled = false;
            this.Score2TxtBox.Location = new System.Drawing.Point(1096, 485);
            this.Score2TxtBox.Margin = new System.Windows.Forms.Padding(6);
            this.Score2TxtBox.Name = "Score2TxtBox";
            this.Score2TxtBox.ReadOnly = true;
            this.Score2TxtBox.Size = new System.Drawing.Size(292, 29);
            this.Score2TxtBox.TabIndex = 5;
            // 
            // Player2Label
            // 
            this.Player2Label.AutoSize = true;
            this.Player2Label.Location = new System.Drawing.Point(1163, 439);
            this.Player2Label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Player2Label.Name = "Player2Label";
            this.Player2Label.Size = new System.Drawing.Size(83, 25);
            this.Player2Label.TabIndex = 7;
            this.Player2Label.Text = "Player 2";
            // 
            // ToggleGrid
            // 
            this.ToggleGrid.AutoSize = true;
            this.ToggleGrid.Location = new System.Drawing.Point(1016, 150);
            this.ToggleGrid.Name = "ToggleGrid";
            this.ToggleGrid.Size = new System.Drawing.Size(140, 29);
            this.ToggleGrid.TabIndex = 8;
            this.ToggleGrid.Text = "Toggle Grid";
            this.ToggleGrid.UseVisualStyleBackColor = true;
            this.ToggleGrid.CheckedChanged += new System.EventHandler(this.ToggleGrid_CheckedChanged);
            // 
            // Ctrl_Toggle
            // 
            this.Ctrl_Toggle.Location = new System.Drawing.Point(1016, 86);
            this.Ctrl_Toggle.Name = "Ctrl_Toggle";
            this.Ctrl_Toggle.Size = new System.Drawing.Size(376, 42);
            this.Ctrl_Toggle.TabIndex = 0;
            this.Ctrl_Toggle.TabStop = false;
            this.Ctrl_Toggle.Text = "Controls: Arrows";
            this.Ctrl_Toggle.UseVisualStyleBackColor = true;
            this.Ctrl_Toggle.Click += new System.EventHandler(this.Ctrl_Toggle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1163, 244);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Player 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1029, 353);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Skin:";
            // 
            // skin2label
            // 
            this.skin2label.AutoSize = true;
            this.skin2label.Location = new System.Drawing.Point(1029, 546);
            this.skin2label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.skin2label.Name = "skin2label";
            this.skin2label.Size = new System.Drawing.Size(57, 25);
            this.skin2label.TabIndex = 12;
            this.skin2label.Text = "Skin:";
            // 
            // skin2comboBox
            // 
            this.skin2comboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skin2comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skin2comboBox.Location = new System.Drawing.Point(1096, 539);
            this.skin2comboBox.Name = "skin2comboBox";
            this.skin2comboBox.Size = new System.Drawing.Size(292, 30);
            this.skin2comboBox.TabIndex = 0;
            this.skin2comboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.skin2comboBox.SelectedIndexChanged += new System.EventHandler(this.skin2comboBox_SelectedIndexChanged);
            // 
            // skin1comboBox
            // 
            this.skin1comboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skin1comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.skin1comboBox.Location = new System.Drawing.Point(1096, 346);
            this.skin1comboBox.Name = "skin1comboBox";
            this.skin1comboBox.Size = new System.Drawing.Size(292, 30);
            this.skin1comboBox.TabIndex = 0;
            this.skin1comboBox.TabStop = false;
            this.skin1comboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.skin1comboBox.SelectedIndexChanged += new System.EventHandler(this.skin1comboBox_SelectedIndexChanged);
            // 
            // SnakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1414, 764);
            this.Controls.Add(this.skin1comboBox);
            this.Controls.Add(this.skin2comboBox);
            this.Controls.Add(this.skin2label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ctrl_Toggle);
            this.Controls.Add(this.ToggleGrid);
            this.Controls.Add(this.Player2Label);
            this.Controls.Add(this.Score2Label);
            this.Controls.Add(this.Score2TxtBox);
            this.Controls.Add(this.ScoreLbl);
            this.Controls.Add(this.ScoreTxtBox);
            this.Controls.Add(this.DareBtn);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.GameCanvas);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SnakeForm";
            this.Text = "Snake";
            ((System.ComponentModel.ISupportInitialize)(this.GameCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GameCanvas;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Timer PauseTimer;
        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Button DareBtn;
        private System.Windows.Forms.TextBox ScoreTxtBox;
        private System.Windows.Forms.Label ScoreLbl;
        //sam
        private System.Windows.Forms.Label Score2Label;
        private System.Windows.Forms.TextBox Score2TxtBox;
        private System.Windows.Forms.Label Player2Label;
        private System.Windows.Forms.CheckBox ToggleGrid;
        private System.Windows.Forms.Button Ctrl_Toggle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label skin2label;
        private System.Windows.Forms.ComboBox skin2comboBox;
        private System.Windows.Forms.ComboBox skin1comboBox;

        //end
    }
}

