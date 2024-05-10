namespace DesktopApp.DashboardMenuControls
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
			dgvProducts = new DataGridView();
			groupBox1 = new GroupBox();
			label2 = new Label();
			label1 = new Label();
			progressBar = new ProgressBar();
			((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// dgvProducts
			// 
			dgvProducts.AllowUserToDeleteRows = false;
			dgvProducts.AllowUserToResizeRows = false;
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
			dgvProducts.Location = new Point(6, 90);
			dgvProducts.MultiSelect = false;
			dgvProducts.Name = "dgvProducts";
			dgvProducts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvProducts.ShowEditingIcon = false;
			dgvProducts.Size = new Size(800, 375);
			dgvProducts.TabIndex = 4;
			// 
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(label1);
			groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			groupBox1.Location = new Point(6, 1);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(800, 83);
			groupBox1.TabIndex = 5;
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
			progressBar.Location = new Point(349, 252);
			progressBar.Name = "progressBar";
			progressBar.Size = new Size(100, 23);
			progressBar.TabIndex = 6;
			// 
			// ProductManagerControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(dgvProducts);
			Controls.Add(groupBox1);
			Controls.Add(progressBar);
			Name = "ProductManagerControl";
			Size = new Size(812, 467);
			((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView dgvProducts;
		private GroupBox groupBox1;
		private Label label2;
		private Label label1;
		private ProgressBar progressBar;
	}
}
