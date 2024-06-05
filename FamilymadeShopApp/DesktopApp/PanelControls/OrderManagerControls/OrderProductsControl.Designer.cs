namespace DesktopApp.PanelControls.OrderManagerControls
{
	partial class OrderProductsControl
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
			btnBack = new Button();
			progressBar = new ProgressBar();
			dgvOrderProducts = new DataGridView();
			lblOrderId = new Label();
			((System.ComponentModel.ISupportInitialize)dgvOrderProducts).BeginInit();
			SuspendLayout();
			// 
			// btnBack
			// 
			btnBack.BackColor = Color.FromArgb(170, 197, 175);
			btnBack.FlatAppearance.BorderColor = Color.White;
			btnBack.FlatStyle = FlatStyle.Flat;
			btnBack.ForeColor = Color.White;
			btnBack.Location = new Point(14, 15);
			btnBack.Name = "btnBack";
			btnBack.Size = new Size(82, 26);
			btnBack.TabIndex = 15;
			btnBack.Text = "> Back";
			btnBack.UseVisualStyleBackColor = false;
			btnBack.Click += btnBack_Click;
			// 
			// progressBar
			// 
			progressBar.ForeColor = Color.LimeGreen;
			progressBar.Location = new Point(335, 268);
			progressBar.Name = "progressBar";
			progressBar.Size = new Size(140, 23);
			progressBar.TabIndex = 36;
			// 
			// dgvOrderProducts
			// 
			dgvOrderProducts.AllowUserToAddRows = false;
			dgvOrderProducts.AllowUserToDeleteRows = false;
			dgvOrderProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvOrderProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvOrderProducts.BackgroundColor = SystemColors.ControlLight;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = Color.FromArgb(170, 197, 175);
			dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
			dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(135, 162, 140);
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			dgvOrderProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			dgvOrderProducts.ColumnHeadersHeight = 37;
			dgvOrderProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvOrderProducts.EnableHeadersVisualStyles = false;
			dgvOrderProducts.Location = new Point(3, 102);
			dgvOrderProducts.MultiSelect = false;
			dgvOrderProducts.Name = "dgvOrderProducts";
			dgvOrderProducts.ReadOnly = true;
			dgvOrderProducts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvOrderProducts.ShowEditingIcon = false;
			dgvOrderProducts.Size = new Size(806, 362);
			dgvOrderProducts.TabIndex = 35;
			// 
			// lblOrderId
			// 
			lblOrderId.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lblOrderId.AutoSize = true;
			lblOrderId.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblOrderId.Location = new Point(731, 41);
			lblOrderId.Name = "lblOrderId";
			lblOrderId.Size = new Size(78, 25);
			lblOrderId.TabIndex = 37;
			lblOrderId.Text = "#00000";
			// 
			// OrderProductsControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(lblOrderId);
			Controls.Add(progressBar);
			Controls.Add(dgvOrderProducts);
			Controls.Add(btnBack);
			Name = "OrderProductsControl";
			Size = new Size(812, 467);
			Load += OrderProductsControl_Load;
			((System.ComponentModel.ISupportInitialize)dgvOrderProducts).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnBack;
		private ProgressBar progressBar;
		private DataGridView dgvOrderProducts;
		private Label lblOrderId;
	}
}
