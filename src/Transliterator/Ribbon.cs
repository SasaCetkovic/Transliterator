using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.


namespace Transliterator
{
	[ComVisible(true)]
	public class Ribbon : Office.IRibbonExtensibility
	{
		private Office.IRibbonUI _ribbon;
		private ITransliterator _transliterator;
		private Action<ITransliterator> _toLatin;
		private Action<ITransliterator> _toCyrillic;

		public Ribbon()
		{
			_transliterator = new DefaultTransliterator();
		}

		public Ribbon(Action<ITransliterator> toLatin, Action<ITransliterator> toCyrillic)
		{
			_transliterator = new DefaultTransliterator();
			_toLatin = toLatin;
			_toCyrillic = toCyrillic;
		}

		#region IRibbonExtensibility Members

		public string GetCustomUI(string ribbonID)
		{
			return GetResourceText("Transliterator.Ribbon.xml");
		}

		#endregion

		#region Ribbon Callbacks
		//Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226

		public void Ribbon_Load(Office.IRibbonUI ribbonUI)
		{
			_ribbon = ribbonUI;
		}

		public Bitmap SetIcon(Office.IRibbonControl control)
		{
			var officeVersion = Globals.ThisAddIn.Application.Version;
			var registryKey = $@"Software\Microsoft\Office\{officeVersion}\Common";
			var themeValueName = "UI Theme";
			var darkTheme = false;

			using (var key = Registry.CurrentUser.OpenSubKey(registryKey, false))
			{
				var theme = (int)key.GetValue(themeValueName, 0);
				darkTheme = theme == 2 || theme == 4;
			}

			switch (control.Id)
			{
				case "toLatin":
					return new Bitmap(darkTheme ? Properties.Resources.CyrLatWhite : Properties.Resources.CyrLatBlack);
				case "toCyrillic":
					return new Bitmap(darkTheme ? Properties.Resources.LatCyrWhite : Properties.Resources.LatCyrBlack);
				default:
					return null;
			}
		}

		public void ChangeToLatin(Office.IRibbonControl control)
		{
			_toLatin(_transliterator);
		}

		public void ChangeToCyrillic(Office.IRibbonControl control)
		{
			_toCyrillic(_transliterator);
		}

		#endregion

		#region Helpers

		private static string GetResourceText(string resourceName)
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			string[] resourceNames = asm.GetManifestResourceNames();
			for (int i = 0; i < resourceNames.Length; ++i)
			{
				if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
					{
						if (resourceReader != null)
						{
							return resourceReader.ReadToEnd();
						}
					}
				}
			}
			return null;
		}

		#endregion
	}
}
