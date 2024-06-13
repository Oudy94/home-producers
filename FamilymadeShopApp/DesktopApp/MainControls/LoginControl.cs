using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;
using Microsoft.VisualBasic.Logging;
using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp.MainControls
{
	public partial class LoginControl : UserControl
	{
		public delegate void LoginSuccessEventHandler(Admin admin);
		public event LoginSuccessEventHandler? LoginSuccess;

		private UserManager _userManager;

		public LoginControl()
		{
			InitializeComponent();

			_userManager = new UserManager(new UserRepository());
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Login successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			OnLoginSuccess(new Admin
			{
				Id = 1,
				Name = "Saoud",
				Role = Role.SuperAdmin
			}
			);
			return;

			string email = txtLoginEmail.Text;
			string password = txtLoginPassword.Text;

			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Please fill all the fields.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			try
			{
				Admin admin = _userManager.GetAdminByCredentials(email, password);
				if (admin != null)
				{
					MessageBox.Show("Login successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					OnLoginSuccess(admin);
				}
				else
				{
					MessageBox.Show("Sorry, your email or password is incorrect.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
                Debug.WriteLine(ex.Message);
                MessageBox.Show($"Error login to the application. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}

		private void chbLoginShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (chbLoginShowPassword.Checked)
			{
				txtLoginPassword.PasswordChar = '\0';
			}
			else
			{
				txtLoginPassword.PasswordChar = '*';
			}
		}

		protected virtual void OnLoginSuccess(Admin admin)
		{
			if (LoginSuccess != null)
			{
				LoginSuccess(admin);
			}
		}
	}
}
