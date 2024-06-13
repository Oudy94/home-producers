using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DesktopApp.PanelControls
{
    public partial class DashboardControl : UserControl
    {
        private DashboardManager _dashboardManager;

        public DashboardControl()
        {
            InitializeComponent();

            _dashboardManager = new DashboardManager(new DashboardRepository());
        }

        private async void HomeControl_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = GetStartOfMonth(DateTime.Now);
            dtpEndDate.Value = DateTime.Now;

            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var (numberOfCustomers, numberOfProducts) = await _dashboardManager.GetEntityStatisticsAsync();
                var (numberOfOrders, totalRevenue) = await _dashboardManager.GetOrderStatisticsAsync(dtpStartDate.Value, dtpEndDate.Value);
                List<KeyValuePair<DateTime, decimal>> revenuesList = await _dashboardManager.GetGrossRevenueAsync(dtpStartDate.Value, dtpEndDate.Value);
                List<KeyValuePair<string, int>> topProductsList = await _dashboardManager.GetTopProductsListAsync(dtpStartDate.Value, dtpEndDate.Value);
                List<Product> understockProducts = await _dashboardManager.GetUnderStockProductsAsync();

                lblNumberOfCustomers.Text = numberOfCustomers.ToString("N0");
                lblNumberOfProducts.Text = numberOfProducts.ToString("N0");

                lblNumberOfOrders.Text = numberOfOrders.ToString("N0");
                lblTotalRevenue.Text = $"€{totalRevenue.ToString("N0")}";

                crtGrossRevenue.Series[0].XValueMember = "Key";
                crtGrossRevenue.Series[0].YValueMembers = "Value";
                crtGrossRevenue.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM";
                crtGrossRevenue.DataSource = revenuesList;
                crtGrossRevenue.DataBind();

                crtTopProducts.DataSource = topProductsList;
                crtTopProducts.Series[0].XValueMember = "Key";
                crtTopProducts.Series[0].YValueMembers = "Value";
                crtTopProducts.DataBind();

                dgvUnderstockProducts.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn stockColumn = new DataGridViewTextBoxColumn();

                idColumn.HeaderText = "Id";
                nameColumn.HeaderText = "Name";
                stockColumn.HeaderText = "Stock";

                idColumn.DataPropertyName = "Id";
                nameColumn.DataPropertyName = "Name";
                stockColumn.DataPropertyName = "Stock";

                idColumn.FillWeight = 1;
                nameColumn.FillWeight = 4;
                stockColumn.FillWeight = 2;

                dgvUnderstockProducts.Columns.Add(idColumn);
                dgvUnderstockProducts.Columns.Add(nameColumn);
                dgvUnderstockProducts.Columns.Add(stockColumn);

                dgvUnderstockProducts.DataSource = understockProducts;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show($"An error retrieving dashboard data. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static DateTime GetStartOfToday(DateTime dateTime)
        {
            return dateTime.Date;
        }

        private static DateTime GetStartOfWeek(DateTime dateTime)
        {
            var diff = (7 + (dateTime.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dateTime.AddDays(-1 * diff).Date;
        }

        private static DateTime GetStartOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        private static DateTime GetStartOfLastWeek(DateTime dateTime)
        {
            return GetStartOfWeek(dateTime).AddDays(-7);
        }

        private static DateTime GetEndOfLastWeek(DateTime dateTime)
        {
            return GetStartOfWeek(dateTime).AddDays(-1);
        }

        private static DateTime GetStartOfLastMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(-1);
        }

        private static DateTime GetEndOfLastMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddDays(-1);
        }

        private async void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = GetStartOfToday(DateTime.Now);
            dtpEndDate.Value = DateTime.Now;

            await LoadData();
        }

        private async void btnThisWeek_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = GetStartOfWeek(DateTime.Now);
            dtpEndDate.Value = DateTime.Now;

            await LoadData();
        }

        private async void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = GetStartOfMonth(DateTime.Now);
            dtpEndDate.Value = DateTime.Now;

            await LoadData();
        }

        private async void btnLastWeek_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = GetStartOfLastWeek(DateTime.Now);
            dtpEndDate.Value = GetEndOfLastWeek(DateTime.Now);

            await LoadData();
        }

        private async void btnLastMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = GetStartOfLastMonth(DateTime.Now);
            dtpEndDate.Value = GetEndOfLastMonth(DateTime.Now);

            await LoadData();
        }

        private async void btnCustom_Click(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}
