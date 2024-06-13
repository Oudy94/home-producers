using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;
using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp.PanelControls.ProductManagerControls
{

	public partial class AddProductControl : UserControl
	{
		public delegate void OpenProductManagerEventHandler(object sender, bool success = false);
		public event OpenProductManagerEventHandler OpenProductManager;
		private ProductManager _productManager;

		public AddProductControl()
		{
			InitializeComponent();

			_productManager = new ProductManager(new ProductRepository());
		}

		private void AddProductControl_Load(object sender, EventArgs e)
		{
			foreach (Category category in Enum.GetValues(typeof(Category)))
			{
				cmbProductCategory.Items.Add(category);
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			OpenProductManager(this);
		}

		private async void btnAddProduct_Click(object sender, EventArgs e)
		{
			string name = txtProductName.Text;
			string description = txtProductDescription.Text;
			int selectedCategory = cmbProductCategory.SelectedIndex;
			string image = txtProductImage.Text;

			if (selectedCategory == -1 || !decimal.TryParse(txtProductPrice.Text, out decimal price) || !int.TryParse(txtProductStock.Text, out int stock) || !int.TryParse(txtProductSalesCount.Text, out int salesCount))
			{
				MessageBox.Show("Please insert valid data.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Product product = new Product { Name = name, Description = description, Category = (Category)selectedCategory, Price = price, Stock = stock, Images = new List<string> { "", "", "" },  SalesCount = salesCount };

			var validationContext = new ValidationContext(product);
			var validationResults = new List<ValidationResult>();
			if (!Validator.TryValidateObject(product, validationContext, validationResults, validateAllProperties: true))
			{
				string errorMessage = string.Join("\n", validationResults.Select(result => result.ErrorMessage));
				MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				await _productManager.AddProductAsync(product);

				MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				OpenProductManager(this, true);
			}
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show($"Error add product data. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
	}
}
