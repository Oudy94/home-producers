using BusinessLogicLayer.Managers;
using ModelLayer.Models;
using System.Windows.Forms;

namespace DesktopApp
{
	public partial class MainForm : Form
	{
		private DashboardControl dashboardControl;
		private LoginControl loginControl;

		public MainForm()
		{
			InitializeComponent();

			dashboardControl = new DashboardControl();
			dashboardControl.Dock = DockStyle.Fill;

			loginControl = new LoginControl();
			loginControl.Dock = DockStyle.Fill;

			loginControl.LoginSuccess += OnLoginSuccess;
			Controls.Add(loginControl);
		}

		private void OnLoginSuccess(Admin admin)
		{
			loginControl.LoginSuccess -= OnLoginSuccess;
			Controls.Remove(loginControl);

			dashboardControl.LogoutSuccess += OnLogoutSuccess;
			dashboardControl.SetLoginUser(admin);
			Controls.Add(dashboardControl);
			dashboardControl.BringToFront();
		}

		private void OnLogoutSuccess(object sender, EventArgs e)
		{
			dashboardControl.LogoutSuccess -= OnLogoutSuccess;
			Controls.Remove(dashboardControl);

			loginControl.LoginSuccess += OnLoginSuccess;
			Controls.Add(loginControl);
			loginControl.BringToFront();
		}
	}
}
