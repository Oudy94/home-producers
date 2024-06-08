using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;
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

namespace DesktopApp.PanelControls.OrderManagerControls
{
	public partial class OrderProductsControl : UserControl
	{
		public event EventHandler OpenOrderManager;
		private OrderManager _orderManager;
		private int _orderId;
		private bool _isLoading;

		public OrderProductsControl(int orderId)
		{
			InitializeComponent();

			_orderManager = new OrderManager(new OrderRepository());
			_orderId = orderId;
			_isLoading = false;
		}

		private async void OrderProductsControl_Load(object sender, EventArgs e)
		{
			BuildOrderProductGridView();

			Order order = await FetchOrderAsync();

			if (order != null)
			{
				lblOrderId.Text = $"#{order.Id}";
				PopulateOrderData(order);
			}
		}

		private void BuildOrderProductGridView()
		{
			dgvOrderProducts.Columns.Add("ProductId", "Product Id");
			dgvOrderProducts.Columns.Add("ProductName", "Product Name");
			dgvOrderProducts.Columns.Add("Quantity", "Quantity");
			dgvOrderProducts.Columns.Add("Price", "Price");
		}

		private void PopulateOrderData(Order order)
		{
			dgvOrderProducts.Rows.Clear();

			foreach (OrderProduct orderProduct in order.Products)
			{
				dgvOrderProducts.Rows.Add(orderProduct.Product.Id, orderProduct.Product.Name, orderProduct.Quantity, orderProduct.Price);
			}
		}

		private async Task<Order> FetchOrderAsync()
		{
			try
			{
				ShowLoadingIndicator();
				
				Order order = await _orderManager.GetOrderByIdAsync(_orderId);
				return order;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				HideLoadingIndicator();
			}

			return null;
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

		private void btnBack_Click(object sender, EventArgs e)
		{
			OpenOrderManager?.Invoke(this, EventArgs.Empty);
		}
	}
}
