import 'cypress-file-upload'

import DataGenerator from "../fixtures/data-generator"

let globalApplicationId = 10041;
// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add('login', (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add('drag', { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add('dismiss', { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite('visit', (originalFn, url, options) => { ... })

Cypress.Commands.add('loginWithWrongUsername', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    username = 'rachel.riley@msn.com'
    password = 'P1ng0*1984'
    cy.get('#username').type(username)
    cy.get('#password').type(password, { log: false})
    cy.contains('Sign in').click()
})
})

Cypress.Commands.add('loginWithNoPassword', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    username = 'dan.good@education.gov.uk'
    password = ''
    cy.get('#username').type(username)
    cy.contains('Sign in').click()
})
})

Cypress.Commands.add('loginWithNoUsername', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    username = ''
    password = 'P1ngO*1984'
    cy.get('#password').type(password, { log: false })
    cy.contains('Sign in').click()
})
})



Cypress.Commands.add('sqlInjectionAndInvalidUsername', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    username = '\' or 1=1 --'
    password = ''
    cy.get('#username').type(username)
    cy.contains('Sign in').click()
})
})

Cypress.Commands.add('crossSiteScriptAndInvalidUsername', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    username = '<script>window.alert("Hello World!")</script>'
    password = ''
    cy.get('#username').type(username)
    cy.contains('Sign in').click()
})
})

Cypress.Commands.add('loginWithWrongPassword', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    username = 'dan.good@education.gov.uk'
    password = 'POTATO'
    cy.get('#username').type(username)
    cy.get('#password').type(password)
    cy.contains('Sign in').click()
})
})

Cypress.Commands.add('login', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    username = 'dan.good@education.gov.uk'
    password = 'P1ngO*1984'
    cy.get('#username').type(username)
    cy.get('#password').type(password, { log: false})
    cy.contains('Sign in').click()
})
})

Cypress.Commands.add('loginToUnauthApplication', (username, password) => {
    //cy.get('#form-signin').should('be.visible')
  
    username = 'dan.good@education.gov.uk'
    password = 'P1ngO*1984'
    cy.get('#username').type(username)
    cy.get('#password').type(password, { log: false})
    cy.contains('Sign in').click()
})

Cypress.Commands.add('createAccount', () => {
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    cy.contains('Create account').click()
    })
})

Cypress.Commands.add('forgotPassword', () => {
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    cy.contains('Forgotten your password?').click()
    })
})

Cypress.Commands.add('forgotPasswordElementsVisible', () => {
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    cy.get('.govuk-heading-xl').contains('Forgotten your password?')
    cy.get('label[for="email"]').contains('Email address')
    cy.get('#email').should('be.visible')
    cy.get('.govuk-button').contains('Continue')
    })
})

Cypress.Commands.add('forgotPasswordEmptyEmailSubmitted', () => {
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    cy.get('.govuk-button').click()
    cy.get('#validation-email').contains('Please enter a valid email address')
})
})

Cypress.Commands.add('forgotPasswordInvalidEmailSubmitted', () => {
    cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
    cy.get('#email').type('POTATO')
    cy.get('.govuk-button').click()
    cy.get('#validation-email').contains('Please enter a valid email address')
})
})

Cypress.Commands.add('forgotPasswordA2BUserEmailSubmitted', (username) => {
    cy.origin('https://test-interactions.signin.education.gov.uk//bb11be36-f9b9-420d-8765-aeab083b495d/usernamepassword', () => {
    username = 'dangood15111984@gmail.com'
    cy.get('#email').type(username)
    cy.get('.govuk-button').click()
})
})

Cypress.Commands.add('forgotPasswordVerifyCodeElementsVisible', () => {
    cy.origin('https://test-interactions.signin.education.gov.uk/bb11be36-f9b9-420d-8765-aeab083b495d/resetpassword/1786759B-D42E-4A52-B189-4788B76DD793/confirm', () => {
    cy.get('#govuk-notification-banner-title').contains('Important')
    cy.get('.govuk-notification-banner__heading.wrap.full-width').contains('Confirm your email address by entering your verification code')
    cy.get('button[type="submit"]')
    cy.get('.govuk-heading-xl').contains('Enter your verification code')
    cy.get('label[for="code"]').contains('Verification code')
    cy.get('#code').should('be.visible')
    cy.get('.govuk-button').contains('Continue')
    })
})

Cypress.Commands.add('createAccountElementsVisible', () => {
    cy.origin('https://test-profile.signin.education.gov.uk/register', () => {
    cy.get('.govuk-heading-xl').contains('Create a DfE Sign-in account')
    cy.get('label[for="firstName"]').contains('First name')
    cy.get('#firstName').should('be.visible')
    cy.get('label[for="lastName"]').contains('Last name')
    cy.get('#lastName').should('be.visible')
    cy.get('label[for="email"]').contains('Email address')
    cy.get('.govuk-grid-column-full').contains('Do not use a generic email')
    cy.get('label[for="email"]').contains('Email address')
    cy.get('p[class="govuk-hint govuk-!-margin-top-1"]').contains('You will receive an email to verify this address')
    cy.get('#email').should('be.visible')
    cy.get('.govuk-inset-text').contains('By continuing you accept the terms and conditions')
    cy.get('.govuk-button').contains('Continue')
    })
    })

    Cypress.Commands.add('createAccountFailsWithNoData', () => {
        cy.origin('https://test-profile.signin.education.gov.uk/register', () => {
            cy.contains('Continue').click()
            cy.get('div[role="alert"]').should('be.visible')
            cy.get('a[href="#firstName"]').contains('Please enter a valid first name')
            cy.get('a[href="#lastName"]').contains('Please enter a valid last name')
            cy.get('a[href="#email"]').contains('Enter an email address')

            cy.get('#validation-firstName').contains('Please enter a valid first name')
            cy.get('#validation-lastName').contains('Please enter a valid last name')
        })
    })

Cypress.Commands.add('createAccountSuccessful', () => {
    let generateData = new DataGenerator()
    const sentArgs = { firstName: generateData.generateName(), lastName: generateData.generateName(), email: generateData.generateEmail() }
    cy.origin('https://test-profile.signin.education.gov.uk/register',
    {args: sentArgs},
    ({ firstName, lastName, email }) => { 
        cy.get('#firstName').type(firstName)
        cy.get('#lastName').type(lastName)
        cy.get('#email').type(email)
        cy.contains('Continue').click()

    })
})

Cypress.Commands.add('createAccountConfirmElementsVisible', () => {
    cy.origin('https://test-profile.signin.education.gov.uk/register', () => {
    cy.get('#notification-title').contains('Success')
    cy.get('h3[class="govuk-notification-banner__heading"]').contains('Verification email sent')
    cy.get('p[class="govuk-body"').contains('We have sent an account verification email to: ')
    cy.get('h2[class="govuk-notification-banner__title"]').contains('Important')
    cy.contains('Confirm your email address')
    cy.contains('is a valid email address, we will have sent you an email containing a verification code. If you are experiencing problems please contact DfE Sign-in')
    cy.get('.govuk-heading-xl').contains('Confirm your email address')
    cy.get('p[class="govuk-body-l"]').contains('Enter your verification code to confirm your email address.')   
    cy.get('label[for="code"]').contains('Verification code')
    cy.get('#code').should('be.visible')
    cy.get('button[type="submit"]').should('be.visible')

})
})

Cypress.Commands.add('yourApplicationsElementsVisible', () => {
    cy.wait(3000)
    cy.get('h1').contains('Your applications')
    cy.get('h2').contains('Applications in progress')
    cy.get('th:nth-child(1)').contains('Application')
    cy.get('th:nth-child(2)').contains('Trust Name')
    cy.get('th:nth-child(3)').contains('School Or Schools Applying To Convert')
    cy.get('a[href="/what-are-you-applying-to-do"]').should('be.visible').contains('Start a new application')
    cy.get('.govuk-grid-column-full > :nth-child(8)').contains('Past applications')
    cy.get('[aria-describedby="completedApplicationsTableDescription"] > .govuk-table__head > .govuk-table__row > :nth-child(1)').contains('Application')
    cy.get('[aria-describedby="completedApplicationsTableDescription"] > .govuk-table__head > .govuk-table__row > :nth-child(2)').contains('Trust Name')
    cy.get('[aria-describedby="completedApplicationsTableDescription"] > .govuk-table__head > .govuk-table__row > :nth-child(3)').contains('School Or Schools Applying To Convert')
})

Cypress.Commands.add('selectJAMNotStartedApplication', () => {
    cy.get('a[href="/application-overview?appId=80"]').contains('Join a multi-academy trust')
    cy.get('a[href="/application-overview?appId=80"]').click()
})

Cypress.Commands.add('selectJAMNotStartedApplicationButSchoolAdded', () => {
    cy.get('a[href="/application-overview?appId=10015"]').contains('Join a multi-academy trust')
    cy.get('ul').contains('Plymstock School')
    cy.get('a[href="/application-overview?appId=10015"]').click()
})

Cypress.Commands.add('yourApplicationNotStartedElementsVisible', () => {
    cy.get('.govuk-body').eq(0).then(($applicationId)=> {
        const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
        cy.log(`Application ID = ${applicationId}`)
        globalApplicationId = applicationId
        cy.log(`Global Application ID = ${globalApplicationId}`)
    
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, the school\'s chair will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')

    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).should('be.visible').contains('Add a school')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).should('be.visible').contains('Add a trust')
    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(3).contains('You will be able to invite other people to help you complete this form after you have added a school.')
})
})

Cypress.Commands.add('yourApplicationNotStartedButSchoolAddedElementsVisible', () => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, the school\'s chair will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('table[aria-describedby="schoolTableDescription"]').contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(1).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(3).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(4).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(5).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(6).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(7).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(8).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('span[class="govuk-!-font-weight-bold govuk-!-padding-right-5"]').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('[aria-describedby="trustTableDescription"]').contains('In Progress')



    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(3).contains('You can invite other people to help you complete this form or see who has already been invited.')
})


Cypress.Commands.add('selectStartANewApplication', () => {
    cy.get('a[href="/what-are-you-applying-to-do"]').click()
})

Cypress.Commands.add('whatAreYouApplyingToDoElementsVisible', () => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('h1').contains('What are you applying to do?')
    cy.get('legend').contains('When a school becomes an academy, it must either join an existing trust or form a new one.')
    cy.get('input[type="radio"]').eq(0).should('exist')
    cy.get('label[for="ApplicationTypeJoinAMat"]').contains('Join a multi-academy trust')
    cy.get('input[type="radio"]').eq(1).should('exist')
    cy.get('label[for="ApplicationTypeFormAMat"]').contains('Form a new multi-academy trust')
    cy.get('.govuk-inset-text').should('be.visible').contains("If your school is unable to either join an existing trust or form one with other schools, you should contact your Regional Director")
    cy.get('.govuk-link').should('be.visible').contains('your Regional Director')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectJAMRadioButton', () => {
    cy.get('input[type="radio"]').eq(0).click()
})

Cypress.Commands.add('verifyJAMRadioButtonChecked', () => {
    cy.get('input[type="radio"]').eq(0).should('be.checked')
})

Cypress.Commands.add('selectFAMRadioButton', () => {
    cy.get('input[type="radio"]').eq(1).click()
})

Cypress.Commands.add('verifyFAMRadioButtonChecked', () => {
    cy.get('input[type="radio"]').eq(1).should('be.checked')
})

Cypress.Commands.add('selectApplyingToDoSaveAndContinue', () => {
 cy.get('input[type="submit"]').click()   
})

Cypress.Commands.add('whatIsYourRoleElementsVisible', () => {
    
    cy.get('h1').contains('What is your role?')
    cy.get('p').contains('Anyone from the school can contribute to this form, but only the chair of governors can complete the declaration section or submit it.')
    cy.get('input[type="radio"]').eq(0).should('exist')
    cy.get('label[for="RoleTypeChairOfGovernors"]').contains('The chair of the school\'s governors')
    cy.get('input[type="radio"]').eq(1).should('exist')
    cy.get('label[for="RoleTypeOther"]').contains('Something else')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
})


Cypress.Commands.add('selectChairOfGovernorsRadioButton', () => {
    cy.get('input[type="radio"]').eq(0).click()
})

Cypress.Commands.add('verifyChairOfGovernorsRadioButtonChecked', () => {
    cy.get('input[type="radio"]').eq(0).should('be.checked')
})

Cypress.Commands.add('selectSomethingElseRadioButton', () => {

})

Cypress.Commands.add('verifySomethingElseRadioButtonChecked', () => {

})

Cypress.Commands.add('selectWhatIsYourRoleSaveAndContinue', () => {
    cy.get('input[type="submit"]').click()
})


Cypress.Commands.add('selectAddASchool', () => {
cy.get('.govuk-body').eq(0).then(($applicationId)=> {
    const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
    cy.log(`Application ID = ${applicationId}`)
    globalApplicationId = applicationId
    cy.log(`Global Application ID = ${globalApplicationId}`)
    cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).click()
    })
})

Cypress.Commands.add('selectAddATrust', () => {
cy.get('.govuk-body').eq(0).then(($applicationId)=> {
    const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
    cy.log(`Application ID = ${applicationId}`)
    globalApplicationId = applicationId
    cy.log(`Global Application ID = ${globalApplicationId}`)
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).click()
})
})


Cypress.Commands.add('whichTrustIsSchoolJoiningElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-heading-m').contains('Trust details')
    cy.get('h2').eq(1).contains('Which trust is the school joining?')
    cy.get('label').contains('Enter the name of the trust, its Companies House number, or its group Id')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectTempSecondHalfCreateNewJAMApplication', () => {
    cy.get(`a[href="/application-overview?appId=${globalApplicationId}"]`).contains('Join a multi-academy trust')
    cy.get(`a[href="/application-overview?appId=${globalApplicationId}"]`).click()
})



Cypress.Commands.add('selectTrustDetails', () => {
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).click()
})


Cypress.Commands.add('selectChangeSchool', () => {
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).click()
})

Cypress.Commands.add('selectSchoolName', () => {
    cy.get('.autocomplete__wrapper > #SearchQueryInput').click()
    cy.get('.autocomplete__wrapper > #SearchQueryInput').type('Plym')
    cy.get('#SearchQueryInput__option--9').click()
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()
})

Cypress.Commands.add('whatIsTheNameOfTheSchoolElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('h2').eq(0).contains('School details')
    cy.get('h2').eq(1).contains('What is the name of the school?')
    cy.get('label').contains('Enter the name of the school, or its 6 digit unique reference number (URN)')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectChangeTrust', () => {
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).click()
})

Cypress.Commands.add('selectTrustName', () => {
    cy.get('.autocomplete__wrapper > #SearchQueryInput').click()
    cy.get('.autocomplete__wrapper > #SearchQueryInput').type('Plym')
    cy.get('#SearchQueryInput__option--4').click()
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()
})

Cypress.Commands.add('youDontHavePermissionsElementsVisible', () => {
    cy.origin('https://s184t01-a2bextcdnendpoint-ezfhhdbpembpanh5.z01.azurefd.net/application-access-exception', () => {
     cy.get('.govuk-back-link').contains('Back')
     cy.get('h1').contains('You do not have permission to view this application')
     cy.get('#main-content > div > div > p:nth-child(2)').contains('To view and contribute:')
     cy.get('li').eq(0).contains('you must have been invited by an existing contributor')
     cy.get('li').eq(1).contains('your email address must match the one entered for you by the person who sent you the invite')
     cy.get('#main-content > div > div > p:nth-child(4)').contains('If you have checked these details and are still seeing this message, contact regionalservices.rg@education.gov.uk')
     cy.get('a[href="mailto:regionalservices.rg@education.gov.uk"]').contains('regionalservices.rg@education.gov.uk')
    })
 })

 Cypress.Commands.add('JAMTrustDetailsSummaryElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Join a multi-academy trust')
    cy.get('.govuk-heading-l').contains('PLYMOUTH CAST')
    cy.get('.govuk-heading-m').eq(0).contains('The trust the school is joining')
    cy.get('.govuk-body').contains('The name of the trust')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}&urn=0"]`).contains('Change your answers')
    cy.get('.govuk-heading-m').eq(1).contains('Details')
    cy.get('p').eq(3).contains('Upload evidence that the trust consents to the school joining')
    cy.get('p').eq(5).contains('Will there be any changes to the governance of the trust due to the school joining?')
    cy.get('p').eq(7).contains('Will there be any changes at a local level due to this school joining?')
    cy.get('p').eq(8).contains('Not Entered')
    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')
    cy.get('a[class="govuk-button"]').should('be.visible').contains('Save and return to your application')
})

Cypress.Commands.add('JAMTrustDetailsSummarySelectStartSection', () => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()
})

Cypress.Commands.add('JAMTrustConsentElementsVisible', () => {
    cy.get('a[class="govuk-back-link"]').contains('Back')
    cy.get('.govuk-caption-l').contains('PLYMOUTH CAST (step 1 of 3)')
    cy.get('.govuk-heading-l').contains('Trust consent')
    cy.get('legend').eq(0).contains('Upload evidence that the trust consents to the school joining')
    cy.get('.govuk-label').eq(0).contains('This can be either a letter of consent from the trust, or the minutes of their board meeting.')
    cy.get('.govuk-label').eq(1).contains('Upload a file')
    cy.get('#trustConsentFileUpload').should('be.visible')
    cy.get('legend').eq(1).contains('Uploaded files')
    cy.get('hr').eq(0).should('be.visible')
    cy.get('p').eq(3).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')

})

Cypress.Commands.add('JAMTrustConsentFileUpload', () => {
  const filepath = '../fixtures/nine-hundredk.docx'
  cy.get('#trustConsentFileUpload').attachFile(filepath)
})

Cypress.Commands.add('JAMTrustConsentSubmit', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('changesToTheTrustElementsVisible', () => {
    cy.get('a[class="govuk-back-link"]').contains('Back')
    cy.get('.govuk-caption-l').eq(0).contains('PLYMOUTH CAST (step 2 of 3)')
    cy.get('.govuk-heading-l').contains('Changes to the trust')
    cy.get('legend').eq(0).contains('Will there be changes to the governance of the trust due to the school joining?')
    cy.get('.govuk-caption-l').eq(1).contains('For example, changes to the trustees or their roles')
    cy.get('#revenueTypeYes').should('not.be.checked')
    cy.get('.govuk-label').eq(0).contains('Yes')
    cy.get('#revenueTypeNo').should('not.be.checked')
    cy.get('.govuk-label').eq(2).contains('No')
    cy.get('#revenueTypeUnknown').should('not.be.checked')
    cy.get('.govuk-label').eq(3).contains('Unknown at this point')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('changesToTheTrustClickYes', () => {
    cy.get('#revenueTypeYes').click()
})

Cypress.Commands.add('enterChangesToTheTrust', () => {
    cy.get('#PFYRevenueStatusExplained').click()
    cy.get('#PFYRevenueStatusExplained').type('What are the changes? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ull')

})


Cypress.Commands.add('changesToTheTrustSubmit', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('localGovernanceArrangementsElementsVisible', () => {
    cy.get('a[class="govuk-back-link"]').contains('Back')
    cy.get('.govuk-caption-l').eq(0).contains('PLYMOUTH CAST (step 3 of 3)')
    cy.get('.govuk-heading-l').contains('Local governance arrangements')
    cy.get('legend').eq(0).contains('Will there be any changes at a local level due to this school joining?')
    cy.get('.govuk-caption-l').eq(1).contains('For example, setting up a local sub-regional hub or changes to any schemes of delegation.')
    cy.get('#changesToLaGovernanceYes').should('not.be.checked')
    cy.get('label[for="changesToLaGovernanceYes"]').contains('Yes')
    cy.get('#changesToLaGovernanceNo').should('be.checked')
    cy.get('label[for="changesToLaGovernanceNo"]').contains('No')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')

})

Cypress.Commands.add('localGovernanceArrangementsClickYes', () => {
    cy.get('#changesToLaGovernanceYes').click()
})

Cypress.Commands.add('enterlocalGovernanceArrangementsChanges', () => {
    cy.get('#ChangesToLaGovernanceExplained').click()
    cy.get('#ChangesToLaGovernanceExplained').type('What are the changes and how do they fit into the current lines of accountability in the trust? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et d')
})

Cypress.Commands.add('localGovernanceArrangementsSubmit', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('JAMTrustDetailsSummarySaveAndReturnToApp', () => {
    cy.get(`a[href="/application-overview?appId=${globalApplicationId}"]`).eq(1).click()
})

Cypress.Commands.add('yourApplicationNotStartedButTrustSectionCompleteElementsVisible', () => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, the school\'s chair will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('table[aria-describedby="schoolTableDescription"]').contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(1).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(3).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(4).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(5).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(6).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(7).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(8).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('span[class="govuk-!-font-weight-bold govuk-!-padding-right-5"]').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('[aria-describedby="trustTableDescription"]').contains('Completed')



    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(3).contains('You can invite other people to help you complete this form or see who has already been invited.')
})

Cypress.Commands.add('selectAboutTheConversion', () => {
    cy.contains('About the conversion').click()
})

Cypress.Commands.add('aboutTheConversionNotStartedElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains('You have not added any information')
    cy.get('hr').eq(0).should('be.visible')

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains('You have not added any information')
    cy.get('hr').eq(1).should('be.visible')

    cy.get('.govuk-body').eq(4).contains('Headteacher\'s telephone number')
    cy.get('.govuk-body').eq(5).contains('You have not added any information')
    cy.get('hr').eq(2).should('be.visible')

    cy.get('.govuk-body').eq(6).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(7).contains('You have not added any information')
    cy.get('hr').eq(3).should('be.visible')

    cy.get('.govuk-body').eq(8).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(9).contains('You have not added any information')
    cy.get('hr').eq(4).should('be.visible')

    cy.get('.govuk-body').eq(10).contains('Chair\'s telephone number')
    cy.get('.govuk-body').eq(11).contains('You have not added any information')
    cy.get('hr').eq(5).should('be.visible')

    cy.get('.govuk-body').eq(12).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(13).contains('You have not added any information')
    cy.get('hr').eq(6).should('be.visible')

    cy.get('.govuk-body').eq(14).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(15).contains('You have not added any information')
    cy.get('hr').eq(7).should('be.visible')

    cy.get('.govuk-body').eq(16).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(17).contains('You have not added any information')
    cy.get('hr').eq(8).should('be.visible')

    cy.get('a[aria-describedby="component-not started"]').eq(0).should('be.visible').contains('Start section')

  // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('.govuk-body').eq(18).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')

    cy.get('hr').eq(9).should('be.visible')

    cy.get('a[aria-describedby="component-not started"]').eq(1).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('.govuk-body').eq(20).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(21).contains('You have not added any information')

    cy.get('hr').eq(10).should('be.visible')

    cy.get('a[aria-describedby="component-not started"]').eq(2).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')
    cy.get('.govuk-body').eq(22).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(23).contains('You have not added any information')

    cy.get('hr').eq(11).should('be.visible')

    cy.get('a[aria-describedby="component-not started"]').eq(3).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')



})

Cypress.Commands.add('selectContactDetailsStartSection', () => {
    cy.get('a[aria-describedby="component-not started"]').eq(0).click()
})

Cypress.Commands.add('mainContactsNotStartedElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Main contacts')
    
    cy.get('.govuk-label').eq(0).contains('Name of headteacher')
    cy.get('#ContactHeadName').should('be.enabled')
    
    cy.get('.govuk-label').eq(1).contains('Headteacher\'s email address')
    cy.get('.govuk-hint').eq(0).contains('We will only use this to contact you regarding your application')
    cy.get('#ContactHeadEmail').should('be.enabled')

    cy.get('.govuk-label').eq(2).contains('Headteacher\'s telephone number')
    cy.get('#ContactHeadTel').should('be.enabled')

    cy.get('.govuk-label').eq(3).contains('Name of the chair of the governing body')
    cy.get('#ContactChairName').should('be.enabled')

    cy.get('.govuk-label').eq(4).contains('Chair\'s email address')
    cy.get('.govuk-hint').eq(1).contains('We will only use this to contact you regarding your application')
    cy.get('#ContactChairEmail').should('be.enabled')

    cy.get('.govuk-label').eq(5).contains('Chair\'s telephone number')
    cy.get('#ContactChairTel')

    cy.get('span[class="govuk-fieldset__legend govuk-fieldset__legend--s"]').contains('Who is the main contact for the conversion?')
    
    cy.get('#ContactTypeHeadTeacher').should('not.be.checked')
    cy.get('label[for="ContactTypeHeadTeacher"]').contains('The headteacher')

    cy.get('#ContactTypeChairOfGoverningBody').should('not.be.checked')
    cy.get('label[for="ContactTypeChairOfGoverningBody"]').contains('The chair of the governing body')

    cy.get('#ContactTypeOther').should('not.be.checked')
    cy.get('label[for="ContactTypeOther"]').contains('Someone else')

    cy.get('.govuk-fieldset__heading').contains('When your schools converts, we need to create a new DfE sign-in account for the academy. Please provide the most suitable contact to manage the new academies account.')

    cy.get('.govuk-hint').eq(3).contains('For more details on the approvers role and responsibilities please see')
    cy.get('a[href="https://help.signin.education.gov.uk/contact/approver"]').contains('the approver guide')

    cy.get('label[for="ApproverContactName"]').contains('Full Name')
    cy.get('#ApproverContactName').should('be.enabled')

    cy.get('label[for="ApproverContactEmail"]').contains('Email address')
    cy.get('.govuk-hint').eq(4).contains('We will only use this to contact you regarding your application')
    cy.get('#ApproverContactEmail').should('be.enabled')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')
})

Cypress.Commands.add('fillHeadTeacherDetails', () => {
    cy.get('#ContactHeadName').type('Paul Lockwood')
    cy.get('#ContactHeadEmail').type('paul.lockwood@education.gov.uk')
    cy.get('#ContactHeadTel').type('01752 404930')
})


Cypress.Commands.add('fillChairDetails', () => {
    cy.get('#ContactChairName').type('Dan Good')
    cy.get('#ContactChairEmail').type('dan.good@education.gov.uk')
    cy.get('#ContactChairTel').type('01752 404000')
})

Cypress.Commands.add('selectMainContactAsChair', () => {
    cy.get('#ContactTypeChairOfGoverningBody').click()
    cy.get('#ContactTypeChairOfGoverningBody').should('be.checked')
})

Cypress.Commands.add('fillApproverDetails', () => {
    cy.get('#ApproverContactName').type('Nicky Price')
    cy.get('#ApproverContactEmail').type('nicky.price@aol.com')
})

Cypress.Commands.add('submitMainContactsForm', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('aboutTheConversionMainContactsCompleteElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains('Paul Lockwood')
    cy.get('hr').eq(0).should('be.visible')

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains('paul.lockwood@education.gov.uk')
    cy.get('hr').eq(1).should('be.visible')

    cy.get('.govuk-body').eq(4).contains('Headteacher\'s telephone number')
    cy.get('.govuk-body').eq(5).contains('01752404930')
    cy.get('hr').eq(2).should('be.visible')

    cy.get('.govuk-body').eq(6).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(7).contains('Dan Good')
    cy.get('hr').eq(3).should('be.visible')

    cy.get('.govuk-body').eq(8).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(9).contains('dan.good@education.gov.uk')
    cy.get('hr').eq(4).should('be.visible')

    cy.get('.govuk-body').eq(10).contains('Chair\'s telephone number')
    cy.get('.govuk-body').eq(11).contains('01752404000')
    cy.get('hr').eq(5).should('be.visible')

    cy.get('.govuk-body').eq(12).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(13).contains('The chair of the governing body')
    cy.get('hr').eq(6).should('be.visible')

    cy.get('.govuk-body').eq(14).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(15).contains('Nicky Price')
    cy.get('hr').eq(7).should('be.visible')

    cy.get('.govuk-body').eq(16).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(17).contains('nicky.price@aol.com')
    cy.get('hr').eq(8).should('be.visible')

    cy.get('a[aria-describedby="component-change"]').eq(0).contains('Change your answers')

   // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('.govuk-body').eq(18).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')

    cy.get('hr').eq(9).should('be.visible')

    cy.get('a[aria-describedby="component-not started"]').eq(0).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('.govuk-body').eq(20).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(21).contains('You have not added any information')

    cy.get('hr').eq(10).should('be.visible')

    cy.get('a[aria-describedby="component-not started"]').eq(1).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')
    cy.get('.govuk-body').eq(22).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(23).contains('You have not added any information')

    cy.get('hr').eq(11).should('be.visible')

    cy.get('a[aria-describedby="component-not started"]').eq(2).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')
})

// OK now we want to click on Start section for Date of Conversion
Cypress.Commands.add('selectDateForConversionStartSection', () => {
    cy.get('a[aria-describedby="component-not started"]').eq(0).click()
})

Cypress.Commands.add('conversionTargetDateElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Conversion target date')
    cy.get('.govuk-body-l').contains('Conversion usually takes around 6 months. It may take longer if the school is part of a private finance initiative (PFI) contract.')
    cy.get('.govuk-heading-s').contains('Do you want the conversion to happen on a particular date?')
    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for="selectoptionYes"]').contains('Yes')
    cy.get('#selectoptionNo').should('be.checked')
    cy.get('label[for="selectoptionNo"]').contains('No')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')

})

Cypress.Commands.add('conversionTargetDateSubmit', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('reasonsForJoiningElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Reasons for joining')

    cy.get('.govuk-label').contains('Why does the school want to join this trust in particular?')

    cy.get('#ApplicationJoinTrustReason').should('be.visible')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')

})

Cypress.Commands.add('reasonsForJoiningInput', () => {
    cy.get('#ApplicationJoinTrustReason').type('Why does the school want to join this trust in particular? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.')
})

Cypress.Commands.add('submitReasonsForJoining', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('changingTheNameOfTheSchoolElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('h1').contains('Changing the name of the school')
    cy.get('#role-hint').contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for="selectoptionYes"]').contains('Yes')
    cy.get('#selectoptionNo').should('be.checked')
    cy.get('label[for="selectoptionNo"]').contains('No')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')

})

Cypress.Commands.add('submitChangingTheNameOfTheSchool', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('aboutTheConversionCompleteElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains('Paul Lockwood')
    cy.get('hr').eq(0).should('be.visible')

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains('paul.lockwood@education.gov.uk')
    cy.get('hr').eq(1).should('be.visible')

    cy.get('.govuk-body').eq(4).contains('Headteacher\'s telephone number')
    cy.get('.govuk-body').eq(5).contains('01752404930')
    cy.get('hr').eq(2).should('be.visible')

    cy.get('.govuk-body').eq(6).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(7).contains('Dan Good')
    cy.get('hr').eq(3).should('be.visible')

    cy.get('.govuk-body').eq(8).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(9).contains('dan.good@education.gov.uk')
    cy.get('hr').eq(4).should('be.visible')

    cy.get('.govuk-body').eq(10).contains('Chair\'s telephone number')
    cy.get('.govuk-body').eq(11).contains('01752404000')
    cy.get('hr').eq(5).should('be.visible')

    cy.get('.govuk-body').eq(12).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(13).contains('The chair of the governing body')
    cy.get('hr').eq(6).should('be.visible')

    cy.get('.govuk-body').eq(14).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(15).contains('Nicky Price')
    cy.get('hr').eq(7).should('be.visible')

    cy.get('.govuk-body').eq(16).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(17).contains('nicky.price@aol.com')
    cy.get('hr').eq(8).should('be.visible')

    cy.get('a[aria-describedby="component-change"]').eq(0).contains('Change your answers')

   // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('.govuk-body').eq(18).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(19).contains('No')

    cy.get('hr').eq(9).should('be.visible')

    cy.get('a[aria-describedby="component-change"]').eq(1).contains('Change your answers')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('.govuk-body').eq(20).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(21).contains('Why does the school want to join this trust in particular? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.')

    cy.get('hr').eq(10).should('be.visible')

    cy.get('a[aria-describedby="component-change"]').eq(2).contains('Change your answers')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')
    cy.get('.govuk-body').eq(22).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(23).contains('No')

    cy.get('hr').eq(11).should('be.visible')

    cy.get('a[aria-describedby="component-change"]').eq(3).contains('Change your answers')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')
})

// OK COMPLETED ABOUT THE CONVERSION FORM IS OK SO SAVE THE FORM AND RETURN TO JAM APP OVERVIEW PAGE
Cypress.Commands.add('submitAboutTheConversion', () => {
    cy.get('.govuk-button').click()
})  


Cypress.Commands.add('yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible', () => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, the school\'s chair will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('table[aria-describedby="schoolTableDescription"]').contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(1).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(3).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(4).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(5).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(6).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(7).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(8).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('span[class="govuk-!-font-weight-bold govuk-!-padding-right-5"]').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('[aria-describedby="trustTableDescription"]').contains('Completed')



    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(3).contains('You can invite other people to help you complete this form or see who has already been invited.')
})

Cypress.Commands.add('selectFurtherInformation', () => {
    cy.contains('Further information').click()
})

Cypress.Commands.add('additionalDetailsSummaryNotStartedElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Further information')

    cy.get('.govuk-heading-m').contains('Additional details')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('hr').eq(0).should('be.visible')

    cy.get('b').eq(0).contains('What will the school bring to the trust they are joining?')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('hr').eq(1).should('be.visible')

    cy.get('b').eq(1).contains('Have Ofsted recently inspected the school but not published the report yet?')
    cy.get('p').eq(4).contains('You have not added any information')

    cy.get('hr').eq(2).should('be.visible')

    cy.get('b').eq(2).contains('Are there any safeguarding investigations ongoing at the school?')
    cy.get('p').eq(6).contains('You have not added any information')

    cy.get('hr').eq(3).should('be.visible')

    cy.get('b').eq(3).contains('Is the school part of a local authority reorganisation?')
    cy.get('p').eq(8).contains('You have not added any information')

    cy.get('hr').eq(4).should('be.visible')

    cy.get('b').eq(4).contains('Is the school part of any local authority closure plans?')
    cy.get('p').eq(10).contains('You have not added any information')

    cy.get('hr').eq(5).should('be.visible')

    cy.get('b').eq(5).contains('Is your school linked to a diocese?')
    cy.get('p').eq(12).contains('You have not added any information')

    cy.get('hr').eq(6).should('be.visible')

    cy.get('b').eq(6).contains('Is the school part of a federation?')
    cy.get('p').eq(14).contains('You have not added any information')

    cy.get('hr').eq(7).should('be.visible')

    cy.get('b').eq(7).contains('Is the school supported by a foundation, trust or other body (e.g. parish council) that appoints foundation governors?')
    cy.get('p').eq(16).contains('You have not added any information')

    cy.get('hr').eq(8).should('be.visible')

    cy.get('b').eq(8).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')
    cy.get('p').eq(18).contains('You have not added any information')

    cy.get('hr').eq(9).should('be.visible')

    cy.get('b').eq(9).contains('Provide a list of your main feeder schools')
    cy.get('p').eq(20).contains('You have not added any information')

    cy.get('hr').eq(10).should('be.visible')

    cy.get('b').eq(10).contains('The school\'s Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school\'s consent to converting and joining the trust.')
    cy.get('p').eq(22).contains('You have not added any information')

    cy.get('hr').eq(11).should('be.visible')

    cy.get('b').eq(11).contains('Has an equalities impact assessment been carried out and considered by the governing body?')
    cy.get('p').eq(24).contains('You have not added any information')

    cy.get('hr').eq(12).should('be.visible')

    cy.get('b').eq(12).contains('Do you want to add any further information?')
    cy.get('p').eq(26).contains('You have not added any information')

    cy.get('hr').eq(13).should('be.visible')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})

Cypress.Commands.add('selectAdditionalDetailsStartSection', () => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()
})

Cypress.Commands.add('additionalDetailsDetailsNotStartedElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Additional details')

    cy.get('label[for="TrustBenefitDetails"]').contains('What will the school bring to the trust they are joining?')
    cy.get('#trust-benefit-hint').contains('Describe the contribution they will make')

    cy.get('#TrustBenefitDetails').should('be.enabled')

    cy.get('legend').eq(0).contains('Have Ofsted recently inspected the school but not published the report yet?')

    cy.get('#ofstedInspectedOptionYes').should('not.be.checked')
    cy.get('label[for="ofstedInspectedOptionYes"]').contains('Yes')

    cy.get('#ofstedInspectedOptionNo').should('be.checked')
    cy.get('label[for="ofstedInspectedOptionNo"]').contains('No')

    cy.get('legend').eq(1).contains('Are there any safeguarding investigations ongoing at the school')

    cy.get('#safeguardingOptionYes').should('not.be.checked')
    cy.get('label[for="safeguardingOptionYes"]').contains('Yes')

    cy.get('#safeguardingOptionNo').should('be.checked')
    cy.get('label[for="safeguardingOptionNo"]').contains('No')

    cy.get('legend').eq(2).contains('Is the school part of a local authority reorganisation?')

    cy.get('#localAuthorityOptionYes').should('not.be.checked')
    cy.get('label[for="localAuthorityOptionYes"]').contains('Yes')

    cy.get('#localAuthorityOptionNo').should('be.checked')
    cy.get('label[for="localAuthorityOptionNo"]').contains('No')


    cy.get('legend').eq(3).contains('Is the school part of any local authority closure plans?')

    cy.get('#localAuthorityClosurePlanOptionYes').should('not.be.checked')
    cy.get('label[for="localAuthorityClosurePlanOptionYes"]').contains('Yes')

    cy.get('#localAuthorityClosurePlanOptionNo').should('be.checked')
    cy.get('label[for="localAuthorityClosurePlanOptionNo"]').contains('No')


    cy.get('legend').eq(4).contains('Is the school linked to a diocese?')

    cy.get('#dioceseOptionYes').should('not.be.checked')
    cy.get('label[for="dioceseOptionYes"]').contains('Yes')

    cy.get('#dioceseOptionNo').should('be.checked')
    cy.get('label[for="dioceseOptionNo"]').contains('No')


    cy.get('legend').eq(6).contains('Is the school part of a federation')
    cy.get('#federation-hint').contains('A federation is a group of maintained schools under one governing body (The School Governance (Federations) (England) Regulations 2012)')
    cy.get('a[href="https://www.legislation.gov.uk/uksi/2012/1035/contents/made"]').contains('(The School Governance (Federations) (England) Regulations 2012)')

    cy.get('#federationOptionYes').should('not.be.checked')
    cy.get('label[for="federationOptionYes"]').contains('Yes')

    cy.get('#federationOptionNo').should('be.checked')
    cy.get('label[for="federationOptionNo"]').contains('No')

    cy.get('legend').eq(7).contains('Is the school supported by a foundation, trust or other body(e.g. parish council) that appoints foundation governors?')

    cy.get('#supportedByFoundationTrustOrBodyOptionYes').should('not.be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionYes"]').contains('Yes')

    cy.get('#supportedByFoundationTrustOrBodyOptionNo').should('be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionNo"]').contains('No')


    cy.get('legend').eq(9).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')

    cy.get('#exemptionFromSACREOptionYes').should('not.be.checked')
    cy.get('label[for="exemptionFromSACREOptionYes"]').contains('Yes')

    cy.get('#exemptionFromSACREOptionNo').should('be.checked')
    cy.get('label[for="exemptionFromSACREOptionNo"]').contains('No')

    cy.get('label[for="MainFeederSchools"]').contains('Please provide a list of your main feeder schools')
    cy.get('#feeder-schools-hint').contains('We recognise you may have many feeder schools, therefore please just detail the top 5')
    cy.get('#MainFeederSchools').should('be.enabled')

    //SCHOOL'S GOVERNING BODY RESOLUTION UPLOAD QUESTION
    cy.get('legend').eq(10).contains('The school\'s Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school\'s consent to converting and joining the trust.')
    cy.get('#Upload-resolutionConsentFiles-hint').contains('This is normally in the form of the minutes from the Governing Body meeting at which the resolution to convert was passed')
    cy.get('label[for="resolutionConsentFileUpload"]').contains('Upload a file')

    //cy.get('#resolutionConsentFileUpload').contains('Choose files')
    //cy.get('#resolutionConsentFileUpload').contains('No file chosen')

    cy.get('legend').eq(11).contains('Uploaded files')
    cy.get('hr').eq(4).should('be.visible')
    cy.get('.govuk-label').contains('No file uploaded')
    cy.get('hr').eq(5).should('be.visible')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('The application cannot be submitted without this. You can carry on the application and come back to this later.')
    // END OF BODY RESOLUTION FILE UPLOAD BLOCK

    cy.get('legend').eq(12).contains('Has an equalities impact assessment been carried out and considered by the governing body?')

    cy.get('#equalitiesImpactAssessmentOptionYes').should('not.be.checked')
    cy.get('label[for="equalitiesImpactAssessmentOptionYes"]').contains('Yes')

    cy.get('#equalitiesImpactAssessmentOptionNo').should('be.checked')
    cy.get('label[for="equalitiesImpactAssessmentOptionNo"]').contains('No')


    cy.get('legend').eq(13).contains('Do you want to add any further information?')

    cy.get('#furtherInformationOptionYes').should('not.be.checked')
    cy.get('label[for="furtherInformationOptionYes"]').contains('Yes')

    cy.get('#furtherInformationOptionNo').should('be.checked')
    cy.get('label[for="furtherInformationOptionNo"]').contains('No')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')


})

Cypress.Commands.add('fillSchoolContribution', () => {
    cy.get('#TrustBenefitDetails').type('What will the school bring to the trust they are joining? Describe the contribution they will make Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore ')
})

Cypress.Commands.add('selectYesIsSchoolLinkedToDiocese', () => {
    cy.get('#dioceseOptionYes').click()
})

Cypress.Commands.add('dioceseSectionElementsVisible', () => {
    cy.get('#dioceseOptionYes').should('be.checked')
    cy.get('label[for="dioceseOptionYes"]').contains('Yes')
    
    // TEST DIOCESE YES SECTION IS VISIBLE
    cy.get('#dioceseOption-yes').should('be.visible')

    cy.get('label[for="DioceseName"]').contains('Name of diocese')

    cy.get('#DioceseName').should('be.visible').should('be.enabled')
    cy.get('#dioceseOption-yes > :nth-child(4)').contains('Upload a letter from the diocese giving permission for the school to convert.')
    cy.get('label[for="dioceseFileUpload"]').contains('Upload a file')
    cy.get(':nth-child(5) > .govuk-hint').contains('The application cannot be submitted without this. You can carry on the application and come back to this later.')
    cy.get('#dioceseOption-yes > .govuk-fieldset__legend').contains('Uploaded files')
    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')
    cy.get('#dioceseOptionNo').should('not.be.checked')
    cy.get('label[for="dioceseOptionNo"]').contains('No')
})

Cypress.Commands.add('inputDioceseName', () => {
    cy.get('#DioceseName').type('Mr Diocese')
})

Cypress.Commands.add('dioceseFileUpload', () => {
  const filepath = '../fixtures/fifty-k.docx'
  cy.get('#dioceseFileUpload').attachFile(filepath)
})

Cypress.Commands.add('selectYesSchoolSupportedByTrustOrFoundation', () => {
    cy.get('#supportedByFoundationTrustOrBodyOptionYes').click()
})

Cypress.Commands.add('schoolSupportedByElementsVisible', () => {
    cy.get('#supportedByFoundationTrustOrBodyOptionYes').should('be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionYes"]').contains('Yes')
    cy.get('label[for="FoundationTrustOrBodyName"]').contains('Name of this body')
    cy.get('#FoundationTrustOrBodyName').should('be.visible').should('be.enabled')
    cy.get('#supportedByFoundationTrustOrBodyOption-yes > :nth-child(5)').contains('Please upload their letter of consent')
    cy.get(':nth-child(6) > label.govuk-label').contains('Upload a file')
    cy.get('#supportedByFoundationTrustOrBodyOption-yes > .govuk-fieldset__legend').contains('Uploaded files')
    cy.get('hr').eq(3).should('be.visible')
    cy.get('#supportedByFoundationTrustOrBodyOption-yes > :nth-child(9)').contains('No file uploaded')
    cy.get('hr').eq(4).should('be.visible')
    cy.get('#supportedByFoundationTrustOrBodyOptionNo').should('not.be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionNo"]').contains('No')


})

Cypress.Commands.add('inputBodyName', () => {
    cy.get('#FoundationTrustOrBodyName').type('Mr Body')
})

Cypress.Commands.add('uploadSchoolSupportedByTrustOrBody', () => {
    const filepath = '../fixtures/fifty-k.pptx'
    cy.get('#foundationConsentFileUpload').attachFile(filepath)
})

Cypress.Commands.add('inputListOfFeederSchools', () => {
    cy.get('#MainFeederSchools').type('Please provide a list of your main feeder schools We recognise you may have many feeder schools, therefore please just detail the top 5 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do ')
})

Cypress.Commands.add('uploadSchoolLetterOfConsent', () => {
    const filepath = '../fixtures/fiftyk.pdf'
    cy.get('#resolutionConsentFileUpload').attachFile(filepath)
})

Cypress.Commands.add('submitAdditionalDetailsDetails', () => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('additionalDetailsSummaryCompleteElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Further information')

    cy.get('.govuk-heading-m').contains('Additional details')

    cy.get('.govuk-link').should('be.visible').contains('Change your answers')

    cy.get('hr').eq(0).should('be.visible')

    cy.get('b').eq(0).contains('What will the school bring to the trust they are joining?')
    cy.get('p').eq(2).contains('What will the school bring to the trust they are joining? Describe the contribution they will make Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore ')

    cy.get('hr').eq(1).should('be.visible')

    cy.get('b').eq(1).contains('Have Ofsted recently inspected the school but not published the report yet?')
    cy.get('p').eq(4).contains('No')

    cy.get('hr').eq(2).should('be.visible')

    cy.get('b').eq(2).contains('Are there any safeguarding investigations ongoing at the school?')
    cy.get('p').eq(6).contains('No')

    cy.get('hr').eq(3).should('be.visible')

    cy.get('b').eq(3).contains('Is the school part of a local authority reorganisation?')
    cy.get('p').eq(8).contains('No')

    cy.get('hr').eq(4).should('be.visible')

    cy.get('b').eq(4).contains('Is the school part of any local authority closure plans?')
    cy.get('p').eq(10).contains('No')

    cy.get('hr').eq(5).should('be.visible')

    cy.get('b').eq(5).contains('Is your school linked to a diocese?')
    cy.get('p').eq(12).contains('Yes')

    cy.get('hr').eq(6).should('be.visible')

    cy.get('b').eq(6).contains('Is the school part of a federation?')
    cy.get('p').eq(14).contains('No')

    cy.get('hr').eq(7).should('be.visible')

    cy.get('b').eq(7).contains('Is the school supported by a foundation, trust or other body (e.g. parish council) that appoints foundation governors?')
    cy.get('p').eq(16).contains('Yes')

    cy.get('hr').eq(8).should('be.visible')

    cy.get('b').eq(8).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')
    cy.get('p').eq(18).contains('No')

    cy.get('hr').eq(9).should('be.visible')

    cy.get('b').eq(9).contains('Provide a list of your main feeder schools')
    cy.get('p').eq(20).contains('Please provide a list of your main feeder schools We recognise you may have many feeder schools, therefore please just detail the top 5 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do ')

    cy.get('hr').eq(10).should('be.visible')

    cy.get('b').eq(10).contains('The school\'s Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school\'s consent to converting and joining the trust.')
    cy.get('p').eq(22).contains('fiftyk.pdf')

    cy.get('hr').eq(11).should('be.visible')

    cy.get('b').eq(11).contains('Has an equalities impact assessment been carried out and considered by the governing body?')
    cy.get('p').eq(24).contains('No')

    cy.get('hr').eq(12).should('be.visible')

    cy.get('b').eq(12).contains('Do you want to add any further information?')
    cy.get('p').eq(26).contains('No')

    cy.get('hr').eq(13).should('be.visible')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')
})

Cypress.Commands.add('submitAdditionalDetailsSummary', () => {
    cy.get('.govuk-button').click()
})

Cypress.Commands.add('yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible', () => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, the school\'s chair will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('table[aria-describedby="schoolTableDescription"]').contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(1).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(3).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(4).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(5).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(6).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(7).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(8).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('span[class="govuk-!-font-weight-bold govuk-!-padding-right-5"]').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('[aria-describedby="trustTableDescription"]').contains('Completed')



    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(3).contains('You can invite other people to help you complete this form or see who has already been invited.')
})

Cypress.Commands.add('yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible', () => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, the school\'s chair will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('table[aria-describedby="schoolTableDescription"]').contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(1).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(3).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(4).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(5).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(6).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(7).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(8).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('span[class="govuk-!-font-weight-bold govuk-!-padding-right-5"]').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('[aria-describedby="trustTableDescription"]').contains('Completed')



    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(3).contains('You can invite other people to help you complete this form or see who has already been invited.')
})

Cypress.Commands.add('selectFinances', () => {
    cy.contains('Finances').click()
})

Cypress.Commands.add('financeSummaryNotStartedElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Finances')

    cy.get('.govuk-heading-m').eq(0).contains('Previous financial year')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).should('be.visible').contains('Start section')
   
    cy.get('hr').eq(0).should('be.visible')
    
    cy.get('b').eq(0).contains('End of previous financial year end date?')
    cy.get('.govuk-body').eq(1).contains('You have not added any information')
    cy.get('hr').eq(1).should('be.visible')

    cy.get('b').eq(1).contains('Revenue carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(3).contains('You have not added any information')
    cy.get('hr').eq(2).should('be.visible')

    cy.get('b').eq(2).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(5).contains('You have not added any information')
    cy.get('hr').eq(3).should('be.visible')

    cy.get('b').eq(3).contains('Capital carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(7).contains('You have not added any information')
    cy.get('hr').eq(4).should('be.visible')

    cy.get('b').eq(4).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(9).contains('You have not added any information')
    cy.get('hr').eq(5).should('be.visible')

    cy.get('.govuk-heading-m').eq(1).contains('Current financial year')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(1).should('be.visible').contains('Start section')

    cy.get('hr').eq(6).should('be.visible')

    cy.get('b').eq(5).contains('End of current financial year end date?')
    cy.get('.govuk-body').eq(11).contains('You have not added any information')
    cy.get('hr').eq(7).should('be.visible')

    cy.get('b').eq(6).contains('Revenue carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(13).contains('You have not added any information')
    cy.get('hr').eq(8).should('be.visible')

    cy.get('b').eq(7).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(15).contains('You have not added any information')
    cy.get('hr').eq(9).should('be.visible')

    cy.get('b').eq(8).contains('Capital carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(17).contains('You have not added any information')
    cy.get('hr').eq(10).should('be.visible')

    cy.get('b').eq(9).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')
    cy.get('hr').eq(11).should('be.visible')

    cy.get('.govuk-heading-m').eq(2).contains('Next financial year')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(2).should('be.visible').contains('Start section')

    cy.get('hr').eq(12).should('be.visible')

    cy.get('b').eq(10).contains('End of next financial year end date?')
    cy.get('.govuk-body').eq(21).contains('You have not added any information')
    cy.get('hr').eq(13).should('be.visible')

    cy.get('b').eq(11).contains('Revenue carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(23).contains('You have not added any information')
    cy.get('hr').eq(14).should('be.visible')

    cy.get('b').eq(12).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(25).contains('You have not added any information')
    cy.get('hr').eq(15).should('be.visible')

    cy.get('b').eq(13).contains('Capital carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(27).contains('You have not added any information')
    cy.get('hr').eq(16).should('be.visible')

    cy.get('b').eq(14).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(29).contains('You have not added any information')
    cy.get('hr').eq(17).should('be.visible')

    cy.get('.govuk-heading-m').eq(3).contains('Loans')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(3).should('be.visible').contains('Start section')

    cy.get('hr').eq(18).should('be.visible')

    cy.get('b').eq(15).contains('Are there any existing loans?')
    cy.get('.govuk-body').eq(31).contains('You have not added any information')
    cy.get('hr').eq(19).should('be.visible')

    cy.get('.govuk-heading-m').eq(4).contains('Leases')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(4).should('be.visible').contains('Start section')

    cy.get('hr').eq(20).should('be.visible')

    cy.get('b').eq(16).contains('Are there any existing leases?')
    cy.get('.govuk-body').eq(33).contains('You have not added any information')
    cy.get('hr').eq(21).should('be.visible')

    cy.get('.govuk-heading-m').eq(5).contains('Financial investigations')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(5).should('be.visible').contains('Start section')

    cy.get('hr').eq(22).should('be.visible')

    cy.get('b').eq(17).contains('Finance ongoing investigations?')
    cy.get('.govuk-body').eq(35).contains('You have not added any information')
    cy.get('hr').eq(23).should('be.visible')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})

Cypress.Commands.add('selectPreviousFinancialYrStartSection', () => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).click()

})

Cypress.Commands.add('previousFinancialYrElementsVisible', () => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Finances (Step 1 of 6)')
    cy.get('.govuk-heading-l').contains('Previous financial year')
    cy.get('legend').contains('End of previous financial year')
    cy.get('#pfy-end-date-hint').contains('For example, 01 09 2022')

    cy.get('label[for="sip_pfyenddate-day"]').contains('Day')
    cy.get('#sip_pfyenddate-day').should('be.visible').should('be.enabled')

    cy.get('label[for="sip_pfyenddate-month"]').contains('Month')
    cy.get('#sip_pfyenddate-month').should('be.visible').should('be.enabled')

    cy.get('label[for="sip_pfyenddate-year"]').contains('Year')
    cy.get('#sip_pfyenddate-year').should('be.visible').should('be.enabled')

    cy.get('label[for="Revenue"]').contains('Revenue carry forward at end of the previous financial year (31 March)')

    cy.get('#Revenue').should('be.visible').should('be.enabled')

    cy.get('#revenueRevenueTypeSurplus').should('not.be.checked')
    cy.get('label[for="revenueRevenueTypeSurplus"]').contains('Surplus')

    cy.get('#revenueRevenueTypeDeficit').should('not.be.checked')
    cy.get('label[for="revenueRevenueTypeDeficit"]').contains('Deficit')

    cy.get('label[for="CapitalCarryForward"]').contains('Capital carry forward at end of the previous financial year (31 March)')

    cy.get('#CapitalCarryForward').should('be.visible').should('be.enabled')

    cy.get('#capitalRevenueTypeSurplus').should('not.be.checked')
    cy.get('label[for="capitalRevenueTypeSurplus"]').contains('Surplus')

    cy.get('#capitalRevenueTypeDeficit').should('not.be.checked')
    cy.get('label[for="capitalRevenueTypeDeficit"]').contains('Deficit')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
    
})