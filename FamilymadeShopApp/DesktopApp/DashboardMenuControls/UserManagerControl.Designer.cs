namespace DesktopApp.DashboardMenuControls
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
			dgvUsers = new DataGridView();
			cmbFilterType = new ComboBox();
			groupBox1 = new GroupBox();
			label2 = new Label();
			txtFilterSearch = new TextBox();
			label1 = new Label();
			progressBar = new ProgressBar();
			((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// dgvUsers
			// 
			dgvUsers.AllowUserToDeleteRows = false;
			dgvUsers.AllowUserToResizeRows = false;
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
			dgvUsers.Location = new Point(6, 89);
			dgvUsers.MultiSelect = false;
			dgvUsers.Name = "dgvUsers";
			dgvUsers.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvUsers.ShowEditingIcon = false;
			dgvUsers.Size = new Size(800, 375);
			dgvUsers.TabIndex = 0;
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
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(txtFilterSearch);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(cmbFilterType);
			groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			groupBox1.Location = new Point(6, 0);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(800, 83);
			groupBox1.TabIndex = 2;
			groupBox1.TabStop = false;
			groupBox1.Text = "Filter";
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
			txtFilterSearch.Font = new Font("Segoe UI", 10F);
			txtFilterSearch.Location = new Point(203, 52);
			txtFilterSearch.Name = "txtFilterSearch";
			txtFilterSearch.Size = new Size(251, 25);
			txtFilterSearch.TabIndex = 3;
			txtFilterSearch.TextChanged += txtFilterSearch_TextChanged;
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
			// progressBar
			// 
			progressBar.Location = new Point(349, 251);
			progressBar.Name = "progressBar";
			progressBar.Size = new Size(100, 23);
			progressBar.TabIndex = 3;
			// 
			// UserManagerControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(progressBar);
			Controls.Add(dgvUsers);
			Controls.Add(groupBox1);
			Name = "UserManagerControl";
			Size = new Size(812, 467);
			Load += UserManagerControl_Load;
			((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView dgvUsers;
		private ComboBox cmbFilterType;
		private GroupBox groupBox1;
		private Label label1;
		private ProgressBar progressBar;
		private Label label2;
		private TextBox txtFilterSearch;
	}
}
