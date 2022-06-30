### Scenario 1: Not Logged In

GIVEN I am not logged in  
WHEN I try to access a page that requires authentication  
THEN I am taken to the DfE Sign-in page


### Scenario 2: Invalid Credentials

GIVEN I have not entered correct credentials for a DfE Sign-in account  
WHEN I attempt to login  
THEN I am shown a validation error that my credentials are incorrect


### Scenario 3: Login Successful

GIVEN I have entered credentials for a DfE Sign-in account  
WHEN I attempt to login  
THEN I am logged in  
AND I am taken to the page that I was trying to access


Pages that do not require authentication:
* Landing Page
* What you will need Page
