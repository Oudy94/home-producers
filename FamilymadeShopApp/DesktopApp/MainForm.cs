using BusinessLogicLayer.Managers;
using DesktopApp.MainControls;
using ModelLayer.Models;
using System.Windows.Forms;

namespace DesktopApp
{
	public partial class MainForm : Form
	{
		private PanelControl dashboardControl;
		private LoginControl loginControl;

		public MainForm()
		{
			InitializeComponent();

			loginControl = new LoginControl();
			loginControl.Dock = DockStyle.Fill;

			loginControl.LoginSuccess += OnLoginSuccess;
			Controls.Add(loginControl);
		}

		private void OnLoginSuccess(Admin admin)
		{
			//loginControl.LoginSuccess -= OnLoginSuccess;
			Controls.Remove(loginControl);

			dashboardControl = new PanelControl(admin);
			dashboardControl.Dock = DockStyle.Fill;
			dashboardControl.LogoutSuccess += OnLogoutSuccess;
			Controls.Add(dashboardControl);
			dashboardControl.BringToFront();
		}

		private void OnLogoutSuccess(object sender, EventArgs e)
		{
			//dashboardControl.LogoutSuccess -= OnLogoutSuccess;
			Controls.Remove(dashboardControl);

			loginControl.LoginSuccess += OnLoginSuccess;
			Controls.Add(loginControl);
			loginControl.BringToFront();
		}
	}
}
