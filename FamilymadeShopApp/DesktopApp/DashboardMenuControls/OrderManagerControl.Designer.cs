namespace DesktopApp.DashboardMenuControls
{
	partial class OrderManagerControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 16F);
			label1.Location = new Point(326, 203);
			label1.Name = "label1";
			label1.Size = new Size(164, 30);
			label1.TabIndex = 0;
			label1.Text = "Order Manager";
			// 
			// OrderManagerControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(label1);
			Name = "OrderManagerControl";
			Size = new Size(812, 467);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
	}
}
