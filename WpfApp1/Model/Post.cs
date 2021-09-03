using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
	/// <summary>
	/// the model of Post 
	/// </summary>
	public class Post
	{
		public int userId { get; set; }
		public int id { get; set; }
		public string title { get; set; }
		public string body { get; set; }
	}
}
