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
	public static class SkinData
	{
		public static string Name { get; private set; } = string.Empty;
		public static bool IsLoad { get; private set; } = false;
		public static bool IsControl { get; private set; } = false;
		public static bool IsShift { get; private set; } = false;
		public static bool IsAlt { get; private set; } = false;
		public static Dictionary<Mode, Bitmap> OnImages { get; set; } = new Dictionary<Mode, Bitmap>();
		public static Dictionary<Mode, Bitmap> OffImages { get; set; } = new Dictionary<Mode, Bitmap>();
		public static Dictionary<Button, ButtonData> Buttons { get; private set; } = new Dictionary<Button, ButtonData>();

		public static bool LoadSkinFile()
		{
			IsLoad = false;
			IsControl = false;
			IsShift = false;
			IsAlt = false;
			OnImages.Clear();
			OffImages.Clear();
			Buttons.Clear();
			string path = Path.Combine(Path.GetDirectoryName(Application.StartupPath), "Skin", $"{Properties.Settings.Default.SkinName}.zip");
			try {
				using (var zip = ZipFile.OpenRead(path)) {
					foreach (var item in zip.Entries) {
						switch (item.Name.ToLower()) {
						case "location.txt":
							ReadLocation(item);
							break;
						case "on.png":
							OnImages.TryAdd(Mode.None, ReadImage(item));
							break;
						case "off.png":
							OffImages.TryAdd(Mode.None, ReadImage(item));
							break;
						case "on_control.png":
							OnImages.TryAdd(Mode.Control, ReadImage(item));
							break;
						case "off_control.png":
							OffImages.TryAdd(Mode.Control, ReadImage(item));
							break;
						case "on_shift.png":
							OnImages.TryAdd(Mode.Shift, ReadImage(item));
							break;
						case "off_shift.png":
							OffImages.TryAdd(Mode.Shift, ReadImage(item));
							break;
						case "on_alt.png":
							OnImages.TryAdd(Mode.Alt, ReadImage(item));
							break;
						case "off_alt.png":
							OffImages.TryAdd(Mode.Alt, ReadImage(item));
							break;
						}
					}
				}
			} catch {
				return false;
			}
			if (Buttons.Count != 0 && OnImages.ContainsKey(Mode.None) && OffImages.ContainsKey(Mode.None)) {
				Name = Properties.Settings.Default.SkinName;
				IsLoad = true;
			} else {
				return false;
			}
			if (OnImages.ContainsKey(Mode.Control) && OffImages.ContainsKey(Mode.Control)) {
				IsControl = true;
			}
			if (OnImages.ContainsKey(Mode.Shift) && OffImages.ContainsKey(Mode.Shift)) {
				IsShift = true;
			}
			if (OnImages.ContainsKey(Mode.Alt) && OffImages.ContainsKey(Mode.Alt)) {
				IsAlt = true;
			}
			foreach (var btn in Buttons.Values) {
				Bitmap bmp;
				bmp = OnImages[Mode.None];
				btn.OnBmp[Mode.None] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
				bmp = OffImages[Mode.None];
				btn.OffBmp[Mode.None] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
				if (IsControl) {
					bmp = OnImages[Mode.Control];
					btn.OnBmp[Mode.Control] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
					bmp = OffImages[Mode.Control];
					btn.OffBmp[Mode.Control] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
				}
				if (IsShift) {
					bmp = OnImages[Mode.Shift];
					btn.OnBmp[Mode.Shift] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
					bmp = OffImages[Mode.Shift];
					btn.OffBmp[Mode.Shift] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
				}
				if (IsAlt) {
					bmp = OnImages[Mode.Alt];
					btn.OnBmp[Mode.Alt] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
					bmp = OffImages[Mode.Alt];
					btn.OffBmp[Mode.Alt] = bmp.Clone(btn.Rectangle, bmp.PixelFormat);
				}
			}
			return true;
		}

		private static void ReadLocation(ZipArchiveEntry item)
		{
			using (var st = item.Open())
			using (var tr = new StreamReader(st)) {
				string line;
				while ((line = tr.ReadLine()) != null) {
					if (string.IsNullOrWhiteSpace(line)) { continue; }
					if (line.StartsWith(";")) { continue; } // 行頭が ; の時コメント行
					line = line.ToLower();

					if (line.StartsWith("disp")) {
						// todo: 表示エリア

						continue;
					}

					// ボタンエリア
					string[] cell = line.Split(",");
					if (cell.Length < 6) { continue; }
					Button btn = Enum.Parse<Button>(cell[0], true);
					string[] cmd = new string[cell.Length - 5];
					Array.Copy(cell, 5, cmd, 0, cell.Length - 5);
					Buttons.TryAdd(btn, new ButtonData() { Key = btn, Rectangle = new Rectangle(int.Parse(cell[1]), int.Parse(cell[2]), int.Parse(cell[3]), int.Parse(cell[4])), Command = cmd });
				}
			}
		}

		private static Bitmap ReadImage(ZipArchiveEntry item)
		{
			using (var st = item.Open()) {
				return new Bitmap(st);
			}
		}

		public static ButtonData GetButton(Point point)
		{
			foreach (var btn in Buttons.Values) {
				if (btn.Rectangle.Contains(point)) {
					return btn;
				}
			}
			return null;
		}

		public static ButtonData GetButton(Button btn)
		{
			if (Buttons.ContainsKey(btn)) {
				return Buttons[btn];
			}
			return null;
		}
	}

	public class ButtonData
	{
		public Button Key { get; init; }
		public Rectangle Rectangle { get; init; }
		public string[] Command { get; init; }
		public Dictionary<Mode, Bitmap> OnBmp { get; set; } = new Dictionary<Mode, Bitmap>();
		public Dictionary<Mode, Bitmap> OffBmp { get; set; } = new Dictionary<Mode, Bitmap>();
	}
}
