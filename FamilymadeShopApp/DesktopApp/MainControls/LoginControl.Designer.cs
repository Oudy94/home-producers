namespace DesktopApp.MainControls
{
	partial class LoginControl
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
			txtLoginEmail = new TextBox();
			label1 = new Label();
			label2 = new Label();
			txtLoginPassword = new TextBox();
			btnLogin = new Button();
			chbLoginShowPassword = new CheckBox();
			pictureBox1 = new PictureBox();
			panel1 = new Panel();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// txtLoginEmail
			// 
			txtLoginEmail.Anchor = AnchorStyles.None;
			txtLoginEmail.BackColor = SystemColors.Window;
			txtLoginEmail.BorderStyle = BorderStyle.None;
			txtLoginEmail.Font = new Font("Segoe UI", 14F);
			txtLoginEmail.Location = new Point(335, 231);
			txtLoginEmail.Name = "txtLoginEmail";
			txtLoginEmail.Size = new Size(236, 25);
			txtLoginEmail.TabIndex = 0;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.None;
			label1.AutoSize = true;
			label1.BackColor = SystemColors.Control;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.ForeColor = Color.FromArgb(135, 162, 140);
			label1.Location = new Point(335, 207);
			label1.Name = "label1";
			label1.Size = new Size(57, 21);
			label1.TabIndex = 2;
			label1.Text = "Email:";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.None;
			label2.AutoSize = true;
			label2.BackColor = SystemColors.Control;
			label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label2.ForeColor = Color.FromArgb(135, 162, 140);
			label2.Location = new Point(335, 267);
			label2.Name = "label2";
			label2.Size = new Size(86, 21);
			label2.TabIndex = 4;
			label2.Text = "Password:";
			// 
			// txtLoginPassword
			// 
			txtLoginPassword.Anchor = AnchorStyles.None;
			txtLoginPassword.BackColor = SystemColors.Window;
			txtLoginPassword.BorderStyle = BorderStyle.None;
			txtLoginPassword.Font = new Font("Segoe UI", 14F);
			txtLoginPassword.ForeColor = SystemColors.WindowText;
			txtLoginPassword.Location = new Point(335, 291);
			txtLoginPassword.Name = "txtLoginPassword";
			txtLoginPassword.PasswordChar = '*';
			txtLoginPassword.Size = new Size(236, 25);
			txtLoginPassword.TabIndex = 3;
			// 
			// btnLogin
			// 
			btnLogin.Anchor = AnchorStyles.None;
			btnLogin.BackColor = Color.FromArgb(170, 197, 175);
			btnLogin.FlatAppearance.BorderSize = 0;
			btnLogin.FlatStyle = FlatStyle.Flat;
			btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			btnLogin.ForeColor = Color.White;
			btnLogin.Location = new Point(335, 371);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(236, 33);
			btnLogin.TabIndex = 5;
			btnLogin.Text = "LOGIN";
			btnLogin.UseVisualStyleBackColor = false;
			btnLogin.Click += btnLogin_Click;
			// 
			// chbLoginShowPassword
			// 
			chbLoginShowPassword.Anchor = AnchorStyles.None;
			chbLoginShowPassword.AutoSize = true;
			chbLoginShowPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			chbLoginShowPassword.ForeColor = Color.FromArgb(135, 162, 140);
			chbLoginShowPassword.Location = new Point(439, 321);
			chbLoginShowPassword.Name = "chbLoginShowPassword";
			chbLoginShowPassword.Size = new Size(132, 23);
			chbLoginShowPassword.TabIndex = 6;
			chbLoginShowPassword.Text = "Show password";
			chbLoginShowPassword.UseVisualStyleBackColor = true;
			chbLoginShowPassword.CheckedChanged += chbLoginShowPassword_CheckedChanged;
			// 
			// pictureBox1
			// 
			pictureBox1.Anchor = AnchorStyles.None;
			pictureBox1.Image = Properties.Resources.logo2;
			pictureBox1.Location = new Point(294, 31);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(317, 106);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 7;
			pictureBox1.TabStop = false;
			// 
			// panel1
			// 
			panel1.Controls.Add(txtLoginEmail);
			panel1.Controls.Add(chbLoginShowPassword);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(btnLogin);
			panel1.Controls.Add(txtLoginPassword);
			panel1.Controls.Add(label2);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(900, 580);
			panel1.TabIndex = 8;
			// 
			// LoginControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(pictureBox1);
			Controls.Add(panel1);
			Name = "LoginControl";
			Size = new Size(900, 580);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TextBox txtLoginEmail;
		private Label label1;
		private Label label2;
		private TextBox txtLoginPassword;
		private Button btnLogin;
		private CheckBox chbLoginShowPassword;
		private PictureBox pictureBox1;
		private Panel panel1;
	}
}
