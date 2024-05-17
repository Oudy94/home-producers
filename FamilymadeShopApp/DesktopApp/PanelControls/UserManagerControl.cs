using BusinessLogicLayer.Managers;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedLayer.Enums;
using static DesktopApp.Utilities.AppConfig;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using DesktopApp.PanelControls.UserManagerControls;

namespace DesktopApp.PanelControls
{
	public partial class UserManagerControl : UserControl
	{
		private UserManager _userManager;
		private bool _isLoading;
		private int _maxPageNumber;
		private DateTime _lastRequestTime;
		private int _requestCount;
		private List<string> _rolesValues;
		private Dictionary<int, Admin> _editedAdminDict;
		private Dictionary<int, Customer> _editedCustomerDict;

		public UserManagerControl()
		{
			InitializeComponent();

			_userManager = new UserManager();
			_isLoading = false;
			_maxPageNumber = 0;
			_lastRequestTime = DateTime.MinValue;
			_requestCount = 0;
			_rolesValues = Enum.GetNames(typeof(Role)).ToList();
			_editedAdminDict = new Dictionary<int, Admin>();
			_editedCustomerDict = new Dictionary<int, Customer>();
		}

		private void UserManagerControl_Load(object sender, EventArgs e)
		{
			BuildUserDataGridView();

			cmbFilterType.SelectedIndex = 0;
		}

		private void BuildUserDataGridView()
		{
			dgvUsers.Columns.Add("Id", "Id");
			dgvUsers.Columns.Add("Name", "Name");
			dgvUsers.Columns.Add("Email", "Email");
			dgvUsers.Columns.Add("RegistrationDate", "Registration date");

			dgvUsers.Columns["Id"].ReadOnly = true;
			dgvUsers.Columns["RegistrationDate"].ReadOnly = true;
		}

		private void BuildAdminDataGridView(List<Admin> admins)
		{
			if (dgvUsers.Columns["Role"] == null)
			{
				DataGridViewColumn shippingAddressColumn = dgvUsers.Columns["ShippingAddress"];
				if (shippingAddressColumn != null)
				{
					dgvUsers.Columns.Remove(shippingAddressColumn);
				}
				//dgvUsers.Columns.Add("Role", "Role");

				DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn();
				comboColumn.Name = "Role";
				comboColumn.HeaderText = "Role";
				comboColumn.DataSource = _rolesValues;
				dgvUsers.Columns.Add(comboColumn);
			}

			dgvUsers.Rows.Clear();


			foreach (Admin admin in admins)
			{
				dgvUsers.Rows.Add(admin.Id, admin.Name, admin.Email, admin.RegistrationDate, _rolesValues[(int)admin.Role]);
			}
		}

		private void BuildCustomerDataGridView(List<Customer> customers)
		{
			if (dgvUsers.Columns["ShippingAddress"] == null)
			{
				DataGridViewColumn roleColumn = dgvUsers.Columns["Role"];
				if (roleColumn != null)
				{
					dgvUsers.Columns.Remove(roleColumn);
				}
				dgvUsers.Columns.Add("ShippingAddress", "Shipping Address");
			}

			dgvUsers.Rows.Clear();

			foreach (Customer customer in customers)
			{
				dgvUsers.Rows.Add(customer.Id, customer.Name, customer.Email, customer.RegistrationDate, customer.ShippingAddress);
			}
		}

		private void BuildPageNumber()
		{
			lblMaxPage.Text = $"of {_maxPageNumber}";

			if (int.TryParse(txtCurrentPage.Text, out int currentPageNumber))
			{
				if (currentPageNumber == 1 && _maxPageNumber == 1)
				{
					btnPreviousPage.Enabled = false;
					btnNextPage.Enabled = false;
				}
				else if (currentPageNumber == 1)
				{
					btnPreviousPage.Enabled = false;
					btnNextPage.Enabled = true;
				}
				else if (currentPageNumber == _maxPageNumber)
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
		}

		private async Task LoadAndBuildUserDataAsync()
		{
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
				return;
			}

			ClearDataEditingChanges();

			if (cmbFilterType.SelectedIndex == 0)
			{
				BuildAdminDataGridView(await LoadAdminDataAsync());
			}
			else if (cmbFilterType.SelectedIndex == 1)
			{
				BuildCustomerDataGridView(await LoadCustomerDataAsync());
			}

			BuildPageNumber();
		}

		private async Task<List<Admin>> LoadAdminDataAsync()
		{
			List<Admin> admins = new List<Admin>();

			try
			{
				ShowLoadingIndicator();

				_maxPageNumber = await _userManager.GetAdminDataMaxPageAsync(s_UserPageSize);

				if (!int.TryParse(txtCurrentPage.Text, out int pageNumber))
				{
					pageNumber = 1;
				}

				admins = await _userManager.GetAdminDataAsync(txtFilterSearch.Text, pageNumber, s_UserPageSize);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				HideLoadingIndicator();
			}

			return admins;
		}

		private async Task<List<Customer>> LoadCustomerDataAsync()
		{
			List<Customer> customers = new List<Customer>();

			try
			{
				ShowLoadingIndicator();

				_maxPageNumber = await _userManager.GetCustomerDataMaxPageAsync(s_UserPageSize);

				if (!int.TryParse(txtCurrentPage.Text, out int pageNumber))
				{
					pageNumber = 1;
				}

				customers = await _userManager.GetCustomerDataAsync(txtFilterSearch.Text, pageNumber, s_UserPageSize);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				HideLoadingIndicator();
			}

			return customers;
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

		private async Task RefreshUserData()
		{
			txtCurrentPage.Text = "1";
			await LoadAndBuildUserDataAsync();
		}

		private void ClearDataEditingChanges()
		{
			foreach (DataGridViewRow row in dgvUsers.Rows)
			{
				foreach (DataGridViewCell cell in row.Cells)
				{
					cell.Style.BackColor = Color.Empty;
				}
			}

			_editedAdminDict.Clear();
			_editedCustomerDict.Clear();
			btnSaveData.Enabled = false;
		}

		private void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ = RefreshUserData();
		}

		private void btnFilterSearch_Click(object sender, EventArgs e)
		{
			_ = RefreshUserData();
		}

		private void txtFilterSearch_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				_ = RefreshUserData();
				e.Handled = true;
			}
		}

		private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
		{
			//Accept only numbers and control button like delete
			if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
			{
				e.Handled = true;
			}

			//When press enter, load the new data
			if (e.KeyChar == (char)Keys.Enter)
			{
				_ = LoadAndBuildUserDataAsync();
				e.Handled = true;
			}

			//Prevent typing a value more than the max page number
			string currentPageText = txtCurrentPage.Text;
			string newPageText = currentPageText.Substring(0, txtCurrentPage.SelectionStart) + e.KeyChar.ToString() +
							 currentPageText.Substring(txtCurrentPage.SelectionStart + txtCurrentPage.SelectionLength);

			if (int.TryParse(newPageText, out int newPageNumber))
			{
				if (newPageNumber > _maxPageNumber)
				{
					e.Handled = true;
				}
			}
			else
			{
				e.Handled = true;
			}
		}

		private void txtCurrentPage_Leave(object sender, EventArgs e)
		{
			if (_maxPageNumber != int.Parse(txtCurrentPage.Text))
			{
				_ = LoadAndBuildUserDataAsync();
			}
		}

		private void btnPreviousPage_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtCurrentPage.Text, out int currentPage))
			{
				currentPage--;

				txtCurrentPage.Text = currentPage.ToString();
				_ = LoadAndBuildUserDataAsync();
			}
		}

		private void btnNextPage_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtCurrentPage.Text, out int currentPage))
			{
				currentPage++;

				txtCurrentPage.Text = currentPage.ToString();
				_ = LoadAndBuildUserDataAsync();
			}
		}

		private void dgvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0 || _editedAdminDict.ContainsKey(e.RowIndex))
				return;

			DataGridViewRow editedRow = dgvUsers.Rows[e.RowIndex];
			DataGridViewCell editedCell = editedRow.Cells[e.ColumnIndex];

			editedCell.Style.BackColor = Color.Yellow;
			btnSaveData.Enabled = true;

			int id;
			if (!int.TryParse(editedRow.Cells["Id"].Value?.ToString(), out id))
			{
				return;
			}

			string name = editedRow.Cells["Name"].Value?.ToString();
			string email = editedRow.Cells["Email"].Value?.ToString();

			if (cmbFilterType.SelectedIndex == 0)
			{
				DataGridViewComboBoxCell comboBoxCell = editedRow.Cells["Role"] as DataGridViewComboBoxCell;
				int roleIndex = comboBoxCell.EditedFormattedValue == null ? -1 : comboBoxCell.Items.IndexOf(comboBoxCell.EditedFormattedValue);

				Admin admin = new Admin { Id = id, Name = name, Email = email, Role = (Role)roleIndex };

				_editedAdminDict.Add(e.RowIndex, admin);
			}
			else
			{
				string shippingAddress = editedRow.Cells["ShippingAddress"].Value?.ToString();

				Customer customer = new Customer { Id = id, Name = name, Email = email, ShippingAddress = shippingAddress };
				_editedCustomerDict.Add(e.RowIndex, customer);

			}
		}

		private void btnSaveData_Click(object sender, EventArgs e)
		{
			if (cmbFilterType.SelectedIndex == 0)
			{
				if (_editedAdminDict.Count == 0)
				{
					MessageBox.Show("Nothing to be updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				foreach (Admin admin in _editedAdminDict.Values)
				{
					if (string.IsNullOrWhiteSpace(admin.Name) || string.IsNullOrWhiteSpace(admin.Email) || (int)admin.Role == -1)
					{
						MessageBox.Show("Please fill all the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}

				try
				{
					bool updateSuccess = _userManager.UpdateAdminData(_editedAdminDict.Values.ToList());
					if (updateSuccess)
					{
						ClearDataEditingChanges();
						MessageBox.Show(($"Update admin{(_editedAdminDict.Count > 1 ? "s" : "")} succeeded."), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				if (_editedCustomerDict.Count == 0)
				{
					MessageBox.Show("Nothing to be updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				foreach (Customer customer in _editedCustomerDict.Values)
				{
					if (string.IsNullOrWhiteSpace(customer.Name) || string.IsNullOrWhiteSpace(customer.Email))
					{
						MessageBox.Show("Please fill all the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}

				try
				{
					bool updateSuccess = _userManager.UpdateCustomerData(_editedCustomerDict.Values.ToList());
					if (updateSuccess)
					{
						ClearDataEditingChanges();
						MessageBox.Show(($"Update customer{(_editedCustomerDict.Count > 1 ? "s" : "")} succeeded."), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
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

		private void btnAddUser_Click(object sender, EventArgs e)
		{
			pnlUserManager.Visible = false;

			AddAdminControl addEmployeeControl = new AddAdminControl();
			addEmployeeControl.Dock = DockStyle.Fill;

			addEmployeeControl.OpenUserManager += OnOpenUserManager;

			this.Controls.Add(addEmployeeControl);
		}

		private void OnOpenUserManager(object sender, bool success)
		{
			this.Controls.Remove((Control)sender);
			pnlUserManager.Visible = true;

			if (success)
			{
				_ = RefreshUserData();
			}
		}

		private void btnRefreshData_Click(object sender, EventArgs e)
		{
			_ = RefreshUserData();
		}
	}
}
