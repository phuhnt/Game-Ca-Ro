namespace GameCaro
{
    partial class fmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMenu));
            this.btnExit = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnPvP = new System.Windows.Forms.Button();
            this.btnPvC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Orange;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(363, 461);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(184, 54);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.Orange;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(363, 366);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(187, 56);
            this.button3.TabIndex = 2;
            this.button3.Text = "Play Again!";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // btnPvP
            // 
            this.btnPvP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPvP.BackColor = System.Drawing.Color.Orange;
            this.btnPvP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPvP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnPvP.ForeColor = System.Drawing.Color.White;
            this.btnPvP.Location = new System.Drawing.Point(363, 274);
            this.btnPvP.Margin = new System.Windows.Forms.Padding(4);
            this.btnPvP.Name = "btnPvP";
            this.btnPvP.Size = new System.Drawing.Size(187, 59);
            this.btnPvP.TabIndex = 1;
            this.btnPvP.Text = "Player vs Player";
            this.btnPvP.UseVisualStyleBackColor = false;
            this.btnPvP.Click += new System.EventHandler(this.btnPvP_Click);
            // 
            // btnPvC
            // 
            this.btnPvC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPvC.BackColor = System.Drawing.Color.Orange;
            this.btnPvC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPvC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnPvC.ForeColor = System.Drawing.Color.White;
            this.btnPvC.Location = new System.Drawing.Point(363, 188);
            this.btnPvC.Margin = new System.Windows.Forms.Padding(4);
            this.btnPvC.Name = "btnPvC";
            this.btnPvC.Size = new System.Drawing.Size(187, 54);
            this.btnPvC.TabIndex = 0;
            this.btnPvC.Text = "Player vs Computer";
            this.btnPvC.UseVisualStyleBackColor = false;
            this.btnPvC.Click += new System.EventHandler(this.btnPvC_Click);
            // 
            // fmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(247)))), ((int)(((byte)(239)))));
            this.BackgroundImage = global::GameCaro.Properties.Resources._86839;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(913, 594);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnPvC);
            this.Controls.Add(this.btnPvP);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(935, 645);
            this.Name = "fmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Caro - 13520630 - Huynh Ngoc Thanh Phu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnPvP;
        private System.Windows.Forms.Button btnPvC;
    }
}