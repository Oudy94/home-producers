namespace DesktopApp.MainControls
{
	partial class PanelControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMenu = new Panel();
            btnLogout = new Button();
            btnOrderManager = new Button();
            btnProductManager = new Button();
            btnUserManager = new Button();
            btnHome = new Button();
            pnlLogo = new Panel();
            pictureBox1 = new PictureBox();
            pnlTitle = new Panel();
            lblGreetAdmin = new Label();
            lblHeaderTitle = new Label();
            pnlContent = new Panel();
            pnlMenu.SuspendLayout();
            pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlTitle.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.Silver;
            pnlMenu.Controls.Add(btnLogout);
            pnlMenu.Controls.Add(btnOrderManager);
            pnlMenu.Controls.Add(btnProductManager);
            pnlMenu.Controls.Add(btnUserManager);
            pnlMenu.Controls.Add(btnHome);
            pnlMenu.Controls.Add(pnlLogo);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(88, 550);
            pnlMenu.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.ForeColor = Color.Gainsboro;
            btnLogout.Image = Properties.Resources.log_out;
            btnLogout.Location = new Point(0, 500);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(88, 50);
            btnLogout.TabIndex = 5;
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnOrderManager
            // 
            btnOrderManager.Dock = DockStyle.Top;
            btnOrderManager.FlatAppearance.BorderSize = 0;
            btnOrderManager.FlatStyle = FlatStyle.Flat;
            btnOrderManager.ForeColor = Color.Gainsboro;
            btnOrderManager.Image = Properties.Resources.orders;
            btnOrderManager.Location = new Point(0, 263);
            btnOrderManager.Name = "btnOrderManager";
            btnOrderManager.Size = new Size(88, 60);
            btnOrderManager.TabIndex = 4;
            btnOrderManager.UseVisualStyleBackColor = true;
            btnOrderManager.Click += btnOrders_Click;
            // 
            // btnProductManager
            // 
            btnProductManager.Dock = DockStyle.Top;
            btnProductManager.FlatAppearance.BorderSize = 0;
            btnProductManager.FlatStyle = FlatStyle.Flat;
            btnProductManager.ForeColor = Color.Gainsboro;
            btnProductManager.Image = Properties.Resources.products;
            btnProductManager.Location = new Point(0, 203);
            btnProductManager.Name = "btnProductManager";
            btnProductManager.Size = new Size(88, 60);
            btnProductManager.TabIndex = 3;
            btnProductManager.UseVisualStyleBackColor = true;
            btnProductManager.Click += btnProducts_Click;
            // 
            // btnUserManager
            // 
            btnUserManager.Dock = DockStyle.Top;
            btnUserManager.FlatAppearance.BorderSize = 0;
            btnUserManager.FlatStyle = FlatStyle.Flat;
            btnUserManager.ForeColor = Color.Gainsboro;
            btnUserManager.Image = Properties.Resources.users;
            btnUserManager.Location = new Point(0, 143);
            btnUserManager.Name = "btnUserManager";
            btnUserManager.Size = new Size(88, 60);
            btnUserManager.TabIndex = 2;
            btnUserManager.UseVisualStyleBackColor = true;
            btnUserManager.Click += btnUsers_Click;
            // 
            // btnHome
            // 
            btnHome.Dock = DockStyle.Top;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.ForeColor = Color.Gainsboro;
            btnHome.Image = Properties.Resources.home;
            btnHome.Location = new Point(0, 83);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(88, 60);
            btnHome.TabIndex = 1;
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // pnlLogo
            // 
            pnlLogo.BackColor = Color.FromArgb(135, 162, 140);
            pnlLogo.Controls.Add(pictureBox1);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(88, 83);
            pnlLogo.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.familymade_shop_small;
            pictureBox1.Location = new Point(3, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(79, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.FromArgb(170, 197, 175);
            pnlTitle.Controls.Add(lblGreetAdmin);
            pnlTitle.Controls.Add(lblHeaderTitle);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(88, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(812, 83);
            pnlTitle.TabIndex = 1;
            // 
            // lblGreetAdmin
            // 
            lblGreetAdmin.AutoSize = true;
            lblGreetAdmin.Dock = DockStyle.Right;
            lblGreetAdmin.Font = new Font("Segoe UI", 12F);
            lblGreetAdmin.ForeColor = Color.White;
            lblGreetAdmin.Location = new Point(731, 0);
            lblGreetAdmin.Name = "lblGreetAdmin";
            lblGreetAdmin.Size = new Size(81, 21);
            lblGreetAdmin.TabIndex = 1;
            lblGreetAdmin.Text = "Welcome, ";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 16F);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(6, 26);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(118, 30);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Dashboard";
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(88, 83);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(812, 467);
            pnlContent.TabIndex = 2;
            // 
            // PanelControl
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(pnlContent);
            Controls.Add(pnlTitle);
            Controls.Add(pnlMenu);
            Name = "PanelControl";
            Size = new Size(900, 550);
            Load += DashboardControl_Load;
            pnlMenu.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
		private Button btnHome;
		private Panel pnlLogo;
		private Button btnOrderManager;
		private Button btnProductManager;
		private Button btnUserManager;
		private Panel pnlTitle;
		private Label lblHeaderTitle;
		private PictureBox pictureBox1;
		private Panel pnlContent;
		private Button btnLogout;
		private Label lblGreetAdmin;
	}
}
