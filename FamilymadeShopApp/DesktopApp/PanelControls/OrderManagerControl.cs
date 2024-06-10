using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;
using DesktopApp.PanelControls.OrderManagerControls;
using DesktopApp.PanelControls.ProductManagerControls;
using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DesktopApp.Utilities.AppConfig;

namespace DesktopApp.PanelControls
{
	public partial class OrderManagerControl : UserControl
	{
		private OrderManager _orderManager;
		private bool _isLoading = false;
		private int _maxPageNumber;
		private int _pageNumber;
		private DateTime _lastRequestTime;
		private int _requestCount;
		private List<string> _statusValues;
		private HashSet<int> _editedRowsSet;

		public OrderManagerControl()
		{
			InitializeComponent();

			_orderManager = new OrderManager(new OrderRepository());
			_isLoading = false;
			_maxPageNumber = 0;
			_pageNumber = 1;
			_lastRequestTime = DateTime.MinValue;
			_requestCount = 0;
			_statusValues = Enum.GetNames(typeof(OrderStatus)).ToList();
			_editedRowsSet = new HashSet<int>();
		}

		private void OrderManagerControl_Load(object sender, EventArgs e)
		{
			BuildOrderGridView();

			cmbFilterStatus.Items.Insert(0, "All");
			cmbFilterStatus.Items.AddRange(_statusValues.ToArray());
			cmbFilterStatus.SelectedIndex = 0;
		}

		private void BuildOrderGridView()
		{
			dgvOrders.Columns.Add("Id", "Id");
			dgvOrders.Columns.Add("CustomerId", "Customer Id");
			DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn();
			statusColumn.Name = "Status";
			statusColumn.HeaderText = "Status";
			statusColumn.DataSource = _statusValues;
			dgvOrders.Columns.Add(statusColumn);
			dgvOrders.Columns.Add("Date", "Date");
			dgvOrders.Columns.Add("ShippingPrice", "Shipping Price");
			dgvOrders.Columns.Add("ShippingAddress", "Shipping Address");
			dgvOrders.Columns.Add("PaymentMethod", "Payment Method");
			DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
			{
				Name = "ViewProducts",
				HeaderText = "View products",
				Text = "Details",
				UseColumnTextForButtonValue = true
			};
			dgvOrders.Columns.Add(buttonColumn);

			dgvOrders.Columns["Id"].ReadOnly = true;
			dgvOrders.Columns["Date"].ReadOnly = true;
		}

		private void PopulateOrderData(List<Order> orders)
		{
			dgvOrders.Rows.Clear();

			foreach (Order order in orders)
			{
				dgvOrders.Rows.Add(order.Id, order.CustomerId, _statusValues[(int)order.Status], order.Date, order.ShippingPrice, order.ShippingAddress, order.PaymentMethod);
			}
		}

		private void BuildPageNumber()
		{
			lblMaxPage.Text = $"of {_maxPageNumber}";
			txtCurrentPage.Text = _pageNumber.ToString();

			if (_pageNumber == 1 && _maxPageNumber == 1)
			{
				btnPreviousPage.Enabled = false;
				btnNextPage.Enabled = false;
			}
			else if (_pageNumber == 1)
			{
				btnPreviousPage.Enabled = false;
				btnNextPage.Enabled = true;
			}
			else if (_pageNumber == _maxPageNumber)
			{
				btnPreviousPage.Enabled = true;
				btnNextPage.Enabled = false;
			}
			else
			{
				btnPreviousPage.Enabled = true;
				btnNextPage.Enabled = true;
			}
		}

		private async Task<bool> LoadAndDisplayOrderDataAsync(int pageNumber = 0)
		{
			if (_isLoading)
			{
				return false;
			}

			DateTime currentTime = DateTime.Now;

			if ((currentTime - _lastRequestTime).TotalSeconds >= s_MaxDBRequestTime)
			{
				_requestCount = 0;
				_lastRequestTime = currentTime;
			}

			_requestCount++;

			if (_requestCount > s_MaxDBRequestCount)
			{
				MessageBox.Show("Too many requests. Please try again later.");
				return false;
			}

			_pageNumber = pageNumber > 0 ? pageNumber : _pageNumber;

			ClearDataEditingChanges();
			PopulateOrderData(await FetchOrderDataAsync());

			BuildPageNumber();
			return true;
		}
		 
		private async Task<List<Order>> FetchOrderDataAsync()
		{
			List<Order> orders = new List<Order>();

			try
			{
				ShowLoadingIndicator();

				OrderStatus? statusSelected = cmbFilterStatus.SelectedIndex > 0 ? (OrderStatus)cmbFilterStatus.SelectedIndex - 1 : null;
				int totalCount = await _orderManager.GetOrdersCountAsync(txtFilterSearch.Text, statusSelected);
				_maxPageNumber = (int)Math.Ceiling((double)totalCount / s_OrdersPageSize);

				orders = await _orderManager.GetOrderDataAsync(_pageNumber, s_OrdersPageSize, txtFilterSearch.Text, statusSelected);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				HideLoadingIndicator();
			}

			return orders;
		}

		private void ClearDataEditingChanges()
		{
			foreach (DataGridViewRow row in dgvOrders.Rows)
			{
				foreach (DataGridViewCell cell in row.Cells)
				{
					cell.Style.BackColor = Color.Empty;
				}
			}

			_editedRowsSet.Clear();
			btnSaveData.Enabled = false;
		}

		private void ShowLoadingIndicator()
		{
			progressBar.Style = ProgressBarStyle.Marquee;
			progressBar.Visible = true;
			_isLoading = true;
		}

		private void HideLoadingIndicator()
		{
			progressBar.Visible = false;
			progressBar.Style = ProgressBarStyle.Blocks;
			_isLoading = false;
		}

		private async Task RefreshOrderData()
		{
			await LoadAndDisplayOrderDataAsync(1);
		}

		private async void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			await RefreshOrderData();
		}

		private async void btnFilterSearch_Click(object sender, EventArgs e)
		{
			await RefreshOrderData();
		}

		private async void txtFilterSearch_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				await RefreshOrderData();
				e.Handled = true;
			}
		}

		private async void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (int.TryParse(txtCurrentPage.Text, out int pageNumber))
				{
					await LoadAndDisplayOrderDataAsync(pageNumber);
				}
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void txtCurrentPage_TextChanged(object sender, EventArgs e)
		{
			string currentPageText = txtCurrentPage.Text;

			if (string.IsNullOrWhiteSpace(currentPageText))
			{
				return;
			}

			if (int.TryParse(currentPageText, out int newPageNumber))
			{
				if (newPageNumber > _maxPageNumber)
				{
					txtCurrentPage.Text = _maxPageNumber.ToString();
					txtCurrentPage.SelectionStart = txtCurrentPage.Text.Length;
				}
			}
			else
			{
				txtCurrentPage.Text = "1";
				txtCurrentPage.SelectionStart = txtCurrentPage.Text.Length;
			}
		}

		private async void txtCurrentPage_Leave(object sender, EventArgs e)
		{
			if (int.TryParse(txtCurrentPage.Text, out int pageNumber))
			{
				await LoadAndDisplayOrderDataAsync(pageNumber);
			}
		}

		private async void btnPreviousPage_Click(object sender, EventArgs e)
		{
			await LoadAndDisplayOrderDataAsync(_pageNumber - 1);
		}

		private async void btnNextPage_Click(object sender, EventArgs e)
		{
			await LoadAndDisplayOrderDataAsync(_pageNumber + 1);
		}

		private void dgvOrders_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;

			DataGridViewRow editedRow = dgvOrders.Rows[e.RowIndex];
			DataGridViewCell editedCell = editedRow.Cells[e.ColumnIndex];

			editedCell.Style.BackColor = Color.Yellow;

			if (!_editedRowsSet.Contains(e.RowIndex))
			{
				btnSaveData.Enabled = true;
				_editedRowsSet.Add(e.RowIndex);
			}
		}

		private async void btnSaveData_Click(object sender, EventArgs e)
		{
			if (_editedRowsSet.Count == 0)
			{
				MessageBox.Show("Nothing to be updated!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!TryGetEditedOrders(out List<Order> orders))
			{
				MessageBox.Show("Please fill all the fields correctly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				bool updateSuccess = await _orderManager.UpdateOrdersAsync(orders);
				if (updateSuccess)
				{
					ClearDataEditingChanges();
					MessageBox.Show(($"Update order{(_editedRowsSet.Count > 1 ? "s" : "")} succeeded."), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool TryGetEditedOrders(out List<Order> orders)
		{
			orders = new List<Order>();

			foreach (int rowIndex in _editedRowsSet)
			{
				if (rowIndex >= 0 && rowIndex < dgvOrders.Rows.Count)
				{
					DataGridViewRow row = dgvOrders.Rows[rowIndex];

					if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id))
					{
						return false;
					}

					if (!int.TryParse(row.Cells["CustomerId"].Value?.ToString(), out int customerId))
					{
						return false;
					}

					DataGridViewComboBoxCell statusCell = row.Cells["Status"] as DataGridViewComboBoxCell;
					if (statusCell.EditedFormattedValue == null)
					{
						return false;
					}
					OrderStatus status = (OrderStatus)statusCell.Items.IndexOf(statusCell.EditedFormattedValue);

					if (!decimal.TryParse(row.Cells["ShippingPrice"].Value?.ToString(), out decimal shippingPrice))
					{
						return false;
					}

					string shippingAddress = row.Cells["ShippingAddress"].Value?.ToString();
					if (string.IsNullOrWhiteSpace(shippingAddress))
					{
						return false;
					}

                    string paymentMethod = row.Cells["PaymentMethod"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(shippingAddress))
                    {
                        return false;
                    }

                    Order order = new Order
					{
						Id = id,
						CustomerId = customerId,
						Status = status,
						ShippingPrice = shippingPrice,
						ShippingAddress = shippingAddress,
                        PaymentMethod = paymentMethod
                    };

					orders.Add(order);
				}
			}

			return true;
		}

		private void btn_EnabledChanged(object sender, EventArgs e)
		{
			System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
			if (!button.Enabled)
			{
				button.BackColor = Color.LightGray;
				button.ForeColor = Color.DarkGray;
			}
			else
			{
				button.BackColor = Color.FromArgb(170, 197, 175);
				button.ForeColor = Color.White;
			}
		}

		private async void btnRefreshData_Click(object sender, EventArgs e)
		{
			await RefreshOrderData();
		}

		private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == dgvOrders.Columns["ViewProducts"].Index && e.RowIndex >= 0)
			{
				int orderId = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["Id"].Value);

				pnlOrderManager.Visible = false;

				OrderProductsControl orderProductControl = new OrderProductsControl(orderId);
				orderProductControl.Dock = DockStyle.Fill;
				orderProductControl.OpenOrderManager += OnOpenOrderManager;

				this.Controls.Add(orderProductControl);
			}
		}

		private void OnOpenOrderManager(object sender, EventArgs e)
		{
			this.Controls.Remove((Control)sender);

			pnlOrderManager.Visible = true;
		}
	}
}
