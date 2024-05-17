namespace DesktopApp.PanelControls.UserManagerControls
{
	partial class AddAdminControl
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
			label1 = new Label();
			groupBox1 = new GroupBox();
			chbShowPassword = new CheckBox();
			btnAddAdmin = new Button();
			txtAdminName = new TextBox();
			label4 = new Label();
			txtAdminPassword = new TextBox();
			label3 = new Label();
			cmbAdminRole = new ComboBox();
			txtAdminEmail = new TextBox();
			label2 = new Label();
			btnBack = new Button();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.None;
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 10F);
			label1.Location = new Point(273, 49);
			label1.Name = "label1";
			label1.Size = new Size(48, 19);
			label1.TabIndex = 0;
			label1.Text = "Name:";
			// 
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupBox1.Controls.Add(chbShowPassword);
			groupBox1.Controls.Add(btnAddAdmin);
			groupBox1.Controls.Add(txtAdminName);
			groupBox1.Controls.Add(label4);
			groupBox1.Controls.Add(txtAdminPassword);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(cmbAdminRole);
			groupBox1.Controls.Add(txtAdminEmail);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(label1);
			groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBox1.Location = new Point(14, 49);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(784, 403);
			groupBox1.TabIndex = 13;
			groupBox1.TabStop = false;
			groupBox1.Text = "Add Admin";
			// 
			// chbShowPassword
			// 
			chbShowPassword.Anchor = AnchorStyles.None;
			chbShowPassword.AutoSize = true;
			chbShowPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
			chbShowPassword.Location = new Point(376, 223);
			chbShowPassword.Name = "chbShowPassword";
			chbShowPassword.Size = new Size(108, 19);
			chbShowPassword.TabIndex = 15;
			chbShowPassword.Text = "Show password";
			chbShowPassword.UseVisualStyleBackColor = true;
			chbShowPassword.CheckedChanged += chbShowPassword_CheckedChanged;
			// 
			// btnAddAdmin
			// 
			btnAddAdmin.Anchor = AnchorStyles.None;
			btnAddAdmin.BackColor = Color.FromArgb(170, 197, 175);
			btnAddAdmin.FlatAppearance.BorderColor = Color.White;
			btnAddAdmin.FlatStyle = FlatStyle.Flat;
			btnAddAdmin.ForeColor = Color.White;
			btnAddAdmin.Location = new Point(273, 316);
			btnAddAdmin.Name = "btnAddAdmin";
			btnAddAdmin.Size = new Size(211, 32);
			btnAddAdmin.TabIndex = 14;
			btnAddAdmin.Text = "Add Admin";
			btnAddAdmin.UseVisualStyleBackColor = false;
			btnAddAdmin.Click += btnAddAdmin_Click;
			// 
			// txtAdminName
			// 
			txtAdminName.Anchor = AnchorStyles.None;
			txtAdminName.Font = new Font("Segoe UI", 10F);
			txtAdminName.Location = new Point(273, 71);
			txtAdminName.Name = "txtAdminName";
			txtAdminName.Size = new Size(211, 25);
			txtAdminName.TabIndex = 5;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.None;
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 10F);
			label4.Location = new Point(273, 241);
			label4.Name = "label4";
			label4.Size = new Size(38, 19);
			label4.TabIndex = 3;
			label4.Text = "Role:";
			// 
			// txtAdminPassword
			// 
			txtAdminPassword.Anchor = AnchorStyles.None;
			txtAdminPassword.Font = new Font("Segoe UI", 10F);
			txtAdminPassword.Location = new Point(273, 192);
			txtAdminPassword.Name = "txtAdminPassword";
			txtAdminPassword.PasswordChar = '*';
			txtAdminPassword.Size = new Size(211, 25);
			txtAdminPassword.TabIndex = 7;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.None;
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 10F);
			label3.Location = new Point(273, 170);
			label3.Name = "label3";
			label3.Size = new Size(70, 19);
			label3.TabIndex = 2;
			label3.Text = "Password:";
			// 
			// cmbAdminRole
			// 
			cmbAdminRole.Anchor = AnchorStyles.None;
			cmbAdminRole.Font = new Font("Segoe UI", 10F);
			cmbAdminRole.FormattingEnabled = true;
			cmbAdminRole.Location = new Point(273, 263);
			cmbAdminRole.Name = "cmbAdminRole";
			cmbAdminRole.Size = new Size(211, 25);
			cmbAdminRole.TabIndex = 4;
			// 
			// txtAdminEmail
			// 
			txtAdminEmail.Anchor = AnchorStyles.None;
			txtAdminEmail.Font = new Font("Segoe UI", 10F);
			txtAdminEmail.Location = new Point(273, 131);
			txtAdminEmail.Name = "txtAdminEmail";
			txtAdminEmail.Size = new Size(211, 25);
			txtAdminEmail.TabIndex = 6;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.None;
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10F);
			label2.Location = new Point(273, 109);
			label2.Name = "label2";
			label2.Size = new Size(44, 19);
			label2.TabIndex = 1;
			label2.Text = "Email:";
			// 
			// btnBack
			// 
			btnBack.BackColor = Color.FromArgb(170, 197, 175);
			btnBack.FlatAppearance.BorderColor = Color.White;
			btnBack.FlatStyle = FlatStyle.Flat;
			btnBack.ForeColor = Color.White;
			btnBack.Location = new Point(14, 16);
			btnBack.Name = "btnBack";
			btnBack.Size = new Size(82, 26);
			btnBack.TabIndex = 12;
			btnBack.Text = "> Back";
			btnBack.UseVisualStyleBackColor = false;
			btnBack.Click += btnBack_Click;
			// 
			// AddAdminControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(groupBox1);
			Controls.Add(btnBack);
			Name = "AddAdminControl";
			Size = new Size(812, 467);
			Load += AddAdminControl_Load;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Label label1;
		private GroupBox groupBox1;
		private Button btnBack;
		private Label label3;
		private Label label2;
		private TextBox txtAdminPassword;
		private TextBox txtAdminEmail;
		private TextBox txtAdminName;
		private ComboBox cmbAdminRole;
		private Label label4;
		private Button btnAddAdmin;
		private CheckBox chbShowPassword;
	}
}
