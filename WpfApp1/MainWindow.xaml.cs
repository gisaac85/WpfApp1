using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WpfApp1.Model;
using WpfApp1.API;
using System.Windows.Controls;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		static List<Post> listToShow = new List<Post>(); // to hold the list of posts which will retrieved from API
		static int swap = 0;   // I use this varaible to swap between ID and UserId :) 
		static string tempId = string.Empty;  // to hold the Id of last clicked button
		static string tempUserId = string.Empty;  // to hold the UserId of last clikced button

		public MainWindow()
		{
			InitializeComponent();
			ChangeVisiblity(false);
		}

		private async void api_btn_ClickAsync(object sender, RoutedEventArgs e)
		{
			ChangeVisiblity(true);
			txt1.Text="";
			swap = 0;

			// call API to get the 100 posts
			var model = await WebAPI.GetPosts("https://jsonplaceholder.typicode.com/posts");

			string result = string.Empty;

			for (int i = 0; i < model.Count; i++)
			{
				listToShow.Add(new Post() { id = model[i].id, userId = model[i].userId, title = model[i].title, body = model[i].body });
			}

			swap += 2;

			listBox.ItemsSource = listToShow.Select(x => x.id).ToList();   // default view is : show Id of post

			// set the text of textBox with all post details
			foreach (var x in model)
			{
				result = Environment.NewLine + "Id: " + x.id + Environment.NewLine + "UserId: " + x.userId + Environment.NewLine + "Title: " + x.title + Environment.NewLine + "Body: " + x.body + Environment.NewLine + "-----------------------------------------------";
				api_txtbox.Text += result;
			}
			label1.Content = "You see Id of Posts Now ...";
			label2.Content = Environment.NewLine + "Count of Posts = " + model.Count;
			label1.Foreground = Brushes.Red;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string content = (sender as Button).Content.ToString();   // get the content of clicked button

			if (swap % 2 == 0)   // the logic of swap depends on result of mod by 2; if mod = 0 then show userId else show Id
			{
				listBox.ItemsSource = listToShow.Select(x => x.userId).ToList();

				var selectedPost = listToShow.Where(x => x.id == Convert.ToInt32(content)).FirstOrDefault();

				tempUserId = selectedPost.userId.ToString();
				tempId = selectedPost.id.ToString();

				txt1.Background = Brushes.Transparent;
				txt1.Text = "Id: " + selectedPost.id + Environment.NewLine + "UserId: " + selectedPost.userId + Environment.NewLine + "Title: " + selectedPost.title + Environment.NewLine + "Body: " + selectedPost.body;

				content = string.Empty;

				label1.Content = "You see UserId of Posts Now ...";
				label1.Foreground = Brushes.Blue;

				swap++;
			}
			else
			{
				listBox.ItemsSource = listToShow.Select(x => x.id).ToList();

				var selectedPost = listToShow.Where(x => x.userId == Convert.ToInt32(tempUserId) && x.id == Convert.ToInt32(tempId)).FirstOrDefault();

				txt1.Text = "Id: " + selectedPost.id + Environment.NewLine + "UserId: " + selectedPost.userId + Environment.NewLine + "Title: " + selectedPost.title + Environment.NewLine + "Body: " + selectedPost.body;

				content = string.Empty;
				EmptyTempVar();

				label1.Content = "You see Id of Posts Now ...";
				label1.Foreground = Brushes.Red;

				swap++;
			}
		}

		/// <summary>
		/// delete the value of temp vars
		/// </summary>
		private void EmptyTempVar()
		{
			tempId = string.Empty;
			tempUserId = string.Empty;
		}

		/// <summary>
		/// change the visiblity of controls depending on the passed value
		/// </summary>
		/// <param name="value"></param>
		private void ChangeVisiblity(bool value)
		{
			if (value)
			{
				api_txtbox.Visibility = Visibility.Visible;
				listBox.Visibility = Visibility.Visible;
				txt1.Visibility = Visibility.Visible;

			}
			else
			{
				api_txtbox.Visibility = Visibility.Hidden;
				listBox.Visibility = Visibility.Hidden;
				txt1.Visibility = Visibility.Hidden;
			}
		}

	}
}
