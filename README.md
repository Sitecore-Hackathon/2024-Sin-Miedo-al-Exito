# Hackathon Submission Entry form

## Team name

⟹ Sin miedo al exito

## Category

⟹ Best Use of AI

## Description

⟹ Looking for a way to facilitate content editors daily work, we have developed an easy, cost-effective tool that facilitates content translation from the Sitecore Rich Text Editor. 
This module solves the necessity of translating text without going outside of the Sitecore Rich Text Editor
The module solves the problem by allowing the editor to select any text inside of the Sitecore Rich Text Editor and translate it on the fly with the help of the OpenAI API

[Translation Screenshot](/docs/images/Translation%20Screenshot%201.jpg)

 
## Video link


⟹ [Video link](https://youtu.be/vnC0zOP8Byc)

## Pre-requisites and Dependencies

- Sitecore XM 10.3 Vanilla On Premise
- OpenAI Account with API Key

## Installation instructions

Project Publishing
1.	Download the code and open the solution SME.sln 
2.	Update the OpenAI Api Key:
	    Keys are invalidated when they are uploaded to git; to enable the component it is required to provide an APIKey (https://www.howtogeek.com/885918/how-to-get-an-openai-api-key/), or you can find some termporary examples at https://docs.google.com/document/d/1qrTxTm4j-hWpHoyIHtHbt353Y-OTII7YMu_FX5W4M2s/edit?hl=es&pli=1
	    Set API Key at from visual studio at Functaion / Translator / SAME.Fundation.Translator / sitecore / shell / Controls /Rich Text Editor / TranslateBtn / TranslateBtnCommands.js
	    “const apiKey =”

3.	In Visual Studio on the Solution Explorer window, right click on Solution ‘SMAE’ / Environment /  Environment.Platform  can click on Publish
4.	On the new Window click on Show all settings and apply the following changes:
        Connection:  Target location : set to the cm website
    	Settings:  Configuration : Release
5.	Publish

[Publish Configuration](/docs/images/Publishing.jpg)

Package Setup
1.	Install the button package TranslateBtn Package-1 which will contain the following items:
    Master:  /sitecore/templates/Project/ItemTest
    Core: /sitecore/system/Settings/Html Editor Profiles/Rich Text IDE/Toolbar 2/TranslateBtn

2. Publish the Template.


### Configuration

1. Update OpenAI Key, detailed on the Instalation Instructions


2. Web.config Update
    Update the site web.config line for Content Security Policy to support OpenAI: 
    C:\inetpub\wwwroot\<local sitecore cm>\web.config  

<location path="sitecore">
<system.webServer>
<httpProtocol>
<customHeaders>
<remove name="X-Content-Type-Options" />
<remove name="X-XSS-Protection" />
<remove name="Content-Security-Policy" />
<add name="X-XSS-Protection" value="1; mode=block" />
<add name="X-Content-Type-Options" value="nosniff " />
<add name="Content-Security-Policy" value="default-src 'self' 'unsafe-inline' 'unsafe-eval'; img-src 'self' data: https://s.gravatar.com https://*.wp.com/cdn.auth0.com/avatars; style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; font-src 'self' 'unsafe-inline' https://fonts.gstatic.com; upgrade-insecure-requests; connect-src 'self' https://api.openai.com/;" />
</customHeaders>
</httpProtocol>
</system.webServer>
</location>



## Usage instructions

1.	Open the CMS on a browser.
2.  Create a page under the Home using the Template /sitecore/templates/Project/ItemTest
3.  On the Rich Text add a text in english like  "Hello World"
4.  With the mouse select the text to be translatd.
5.  Click on the new icon button on the rich for translation
6.  Select the language and click OK.
7.  Text should be translated to the selected language.

[Translation Screenshot](/docs/images/Translation%20Screenshot%201.jpg)




![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments

If you'd like to make additional comments that is important for your module entry.
