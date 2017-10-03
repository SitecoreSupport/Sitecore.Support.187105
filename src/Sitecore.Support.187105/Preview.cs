using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Web;
using Sitecore.Web;
using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Support.Shell.Controls.RADEditor
{
    /// <summary>The preview.</summary>
    public class Preview : Sitecore.Web.UI.HtmlControls.Page
    {
        /// <summary>Stylesheets control.</summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected PlaceHolder Stylesheets;
        /// <summary>DisabledFlag control.</summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected PlaceHolder DisabledFlag;
        /// <summary>InitialValue control.</summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected PlaceHolder InitialValue;
        /// <summary>HelpText control.</summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected PlaceHolder HelpText;
        /// <summary>Content control.</summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected PlaceHolder Content;

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"></see> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="T:System.EventArgs"></see> object that contains the event data.
        /// </param>
        protected override void OnLoad(EventArgs e)
        {
            ShellPage.IsLoggedIn();
            Assert.ArgumentNotNull((object)e, "e");
            base.OnLoad(e);
            this.PageScriptManager.AddScripts = false;
            this.PageScriptManager.AddStylesheets = false;
            this.GetText();
            this.GetStyleSheet();
            if (WebUtil.GetQueryString("di") == "1")
            {
                this.Stylesheets.Controls.Add((System.Web.UI.Control)new LiteralControl("<style> BODY { color:#999999; background:#f9f9f9; } </style>"));
                this.DisabledFlag.Controls.Add((System.Web.UI.Control)new LiteralControl("1"));
            }
            this.HelpText.Controls.Add((System.Web.UI.Control)new LiteralControl(Translate.Text("Double click here to edit the text.")));
        }

        /// <summary>Gets the style sheet.</summary>
        private void GetStyleSheet()
        {
            this.Stylesheets.Controls.Add((System.Web.UI.Control)new LiteralControl(string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", (object)Settings.WebStylesheet)));
        }

        /// <summary>Gets the text.</summary>
        private void GetText()
        {
            string queryString = WebUtil.GetQueryString("hdl");
            string text = WebUtil.GetSessionString(queryString, (string)null);
            WebUtil.RemoveSessionValue(queryString);

            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();



            if (text == null)
                text = "__#!$No value$!#__";

            text = text.Replace(lineSeparator, "<br />").Replace(paragraphSeparator, "<br />");

            NameValueCollection nameValueCollection = new NameValueCollection(1);
            nameValueCollection.Add("sc_live", "0");
            nameValueCollection.Add("sc_disableproperties", "1");

            this.InitialValue.Controls.Add((System.Web.UI.Control)new LiteralControl(StringUtil.EscapeJavascriptString(text).Replace("</script>", "</scr\" + \"ipt>")));
            this.Content.Controls.Add((System.Web.UI.Control)new LiteralControl(text));
        }
    }
}
