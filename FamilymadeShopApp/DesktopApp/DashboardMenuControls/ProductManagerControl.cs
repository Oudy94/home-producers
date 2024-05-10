using BusinessLogicLayer.Managers;
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
	public partial class ProductManagerControl : UserControl
	{
		private ProductManager _productManager;
		private bool _isLoading = false;

		public ProductManagerControl()
		{
			InitializeComponent();

			_productManager = new ProductManager();
			cmbFilterType.SelectedIndex = 0;
		}
	}
}
