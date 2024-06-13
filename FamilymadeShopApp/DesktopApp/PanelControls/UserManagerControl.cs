using BusinessLogicLayer.Managers;
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
using DesktopApp.PanelControls.UserManagerControls;
using System.Drawing.Printing;
using DataAccessLayer.DataAccess;
using Microsoft.VisualBasic.ApplicationServices;

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
        private int _pageNumber;

        public UserManagerControl()
        {
            InitializeComponent();

            _userManager = new UserManager(new UserRepository());
            _isLoading = false;
            _maxPageNumber = 0;
            _lastRequestTime = DateTime.MinValue;
            _requestCount = 0;
            _rolesValues = Enum.GetNames(typeof(Role)).ToList();
            _editedAdminDict = new Dictionary<int, Admin>();
            _editedCustomerDict = new Dictionary<int, Customer>();
            _pageNumber = 1;
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

        private void BuildAdminDataGridView()
        {
            if (dgvUsers.Columns["Role"] == null)
            {
                DataGridViewComboBoxColumn roleColumn = new DataGridViewComboBoxColumn();
                roleColumn.Name = "Role";
                roleColumn.HeaderText = "Role";
                roleColumn.DataSource = _rolesValues;
                dgvUsers.Columns.Add(roleColumn);
            }
        }

        private void PopulateAdminData(List<Admin> admins)
        {
            dgvUsers.Rows.Clear();

            foreach (Admin admin in admins)
            {
                dgvUsers.Rows.Add(admin.Id, admin.Name, admin.Email, admin.RegistrationDate, _rolesValues[(int)admin.Role]);
            }
        }

        private void BuildCustomerDataGridView()
        {
            DataGridViewColumn roleColumn = dgvUsers.Columns["Role"];
            if (roleColumn != null)
            {
                dgvUsers.Columns.Remove(roleColumn);    
            }
        }

        private void PopulateCustomerData(List<Customer> customers)
        {
            dgvUsers.Rows.Clear();

            foreach (Customer customer in customers)
            {
                dgvUsers.Rows.Add(customer.Id, customer.Name, customer.Email, customer.RegistrationDate);
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

        private async Task<bool> LoadAndBuildUserDataAsync(int pageNumber = 0)
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

            if (cmbFilterType.SelectedIndex == 0)
            {
                BuildAdminDataGridView();
                PopulateAdminData(await FetchAdminDataAsync());
            }
            else if (cmbFilterType.SelectedIndex == 1)
            {
                BuildCustomerDataGridView();
                PopulateCustomerData(await FetchCustomerDataAsync());
            }

            BuildPageNumber();
            return true;
        }

        private async Task<List<Admin>> FetchAdminDataAsync()
        {
            List<Admin> admins = new List<Admin>();

            try
            {
                ShowLoadingIndicator();

                int totalCount = await _userManager.GetAdminCountAsync(txtFilterSearch.Text);
                _maxPageNumber = (int)Math.Ceiling((double)totalCount / s_UsersPageSize);

                admins = await _userManager.GetAllAdminsAsync(txtFilterSearch.Text, _pageNumber, s_UsersPageSize);
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

        private async Task<List<Customer>> FetchCustomerDataAsync()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                ShowLoadingIndicator();

                int totalCount = await _userManager.GetCustomerCountAsync(txtFilterSearch.Text);
                _maxPageNumber = (int)Math.Ceiling((double)totalCount / s_UsersPageSize);

                customers = await _userManager.GetAllCustomersAsync(txtFilterSearch.Text, _pageNumber, s_UsersPageSize);
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
            _pageNumber = 1;
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

        private async void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RefreshUserData();
        }

        private async void btnFilterSearch_Click(object sender, EventArgs e)
        {
            await RefreshUserData();
        }

        private async void txtFilterSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                await RefreshUserData();
                e.Handled = true;
            }
        }

        private async void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txtCurrentPage.Text, out int pageNumber))
                {
                    await LoadAndBuildUserDataAsync(pageNumber);
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
                await LoadAndBuildUserDataAsync(pageNumber);
            }
        }

        private async void btnPreviousPage_Click(object sender, EventArgs e)
        {
            await LoadAndBuildUserDataAsync(_pageNumber - 1);
        }

        private async void btnNextPage_Click(object sender, EventArgs e)
        {
            await LoadAndBuildUserDataAsync(_pageNumber + 1);
        }

        private void dgvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridViewRow editedRow = dgvUsers.Rows[e.RowIndex];
            DataGridViewCell editedCell = editedRow.Cells[e.ColumnIndex];

            int id;
            if (!int.TryParse(editedRow.Cells["Id"].Value?.ToString(), out id))
            {
                return;
            }

            string name = editedRow.Cells["Name"].Value?.ToString();
            string email = editedRow.Cells["Email"].Value?.ToString();

            if (cmbFilterType.SelectedIndex == 0)
            {
                DataGridViewComboBoxCell roleCell = editedRow.Cells["Role"] as DataGridViewComboBoxCell;
                if (roleCell.EditedFormattedValue == null)
                {
                    return;
                }

                Admin admin = new Admin { Id = id, Name = name, Email = email, Role = (Role)roleCell.Items.IndexOf(roleCell.EditedFormattedValue) };

                _editedAdminDict.Add(e.RowIndex, admin);
            }
            else
            {
                Customer customer = new Customer { Id = id, Name = name, Email = email };
                _editedCustomerDict.Add(e.RowIndex, customer);

            }

            editedCell.Style.BackColor = Color.Yellow;
            btnSaveData.Enabled = true;
        }

        private async void btnSaveData_Click(object sender, EventArgs e)
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
                    bool updateSuccess = await _userManager.UpdateAdminsAsync(_editedAdminDict.Values.ToList());
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
                    bool updateSuccess = await _userManager.UpdateCustomersAsync(_editedCustomerDict.Values.ToList());
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

            AddAdminControl addAdminControl = new AddAdminControl();
            addAdminControl.Dock = DockStyle.Fill;

            addAdminControl.OpenUserManager += OnOpenUserManager;

            this.Controls.Add(addAdminControl);
        }

        private async void OnOpenUserManager(object sender, bool success)
        {
            this.Controls.Remove((Control)sender);
            pnlUserManager.Visible = true;

            if (success)
            {
                await RefreshUserData();
            }
        }

        private async void btnRefreshData_Click(object sender, EventArgs e)
        {
            await RefreshUserData();
        }

        private async void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count <= 0)
            {
                MessageBox.Show("No user selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];

            int userId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            string userName = selectedRow.Cells["Name"].Value.ToString();

            DialogResult result = MessageBox.Show($"Are you sure you want to remove user {userName}?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await _userManager.RemoveUserByIdAsync(userId);

                    dgvUsers.Rows.Remove(selectedRow);

                    MessageBox.Show($"User {userName} has been removed.", "User Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
