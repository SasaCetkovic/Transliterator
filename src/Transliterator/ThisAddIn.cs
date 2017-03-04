using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;

namespace Transliterator
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
        }

		protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
		{
			return new Ribbon(ToLatin, ToCyrillic);
		}

		private void ToLatin(ITransliterator transliterator)
		{
			var selectedRange = Application.Selection.Range;
			var format = selectedRange.ParagraphFormat.Duplicate;
			selectedRange.Text = transliterator.ToLatin(selectedRange.Text);
			selectedRange.ParagraphFormat = format;
			selectedRange.Select();
		}

		private void ToCyrillic(ITransliterator transliterator)
		{
			var selectedRange = Application.Selection.Range;
			var format = selectedRange.ParagraphFormat.Duplicate;
			selectedRange.Text = transliterator.ToCyrillic(selectedRange.Text);
			selectedRange.ParagraphFormat = format;
			selectedRange.Select();
		}


		#region VSTO generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
