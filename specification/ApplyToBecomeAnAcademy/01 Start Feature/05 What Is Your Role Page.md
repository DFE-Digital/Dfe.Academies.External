### Scenario 1: Design and Content of What is your role Page

GIVEN I am viewing the 'What is your role' page  
THEN the page looks and reads as per the Figma element
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=21%3A1770


### Scenario 2: Hyperlink (Back to start)

GIVEN I am viewing the 'What is your role' page  
WHEN I click on the 'Back' hyperlink within the content  
THEN I am redirected back to the [**What Are You Applying To Do** page](04%20What%20Are%20You%20Applying%20To%20Do%20Page.md)


### Scenario 3: Radio Buttons (NOT Selected an Option - Validation Text)

GIVEN I am viewing the 'What is your role' page  
WHEN I don't select one of the relevant radio buttons for what type of role I hold  
AND I click on the 'Save and continue' CTA on the page  
THEN I am presented with an error message on the screen as per the Figma element  
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=939%3A7567


### Scenario 4: Radio Button (Something else option - NO Text inputted  - Validation Text)

GIVEN I am viewing the 'What is your role' page  
WHEN I select 'Something else' for what type of role I hold  
AND I don't fill in the text box to state the name of my role  
AND I click on the 'Save and continue' CTA on the page  
THEN I am presented with an error message on the screen as per the Figma element  
https://www.figma.com/file/xqCVptyOcxauUXNum87EBG/A2B-v2.0?node-id=938%3A7357


### Scenario 5: Radio Buttons (The chair of the school’s governors OR A headteacher acting on their behalf option)

GIVEN I am viewing the 'What is your role' page  
WHEN I select one of the two radio buttons  
AND I click on the 'Save and continue' CTA on the page  
THEN I am directed to the [**Application Overview** page]()


### Scenario 6: Radio Button (Something else option)

GIVEN I am viewing the 'What is your role' page  
WHEN I select 'Something else' for what type of role I hold  
AND I am fill in the text box to state the name of my role  
AND I click on the 'Save and continue' CTA on the page  
THEN I am directed to the [**Application Overview** page]()


### Scenario 6: Hyperlink (Cancel and return to your applications)

GIVEN I am viewing the 'What is your role' page  
WHEN I click on the 'Cancel and return to your applications' hyperlink within the content
THEN I am redirected to the [**Your applications list** page](03%20Your%20applications%20list%20Page.md)
