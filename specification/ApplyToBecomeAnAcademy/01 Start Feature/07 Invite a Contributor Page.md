​Scenario 1: Design and Content of Invite a contributor Page

GIVEN I am viewing the 'Invite a contributor Page' page

THEN the page looks as per the text (See attached Picture 1)



Scenario 2: Hyperlink (Back to start)

GIVEN I am viewing the 'Invite a contributor Page' page
WHEN I click on the 'Back' hyperlink within the content (See highlighted area attached in Picture 2)
THEN I am redirected back to the 'Application Overview' page (#97804)

Scenario 3: Radio Buttons (NO email address inputted in Text Field - Validation Text)

GIVEN I am viewing the 'Invite a contributor Page' page
AND I haven't entered an email address

WHEN I click on the 'Send invite' CTA on the page 
THEN I am presented with an error message on the screen (See attached Picture 3)

Scenario 4: Radio Buttons (Invalid email address inputted in Text Field - Validation Text)

GIVEN I am viewing the 'Invite a contributor Page' page
AND I haven't entered a valid email address

WHEN I click on the 'Send invite' CTA on the page 
THEN I am presented with an error message on the screen (See attached Picture 4)

Scenario 5: Radio Buttons (NOT Selected an Option - Validation Text)

GIVEN I am viewing the 'Invite a contributor' page
AND I don't select one of the relevant radio buttons for the type of role I am applying to do

WHEN I click on the 'Send invite' CTA on the page 
THEN I am presented with an error message on the screen (See attached Picture 5)

Scenario 6: Radio Button (Something else option - NO Text inputted  - Validation Text)

GIVEN I am viewing the 'Invite a contributor' page
WHEN I select 'Something else' for what type of role I hold

AND I don't fill in the text box to state the name of my role 

AND I click on the 'Send invite' CTA on the page 
THEN I am presented with an error message on the screen (See attached in Picture 6)

Scenario 7: Send invite Button

GIVEN I am viewing the 'What is your role' page
WHEN I select 'Something else' for what type of role I hold

AND I am fill in the text box to state the name of my role

AND I click on the 'Send invite' CTA on the page (See highlighted area attached in Picture 7)
THEN I am redirected back to the 'Application Overview' page (#97804)

Scenario 8: Hyperlink (Return to application)

GIVEN I am viewing the 'Invite a contributor' page
WHEN I click on the 'Return to application' hyperlink within the content (See highlighted area attached in Picture 8)
THEN I am redirected back to the 'Application Overview' page (#97804)
