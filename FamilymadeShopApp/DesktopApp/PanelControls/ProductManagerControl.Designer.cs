namespace DesktopApp.PanelControls
{
	partial class ProductManagerControl
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
			pnlProductManager = new Panel();
			btnAddProduct = new Button();
			btnSaveData = new Button();
			lblMaxPage = new Label();
			txtCurrentPage = new TextBox();
			btnNextPage = new Button();
			btnPreviousPage = new Button();
			progressBar = new ProgressBar();
			dgvProducts = new DataGridView();
			btnRefreshData = new Button();
			groupBox1 = new GroupBox();
			btnFilterSearch = new Button();
			label2 = new Label();
			txtFilterSearch = new TextBox();
			label1 = new Label();
			cmbFilterCategory = new ComboBox();
			pnlProductManager.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// pnlProductManager
			// 
			pnlProductManager.Controls.Add(btnAddProduct);
			pnlProductManager.Controls.Add(btnSaveData);
			pnlProductManager.Controls.Add(lblMaxPage);
			pnlProductManager.Controls.Add(txtCurrentPage);
			pnlProductManager.Controls.Add(btnNextPage);
			pnlProductManager.Controls.Add(btnPreviousPage);
			pnlProductManager.Controls.Add(progressBar);
			pnlProductManager.Controls.Add(dgvProducts);
			pnlProductManager.Controls.Add(btnRefreshData);
			pnlProductManager.Controls.Add(groupBox1);
			pnlProductManager.Dock = DockStyle.Fill;
			pnlProductManager.Location = new Point(0, 0);
			pnlProductManager.Name = "pnlProductManager";
			pnlProductManager.Size = new Size(812, 467);
			pnlProductManager.TabIndex = 0;
			// 
			// btnAddProduct
			// 
			btnAddProduct.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnAddProduct.BackColor = Color.FromArgb(170, 197, 175);
			btnAddProduct.FlatAppearance.BorderColor = Color.White;
			btnAddProduct.FlatStyle = FlatStyle.Flat;
			btnAddProduct.ForeColor = Color.White;
			btnAddProduct.Location = new Point(616, 434);
			btnAddProduct.Name = "btnAddProduct";
			btnAddProduct.Size = new Size(89, 26);
			btnAddProduct.TabIndex = 42;
			btnAddProduct.Text = "Add Product";
			btnAddProduct.UseVisualStyleBackColor = false;
			btnAddProduct.Click += btnAddProduct_Click;
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
			btnSaveData.TabIndex = 41;
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
			lblMaxPage.TabIndex = 40;
			lblMaxPage.Text = "of 999";
			// 
			// txtCurrentPage
			// 
			txtCurrentPage.Anchor = AnchorStyles.Bottom;
			txtCurrentPage.Font = new Font("Segoe UI", 10F);
			txtCurrentPage.Location = new Point(341, 431);
			txtCurrentPage.Name = "txtCurrentPage";
			txtCurrentPage.Size = new Size(50, 25);
			txtCurrentPage.TabIndex = 39;
			txtCurrentPage.Text = "1";
			txtCurrentPage.TextAlign = HorizontalAlignment.Center;
			txtCurrentPage.KeyPress += txtCurrentPage_KeyPress;
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
			btnNextPage.TabIndex = 38;
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
			btnPreviousPage.TabIndex = 37;
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
			progressBar.TabIndex = 33;
			// 
			// dgvProducts
			// 
			dgvProducts.AllowUserToAddRows = false;
			dgvProducts.AllowUserToDeleteRows = false;
			dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvProducts.BackgroundColor = SystemColors.ControlLight;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = Color.FromArgb(170, 197, 175);
			dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(135, 162, 140);
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			dgvProducts.ColumnHeadersHeight = 37;
			dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvProducts.EnableHeadersVisualStyles = false;
			dgvProducts.Location = new Point(9, 92);
			dgvProducts.MultiSelect = false;
			dgvProducts.Name = "dgvProducts";
			dgvProducts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvProducts.ShowEditingIcon = false;
			dgvProducts.Size = new Size(794, 326);
			dgvProducts.TabIndex = 32;
			dgvProducts.CellValueChanged += dgvProducts_CellValueChanged;
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
			btnRefreshData.TabIndex = 30;
			btnRefreshData.UseVisualStyleBackColor = false;
			btnRefreshData.Click += btnRefreshData_Click;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(btnFilterSearch);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(txtFilterSearch);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(cmbFilterCategory);
			groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			groupBox1.Location = new Point(6, 6);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(506, 83);
			groupBox1.TabIndex = 31;
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
			label1.Size = new Size(68, 19);
			label1.TabIndex = 2;
			label1.Text = "Category:";
			// 
			// cmbFilterCategory
			// 
			cmbFilterCategory.Font = new Font("Segoe UI", 10F);
			cmbFilterCategory.FormattingEnabled = true;
			cmbFilterCategory.Location = new Point(6, 52);
			cmbFilterCategory.Name = "cmbFilterCategory";
			cmbFilterCategory.Size = new Size(140, 25);
			cmbFilterCategory.TabIndex = 1;
			cmbFilterCategory.SelectedIndexChanged += cmbFilterCategory_SelectedIndexChanged;
			// 
			// ProductManagerControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(pnlProductManager);
			Name = "ProductManagerControl";
			Size = new Size(812, 467);
			Load += ProductManagerControl_Load;
			pnlProductManager.ResumeLayout(false);
			pnlProductManager.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel pnlProductManager;
		private Button btnRefreshData;
		private GroupBox groupBox1;
		private Button btnFilterSearch;
		private Label label2;
		private TextBox txtFilterSearch;
		private Label label1;
		private ComboBox cmbFilterCategory;
		private ProgressBar progressBar;
		private DataGridView dgvProducts;
		private Button btnAddProduct;
		private Button btnSaveData;
		private Label lblMaxPage;
		private TextBox txtCurrentPage;
		private Button btnNextPage;
		private Button btnPreviousPage;
	}
}
