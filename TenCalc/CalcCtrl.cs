using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	class CalcCtrl : IDisposable
	{
		KeyboardHook hook;

		public Bitmap BackgroundImage { get { return Off1; } }

		public Dictionary<Keys, Button> Buttons { get; private set; } = new Dictionary<Keys, Button>();
		Bitmap On1;
		Bitmap Off1;

		internal void Initialize()
		{
			string name = $"{Properties.Settings.Default.SkinName}.zip";
			string path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "Skin", name);

			System.Diagnostics.Debug.WriteLine(path);
			using (var zip = ZipFile.OpenRead(path)) {
				foreach (var item in zip.Entries) {
					switch (item.Name.ToLower()) {
					case "location.txt":
						ReadLocation(item);
						break;
					case "on1.bmp":
						On1 = ReadImage(item);
						break;
					case "off1.bmp":
						Off1 = ReadImage(item);
						break;
					}
				}
			}
			foreach (var btn in Buttons.Values) {
				btn.Bmp[Button.Status.On] = On1.Clone(btn.Rectangle, On1.PixelFormat);
				btn.Bmp[Button.Status.Off] = Off1.Clone(btn.Rectangle, Off1.PixelFormat);
			}

			hook = new KeyboardHook();
			hook.Hook();
		}

		public void Dispose()
		{
			hook.UnHook();
		}

		private void ReadLocation(ZipArchiveEntry item)
		{
			using (var st = item.Open())
			using (var tr = new StreamReader(st)) {
				string line;
				while ((line = tr.ReadLine()) != null) {
					var cell = line.Split(",");
					if (cell.Length >= 5) {
						Keys key;
						switch (cell[0].ToLower()) {
						case "num":
							key = Keys.NumLock;
							break;
						case "0":
							key = Keys.NumPad0;
							break;
						case "1":
							key = Keys.NumPad1;
							break;
						case "2":
							key = Keys.NumPad2;
							break;
						case "3":
							key = Keys.NumPad3;
							break;
						case "4":
							key = Keys.NumPad4;
							break;
						case "5":
							key = Keys.NumPad5;
							break;
						case "6":
							key = Keys.NumPad6;
							break;
						case "7":
							key = Keys.NumPad7;
							break;
						case "8":
							key = Keys.NumPad8;
							break;
						case "9":
							key = Keys.NumPad9;
							break;
						case "p":
							key = Keys.Decimal;
							break;
						case "ent":
							key = Keys.Return;
							break;
						case "add":
							key = Keys.Add;
							break;
						case "sub":
							key = Keys.Subtract;
							break;
						case "mul":
							key = Keys.Multiply;
							break;
						case "div":
							key = Keys.Divide;
							break;
						default:
							continue;
						}
						Buttons.Add(key, new Button() { Keys = key, Rectangle = new Rectangle(int.Parse(cell[1]), int.Parse(cell[2]), int.Parse(cell[3]), int.Parse(cell[4])) });
					}
				}
			}
		}

		private Bitmap ReadImage(ZipArchiveEntry item)
		{
			using (var st = item.Open()) {
				return new Bitmap(st);
			}
		}

		public Button CheckButton(Point point)
		{
			foreach (var btn in Buttons.Values) {
				if (btn.Rectangle.Contains(point)) {
					return btn;
				}
			}
			return null;
		}

	}

	class Button
	{
		public enum Status { On, Off }
		public Keys Keys { get; init; }
		public Rectangle Rectangle { get; init; }
		public Dictionary<Status, Bitmap> Bmp { get; set; } = new Dictionary<Status, Bitmap>();
	}

}
