﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
	public partial class ProgressPage : WizardPage
	{
		private frmWizard frmwiz;
		private Timer timer2;
		private int duration = 30;
		public ProgressPage(frmWizard frm)
		{
			InitializeComponent();
			timer2 = new()
			{
				Interval = 1000
			};
			timer2.Tick += Timer2_Tick;
			frmwiz = frm;
		}
		public void StartReset()
		{
			timer1.Stop();
			progressText.Text = "Restarting your PC";
			progressInfo.Text = "Rectify11 has finished patching your system, your pc needs to be restarted in order to apply the changes, it will automatically restart in " + duration.ToString()+ " seconds";
			frmwiz.InstallerProgress = "Restarting in " + duration.ToString() + " seconds";
			frmwiz.UpdateSideImage = global::Rectify11Installer.Properties.Resources.incomplete;
			timer2.Start();
		}

		private void Timer2_Tick(object sender, EventArgs e)
		{
			duration -= 1;
			frmwiz.InstallerProgress = "Restarting in " + duration.ToString() + " seconds";
			if (duration == 0)
			{
				timer2.Stop();
				Win32.NativeMethods.Reboot();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			NextText();
		}
		public void Start()
		{
			timer1.Start();
			NextText();
		}
		private static InstallerTexts[] Rectify11InstallerTexts =
		{
			new InstallerTexts("Did you know that...", "Rectify11 has better Win32 DPI support because we scale controls correctly.", Properties.Resources.install),
			new InstallerTexts("Rectify11 has a better theme", "It took 3 months for the Rectify11 team to create a great consistent light and dark theme.", Properties.Resources.themepage),
			new InstallerTexts("Rectify11 has better performance", "At the Rectify11 team, we value performance strongly.", Properties.Resources.installoptns),
			new InstallerTexts("Rectify11 has changed everything", "We changed many icons in many different DLL's, resulting in a more consistent operating system.", Properties.Resources.install),
			new InstallerTexts("Control panel Rectified", "We changed many details in the control panel, such as removing old gradients and adding back removed items", Properties.Resources.ep),
			new InstallerTexts("Thank you!", "Rectify11's team appreciates your support, thanks for choosing us.", Properties.Resources.install)
		};
		private class InstallerTexts
		{
			public string Title { get; set; }
			public string Description { get; set; }
			public Bitmap Side { get; set; }

			public InstallerTexts(string Title, string Description, Bitmap image)
			{
				this.Title = Title;
				this.Description = Description;
				Side = image;
			}
		}
		private int CurrentTextIndex = -1;
		private void NextText()
		{
			CurrentTextIndex++;
			if (CurrentTextIndex >= Rectify11InstallerTexts.Length)
			{
				CurrentTextIndex = -1;
			}
			else
			{
				var t = Rectify11InstallerTexts[CurrentTextIndex];
				progressText.Text = t.Title;
				progressInfo.Text = t.Description;
				frmwiz.UpdateSideImage = t.Side;
			}
		}
	}
}
