### Scenario 1: Design and Content of Invite a contributor Page

GIVEN I am viewing the **Invite a Contributor** page  
THEN the page looks as per the Figma element
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=754%3A6718


### Scenario 2: Hyperlink (Back to start)

GIVEN I am viewing the **Invite a Contributor** page  
WHEN I click on the 'Back' hyperlink within the content  
THEN I am redirected back to the [**Application Overview** page](06%20Application%20Overview.md)  


### Scenario 3: Radio Buttons (NO email address inputted in Text Field - Validation Text)

GIVEN I am viewing the **Invite a Contributor** page  
AND I haven't entered an email address  
WHEN I click on the 'Send invite' CTA   
THEN I am presented with an error message as per the Figma element
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=985%3A8003


### Scenario 4: Radio Buttons (Invalid email address inputted in Text Field - Validation Text)

GIVEN I am viewing the **Invite a Contributor** page  
AND I haven't entered a valid email address  
WHEN I click on the 'Send invite' CTA
THEN I am presented with an error message as per the Figma element
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=986%3A8589


### Scenario 5: Radio Buttons (NOT Selected an Option - Validation Text)

GIVEN I am viewing the **Invite a Contributor** page  
AND I don't select one of the relevant radio buttons for the type of role I am applying to do  
WHEN I click on the 'Send invite' CTA on the page  
THEN I am presented with an error message as per the Figma element
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=985%3A8279


### Scenario 6: Radio Button (Something else option - NO Text inputted  - Validation Text)

GIVEN I am viewing the **Invite a Contributor** page  
WHEN I select 'Something else' for what type of role I hold  
AND I don't fill in the text box to state the name of my role  
AND I click on the 'Send invite' CTA  
THEN I am presented with an error message as per the Figma element
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=985%3A7711


### Scenario 7: Send invite Button

GIVEN I am viewing the **What Is Your Role** page  
WHEN I select 'Something else' for what type of role I hold  
AND I am fill in the text box to state the name of my role  
AND I click on the 'Send invite' CTA
THEN I am redirected back to the [**Application Overview** page](06%20Application%20Overview.md)


### Scenario 8: Hyperlink (Return to application)

GIVEN I am viewing the **Invite a Contributor** page 
WHEN I click on the 'Return to application' hyperlink
THEN I am redirected back to the [**Application Overview** page](06%20Application%20Overview.md)
