using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using DesktopApp.DashboardMenuControls;

namespace DesktopApp
{
	public partial class DashboardControl : UserControl
	{
		private Button currentButton;
		private UserControl activeControl;
		public delegate void LogoutSuccessEventHandler(object sender, EventArgs e);
		public event LogoutSuccessEventHandler LogoutSuccess;

		private HomeControl homeControl;
		private UserManagerControl userManagerControl;
		private ProductManagerControl productManagerControl;
		private OrderManagerControl orderManagerControl;

		public DashboardControl()
		{
			InitializeComponent();

			homeControl = new HomeControl();
			userManagerControl = new UserManagerControl();
			productManagerControl = new ProductManagerControl();
			orderManagerControl = new OrderManagerControl();
		}

		private void DashboardControl_Load(object sender, EventArgs e)
		{
			OpenMenuForm(new DashboardMenuControls.HomeControl(), btnHome, "Home");
		}

		private void ActivateButton(object btnSender)
		{
			if (btnSender != null)
			{
				if (currentButton != (Button)btnSender)
				{
					DisableButtons();
					currentButton = (Button)btnSender;
					currentButton.BackColor = Color.FromArgb(185, 212, 190);
				}
			}
		}

		private void DisableButtons()
		{
			foreach (Control previousBtn in pnlMenu.Controls)
			{
				if (previousBtn.GetType() == typeof(Button))
				{
					previousBtn.BackColor = Color.FromArgb(192, 192, 192);
					previousBtn.ForeColor = Color.Gainsboro;
				}
			}
		}

		private void OpenMenuForm(UserControl menuControl, Object btnSender, string name)
		{
			ActivateButton(btnSender);
			activeControl = menuControl;
			menuControl.Dock = DockStyle.Fill;
			this.pnlContent.Controls.Add(menuControl);
			menuControl.BringToFront();
			//menuControl.Show();
			lblHeaderTitle.Text = name;
		}

		private void btnHome_Click(object sender, EventArgs e)
		{
			OpenMenuForm(homeControl, sender, "Home");
		}

		private void btnUsers_Click(object sender, EventArgs e)
		{
			OpenMenuForm(userManagerControl, sender, "User Manager");
		}

		private void btnProducts_Click(object sender, EventArgs e)
		{
			OpenMenuForm(productManagerControl, sender, "Product Manager");
		}

		private void btnOrders_Click(object sender, EventArgs e)
		{
			OpenMenuForm(orderManagerControl, sender, "Order Manager");
		}

		private void btnLogout_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				OnLogoutSuccess(EventArgs.Empty);
			}
		}

		protected virtual void OnLogoutSuccess(EventArgs e)
		{
			if (LogoutSuccess != null)
			{
				LogoutSuccess(this, e);
			}
		}

		public void SetLoginUser(Admin admin)
		{
			lblGreetAdmin.Text = $"Welcome, {admin.Name}";
		}
	}
}
