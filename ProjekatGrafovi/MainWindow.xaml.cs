using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatGrafovi
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public static Dictionary<int, Cvor> allVerticles = new Dictionary<int, Cvor>();
		public static List<Grana> edgesList = new List<Grana>();
		public static Random rand = new Random();

		private string verticlesString;
		public string VerticlesString
		{
			get
			{
				return verticlesString;
			}
			set
			{
				verticlesString = value;
				OnPropertyChanged("VerticlesString");
			}
		}

		private string edgesString;
		public string EdgesString
		{
			get
			{
				return edgesString;
			}
			set
			{
				edgesString = value;
				OnPropertyChanged("EdgesString");
			}
		}


		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
		}


		public bool ValidationNoEmptySpace()
		{
			bool valid = true;

			if(VerticlesString.Equals("") || VerticlesString == null)
			{
				valid = false;
				verticles.BorderBrush = Brushes.Red;
				verticles.BorderThickness = new Thickness(3);
			}

			if (EdgesString.Equals("") || EdgesString == null)
			{
				valid = false;
				edges.BorderBrush = Brushes.Red;
				edges.BorderThickness = new Thickness(5);
			} 


			return valid;
		}

		private void StartAgain()
		{
			VerticlesString = "";
			EdgesString = "";
			//verticles.Text = "";
			//edges.Text = "";
			edgesList.Clear();
			allVerticles.Clear();
		}

		private void AddNewVertex(string[] verticlesSplit)
		{
			for (int i = 0; i < verticlesSplit.Length; i++)
			{
				int id;
				if (!Int32.TryParse(verticlesSplit[i], out id))
				{
					MessageBox.Show("Input was not in correct form", "Add new vertex", MessageBoxButton.OK, MessageBoxImage.Error);
					VerticlesString = "";
					EdgesString = "";
					//verticles.Text = "";
					//edges.Text = "";
					verticles.BorderBrush = Brushes.Red;
					verticles.BorderThickness = new Thickness(3);
					return;
				}
				else
				{
					int x = rand.Next(50, 700);
					int y = rand.Next(1, 200);
					allVerticles.Add(id, new Cvor(id, x, y));
				}
			}
		}

		private void AddNewEdge(string[] edgesSplit)
		{
			for (int i = 0; i < edgesSplit.Count(); i++)
			{
				string[] numbers = edgesSplit[i].Split(',');

				int prviID;
				int drugiID;

				if (!Int32.TryParse(numbers[0], out prviID) || !Int32.TryParse(numbers[1], out drugiID))
				{
					MessageBox.Show("Input was not in correct form", "Add new edge", MessageBoxButton.OK, MessageBoxImage.Error);
					VerticlesString = "";
					EdgesString = "";
					//verticles.Text = "";
					//edges.Text = "";
					edges.BorderBrush = Brushes.Red;
					edges.BorderThickness = new Thickness(5);
					return;
				}
				else
				{
					Cvor prvi = new Cvor();
					Cvor drugi = new Cvor();

					if (!allVerticles.ContainsKey(prviID) || !allVerticles.ContainsKey(drugiID))
					{
						MessageBox.Show($"Grana sa cvorovima {prviID} i {drugiID} sadrzi neinicijalizovan cvor!");
						VerticlesString = "";
						EdgesString = "";
						//verticles.Text = "";
						//edges.Text = "";
						return;
					}
					else
					{
						Grana g = new Grana(allVerticles[prviID], allVerticles[drugiID]);
						edgesList.Add(g);
					}
				}
			}
		}


		private void DrawGraph()
		{
			foreach (Grana g in edgesList)
			{
				Line line = new Line();

				line.X1 = g.prvi.X;
				line.Y1 = g.prvi.Y;
				line.X2 = g.drugi.X;
				line.Y2 = g.drugi.Y;

				line.StrokeThickness = 5;

				line.Stroke = Brushes.Red;

				Ellipse ellipse1 = new Ellipse();
				Ellipse ellipse2 = new Ellipse();

				ellipse1.Width = 30;
				ellipse1.Height = 30;
				ellipse1.StrokeThickness = 5;
				ellipse1.Stroke = Brushes.Red;
				ellipse1.Fill = Brushes.Red;

				ellipse2.Width = 30;
				ellipse2.Height = 30;
				ellipse2.StrokeThickness = 5;
				ellipse2.Stroke = Brushes.Red;
				ellipse2.Fill = Brushes.Red;

				Canvas.SetLeft(ellipse1, g.prvi.X * 1.0 - 15);
				Canvas.SetTop(ellipse1, g.prvi.Y * 1.0 - 15);

				Canvas.SetLeft(ellipse2, g.drugi.X * 1.0 - 15);
				Canvas.SetTop(ellipse2, g.drugi.Y * 1.0 - 15);

				TextBlock prviText = new TextBlock();
				prviText.Text = g.prvi.Id.ToString();
				prviText.Foreground = Brushes.White;
				prviText.FontWeight = FontWeights.Bold;
				prviText.FontSize = 25;
				prviText.TextAlignment = TextAlignment.Center;
				prviText.HorizontalAlignment = HorizontalAlignment.Center;
				prviText.VerticalAlignment = VerticalAlignment.Center;
				prviText.Width = 30;
				prviText.Height = 30;

				double prviX = Canvas.GetLeft(ellipse1) + ellipse1.ActualWidth / 2;
				double prviY = Canvas.GetTop(ellipse1) + ellipse1.ActualHeight / 2;

				double drugiX = Canvas.GetLeft(ellipse2) + ellipse2.ActualWidth / 2;
				double drugiY = Canvas.GetTop(ellipse2) + ellipse2.ActualHeight / 2;

				Canvas.SetLeft(prviText, prviX - ellipse1.ActualWidth / 2);
				Canvas.SetTop(prviText, prviY - ellipse1.ActualHeight / 2);

				TextBlock drugiText = new TextBlock();
				drugiText.Text = g.drugi.Id.ToString();
				drugiText.Foreground = Brushes.White;
				drugiText.FontWeight = FontWeights.Bold;
				drugiText.FontSize = 25;
				drugiText.TextAlignment = TextAlignment.Center;
				drugiText.HorizontalAlignment = HorizontalAlignment.Center;
				drugiText.VerticalAlignment = VerticalAlignment.Center;
				drugiText.Width = 30;
				drugiText.Height = 30;

				Canvas.SetLeft(drugiText, drugiX - ellipse2.ActualWidth / 2);
				Canvas.SetTop(drugiText, drugiY - ellipse2.ActualHeight / 2);

				canvas.Children.Add(ellipse1);
				canvas.Children.Add(ellipse2);
				canvas.Children.Add(line);
				canvas.Children.Add(prviText);
				canvas.Children.Add(drugiText);
			}

			FileClass fc = new FileClass();

			int idSCV = fc.ReadNumberTxt();
			fc.SaveToSvg(canvas, idSCV);
			idSCV++;
			fc.WriteNumberText(idSCV);
		}

		private void Generisi_Click(object sender, RoutedEventArgs e)
		{
			if (!ValidationNoEmptySpace())
			{
				MessageBox.Show("You can't leave empty spaces for verticles or edges!", "Error - empty spaces", MessageBoxButton.OK, MessageBoxImage.Warning) ;
			}
			else
			{
				canvas.Children.Clear();

				string[] cvoroviSplit = VerticlesString.Split(',');

				AddNewVertex(cvoroviSplit);

				string[] graneSplit = edgesString.Split(';');

				AddNewEdge(graneSplit);

				DrawGraph();
				verticles.BorderBrush = Brushes.AliceBlue;
				edges.BorderBrush = Brushes.AliceBlue;

				StartAgain();
			}

		}
	}
}
