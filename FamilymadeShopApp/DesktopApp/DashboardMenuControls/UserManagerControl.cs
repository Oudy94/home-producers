using BusinessLogicLayer.Managers;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DesktopApp.DashboardMenuControls
{
	public partial class UserManagerControl : UserControl
	{
		private UserManager _userManager;
		private bool _isLoading = false;

		public UserManagerControl()
		{
			InitializeComponent();

			_userManager = new UserManager();
			cmbFilterType.SelectedIndex = 0;
		}

		private async void UserManagerControl_Load(object sender, EventArgs e)
		{
			await LoadUsersAsync();
		}

		private async Task LoadUsersAsync()
		{
			dgvUsers.Columns.Clear();

			dgvUsers.Columns.Add("Id", "Id");
			dgvUsers.Columns.Add("Name", "Name");
			dgvUsers.Columns.Add("Email", "Email");
			dgvUsers.Columns.Add("RegistrationDate", "Registration date");

			if (cmbFilterType.SelectedIndex == 0)
			{
				dgvUsers.Columns.Add("Role", "Role");

				try
				{
					ShowLoadingIndicator();

					List<Admin> admins = await _userManager.GetAdminsAsync();

					foreach (Admin admin in admins)
					{
						dgvUsers.Rows.Add(admin.Id, admin.Name, admin.Email, admin.RegistrationDate, admin.Role);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					HideLoadingIndicator();
				}
			}
			else
			{
				dgvUsers.Columns.Add("ShippingAddress", "Shipping address");

				try
				{
					ShowLoadingIndicator();

					List<Customer> customers = await _userManager.GetCustomersAsync();

					foreach (Customer customer in customers)
					{
						dgvUsers.Rows.Add(customer.Id, customer.Name, customer.Email, customer.RegistrationDate, customer.ShippingAddress);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					HideLoadingIndicator();
				}
			}
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

		private void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ = LoadUsersAsync();
		}

		private void SearchInDataGridView(string searchText)
		{
			dgvUsers.ClearSelection();

			int columnIndex = dgvUsers.Columns["Name"].Index;

			foreach (DataGridViewRow row in dgvUsers.Rows)
			{
				DataGridViewCell cell = row.Cells[columnIndex];

				if (cell.Value != null && cell.Value.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					row.Selected = true;
					dgvUsers.FirstDisplayedScrollingRowIndex = row.Index;
					break;
				}
			}
		}

		private void txtFilterSearch_TextChanged(object sender, EventArgs e)
		{
			string searchText = txtFilterSearch.Text;
			SearchInDataGridView(searchText);
		}
	}
}
