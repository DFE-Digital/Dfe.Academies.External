import 'cypress-file-upload'

import DataGenerator from "../fixtures/data-generator"

let globalApplicationId = 10015;
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
    
    cy.get('a[href="/your-applications"]').contains('Back to your applications')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')

    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).should('be.visible').contains('Add a school')

    cy.get('h2').eq(1).contains('The trust the school will join')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).should('be.visible').contains('Add a trust')
    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(3).contains('You will be able to invite other people to help you complete this form after you have added a school.')
})
})

ypress.Commands.add('yourApplicationNotStartedButSchoolAddedElementsVisible', () => {
    cy.get('a[href="/your-applications"]').contains('Back to your applications')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
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
    cy.get('.govuk-back-link').contains('Back to application overview')
    cy.get('.govuk-heading-m').contains('Trust details')
    cy.get('h2').eq(1).contains('Which trust is the school joining?')
    cy.get('label').contains('Enter the name of the trust, its Companies House number, or its group Id')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectTempSecondHalfCreateNewJAMApplication', () => {
    cy.get('a[href="/application-overview?appId=10015"]').contains('Join a multi-academy trust')
    cy.get('a[href="/application-overview?appId=10015"]').click()
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
    cy.get('.govuk-back-link').contains('Back to application overview')
    cy.get('h2').eq(0).contains('School details')
    cy.get('h2').eq(1).contains('What is the name of the school?')
    cy.get('label').contains('Enter the name of the school, or its 6 digit unique reference number (URN)')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectChangeTrust', () => {
    cy.get('a[href="/trust/join-amat/application-select-trust?appId=10015"]').click()
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
    cy.get('.govuk-back-link').contains('Back to application overview')
    cy.get('.govuk-caption-l').contains('Join a multi-academy trust')
    cy.get('.govuk-heading-l').contains('PLYMOUTH CAST')
    cy.get('.govuk-heading-m').eq(0).contains('The trust the school is joining')
    cy.get('.govuk-body').contains('The name of the trust')
    cy.get('a[href="/trust/join-amat/application-select-trust?appId=87&urn=0"]').contains('Change your answers')
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
    cy.get('a[class="govuk-back-link"]').contains('Back to trust summary')
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
    cy.get('a[class="govuk-back-link"]').contains('Back to trust summary')
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
    cy.get('a[class="govuk-back-link"]').contains('Back to trust summary')
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
    cy.get('a[href="/your-applications"]').contains('Back to your applications')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
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