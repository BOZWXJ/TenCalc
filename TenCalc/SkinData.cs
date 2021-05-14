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
	class SkinData
	{
		public bool IsLoad { get; private set; } = false;
		public bool IsShift { get; private set; } = false;
		public bool IsControl { get; private set; } = false;
		public bool IsAlt { get; private set; } = false;
		public Dictionary<Button.Status, Bitmap> BackgroundImages { get; private set; } = new Dictionary<Button.Status, Bitmap>();
		public Dictionary<Keys, Button> Buttons { get; private set; } = new Dictionary<Keys, Button>();

		public bool LoadSkinFile()
		{
			IsLoad = false;
			IsShift = false;
			IsControl = false;
			IsAlt = false;

			string path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "Skin", $"{Properties.Settings.Default.SkinName}.zip");
			try {
				using (var zip = ZipFile.OpenRead(path)) {
					foreach (var item in zip.Entries) {
						switch (item.Name.ToLower()) {
						case "location.txt":
							ReadLocation(item);
							break;
						case "on.png":
							BackgroundImages.TryAdd(Button.Status.On, ReadImage(item));
							break;
						case "off.png":
							BackgroundImages.TryAdd(Button.Status.Off, ReadImage(item));
							break;
						case "on_control.png":
							BackgroundImages.TryAdd(Button.Status.OnControl, ReadImage(item));
							break;
						case "off_control.png":
							BackgroundImages.TryAdd(Button.Status.OffControl, ReadImage(item));
							break;
						case "on_shift.png":
							BackgroundImages.TryAdd(Button.Status.OnShift, ReadImage(item));
							break;
						case "off_shift.png":
							BackgroundImages.TryAdd(Button.Status.OffShift, ReadImage(item));
							break;
						case "on_alt.png":
							BackgroundImages.TryAdd(Button.Status.OnAlt, ReadImage(item));
							break;
						case "off_alt.png":
							BackgroundImages.TryAdd(Button.Status.OffAlt, ReadImage(item));
							break;
						}
					}
				}
			} catch {
				return false;
			}
			if (Buttons.Count != 0 && BackgroundImages.ContainsKey(Button.Status.Off) && BackgroundImages.ContainsKey(Button.Status.On)) {
				IsLoad = true;
			} else {
				return false;
			}
			if (BackgroundImages.ContainsKey(Button.Status.OffControl) && BackgroundImages.ContainsKey(Button.Status.OnControl)) {
				IsControl = true;
			}
			if (BackgroundImages.ContainsKey(Button.Status.OffShift) && BackgroundImages.ContainsKey(Button.Status.OnShift)) {
				IsShift = true;
			}
			if (BackgroundImages.ContainsKey(Button.Status.OffAlt) && BackgroundImages.ContainsKey(Button.Status.OnAlt)) {
				IsAlt = true;
			}
			foreach (var btn in Buttons.Values) {
				btn.Bmp[Button.Status.On] = BackgroundImages[Button.Status.On].Clone(btn.Rectangle, BackgroundImages[Button.Status.On].PixelFormat);
				btn.Bmp[Button.Status.Off] = BackgroundImages[Button.Status.Off].Clone(btn.Rectangle, BackgroundImages[Button.Status.Off].PixelFormat);
				if (IsControl) {
					btn.Bmp[Button.Status.OnControl] = BackgroundImages[Button.Status.OnControl].Clone(btn.Rectangle, BackgroundImages[Button.Status.OnControl].PixelFormat);
					btn.Bmp[Button.Status.OffControl] = BackgroundImages[Button.Status.OffControl].Clone(btn.Rectangle, BackgroundImages[Button.Status.OffControl].PixelFormat);
				}
				if (IsShift) {
					btn.Bmp[Button.Status.OnShift] = BackgroundImages[Button.Status.OnShift].Clone(btn.Rectangle, BackgroundImages[Button.Status.OnShift].PixelFormat);
					btn.Bmp[Button.Status.OffShift] = BackgroundImages[Button.Status.OffShift].Clone(btn.Rectangle, BackgroundImages[Button.Status.OffShift].PixelFormat);
				}
				if (IsAlt) {
					btn.Bmp[Button.Status.OnAlt] = BackgroundImages[Button.Status.OnAlt].Clone(btn.Rectangle, BackgroundImages[Button.Status.OnAlt].PixelFormat);
					btn.Bmp[Button.Status.OffAlt] = BackgroundImages[Button.Status.OffAlt].Clone(btn.Rectangle, BackgroundImages[Button.Status.OffAlt].PixelFormat);
				}
			}
			return true;
		}

		private void ReadLocation(ZipArchiveEntry item)
		{
			using (var st = item.Open())
			using (var tr = new StreamReader(st)) {
				string line;
				while ((line = tr.ReadLine()) != null) {
					// todo: 表示エリア

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
							key = Keys.Enter;
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
						Buttons.Add(key, new Button() { Key = key, Rectangle = new Rectangle(int.Parse(cell[1]), int.Parse(cell[2]), int.Parse(cell[3]), int.Parse(cell[4])) });
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

		public Button GetButton(Point point)
		{
			foreach (var btn in Buttons.Values) {
				if (btn.Rectangle.Contains(point)) {
					return btn;
				}
			}
			return null;
		}

		public Button GetButton(Keys key)
		{
			foreach (var btn in Buttons.Values) {
				if (btn.Key == key) {
					return btn;
				}
			}
			return null;
		}
	}

	class Button
	{
		public enum Status { On, Off, OnControl, OffControl, OnShift, OffShift, OnAlt, OffAlt }
		public Keys Key { get; init; }
		public Rectangle Rectangle { get; init; }
		public Dictionary<Status, Bitmap> Bmp { get; set; } = new Dictionary<Status, Bitmap>();
	}
}
