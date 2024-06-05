namespace DesktopApp.PanelControls
{
	partial class UserManagerControl
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
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			pnlUserManager = new Panel();
			btnRefreshData = new Button();
			btnAddUser = new Button();
			btnSaveData = new Button();
			lblMaxPage = new Label();
			txtCurrentPage = new TextBox();
			btnNextPage = new Button();
			btnPreviousPage = new Button();
			progressBar = new ProgressBar();
			dgvUsers = new DataGridView();
			groupBox1 = new GroupBox();
			btnFilterSearch = new Button();
			label2 = new Label();
			txtFilterSearch = new TextBox();
			label1 = new Label();
			cmbFilterType = new ComboBox();
			pnlUserManager.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// pnlUserManager
			// 
			pnlUserManager.Controls.Add(btnRefreshData);
			pnlUserManager.Controls.Add(btnAddUser);
			pnlUserManager.Controls.Add(btnSaveData);
			pnlUserManager.Controls.Add(lblMaxPage);
			pnlUserManager.Controls.Add(txtCurrentPage);
			pnlUserManager.Controls.Add(btnNextPage);
			pnlUserManager.Controls.Add(btnPreviousPage);
			pnlUserManager.Controls.Add(progressBar);
			pnlUserManager.Controls.Add(dgvUsers);
			pnlUserManager.Controls.Add(groupBox1);
			pnlUserManager.Dock = DockStyle.Fill;
			pnlUserManager.Location = new Point(0, 0);
			pnlUserManager.Name = "pnlUserManager";
			pnlUserManager.Size = new Size(812, 467);
			pnlUserManager.TabIndex = 0;
			// 
			// btnRefreshData
			// 
			btnRefreshData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnRefreshData.BackColor = Color.FromArgb(170, 197, 175);
			btnRefreshData.FlatAppearance.BorderColor = Color.White;
			btnRefreshData.FlatStyle = FlatStyle.Flat;
			btnRefreshData.Image = Properties.Resources.refresh_icon;
			btnRefreshData.Location = new Point(758, 64);
			btnRefreshData.Name = "btnRefreshData";
			btnRefreshData.Size = new Size(45, 25);
			btnRefreshData.TabIndex = 6;
			btnRefreshData.UseVisualStyleBackColor = false;
			btnRefreshData.Click += btnRefreshData_Click;
			// 
			// btnAddUser
			// 
			btnAddUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnAddUser.BackColor = Color.FromArgb(170, 197, 175);
			btnAddUser.FlatAppearance.BorderColor = Color.White;
			btnAddUser.FlatStyle = FlatStyle.Flat;
			btnAddUser.ForeColor = Color.White;
			btnAddUser.Location = new Point(623, 434);
			btnAddUser.Name = "btnAddUser";
			btnAddUser.Size = new Size(82, 26);
			btnAddUser.TabIndex = 36;
			btnAddUser.Text = "Add User";
			btnAddUser.UseVisualStyleBackColor = false;
			btnAddUser.Click += btnAddUser_Click;
			// 
			// btnSaveData
			// 
			btnSaveData.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnSaveData.BackColor = Color.FromArgb(170, 197, 175);
			btnSaveData.FlatAppearance.BorderColor = Color.White;
			btnSaveData.FlatStyle = FlatStyle.Flat;
			btnSaveData.ForeColor = Color.White;
			btnSaveData.Location = new Point(711, 434);
			btnSaveData.Name = "btnSaveData";
			btnSaveData.Size = new Size(95, 26);
			btnSaveData.TabIndex = 35;
			btnSaveData.Text = "Save Changes";
			btnSaveData.UseVisualStyleBackColor = false;
			btnSaveData.EnabledChanged += btn_EnabledChanged;
			btnSaveData.Click += btnSaveData_Click;
			// 
			// lblMaxPage
			// 
			lblMaxPage.Anchor = AnchorStyles.Bottom;
			lblMaxPage.AutoSize = true;
			lblMaxPage.Font = new Font("Segoe UI", 10F);
			lblMaxPage.Location = new Point(392, 434);
			lblMaxPage.Name = "lblMaxPage";
			lblMaxPage.Size = new Size(49, 19);
			lblMaxPage.TabIndex = 34;
			lblMaxPage.Text = "of 999";
			// 
			// txtCurrentPage
			// 
			txtCurrentPage.Anchor = AnchorStyles.Bottom;
			txtCurrentPage.Font = new Font("Segoe UI", 10F);
			txtCurrentPage.Location = new Point(341, 431);
			txtCurrentPage.Name = "txtCurrentPage";
			txtCurrentPage.Size = new Size(50, 25);
			txtCurrentPage.TabIndex = 33;
			txtCurrentPage.Text = "1";
			txtCurrentPage.TextAlign = HorizontalAlignment.Center;
			txtCurrentPage.KeyPress += txtCurrentPage_KeyPress;
			txtCurrentPage.KeyDown += txtCurrentPage_KeyDown;
			txtCurrentPage.TextChanged += txtCurrentPage_TextChanged;
			txtCurrentPage.Leave += txtCurrentPage_Leave;
			// 
			// btnNextPage
			// 
			btnNextPage.Anchor = AnchorStyles.Bottom;
			btnNextPage.BackColor = Color.FromArgb(170, 197, 175);
			btnNextPage.FlatStyle = FlatStyle.Flat;
			btnNextPage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnNextPage.ForeColor = Color.White;
			btnNextPage.Location = new Point(442, 427);
			btnNextPage.Name = "btnNextPage";
			btnNextPage.Size = new Size(29, 30);
			btnNextPage.TabIndex = 32;
			btnNextPage.Text = ">";
			btnNextPage.UseVisualStyleBackColor = false;
			btnNextPage.EnabledChanged += btn_EnabledChanged;
			btnNextPage.Click += btnNextPage_Click;
			// 
			// btnPreviousPage
			// 
			btnPreviousPage.Anchor = AnchorStyles.Bottom;
			btnPreviousPage.BackColor = Color.FromArgb(170, 197, 175);
			btnPreviousPage.FlatStyle = FlatStyle.Flat;
			btnPreviousPage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnPreviousPage.ForeColor = Color.White;
			btnPreviousPage.Location = new Point(306, 427);
			btnPreviousPage.Name = "btnPreviousPage";
			btnPreviousPage.Size = new Size(29, 30);
			btnPreviousPage.TabIndex = 31;
			btnPreviousPage.Text = "<";
			btnPreviousPage.UseVisualStyleBackColor = false;
			btnPreviousPage.EnabledChanged += btn_EnabledChanged;
			btnPreviousPage.Click += btnPreviousPage_Click;
			// 
			// progressBar
			// 
			progressBar.ForeColor = Color.LimeGreen;
			progressBar.Location = new Point(331, 242);
			progressBar.Name = "progressBar";
			progressBar.Size = new Size(140, 23);
			progressBar.TabIndex = 30;
			// 
			// dgvUsers
			// 
			dgvUsers.AllowUserToAddRows = false;
			dgvUsers.AllowUserToDeleteRows = false;
			dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvUsers.BackgroundColor = SystemColors.ControlLight;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = Color.FromArgb(170, 197, 175);
			dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(135, 162, 140);
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			dgvUsers.ColumnHeadersHeight = 37;
			dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvUsers.EnableHeadersVisualStyles = false;
			dgvUsers.Location = new Point(9, 92);
			dgvUsers.MultiSelect = false;
			dgvUsers.Name = "dgvUsers";
			dgvUsers.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvUsers.ShowEditingIcon = false;
			dgvUsers.Size = new Size(794, 326);
			dgvUsers.TabIndex = 28;
			dgvUsers.CellValueChanged += dgvUsers_CellValueChanged;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(btnFilterSearch);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(txtFilterSearch);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(cmbFilterType);
			groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			groupBox1.Location = new Point(6, 6);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(506, 83);
			groupBox1.TabIndex = 29;
			groupBox1.TabStop = false;
			groupBox1.Text = "Filter";
			// 
			// btnFilterSearch
			// 
			btnFilterSearch.BackColor = Color.FromArgb(170, 197, 175);
			btnFilterSearch.FlatStyle = FlatStyle.Flat;
			btnFilterSearch.Image = Properties.Resources.search_icon;
			btnFilterSearch.Location = new Point(451, 52);
			btnFilterSearch.Name = "btnFilterSearch";
			btnFilterSearch.Size = new Size(45, 25);
			btnFilterSearch.TabIndex = 5;
			btnFilterSearch.UseVisualStyleBackColor = false;
			btnFilterSearch.Click += btnFilterSearch_Click;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10F);
			label2.Location = new Point(203, 26);
			label2.Name = "label2";
			label2.Size = new Size(52, 19);
			label2.TabIndex = 4;
			label2.Text = "Search:";
			// 
			// txtFilterSearch
			// 
			txtFilterSearch.BorderStyle = BorderStyle.FixedSingle;
			txtFilterSearch.Font = new Font("Segoe UI", 10F);
			txtFilterSearch.ForeColor = SystemColors.ControlText;
			txtFilterSearch.Location = new Point(203, 52);
			txtFilterSearch.Name = "txtFilterSearch";
			txtFilterSearch.Size = new Size(251, 25);
			txtFilterSearch.TabIndex = 3;
			txtFilterSearch.KeyPress += txtFilterSearch_KeyPress;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 10F);
			label1.Location = new Point(6, 26);
			label1.Name = "label1";
			label1.Size = new Size(40, 19);
			label1.TabIndex = 2;
			label1.Text = "Type:";
			// 
			// cmbFilterType
			// 
			cmbFilterType.Font = new Font("Segoe UI", 10F);
			cmbFilterType.FormattingEnabled = true;
			cmbFilterType.Items.AddRange(new object[] { "Admins", "Customers" });
			cmbFilterType.Location = new Point(6, 52);
			cmbFilterType.Name = "cmbFilterType";
			cmbFilterType.Size = new Size(140, 25);
			cmbFilterType.TabIndex = 1;
			cmbFilterType.SelectedIndexChanged += cmbFilterType_SelectedIndexChanged;
			// 
			// UserManagerControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(pnlUserManager);
			Name = "UserManagerControl";
			Size = new Size(812, 467);
			Load += UserManagerControl_Load;
			pnlUserManager.ResumeLayout(false);
			pnlUserManager.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel pnlUserManager;
		private Button btnAddUser;
		private Button btnSaveData;
		private Label lblMaxPage;
		private TextBox txtCurrentPage;
		private Button btnNextPage;
		private Button btnPreviousPage;
		private ProgressBar progressBar;
		private DataGridView dgvUsers;
		private GroupBox groupBox1;
		private Button btnFilterSearch;
		private Label label2;
		private TextBox txtFilterSearch;
		private Label label1;
		private ComboBox cmbFilterType;
		private Button btnRefreshData;
	}
}
