namespace DesktopApp.PanelControls.ProductManagerControls
{
	partial class AddProductControl
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
			label7 = new Label();
			txtProductSalesCount = new TextBox();
			label6 = new Label();
			txtProductImage = new TextBox();
			label5 = new Label();
			txtProductStock = new TextBox();
			btnAddProduct = new Button();
			txtProductName = new TextBox();
			label4 = new Label();
			txtProductPrice = new TextBox();
			label3 = new Label();
			cmbProductCategory = new ComboBox();
			txtProductDescription = new TextBox();
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
			label1.Location = new Point(169, 39);
			label1.Name = "label1";
			label1.Size = new Size(48, 19);
			label1.TabIndex = 0;
			label1.Text = "Name:";
			// 
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupBox1.Controls.Add(label7);
			groupBox1.Controls.Add(txtProductSalesCount);
			groupBox1.Controls.Add(label6);
			groupBox1.Controls.Add(txtProductImage);
			groupBox1.Controls.Add(label5);
			groupBox1.Controls.Add(txtProductStock);
			groupBox1.Controls.Add(btnAddProduct);
			groupBox1.Controls.Add(txtProductName);
			groupBox1.Controls.Add(label4);
			groupBox1.Controls.Add(txtProductPrice);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(cmbProductCategory);
			groupBox1.Controls.Add(txtProductDescription);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(label1);
			groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBox1.Location = new Point(14, 48);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(784, 403);
			groupBox1.TabIndex = 15;
			groupBox1.TabStop = false;
			groupBox1.Text = "Add Product";
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.None;
			label7.AutoSize = true;
			label7.Font = new Font("Segoe UI", 10F);
			label7.Location = new Point(430, 167);
			label7.Name = "label7";
			label7.Size = new Size(84, 19);
			label7.TabIndex = 19;
			label7.Text = "Sales Count:";
			// 
			// txtProductSalesCount
			// 
			txtProductSalesCount.Anchor = AnchorStyles.None;
			txtProductSalesCount.Font = new Font("Segoe UI", 10F);
			txtProductSalesCount.Location = new Point(430, 189);
			txtProductSalesCount.Name = "txtProductSalesCount";
			txtProductSalesCount.Size = new Size(211, 25);
			txtProductSalesCount.TabIndex = 20;
			// 
			// label6
			// 
			label6.Anchor = AnchorStyles.None;
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 10F);
			label6.Location = new Point(430, 100);
			label6.Name = "label6";
			label6.Size = new Size(50, 19);
			label6.TabIndex = 17;
			label6.Text = "Image:";
			// 
			// txtProductImage
			// 
			txtProductImage.Anchor = AnchorStyles.None;
			txtProductImage.Font = new Font("Segoe UI", 10F);
			txtProductImage.Location = new Point(430, 122);
			txtProductImage.Name = "txtProductImage";
			txtProductImage.Size = new Size(211, 25);
			txtProductImage.TabIndex = 18;
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.None;
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 10F);
			label5.Location = new Point(430, 39);
			label5.Name = "label5";
			label5.Size = new Size(45, 19);
			label5.TabIndex = 15;
			label5.Text = "Stock:";
			// 
			// txtProductStock
			// 
			txtProductStock.Anchor = AnchorStyles.None;
			txtProductStock.Font = new Font("Segoe UI", 10F);
			txtProductStock.Location = new Point(430, 61);
			txtProductStock.Name = "txtProductStock";
			txtProductStock.Size = new Size(211, 25);
			txtProductStock.TabIndex = 16;
			// 
			// btnAddProduct
			// 
			btnAddProduct.Anchor = AnchorStyles.None;
			btnAddProduct.BackColor = Color.FromArgb(170, 197, 175);
			btnAddProduct.FlatAppearance.BorderColor = Color.White;
			btnAddProduct.FlatStyle = FlatStyle.Flat;
			btnAddProduct.ForeColor = Color.White;
			btnAddProduct.Location = new Point(303, 337);
			btnAddProduct.Name = "btnAddProduct";
			btnAddProduct.Size = new Size(211, 32);
			btnAddProduct.TabIndex = 14;
			btnAddProduct.Text = "Add Product";
			btnAddProduct.UseVisualStyleBackColor = false;
			btnAddProduct.Click += btnAddProduct_Click;
			// 
			// txtProductName
			// 
			txtProductName.Anchor = AnchorStyles.None;
			txtProductName.Font = new Font("Segoe UI", 10F);
			txtProductName.Location = new Point(169, 61);
			txtProductName.Name = "txtProductName";
			txtProductName.Size = new Size(211, 25);
			txtProductName.TabIndex = 5;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.None;
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 10F);
			label4.Location = new Point(169, 261);
			label4.Name = "label4";
			label4.Size = new Size(41, 19);
			label4.TabIndex = 3;
			label4.Text = "Price:";
			// 
			// txtProductPrice
			// 
			txtProductPrice.Anchor = AnchorStyles.None;
			txtProductPrice.Font = new Font("Segoe UI", 10F);
			txtProductPrice.Location = new Point(169, 283);
			txtProductPrice.Name = "txtProductPrice";
			txtProductPrice.Size = new Size(211, 25);
			txtProductPrice.TabIndex = 7;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.None;
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 10F);
			label3.Location = new Point(169, 195);
			label3.Name = "label3";
			label3.Size = new Size(68, 19);
			label3.TabIndex = 2;
			label3.Text = "Category:";
			// 
			// cmbProductCategory
			// 
			cmbProductCategory.Anchor = AnchorStyles.None;
			cmbProductCategory.Font = new Font("Segoe UI", 10F);
			cmbProductCategory.FormattingEnabled = true;
			cmbProductCategory.Location = new Point(169, 219);
			cmbProductCategory.Name = "cmbProductCategory";
			cmbProductCategory.Size = new Size(211, 25);
			cmbProductCategory.TabIndex = 4;
			// 
			// txtProductDescription
			// 
			txtProductDescription.Anchor = AnchorStyles.None;
			txtProductDescription.Font = new Font("Segoe UI", 10F);
			txtProductDescription.Location = new Point(169, 122);
			txtProductDescription.Multiline = true;
			txtProductDescription.Name = "txtProductDescription";
			txtProductDescription.Size = new Size(211, 56);
			txtProductDescription.TabIndex = 6;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.None;
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10F);
			label2.Location = new Point(169, 100);
			label2.Name = "label2";
			label2.Size = new Size(81, 19);
			label2.TabIndex = 1;
			label2.Text = "Description:";
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
			btnBack.TabIndex = 14;
			btnBack.Text = "> Back";
			btnBack.UseVisualStyleBackColor = false;
			btnBack.Click += btnBack_Click;
			// 
			// AddProductControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(groupBox1);
			Controls.Add(btnBack);
			Name = "AddProductControl";
			Size = new Size(812, 467);
			Load += AddProductControl_Load;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Label label1;
		private GroupBox groupBox1;
		private Button btnAddProduct;
		private TextBox txtProductName;
		private Label label4;
		private TextBox txtProductPrice;
		private Label label3;
		private ComboBox cmbProductCategory;
		private TextBox txtProductDescription;
		private Label label2;
		private Button btnBack;
		private Label label5;
		private TextBox txtProductStock;
		private Label label7;
		private TextBox txtProductSalesCount;
		private Label label6;
		private TextBox txtProductImage;
	}
}
