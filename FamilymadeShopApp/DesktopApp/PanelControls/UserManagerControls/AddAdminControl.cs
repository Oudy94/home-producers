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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ComponentModel.DataAnnotations;

namespace DesktopApp.PanelControls.UserManagerControls
{
	public partial class AddAdminControl : UserControl
	{
		public delegate void OpenUserManagerEventHandler(object sender, bool success = false);
		public event OpenUserManagerEventHandler OpenUserManager;
		private UserManager _userManager;

		public AddAdminControl()
		{
			InitializeComponent();

			_userManager = new UserManager();
		}
		private void AddAdminControl_Load(object sender, EventArgs e)
		{
			foreach (Role enumValue in Enum.GetValues(typeof(Role)))
			{
				cmbAdminRole.Items.Add(enumValue);
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			OpenUserManager(this);
		}

		private void btnAddAdmin_Click(object sender, EventArgs e)
		{
			string name = txtAdminName.Text;
			string email = txtAdminEmail.Text;
			string password = txtAdminPassword.Text;
			int selectedRole = cmbAdminRole.SelectedIndex;

			if (selectedRole == -1)
			{
				MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var admin = new Admin { Name = name, Email = email, Password = password, Role = (Role)selectedRole };

			var validationContext = new ValidationContext(admin);
			var validationResults = new List<ValidationResult>();
			if (!Validator.TryValidateObject(admin, validationContext, validationResults, validateAllProperties: true))
			{
				string errorMessage = string.Join("\n", validationResults.Select(result => result.ErrorMessage));
				MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				_userManager.AddAdmin(admin);

				MessageBox.Show("Admin added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				OpenUserManager(this, true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (chbShowPassword.Checked)
			{
				txtAdminPassword.PasswordChar = '\0';
			}
			else
			{
				txtAdminPassword.PasswordChar = '*';
			}
		}
	}
}
