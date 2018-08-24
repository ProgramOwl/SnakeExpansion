namespace Snake
{
    partial class Startscreen
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
            this.btn1Player = new System.Windows.Forms.Button();
            this.btn2Player = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn1Player
            // 
            this.btn1Player.Location = new System.Drawing.Point(320, 114);
            this.btn1Player.Name = "btn1Player";
            this.btn1Player.Size = new System.Drawing.Size(142, 62);
            this.btn1Player.TabIndex = 0;
            this.btn1Player.Text = "1 Player";
            this.btn1Player.UseVisualStyleBackColor = true;
            this.btn1Player.Click += new System.EventHandler(this.btn1Player_Click);
            // 
            // btn2Player
            // 
            this.btn2Player.Location = new System.Drawing.Point(320, 211);
            this.btn2Player.Name = "btn2Player";
            this.btn2Player.Size = new System.Drawing.Size(142, 62);
            this.btn2Player.TabIndex = 1;
            this.btn2Player.Text = "2 Player";
            this.btn2Player.UseVisualStyleBackColor = true;
            this.btn2Player.Click += new System.EventHandler(this.btn2Player_Click);
            // 
            // Startscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn2Player);
            this.Controls.Add(this.btn1Player);
            this.Name = "Startscreen";
            this.Text = "Game of Snake";
            this.Load += new System.EventHandler(this.Startscreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn1Player;
        private System.Windows.Forms.Button btn2Player;
    }
}