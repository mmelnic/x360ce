﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace x360ce.App.Controls
{
	/// <summary>
	/// Use Direct Input device to emulate keyboard keys.
	/// </summary>
	public partial class KeyboardControl : UserControl
	{
		public KeyboardControl()
		{
			InitializeComponent();
		}

		private void MapKeyboardControl_Load(object sender, EventArgs e)
		{


			//KeyboardContextMenuStrip.Items.Add(
		}

		private void MapDataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			//e.KeyCode 
		}

		private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{

		}

	
		private void KeyboardTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			//Control.ModifierKeys == Keys.Shift

			var key = string.Empty;
			key += e.Shift ? "SHIFT+" : "";
			key += e.Control ? "CTRL+" : "";
			key += e.Alt ? "ALT+" : "";
			key += e.KeyData.ToString()
				.Replace("Key", "")
				.Replace("Shift", "")
				.Replace("Control", "")
				.Replace("Alt", "")
				.Replace("Menu", "")
				.Replace(",", "")
				.Replace(" ", "")
				.Trim('+');
			KeyboardTextBox.Text = key;
			textBox1.Text = e.KeyData.ToString();
			e.Handled = true;
			e.SuppressKeyPress = true;

		}

		private void AppendButton_Click(object sender, EventArgs e)
		{
			var m = string.Format("{0},{1},{2};", KeyboardTextBox.Text, DelayNumericUpDown.Value, LoopCheckBox.Checked ? "1" : "0");
			ScriptTextBox.AppendText(m);
		}

		private void KeyboardTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		private void KeyboardTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
		}

		private void KeyboardTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
		}



	}
}
