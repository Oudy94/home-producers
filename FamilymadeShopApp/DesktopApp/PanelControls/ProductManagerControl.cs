using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;
using DesktopApp.PanelControls.ProductManagerControls;
using DesktopApp.PanelControls.UserManagerControls;
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

namespace DesktopApp.PanelControls
{
	public partial class ProductManagerControl : UserControl
	{
		private ProductManager _productManager;
		private bool _isLoading = false;
		private int _maxPageNumber;
		private int _pageNumber;
		private DateTime _lastRequestTime;
		private int _requestCount;
		private List<string> _categoryValues;
		private HashSet<int> _editedRowsSet;
        private AutoCompleteStringCollection _autoCompleteCollection;

        public ProductManagerControl()
		{
			InitializeComponent();

			_productManager = new ProductManager(new ProductRepository());
			_isLoading = false;
			_maxPageNumber = 0;
			_pageNumber = 1;
			_lastRequestTime = DateTime.MinValue;
			_requestCount = 0;
			_categoryValues = Enum.GetNames(typeof(Category)).ToList();
			_editedRowsSet = new HashSet<int>();
            _autoCompleteCollection = new AutoCompleteStringCollection();
        }

		private async void ProductManagerControl_Load(object sender, EventArgs e)
		{
			BuildProductGridView();

            txtFilterSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            List<string> productNames = await FetchProductSearchAutoCompleteData();
            _autoCompleteCollection.AddRange(productNames.ToArray());
            txtFilterSearch.AutoCompleteCustomSource = _autoCompleteCollection;

            cmbFilterCategory.Items.Insert(0, "All");
			cmbFilterCategory.Items.AddRange(_categoryValues.ToArray());
			cmbFilterCategory.SelectedIndex = 0;
		}

		private async Task<List<string>> FetchProductSearchAutoCompleteData()
		{
            List<string> productsNames = new List<string>();

            try
            {
                productsNames = await _productManager.GetProductsNamesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return productsNames;
        }

        private void BuildProductGridView()
		{
			dgvProducts.Columns.Add("Id", "Id");
			dgvProducts.Columns.Add("Name", "Name");
			dgvProducts.Columns.Add("Description", "Description");
			DataGridViewComboBoxColumn categoryColumn = new DataGridViewComboBoxColumn();
			categoryColumn.Name = "Category";
			categoryColumn.HeaderText = "Category";
			categoryColumn.DataSource = _categoryValues;
			dgvProducts.Columns.Add(categoryColumn);
			dgvProducts.Columns.Add("Price", "Price");
			dgvProducts.Columns.Add("Stock", "Stock");
			dgvProducts.Columns.Add("Image", "Image");
			dgvProducts.Columns.Add("SalesCount", "Sales Count");

			dgvProducts.Columns["Id"].ReadOnly = true;
		}

		private void PopulateProductData(List<Product> products)
		{
			dgvProducts.Rows.Clear();

			foreach (Product product in products)
			{
				dgvProducts.Rows.Add(product.Id, product.Name, product.Description, _categoryValues[(int)product.Category], product.Price, product.Stock, product.Images[0], product.SalesCount);
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

		private async Task<bool> LoadAndDisplayProductDataAsync(int pageNumber = 0)
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
			PopulateProductData(await FetchProductDataAsync());

			BuildPageNumber();
			return true;
		}

		private async Task<List<Product>> FetchProductDataAsync()
		{
			List<Product> products = new List<Product>();

			try
			{
				ShowLoadingIndicator();

				Category? categorySelected = cmbFilterCategory.SelectedIndex > 0 ? (Category)cmbFilterCategory.SelectedIndex - 1 : null;
				int totalCount = await _productManager.GetProductsCountAsync(txtFilterSearch.Text, categorySelected);
				_maxPageNumber = (int)Math.Ceiling((double)totalCount / s_ProductsPageSize);

				products = await _productManager.GetAllProductsAsync(_pageNumber, s_ProductsPageSize, txtFilterSearch.Text, categorySelected);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				HideLoadingIndicator();
			}

			return products;
		}

		private void ClearDataEditingChanges()
		{
			foreach (DataGridViewRow row in dgvProducts.Rows)
			{
				foreach (DataGridViewCell cell in row.Cells)
				{
					cell.Style.BackColor = Color.Empty;
				}
			}

			_editedRowsSet.Clear();
			btnSaveData.Enabled = false;
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

		private async Task RefreshProductData()
		{
			await LoadAndDisplayProductDataAsync(1);
		}

		private async void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			await RefreshProductData();
		}

		private async void btnFilterSearch_Click(object sender, EventArgs e)
		{
			await RefreshProductData();
		}

		private async void txtFilterSearch_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				await RefreshProductData();
				e.Handled = true;
			}
		}

		private async void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (int.TryParse(txtCurrentPage.Text, out int pageNumber))
				{
					await LoadAndDisplayProductDataAsync(pageNumber);
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
				await LoadAndDisplayProductDataAsync(pageNumber);
			}
		}

		private async void btnPreviousPage_Click(object sender, EventArgs e)
		{
			await LoadAndDisplayProductDataAsync(_pageNumber - 1);
		}

		private async void btnNextPage_Click(object sender, EventArgs e)
		{
			await LoadAndDisplayProductDataAsync(_pageNumber + 1);
		}

		private void dgvProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;

			DataGridViewRow editedRow = dgvProducts.Rows[e.RowIndex];
			DataGridViewCell editedCell = editedRow.Cells[e.ColumnIndex];

			editedCell.Style.BackColor = Color.Yellow;

			if (!_editedRowsSet.Contains(e.RowIndex))
			{
				btnSaveData.Enabled = true;
				_editedRowsSet.Add(e.RowIndex);
			}
		}

		private async void btnSaveData_Click(object sender, EventArgs e)
		{
			if (_editedRowsSet.Count == 0)
			{
				MessageBox.Show("Nothing to be updated!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!TryGetEditedProducts(out List<Product> products))
			{
				MessageBox.Show("Please fill all the fields correctly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				bool updateSuccess = await _productManager.UpdateProductsAsync(products);
				if (updateSuccess)
				{
					ClearDataEditingChanges();
					MessageBox.Show(($"Update product{(_editedRowsSet.Count > 1 ? "s" : "")} succeeded."), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool TryGetEditedProducts(out List<Product> products)
		{
			products = new List<Product>();

			foreach (int rowIndex in _editedRowsSet)
			{
				if (rowIndex >= 0 && rowIndex < dgvProducts.Rows.Count)
				{
					DataGridViewRow row = dgvProducts.Rows[rowIndex];

					if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id))
					{
						return false;
					}

					string name = row.Cells["Name"].Value?.ToString();
					if (string.IsNullOrWhiteSpace(name))
					{
						return false;
					}

					string description = row.Cells["Description"].Value?.ToString();

					DataGridViewComboBoxCell categoryCell = row.Cells["Category"] as DataGridViewComboBoxCell;
					if (categoryCell.EditedFormattedValue == null)
					{
						return false;
					}
					Category category = (Category)categoryCell.Items.IndexOf(categoryCell.EditedFormattedValue);

					if (!decimal.TryParse(row.Cells["Price"].Value?.ToString(), out decimal price))
					{
						return false;
					}

					if (!int.TryParse(row.Cells["Stock"].Value?.ToString(), out int stock))
					{
						return false;
					}

					string image = row.Cells["Image"].Value?.ToString();

					if (!int.TryParse(row.Cells["SalesCount"].Value?.ToString(), out int salesCount))
					{
						return false;
					}

					Product product = new Product
					{
						Id = id,
						Name = name,
						Description = description,
						Category = category,
						Price = price,
						Stock = stock,
						Images = new List<string> { "", "", "" },
						SalesCount = salesCount
					};

					products.Add(product);
				}
			}

			return true;
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

		private async void btnRefreshData_Click(object sender, EventArgs e)
		{
			await RefreshProductData();
		}

		private void btnAddProduct_Click(object sender, EventArgs e)
		{
			pnlProductManager.Visible = false;

			AddProductControl addProductControl = new AddProductControl();
			addProductControl.Dock = DockStyle.Fill;

			addProductControl.OpenProductManager += OnOpenProduct;

			this.Controls.Add(addProductControl);
		}

		private async void OnOpenProduct(object sender, bool success)
		{
			this.Controls.Remove((Control)sender);
			pnlProductManager.Visible = true;

			if (success)
			{
				await RefreshProductData();

                //if (!_autoCompleteCollection.Contains(newProductName))
                //{
                //    _autoCompleteCollection.Add(newProductName);
                //}
            }
		}
	}
}
