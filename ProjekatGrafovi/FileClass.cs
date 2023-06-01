using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ProjekatGrafovi
{
    public class FileClass
    {
		public int ReadNumberTxt()
		{
			if (!File.Exists("IDCsv.txt"))
			{
				File.Create("IDCsv.txt").Dispose();

				using (TextWriter tw = new StreamWriter("IDCsv.txt"))
				{
					tw.WriteLine("0");
				}
			}
			else if (File.Exists("IDCsv.txt"))
			{
				string content = File.ReadAllText("IDCsv.txt", Encoding.UTF8);

				return Int32.Parse(content);
			}
			return 0;
		}

		public void WriteNumberText(int id)
		{
			if (!File.Exists("IDCsv.txt"))
			{
				File.Create("IDCsv.txt").Dispose();

				using (TextWriter tw = new StreamWriter("IDCsv.txt"))
				{
					tw.WriteLine("0");
				}

			}
			else if (File.Exists("IDCsv.txt"))
			{
				using (TextWriter tw = new StreamWriter("IDCsv.txt"))
				{
					tw.WriteLine(id);
				}
			}
		}

		public void SaveToSvg(Canvas canvas, int idSCV)
		{
			string svg = XamlWriter.Save(canvas);
			string filePath = $"ProjekatGraf{idSCV}.svg";

			using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
			{
				writer.Write(svg);
			}
		}
    }
}
