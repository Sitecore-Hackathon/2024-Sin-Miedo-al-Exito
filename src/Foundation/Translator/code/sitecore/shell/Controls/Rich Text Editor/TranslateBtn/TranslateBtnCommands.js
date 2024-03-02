
var scEditor = null;
var scTool = null;

//Set the Id of your button into the RadEditorCommandList[]
Telerik.Web.UI.Editor.CommandList["TranslateBtn"] = function (commandName, editor, args) {
    var d = Telerik.Web.UI.Editor.CommandList._getLinkArgument(editor);
    Telerik.Web.UI.Editor.CommandList._getDialogArguments(d, "A", editor, "DocumentManager");

    //Retrieve the html selected in the editor
    var html = editor.getSelectionHtml();
    console.log('s', html);
    scEditor = editor;
    scEditor.pasteHtml("new text ", "DocumentManager");


    //FT CODE
    const apiKey = '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';

    // Define the prompt you want to send to ChatGPT
    //const prompt = 'Translate the following English text to French: My name is';

    // Define additional parameters such as temperature and max tokens
    const options = {
        temperature: 0.7,
        max_tokens: 150,
        stop: '\n',
    };

    // Make the API request
    function makeApiRequest() {
        const apiUrl = 'https://api.openai.com/v1/chat/completions';

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
                    { role: 'system', content: 'You are a helpful assistant.' },
                    { role: 'user', content: html },
                ],
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

    // Call the function to make the API request
    makeApiRequest();

    //FT CODE




    //Call your custom dialog box
    editor.showExternalDialog(
        "/sitecore/shell/default.aspx?xmlcontrol=RichText.TranslateBtn&la=" + scLanguage,
        null, //argument
        500, //Height
        180, //Width
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

    //You may retreive some code from your returnValue

    //For the example I add Hello and my return value in the Rich Text
    scEditor.pasteHtml("Hello " + returnValue.text, "DocumentManager");
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