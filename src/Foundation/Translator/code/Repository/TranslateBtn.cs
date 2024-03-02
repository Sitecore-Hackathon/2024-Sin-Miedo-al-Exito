using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;

namespace SMAE.Foundation.Translator.Repository
{
    public class TranslateBtn : DialogForm
    {
        // Fields from the dialog
        protected Listbox LanguagesList;
        protected Memo memCode;
        /// <summary>
        /// When click on cancel does nothing and closes the dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnCancel(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            if (this.Mode == "webedit")
            {
                base.OnCancel(sender, args);
            }
            else
            {
                SheerResponse.Eval("scCancel()");
            }
        }
        /// <summary>
        /// When click on translate Button (OnOk) sends the data to the js to call the API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");


            if (this.Mode == "webedit")
            {
                base.OnOK(sender, args);
            }
            else
            {
                string code = memCode.Value;
                SheerResponse.Eval("scClose('" + LanguagesList.SelectedItem.Value + "|" + code + "')");
            }
        }

        /// <summary>
        /// Check the mode of the window
        /// </summary>
        protected string Mode
        {
            get
            {
                string str = StringUtil.GetString(base.ServerProperties["Mode"]);
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
                return "shell";
            }
            set
            {
                Assert.ArgumentNotNull(value, "value");
                base.ServerProperties["Mode"] = value;
            }
        }
        /// <summary>
        /// Loads droplist with languages and takes the selected text and populates an input
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Context.ClientPage.IsEvent)
            {
                // Populate fields into settings form fields 
                PopulateFormFields();
                string text = WebUtil.GetQueryString("selectedText");
                memCode.Value = text;
            }
        }
        //populate
        private void PopulateFormFields()
        {
            List<string> auxLanguages = new List<string> { "English", "Spanish", "French" };
            foreach (var field in auxLanguages)
            {
                LanguagesList.Controls.Add(new ListItem()
                {
                    Header = field,
                    Selected = field == auxLanguages.LastOrDefault(),
                    ID = Control.GetUniqueID("efn"),
                    Value = field
                });
            }
        }
    }
}