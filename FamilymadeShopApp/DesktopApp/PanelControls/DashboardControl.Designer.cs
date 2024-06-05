namespace DesktopApp.PanelControls
{
	partial class DashboardControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            btnThisMonth = new Button();
            btnLastMonth = new Button();
            btnToday = new Button();
            panel1 = new Panel();
            lblNumberOfOrders = new Label();
            label1 = new Label();
            panel2 = new Panel();
            lblTotalRevenue = new Label();
            label4 = new Label();
            crtGrossRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            crtTopProducts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel4 = new Panel();
            label9 = new Label();
            lblNumberOfProducts = new Label();
            label6 = new Label();
            lblNumberOfCustomers = new Label();
            label8 = new Label();
            btnThisWeek = new Button();
            btnLastWeek = new Button();
            dtpEndDate = new DateTimePicker();
            dtpStartDate = new DateTimePicker();
            btnCustom = new Button();
            panel5 = new Panel();
            dgvUnderstockProducts = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Name = new DataGridViewTextBoxColumn();
            Stock = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)crtGrossRevenue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)crtTopProducts).BeginInit();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUnderstockProducts).BeginInit();
            SuspendLayout();
            // 
            // btnThisMonth
            // 
            btnThisMonth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThisMonth.BackColor = Color.FromArgb(170, 197, 175);
            btnThisMonth.FlatAppearance.BorderColor = Color.White;
            btnThisMonth.FlatStyle = FlatStyle.Flat;
            btnThisMonth.ForeColor = Color.White;
            btnThisMonth.Location = new Point(542, 3);
            btnThisMonth.Name = "btnThisMonth";
            btnThisMonth.Size = new Size(85, 26);
            btnThisMonth.TabIndex = 48;
            btnThisMonth.Text = "This month";
            btnThisMonth.UseVisualStyleBackColor = false;
            btnThisMonth.Click += btnThisMonth_Click;
            // 
            // btnLastMonth
            // 
            btnLastMonth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLastMonth.BackColor = Color.FromArgb(170, 197, 175);
            btnLastMonth.FlatAppearance.BorderColor = Color.White;
            btnLastMonth.FlatStyle = FlatStyle.Flat;
            btnLastMonth.ForeColor = Color.White;
            btnLastMonth.Location = new Point(724, 3);
            btnLastMonth.Name = "btnLastMonth";
            btnLastMonth.Size = new Size(85, 26);
            btnLastMonth.TabIndex = 49;
            btnLastMonth.Text = "Last month";
            btnLastMonth.UseVisualStyleBackColor = false;
            btnLastMonth.Click += btnLastMonth_Click;
            // 
            // btnToday
            // 
            btnToday.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnToday.BackColor = Color.FromArgb(170, 197, 175);
            btnToday.FlatAppearance.BorderColor = Color.White;
            btnToday.FlatStyle = FlatStyle.Flat;
            btnToday.ForeColor = Color.White;
            btnToday.Location = new Point(360, 3);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(85, 26);
            btnToday.TabIndex = 51;
            btnToday.Text = "Today";
            btnToday.UseVisualStyleBackColor = false;
            btnToday.Click += btnToday_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblNumberOfOrders);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(3, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(146, 44);
            panel1.TabIndex = 52;
            // 
            // lblNumberOfOrders
            // 
            lblNumberOfOrders.AutoSize = true;
            lblNumberOfOrders.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNumberOfOrders.Location = new Point(12, 15);
            lblNumberOfOrders.Name = "lblNumberOfOrders";
            lblNumberOfOrders.Size = new Size(55, 21);
            lblNumberOfOrders.TabIndex = 1;
            lblNumberOfOrders.Text = "10000";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, -2);
            label1.Name = "label1";
            label1.Size = new Size(117, 17);
            label1.TabIndex = 0;
            label1.Text = "Number of Orders";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(lblTotalRevenue);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(155, 34);
            panel2.Name = "panel2";
            panel2.Size = new Size(218, 44);
            panel2.TabIndex = 53;
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalRevenue.Location = new Point(12, 15);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(55, 21);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "10000";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(3, -2);
            label4.Name = "label4";
            label4.Size = new Size(89, 17);
            label4.TabIndex = 0;
            label4.Text = "Total Revenue";
            // 
            // crtGrossRevenue
            // 
            crtGrossRevenue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartArea3.AxisY.LabelStyle.Format = "€{0}";
            chartArea3.Name = "ChartArea1";
            crtGrossRevenue.ChartAreas.Add(chartArea3);
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.Name = "Legend1";
            crtGrossRevenue.Legends.Add(legend3);
            crtGrossRevenue.Location = new Point(6, 84);
            crtGrossRevenue.Name = "crtGrossRevenue";
            series3.BorderWidth = 4;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series3.Legend = "Legend1";
            series3.MarkerSize = 10;
            series3.Name = "Series1";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            crtGrossRevenue.Series.Add(series3);
            crtGrossRevenue.Size = new Size(557, 224);
            crtGrossRevenue.TabIndex = 54;
            crtGrossRevenue.Text = "chart1";
            title3.Alignment = ContentAlignment.TopLeft;
            title3.Font = new Font("Microsoft Sans Serif", 15F);
            title3.Name = "Title1";
            title3.Text = "Gross Revenue";
            crtGrossRevenue.Titles.Add(title3);
            // 
            // crtTopProducts
            // 
            crtTopProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            chartArea4.Name = "ChartArea1";
            crtTopProducts.ChartAreas.Add(chartArea4);
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Name = "Legend1";
            crtTopProducts.Legends.Add(legend4);
            crtTopProducts.Location = new Point(569, 84);
            crtTopProducts.Name = "crtTopProducts";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series4.Font = new Font("Microsoft Sans Serif", 12F);
            series4.IsValueShownAsLabel = true;
            series4.LabelForeColor = Color.White;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            crtTopProducts.Series.Add(series4);
            crtTopProducts.Size = new Size(231, 371);
            crtTopProducts.TabIndex = 55;
            crtTopProducts.Text = "chart2";
            title4.Alignment = ContentAlignment.TopLeft;
            title4.Font = new Font("Microsoft Sans Serif", 15F);
            title4.Name = "Title1";
            title4.Text = "Top Products";
            crtTopProducts.Titles.Add(title4);
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panel4.BackColor = Color.White;
            panel4.Controls.Add(label9);
            panel4.Controls.Add(lblNumberOfProducts);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(lblNumberOfCustomers);
            panel4.Controls.Add(label8);
            panel4.Location = new Point(6, 314);
            panel4.Name = "panel4";
            panel4.Size = new Size(152, 141);
            panel4.TabIndex = 54;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(4, -3);
            label9.Name = "label9";
            label9.Size = new Size(113, 21);
            label9.TabIndex = 4;
            label9.Text = "Total Counter";
            // 
            // lblNumberOfProducts
            // 
            lblNumberOfProducts.AutoSize = true;
            lblNumberOfProducts.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNumberOfProducts.Location = new Point(12, 88);
            lblNumberOfProducts.Name = "lblNumberOfProducts";
            lblNumberOfProducts.Size = new Size(55, 21);
            lblNumberOfProducts.TabIndex = 3;
            lblNumberOfProducts.Text = "10000";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(3, 71);
            label6.Name = "label6";
            label6.Size = new Size(127, 17);
            label6.TabIndex = 2;
            label6.Text = "Number of Products";
            // 
            // lblNumberOfCustomers
            // 
            lblNumberOfCustomers.AutoSize = true;
            lblNumberOfCustomers.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNumberOfCustomers.Location = new Point(12, 46);
            lblNumberOfCustomers.Name = "lblNumberOfCustomers";
            lblNumberOfCustomers.Size = new Size(55, 21);
            lblNumberOfCustomers.TabIndex = 1;
            lblNumberOfCustomers.Text = "10000";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(3, 29);
            label8.Name = "label8";
            label8.Size = new Size(138, 17);
            label8.TabIndex = 0;
            label8.Text = "Number of Customers";
            // 
            // btnThisWeek
            // 
            btnThisWeek.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThisWeek.BackColor = Color.FromArgb(170, 197, 175);
            btnThisWeek.FlatAppearance.BorderColor = Color.White;
            btnThisWeek.FlatStyle = FlatStyle.Flat;
            btnThisWeek.ForeColor = Color.White;
            btnThisWeek.Location = new Point(451, 3);
            btnThisWeek.Name = "btnThisWeek";
            btnThisWeek.Size = new Size(85, 26);
            btnThisWeek.TabIndex = 57;
            btnThisWeek.Text = "This week";
            btnThisWeek.UseVisualStyleBackColor = false;
            btnThisWeek.Click += btnThisWeek_Click;
            // 
            // btnLastWeek
            // 
            btnLastWeek.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLastWeek.BackColor = Color.FromArgb(170, 197, 175);
            btnLastWeek.FlatAppearance.BorderColor = Color.White;
            btnLastWeek.FlatStyle = FlatStyle.Flat;
            btnLastWeek.ForeColor = Color.White;
            btnLastWeek.Location = new Point(633, 3);
            btnLastWeek.Name = "btnLastWeek";
            btnLastWeek.Size = new Size(85, 26);
            btnLastWeek.TabIndex = 50;
            btnLastWeek.Text = "Last week";
            btnLastWeek.UseVisualStyleBackColor = false;
            btnLastWeek.Click += btnLastWeek_Click;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpEndDate.CustomFormat = "dd MMM yyyy";
            dtpEndDate.Format = DateTimePickerFormat.Custom;
            dtpEndDate.Location = new Point(170, 5);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(92, 23);
            dtpEndDate.TabIndex = 58;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpStartDate.CustomFormat = "dd MMM yyyy";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.Location = new Point(72, 5);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(92, 23);
            dtpStartDate.TabIndex = 59;
            // 
            // btnCustom
            // 
            btnCustom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCustom.BackColor = Color.FromArgb(170, 197, 175);
            btnCustom.FlatAppearance.BorderColor = Color.White;
            btnCustom.FlatStyle = FlatStyle.Flat;
            btnCustom.ForeColor = Color.White;
            btnCustom.Location = new Point(269, 3);
            btnCustom.Name = "btnCustom";
            btnCustom.Size = new Size(85, 26);
            btnCustom.TabIndex = 60;
            btnCustom.Text = "Custom";
            btnCustom.UseVisualStyleBackColor = false;
            btnCustom.Click += btnCustom_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(dgvUnderstockProducts);
            panel5.Controls.Add(dtpStartDate);
            panel5.Controls.Add(btnCustom);
            panel5.Controls.Add(dtpEndDate);
            panel5.Controls.Add(btnLastMonth);
            panel5.Controls.Add(btnLastWeek);
            panel5.Controls.Add(btnThisMonth);
            panel5.Controls.Add(btnThisWeek);
            panel5.Controls.Add(btnToday);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(812, 467);
            panel5.TabIndex = 61;
            // 
            // dgvUnderstockProducts
            // 
            dgvUnderstockProducts.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUnderstockProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUnderstockProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUnderstockProducts.Columns.AddRange(new DataGridViewColumn[] { Id, Name, Stock });
            dgvUnderstockProducts.Location = new Point(164, 314);
            dgvUnderstockProducts.Name = "dgvUnderstockProducts";
            dgvUnderstockProducts.Size = new Size(399, 141);
            dgvUnderstockProducts.TabIndex = 61;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.FillWeight = 20F;
            Id.HeaderText = "Id";
            Id.Name = "Id";
            // 
            // Name
            // 
            Name.DataPropertyName = "Name";
            Name.FillWeight = 65F;
            Name.HeaderText = "Name";
            Name.Name = "Name";
            // 
            // Stock
            // 
            Stock.DataPropertyName = "Stock";
            Stock.FillWeight = 15F;
            Stock.HeaderText = "Stock";
            Stock.Name = "Stock";
            // 
            // DashboardControl
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(panel4);
            Controls.Add(crtTopProducts);
            Controls.Add(crtGrossRevenue);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel5);
            Size = new Size(812, 467);
            Load += HomeControl_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)crtGrossRevenue).EndInit();
            ((System.ComponentModel.ISupportInitialize)crtTopProducts).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUnderstockProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnThisMonth;
        private Button btnLastMonth;
        private Button btnToday;
        private Panel panel1;
        private Label lblNumberOfOrders;
        private Label label1;
        private Panel panel2;
        private Label lblTotalRevenue;
        private Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtGrossRevenue;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtTopProducts;
        private Panel panel4;
        private Label label9;
        private Label lblNumberOfProducts;
        private Label label6;
        private Label lblNumberOfCustomers;
        private Label label8;
        private Button btnThisWeek;
        private Button btnLastWeek;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpStartDate;
        private Button btnCustom;
        private Panel panel5;
        private DataGridView dgvUnderstockProducts;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn Stock;
    }
}
