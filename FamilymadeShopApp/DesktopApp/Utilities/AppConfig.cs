using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Utilities
{
	public static class AppConfig
	{
		public const int s_UsersPageSize = 50;
		public const int s_ProductsPageSize = 50;
		public const int s_OrdersPageSize = 50;

		public const int s_MaxDBRequestCount = 5;
		public const int s_MaxDBRequestTime = 10;
	}
}
