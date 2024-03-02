using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Shell.Framework;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;

namespace SMAE.Foundation.Translator.Repository
{
    public class TranslateBtn : DialogForm
    {
        // Fields
        protected Edit Language1;
        protected Edit Language2;
        protected Listbox LanguagesList;
        protected Listbox LanguagesList2;
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
                SheerResponse.Eval("scClose('" + LanguagesList.SelectedItem.Value + " --- " + LanguagesList2.SelectedItem.Value + "')");
            }
        }

        // Properties
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Context.ClientPage.IsEvent)
            {
                // Populate WFM fields into settings form fields 
                PopulateFormFields();
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
                    Selected = field == auxLanguages.First(),
                    ID = Control.GetUniqueID("efn"),
                    Value = field
                });
                LanguagesList2.Controls.Add(new ListItem()
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