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

namespace MyNamespace
{
    public class TranslateBtn : DialogForm
    {
        // Fields
        protected Edit TextEdit;

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
            if (string.IsNullOrWhiteSpace(TextEdit.Value))
            {
                SheerResponse.Alert("Please enter a html code.", new string[0]);
            }

            if (this.Mode == "webedit")
            {
                //SheerResponse.SetDialogValue(StringUtil.EscapeJavascriptString(mediaUrl));
                base.OnOK(sender, args);
            }
            else
            {
                SheerResponse.Eval("scClose('" + TextEdit.Value + "')");
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
    }
}