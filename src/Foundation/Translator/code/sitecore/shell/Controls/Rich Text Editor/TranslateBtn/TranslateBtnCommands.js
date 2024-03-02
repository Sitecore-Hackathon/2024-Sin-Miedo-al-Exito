
var scEditor = null;
var scTool = null;

//Set the Id of your button into the RadEditorCommandList[]
Telerik.Web.UI.Editor.CommandList["TranslateBtn"] = function (commandName, editor, args) {
    var d = Telerik.Web.UI.Editor.CommandList._getLinkArgument(editor);
    Telerik.Web.UI.Editor.CommandList._getDialogArguments(d, "A", editor, "DocumentManager");

    //Retrieve the html selected in the editor
    var html = editor.getSelectionHtml();
    scEditor = editor;
    //Call your custom dialog box
    editor.showExternalDialog(
        "/sitecore/shell/default.aspx?xmlcontrol=RichText.TranslateBtn&la=" + scLanguage + "&selectedText=" + escape(html),
        null, //argument
        700, //Height
        280, //Width
        scTranslateBtnCallback, //callback
        null, // callback args
        "TranslateBtn",
        true, //modal
        Telerik.Web.UI.WindowBehaviors.Close, // behaviors
        false, //showStatusBar
        false //showTitleBar
    );
};

//The function called when the user close the dialog
function scTranslateBtnCallback(sender, returnValue) {
    if (!returnValue) {
        return;
    }
    const returnArray = returnValue.text.split('|');

    // Call the function to make the API request
    makeApiRequest(returnArray[0], returnArray[1]);

}




//FT CODE
const apiKey = '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';

// Make the API request
function makeApiRequest(language, valueHtml) {
    const apiUrl = 'https://api.openai.com/v1/chat/completions';
    console.log('MakeAPIRequest Start');

    return fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Authorization': `Bearer ${apiKey}`,
        },
        body: JSON.stringify({
            model: 'gpt-3.5-turbo',
            messages: [
                { role: 'user', content: 'Translate to ' + language + ': ' + valueHtml }
            ],
            temperature: 0,
            max_tokens: 20,
        }),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            // Output the generated message
            const generatedMessage = data.choices[0].message.content;
            scEditor.pasteHtml(generatedMessage, "DocumentManager");
            console.log('Generated Message:', generatedMessage);
        })
        .catch(error => {
            console.error('Error making API request:', error.message);
        });
}

function GetDialogArguments() {
    return getRadWindow().ClientParameters;
}

function getRadWindow() {
    if (window.radWindow) {
        return window.radWindow;
    }

    if (window.frameElement && window.frameElement.radWindow) {
        return window.frameElement.radWindow;
    }

    return null;
}

var isRadWindow = true;

var radWindow = getRadWindow();

if (radWindow) {
    if (window.dialogArguments) {
        radWindow.Window = window;
    }
}


function scClose(text) {
    var returnValue = {
        text: text
    };

    getRadWindow().close(returnValue);

}

function scCancel() {
    getRadWindow().close();
}

function scCloseWebEdit(text) {
    window.returnValue = text;
    window.close();
}

if (window.focus && Prototype.Browser.Gecko) {
    window.focus();
}