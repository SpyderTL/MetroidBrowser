using System.ComponentModel.Design;
using System.ComponentModel;

namespace MetroidBrowser
{
	internal static class BrowserProgram
	{
		internal static BrowserForm Form = new();

		internal static void Start()
		{
			ApplicationConfiguration.Initialize();

			LoadRom();

			AddRomNode();

			SongPlayer.NoteOn += SongPlayer_NoteOn;
			SongPlayer.NoteOff += SongPlayer_NoteOff;
			SongPlayer.Stopped += SongPlayer_Stopped;

			Form.TreeView.AfterSelect += TreeView_AfterSelect;
			Form.TreeView.AfterExpand += TreeView_AfterExpand;
			Form.PlayButton.Click += PlayButton_Click;

			Form.FormClosing += Form_FormClosing;
			Form.Text = "Metroid ROM Browser v1.0";

			Application.Run(Form);
		}

		private static void Form_FormClosing(object? sender, FormClosingEventArgs e)
		{
			if (SongPlayer.Playing)
				SongPlayer.Stop();
		}

		private static void SongPlayer_NoteOn(int channel, int note)
		{
			switch (channel)
			{
				case 0:
					Midi.NoteOn(0, Notes[note] + 36, 127);

					Form.Invoke((int i) =>
					{
						Form.SquareLabel.Top = 500 - (note * 5);
						Form.SquareLabel.Text = note.ToString("X2");
					}, new object[] { note });
					break;

				case 1:
					Midi.NoteOn(1, Notes[note] + 36, 127);

					Form.Invoke((int i) =>
					{
						Form.Square2Label.Top = 500 - (note * 5);
						Form.Square2Label.Text = note.ToString("X2");
					}, new object[] { note });
					break;

				case 2:
					Midi.NoteOn(2, Notes[note] + 24, 127);

					Form.Invoke((int i) =>
					{
						Form.TriangleLabel.Top = 500 - (note * 5);
						Form.TriangleLabel.Text = note.ToString("X2");
					}, new object[] { note });
					break;

				case 3:
					Midi.NoteOn(9, Drums[note], 127);

					Form.Invoke((int i) =>
					{
						Form.NoiseLabel.Top = 500 - (note * 5);
						Form.NoiseLabel.Text = note.ToString("X2");
					}, new object[] { note });
					break;
			}

		}

		private static void SongPlayer_NoteOff(int channel, int note)
		{
			switch (channel)
			{
				case 0:
					Midi.NoteOff(0, Notes[note] + 36, 127);

					Form.Invoke(() =>
					{
						Form.SquareLabel.Text = string.Empty;
					});
					break;

				case 1:
					Midi.NoteOff(1, Notes[note] + 36, 127);

					Form.Invoke(() =>
					{
						Form.Square2Label.Text = string.Empty;
					});
					break;

				case 2:
					Midi.NoteOff(2, Notes[note] + 24, 127);

					Form.Invoke(() =>
					{
						Form.TriangleLabel.Text = string.Empty;
					});
					break;

				case 3:
					Midi.NoteOff(9, Drums[note], 127);

					Form.Invoke(() =>
					{
						Form.NoiseLabel.Text = string.Empty;
					});
					break;
			}
		}

		private static void SongPlayer_Stopped()
		{
			Form.Invoke(() => Form.PlayButton.Text = "4");

			Midi.Disable();
		}

		private static void PlayButton_Click(object? sender, EventArgs e)
		{
			if (SongPlayer.Playing)
			{
				SongPlayer.Stop();
			}
			else
			{
				Midi.Refresh();
				Midi.Enable();

				Midi.ProgramChange(0, Patches[Song.Envelope]);
				Midi.ProgramChange(1, Patches[Song.Envelope2]);
				Midi.ProgramChange(2, Midi.Patches.FingerBass);

				Midi.ControlChange(0, Midi.Controls.Chorus, 0);
				Midi.ControlChange(1, Midi.Controls.Chorus, 0);
				Midi.ControlChange(2, Midi.Controls.Chorus, 0);

				Midi.ControlChange(0, Midi.Controls.Reverb, 127);
				Midi.ControlChange(1, Midi.Controls.Reverb, 127);
				Midi.ControlChange(2, Midi.Controls.Reverb, 127);
				Midi.ControlChange(9, Midi.Controls.Reverb, 127);

				SongPlayer.Play();
				Form.PlayButton.Text = "<";
			}
		}

		private static void TreeView_AfterExpand(object? sender, TreeViewEventArgs e)
		{
			switch (e.Node?.Name)
			{
				case "area":
					var area = (int)e.Node.Tag;

					var roomCount = (RomMap.AreaStructures[area] - RomMap.AreaRooms[area]) >> 1;
					var structuresCount = (RomMap.AreaSpecialItems[area] - RomMap.AreaStructures[area]) >> 1;

					var roomsNode = new TreeNode("Rooms");

					for (var x = 0; x < roomCount; x++)
					{
						var roomNode = new TreeNode(x.ToString("X2"));
						roomNode.Name = "room";
						roomNode.Tag = x;

						roomsNode.Nodes.Add(roomNode);
					}

					var structuresNode = new TreeNode("Structures");

					for (var x = 0; x < structuresCount; x++)
					{
						var structureNode = new TreeNode(x.ToString("X2"));
						structureNode.Name = "structure";
						structureNode.Tag = x;

						structuresNode.Nodes.Add(structureNode);
					}

					e.Node.Nodes.Clear();
					e.Node.Nodes.Add(roomsNode);
					e.Node.Nodes.Add(structuresNode);

					break;

				case "song":
					var song = (int)e.Node.Tag;

					RomSongs.LoadSong(song);

					SongReader.Reset();

					var squareNode = new TreeNode("Square");

					while (SongReader.ReadSquare())
					{
						squareNode.Nodes.Add(SongNode());
					}

					var squareNode2 = new TreeNode("Square 2");

					while (SongReader.ReadSquare2())
					{
						squareNode2.Nodes.Add(SongNode());
					}

					var triangleNode = new TreeNode("Triangle");

					while (SongReader.ReadTriangle())
					{
						triangleNode.Nodes.Add(SongNode());
					}

					var noiseNode = new TreeNode("Noise");

					while (SongReader.ReadNoise())
					{
						noiseNode.Nodes.Add(SongNode());
					}

					e.Node.Nodes.Clear();

					e.Node.Nodes.Add(squareNode);
					e.Node.Nodes.Add(squareNode2);
					e.Node.Nodes.Add(triangleNode);
					e.Node.Nodes.Add(noiseNode);

					break;
			}
		}

		private static string SongNode()
		{
			return SongReader.Command switch
			{
				SongReader.CommandType.Note => "Note " + SongReader.Note,
				SongReader.CommandType.Duration => "Duration " + SongReader.Duration,
				SongReader.CommandType.BeginLoop => "Begin Loop " + SongReader.Loop,
				_ => SongReader.Command.ToString(),
			};
		}

		private static void TreeView_AfterSelect(object? sender, TreeViewEventArgs e)
		{
			switch (e.Node?.Name)
			{
				case "map":
					CreateMapImage();

					Form.SongPanel.Hide();
					Form.ImagePanel.Show();
					break;

				case "area":
					var area = (int)e.Node.Tag;

					Form.PropertyGrid.SelectedObject = new
					{
						Name = RomMap.AreaNames[area]
					};

					Form.SongPanel.Hide();
					Form.ImagePanel.Hide();
					break;

				case "room":
					var room = (int)e.Node.Tag;
					area = (int)e.Node.Parent.Parent.Tag;

					RomMap.LoadRoom(area, room);

					Form.PropertyGrid.SelectedObject = new
					{
						Room.Color,
						Room.Doors,
						Room.ObjectColors,
						Room.ObjectStructures,
						Room.ObjectLocations,
						Room.EnemySprites,
						Room.EnemyTypes,
						Room.EnemyLocations
					};

					break;

				case "structure":
					var structure = (int)e.Node.Tag;
					area = (int)e.Node.Parent.Parent.Tag;

					RomMap.LoadStructure(area, structure);

					Form.PropertyGrid.SelectedObject = new
					{
						Structure.Columns,
						Rows = $"{Structure.Tiles.Length / Structure.Columns} (+{Structure.Tiles.Length % Structure.Columns})",
						Structure.Tiles
					};

					break;

				case "song":
					var song = (int)e.Node.Tag;

					if (SongPlayer.Playing)
						SongPlayer.Stop();

					RomSongs.LoadSong(song);

					Form.PropertyGrid.SelectedObject = new SongProperties
					{
						Name = RomSongs.SongNames[song],
						Loop = RomSongs.Loop[song],
						Square = Song.Square,
						Square2 = Song.Square2,
						Triangle = Song.Triangle,
						Noise = Song.Noise,
						SquareEnvelope = Song.Envelope,
						Square2Envelope = Song.Envelope2
					};

					Form.SongNameLabel.Text = RomSongs.SongNames[song];

					Form.SquareLabel.Text = string.Empty;
					Form.Square2Label.Text = string.Empty;
					Form.TriangleLabel.Text = string.Empty;
					Form.NoiseLabel.Text = string.Empty;

					Form.ImagePanel.Hide();
					Form.SongPanel.Show();
					break;

				default:
					Form.ImagePanel.Hide();
					Form.SongPanel.Hide();
					break;
			}
		}

		private static void CreateMapImage()
		{
			var bitmap = new Bitmap(32 * 32, 32 * 32);

			using var graphics = Graphics.FromImage(bitmap);
			graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			var font = SystemFonts.DialogFont;
			var brush = SystemBrushes.ControlText;

			for (int y = 0; y < RomMap.MapHeight; y++)
			{
				for (int x = 0; x < RomMap.MapWidth; x++)
				{
					graphics.DrawString(Map.Rooms[x, y].ToString("X2"), font, brush, x * 32, y * 32);
				}
			}

			Form.PictureBox.Image = bitmap;
		}

		private static void LoadRom()
		{
			Rom.Contents = Properties.Resources.Rom;

			RomMap.Load();
			RomSongs.Load();
		}

		private static void AddRomNode()
		{
			// ROM Node
			var romNode = new TreeNode("Metroid (U)");

			// Maps
			var mapNode = new TreeNode("Map");
			mapNode.Name = "map";

			var areasNode = new TreeNode("Areas");
			areasNode.Name = "areas";

			for (var x = 0; x < RomMap.AreaCount; x++)
			{
				var areaNode = new TreeNode(RomMap.AreaNames[x]);
				areaNode.Name = "area";
				areaNode.Tag = x;

				areaNode.Nodes.Add("Loading...");

				areasNode.Nodes.Add(areaNode);
			}

			mapNode.Nodes.Add(areasNode);

			romNode.Nodes.Add(mapNode);

			// Songs
			var songsNode = new TreeNode("Songs");

			for (var song = 0; song < RomSongs.SongCount; song++)
			{
				var songNode = new TreeNode(RomSongs.SongNames[song])
				{
					Name = "song",
					Tag = song
				};

				songNode.Nodes.Add("Loading...");

				songsNode.Nodes.Add(songNode);
			}

			romNode.Nodes.Add(songsNode);

			Form.TreeView.Nodes.Add(romNode);
		}

		private class SongProperties
		{

			public string? Name { get; set; }
			public int Loop { get; set; }
			public byte[]? Square { get; set; }
			public byte[]? Square2 { get; set; }
			public byte[]? Triangle { get; set; }
			public byte[]? Noise { get; set; }
			public int SquareEnvelope { get; set; }
			public int Square2Envelope { get; set; }
		}

		private static readonly int[] Notes =
		{
			0,
			1,
			1,
			2,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,

			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,

			24,
			25,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,

			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,

			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,

			60,
			61,
			62,
			65,
		};

		private static readonly int[] Patches = new int[]
		{
			Midi.Patches.SquareLead,
			Midi.Patches.Oboe,
			Midi.Patches.Clarinet,
			//Midi.Patches.SteelGuitar,
			//Midi.Patches.JazzGuitar,
			//Midi.Patches.CleanGuitar,
			//Midi.Patches.BrassSection,
			Midi.Patches.FrenchHorn,
			Midi.Patches.Harpsichord,
			Midi.Patches.Celesta,
		};

		private static readonly int[] Drums = new int[]
		{
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.BassDrum,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.SnareDrum2,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.SplashCymbal,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat,
			Midi.Drums.HiHat
		};
	}
}