/// <reference types="cypress" />
import DataGenerator from "../fixtures/data-generator";
import * as dotenv from "dotenv";
import { dfeSignInTestEnvURLForA2BDevAndA2BTest, dfeSignInTestEnvForgotPasswordCodeInputURLForA2BDevAndA2BTest, dfeSignInTestEnvCreateAccountForA2BDevAndA2BTest } from '../../customConfig'
import 'cypress-file-upload';

dotenv.config();

let globalApplicationId = 10080;


Cypress.Commands.add("excuteAccessibilityTests", () => {
    // FUNCTION COURTESY OF FAHAD DARWISH - NIMBLE APPROACH CONFLUENECE
        const wcagStandards = ["wcag22aa"];
        const impactLevel = ["critical", "minor", "moderate", "serious"];
        const continueOnFail = false;
        cy.injectAxe();
        cy.checkA11y(
            null,
            {
                runOnly: {
                    type: "tag",
                    values: wcagStandards,
                },
                includedImpacts: impactLevel,
            },
            null,
            continueOnFail
        );
      });

      Cypress.Commands.add('fillDetailsAndSubmit', ():void => {
        cy.get('#EmailAddress').click()
        cy.get('#EmailAddress').type('dangood84@me.com')
    
        cy.get('#Name').click()
        cy.get('#Name').type('Daniel')
    
        cy.get('#role-2').click()
    
        cy.get('#role-description').click()
        cy.get('#role-description').type('Headmaster')
    
        cy.get('input[type="submit"]').click()
    })
    
    Cypress.Commands.add('verifySuccessBannerAndContributorList', ():void => {
        cy.get('div[role="alert"]').contains('Success')
        cy.get('div[role="alert"]').contains('Contributor added')
        cy.get('div[role="alert"]').contains('Daniel has been sent an invitation to help with this application.')
    
        cy.get('.govuk-form-group').contains('Daniel')
        cy.get('.govuk-form-group').contains('Headmaster')
        cy.get('.govuk-form-group').contains('Remove contributor')
    
    })
    
    Cypress.Commands.add('verifyContributorRemovedAndSuccessRemoved', ():void => {
        cy.get('div[role="alert"]').contains('Success')
        cy.get('div[role="alert"]').contains('Contributor removed')
        cy.get('div[role="alert"]').contains('Daniel can no longer contribute to this application.')
    
        cy.get('.govuk-form-group').contains('Daniel').should('not.exist')
    })
    
    Cypress.Commands.add('checkAppIDIsCorrectAndselectConfirmDelete', ():void => {
        cy.log(`Global Application ID = ${globalApplicationId}`)
    
    
        cy.get('.govuk-caption-xl').contains(`Application reference: A2B_${globalApplicationId}`)
    
        
        cy.get('#deleteButton').should('be.visible').contains('Yes, delete')
        cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('No, take me back')
    
        cy.get('#deleteButton').should('be.visible').contains('Yes, delete').click()
    
    })
    
    Cypress.Commands.add('verifyApplicationDeleted', ():void => {
        cy.log(`Global Application ID = ${globalApplicationId}`)
    
        cy.get('.govuk-body').eq(0).contains(`trust A2B_${globalApplicationId} has been successfully removed.`)
    
        cy.get('.govuk-table').contains(`${globalApplicationId}`).should('not.exist')
    
    
    })
  

Cypress.Commands.add('checkAppIDIsCorrectAndselectConfirmDelete', ():void => {
    cy.log(`Global Application ID = ${globalApplicationId}`)


    cy.get('.govuk-caption-xl').contains(`Application reference: A2B_${globalApplicationId}`)

    
    cy.get('#deleteButton').should('be.visible').contains('Yes, delete')
    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('No, take me back')

    cy.get('#deleteButton').should('be.visible').contains('Yes, delete').click()

})

Cypress.Commands.add('verifyApplicationDeleted', ():void => {
    cy.log(`Global Application ID = ${globalApplicationId}`)

    cy.get('.govuk-body').eq(0).contains(`trust A2B_${globalApplicationId} has been successfully removed.`)

    cy.get('.govuk-table').contains(`${globalApplicationId}`).should('not.exist')


})



Cypress.Commands.add('checkFooterLinksVisible', ():void => {
        cy.get('a[href="/accessibility-statement"]').should('be.visible').contains('Accessibility statement')
        cy.contains('Cookie policy').should('be.visible').contains('Cookie policy')
        cy.get('a[href="/terms"]').should('be.visible').contains('Terms and Conditions')
    
        cy.get('a[href="/privacy"]').should('be.visible').contains('Privacy')
        cy.get('.govuk-footer__licence-logo').should('be.visible')
        cy.get('.govuk-footer__licence-description').should('be.visible').contains('All content is available under the Open Government Licence v3.0, except where otherwise stated')
        cy.get('a[href="https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/"]').should('be.visible').contains('Open Government Licence v3.0')
        cy.get('.govuk-footer__link.govuk-footer__copyright-logo').should('be.visible').contains('© Crown copyright')
})

Cypress.Commands.add('login', ():void => {
    //cy.get('#form-signin').should('be.visible')
    cy.origin(dfeSignInTestEnvURLForA2BDevAndA2BTest, () => {
        const username :string = Cypress.env('LOGIN_USERNAME')
        const password :string = Cypress.env('LOGIN_PASSWORD')
        console.log(username)

    cy.get('#username').type(username)
    cy.get('#password').type(password, { log: false})
    cy.contains('Sign in').click()
})
})

Cypress.Commands.add('loginToUnauthApplication', (username:string, password:string):void => {
    //cy.get('#form-signin').should('be.visible')
  
    username = Cypress.env('LOGIN_USERNAME')
    password = Cypress.env('LOGIN_PASSWORD')
    cy.get('#username').type(username)
    cy.get('#password').type(password, { log: false})
    cy.contains('Sign in').click()
})


Cypress.Commands.add('createAccount', ():void => {
    cy.origin(dfeSignInTestEnvURLForA2BDevAndA2BTest, () => {
    cy.contains('Create account').click()
    })
})

Cypress.Commands.add('forgotPassword', ():void => {
    cy.origin(dfeSignInTestEnvURLForA2BDevAndA2BTest, () => {
    cy.contains('Forgotten your password?').click()
    })
})

Cypress.Commands.add('forgotPasswordElementsVisible', ():void => {
    cy.origin(dfeSignInTestEnvURLForA2BDevAndA2BTest, () => {
    cy.get('.govuk-heading-xl').contains('Forgotten your password?')
    cy.get('label[for="email"]').contains('Email address')
    cy.get('#email').should('be.visible')
    cy.get('.govuk-button').contains('Continue')
    })
})

Cypress.Commands.add('forgotPasswordEmptyEmailSubmitted', ():void => {
    cy.origin(dfeSignInTestEnvURLForA2BDevAndA2BTest, () => {
    cy.get('.govuk-button').click()
    cy.get('#validation-email').contains('Please enter a valid email address')
})
})

Cypress.Commands.add('forgotPasswordInvalidEmailSubmitted', ():void => {
    cy.origin(dfeSignInTestEnvURLForA2BDevAndA2BTest, () => {
    cy.get('#email').type('POTATO')
    cy.get('.govuk-button').click()
    cy.get('#validation-email').contains('Please enter a valid email address')
})
})

Cypress.Commands.add('forgotPasswordA2BUserEmailSubmitted', (username):void => {
    cy.origin(dfeSignInTestEnvURLForA2BDevAndA2BTest, () => {
    username = Cypress.env('LOGIN_USERNAME')
    cy.get('#email').type(username)
    cy.get('.govuk-button').click()
})
})

Cypress.Commands.add('forgotPasswordVerifyCodeElementsVisible', ():void => {
    cy.origin(dfeSignInTestEnvForgotPasswordCodeInputURLForA2BDevAndA2BTest, () => {
    cy.get('#govuk-notification-banner-title').contains('Important')
    cy.get('.govuk-notification-banner__heading.wrap.full-width').contains('Confirm your email address by entering your verification code')
    cy.get('button[type="submit"]')
    cy.get('.govuk-heading-xl').contains('Enter your verification code')
    cy.get('label[for="code"]').contains('Verification code')
    cy.get('#code').should('be.visible')
    cy.get('.govuk-button').contains('Continue')
    })
})

Cypress.Commands.add('createAccountElementsVisible', ():void => {
    cy.origin(dfeSignInTestEnvCreateAccountForA2BDevAndA2BTest, () => {
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

    Cypress.Commands.add('createAccountFailsWithNoData', ():void => {
        cy.origin(dfeSignInTestEnvCreateAccountForA2BDevAndA2BTest, () => {
            cy.contains('Continue').click()
            cy.get('div[role="alert"]').should('be.visible')
            cy.get('a[href="#firstName"]').contains('Please enter a valid first name')
            cy.get('a[href="#lastName"]').contains('Please enter a valid last name')
            cy.get('a[href="#email"]').contains('Enter an email address')

            cy.get('#validation-firstName').contains('Please enter a valid first name')
            cy.get('#validation-lastName').contains('Please enter a valid last name')
        })
    })

Cypress.Commands.add('createAccountSuccessful', ():void => {
    const generateData = new DataGenerator()
    const sentArgs = { firstName: generateData.generateName(), lastName: generateData.generateName(), email: generateData.generateEmail() }
    cy.origin(dfeSignInTestEnvCreateAccountForA2BDevAndA2BTest,
    {args: sentArgs},
    ({ firstName, lastName, email }) => { 
        cy.get('#firstName').type(firstName)
        cy.get('#lastName').type(lastName)
        cy.get('#email').type(email)
        cy.contains('Continue').click()

    })
})

Cypress.Commands.add('createAccountConfirmElementsVisible', ():void => {
    cy.origin(dfeSignInTestEnvCreateAccountForA2BDevAndA2BTest, () => {
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

Cypress.Commands.add('yourApplicationsElementsVisible', ():void => {
    // eslint-disable-next-line cypress/no-unnecessary-waiting
    cy.wait(3000)
    cy.get('h1').contains('Your applications')
    cy.get('h2').contains('Applications in progress')
    cy.get('th:nth-child(1)').contains('Application')
    cy.get('th:nth-child(2)').contains('Trust Name')
    cy.get('th:nth-child(3)').contains('School Or Schools Applying To Convert')
    cy.get('a[href="/what-are-you-applying-to-do"]').should('be.visible').contains('Start a new application')
   
   // cy.get('.govuk-grid-column-full > :nth-child(8)').contains('Past applications')
   // cy.get('[aria-describedby="completedApplicationsTableDescription"] > .govuk-table__head > .govuk-table__row > :nth-child(1)').contains('Application')
   // cy.get('[aria-describedby="completedApplicationsTableDescription"] > .govuk-table__head > .govuk-table__row > :nth-child(2)').contains('Trust Name')
   // cy.get('[aria-describedby="completedApplicationsTableDescription"] > .govuk-table__head > .govuk-table__row > :nth-child(3)').contains('School Or Schools Applying To Convert')
})

Cypress.Commands.add('selectJAMNotStartedApplication', ():void => {
    cy.get('a[href="/application-overview?appId=10049"]').contains('Join a multi-academy trust')
    cy.get('a[href="/application-overview?appId=10049"]').click()
})

Cypress.Commands.add('selectJAMNotStartedApplicationButSchoolAdded', ():void => {
    cy.get('a[href="/application-overview?appId=10048"]').contains('Join a multi-academy trust')
    cy.get('ul').contains('Plymstock School')
    cy.get('a[href="/application-overview?appId=10048"]').click()
})

Cypress.Commands.add('yourApplicationNotStartedElementsVisible', ():void => {
    cy.get('.govuk-body').eq(0).then(($applicationId)=> {
        const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
        cy.log(`Application ID = ${applicationId}`)
        globalApplicationId = Number(applicationId)
        cy.log(`Global Application ID = ${globalApplicationId}`)
    
    cy.get('a[href="/your-applications"]').contains('Back')
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

Cypress.Commands.add('yourApplicationNotStartedButSchoolAddedElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag govuk-tag--blue"]').contains('In Progress')



    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(4).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')
})

Cypress.Commands.add('whatAreYouApplyingToDoElementsVisible', ():void => {
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

Cypress.Commands.add('whatIsYourRoleElementsVisible', ():void => {
    
    cy.get('h1').contains('What is your role?')
    cy.get('p').contains('Anyone from the school can contribute to this form, but only the chair of governors can complete the declaration section or submit it.')
    cy.get('input[type="radio"]').eq(0).should('exist')
    cy.get('label[for="RoleTypeChairOfGovernors"]').contains('The chair of the school\'s governors')
    cy.get('input[type="radio"]').eq(1).should('exist')
    cy.get('label[for="RoleTypeOther"]').contains('Something else')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectAddASchool', ():void => {
    cy.get('.govuk-body').eq(0).then(($applicationId)=> {
        const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
        cy.log(`Application ID = ${applicationId}`)
        globalApplicationId = Number(applicationId)
        cy.log(`Global Application ID = ${globalApplicationId}`)
        cy.get(`a[href="/school/application-select-school?appId=${applicationId}"]`).click()
    })
})

Cypress.Commands.add('selectAddATrust', ():void => {
cy.get('.govuk-body').eq(0).then(($applicationId)=> {
    const applicationId = $applicationId.text().split('_').pop().replace(/\s/g, '')
    cy.log(`Application ID = ${applicationId}`)
    globalApplicationId = Number(applicationId)
    cy.log(`Global Application ID = ${globalApplicationId}`)
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}"]`).click()
})
})


Cypress.Commands.add('whichTrustIsSchoolJoiningElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-heading-m').contains('Trust details')
    cy.get('h2').eq(1).contains('Which trust is the school joining?')
    cy.get('label').contains('Enter the name of the trust, its Companies House number, or its group Id')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectTempSecondHalfCreateNewJAMApplication', ():void => {
    cy.get(`a[href="/application-overview?appId=${globalApplicationId}"]`).contains('Join a multi-academy trust')
    cy.get(`a[href="/application-overview?appId=${globalApplicationId}"]`).click()
})


Cypress.Commands.add('selectTrustDetails', ():void => {
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).click()
})


Cypress.Commands.add('selectChangeSchool', ():void => {
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).click()
})

Cypress.Commands.add('selectSchoolName', (schoolNameSearchPartial:string):void => {
    schoolNameSearchPartial = 'Plym'
    cy.get('.autocomplete__wrapper > #SearchQueryInput').click()
    cy.get('.autocomplete__wrapper > #SearchQueryInput').type(schoolNameSearchPartial)
    cy.get('#SearchQueryInput__option--9').click()
    // eslint-disable-next-line cypress/no-unnecessary-waiting
    cy.wait(2000)
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()
})

Cypress.Commands.add('whatIsTheNameOfTheSchoolElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('h2').eq(0).contains('School details')
    cy.get('h2').eq(1).contains('What is the name of the school?')
    cy.get('label').contains('Enter the name of the school, or its 6 digit unique reference number (URN)')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')
})

Cypress.Commands.add('selectChangeTrust', ():void => {
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).click()
})

Cypress.Commands.add('selectTrustName', (trustName:string):void => {
    trustName = 'Plym'
    cy.get('.autocomplete__wrapper > #SearchQueryInput').click()
    cy.get('.autocomplete__wrapper > #SearchQueryInput').type(trustName)
    cy.get('#SearchQueryInput__option--4').click() 
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

Cypress.Commands.add('JAMTrustDetailsSummaryElementsVisible', ():void => {
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

Cypress.Commands.add('JAMTrustConsentElementsVisible', ():void => {
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

Cypress.Commands.add('JAMTrustConsentFileUpload', ():void => {
  const filepath = '../fixtures/fifty-k.docx'
  cy.get('#trustConsentFileUpload').attachFile(filepath)
})

Cypress.Commands.add('changesToTheTrustElementsVisible', ():void => {
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

Cypress.Commands.add('enterChangesToTheTrust', (changesToTheTrust:string):void => {
    changesToTheTrust = 'What are the changes? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ull'
    cy.get('#PFYRevenueStatusExplained').click()
    cy.get('#PFYRevenueStatusExplained').type(changesToTheTrust)

})

Cypress.Commands.add('localGovernanceArrangementsElementsVisible', ():void => {
    cy.get('a[class="govuk-back-link"]').contains('Back')
    cy.get('.govuk-caption-l').eq(0).contains('PLYMOUTH CAST (step 3 of 3)')
    cy.get('.govuk-heading-l').contains('Local governance arrangements')
    cy.get('legend').eq(0).contains('Will there be any changes at a local level due to this school joining?')
    cy.get('.govuk-caption-l').eq(1).contains('For example, setting up a local sub-regional hub or changes to any schemes of delegation.')
    cy.get('#changesToLaGovernanceYes').should('not.be.checked')
    cy.get('label[for="changesToLaGovernanceYes"]').contains('Yes')
    cy.get('#changesToLaGovernanceNo').should('not.be.checked')
    cy.get('label[for="changesToLaGovernanceNo"]').contains('No')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')

})


Cypress.Commands.add('enterlocalGovernanceArrangementsChanges', (localGovernanceArrangements:string):void => {
    localGovernanceArrangements = 'What are the changes and how do they fit into the current lines of accountability in the trust? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et d'
    cy.get('#changesToLaGovernanceYes').click()
    cy.get('#ChangesToLaGovernanceExplained').click()
    cy.get('#ChangesToLaGovernanceExplained').type(localGovernanceArrangements)
})

Cypress.Commands.add('JAMTrustDetailsSummarySaveAndReturnToApp', ():void => {
    cy.get(`a[href="/application-overview?appId=${globalApplicationId}"]`).eq(1).click()
})

Cypress.Commands.add('yourApplicationNotStartedButTrustSectionCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('aboutTheConversionNotStartedElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains('You have not added any information')

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains('You have not added any information')

    //cy.get('.govuk-body').eq(4).contains('Headteacher\'s telephone number')
    //cy.get('.govuk-body').eq(5).contains('You have not added any information')

    cy.get('.govuk-body').eq(4).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(5).contains('You have not added any information')

    cy.get('.govuk-body').eq(6).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(7).contains('You have not added any information')

    //cy.get('.govuk-body').eq(10).contains('Chair\'s telephone number')
    //cy.get('.govuk-body').eq(11).contains('You have not added any information')

    cy.get('.govuk-body').eq(8).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(9).contains('You have not added any information')

    cy.get('.govuk-body').eq(10).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(11).contains('You have not added any information')

    cy.get('.govuk-body').eq(12).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(13).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).should('be.visible').contains('Start section')

   // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('.govuk-body').eq(14).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(15).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(1).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('.govuk-body').eq(16).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(17).contains('You have not added any information')

  //  cy.get('hr').eq(10).should('be.visible')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(2).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')
    cy.get('.govuk-body').eq(18).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')


    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(3).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')



})

Cypress.Commands.add('mainContactsNotStartedElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Main contacts')
    
    cy.get('.govuk-label').eq(0).contains('Name of headteacher')
    cy.get('#ViewModel\\.ContactHeadName').should('be.enabled')
    
    cy.get('.govuk-label').eq(1).contains('Headteacher\'s email address')
    cy.get('.govuk-hint').eq(0).contains('We will only use this to contact you regarding your application')
    cy.get('#ViewModel\\.ContactHeadEmail').should('be.enabled')

   // cy.get('.govuk-label').eq(2).contains('Headteacher\'s telephone number')
   // cy.get('#ViewModel\\.ContactHeadTel').should('be.enabled')

    cy.get('.govuk-label').eq(2).contains('Name of the chair of the governing body')
    cy.get('#ViewModel\\.ContactChairName').should('be.enabled')

    cy.get('.govuk-label').eq(3).contains('Chair\'s email address')
    cy.get('.govuk-hint').eq(1).contains('We will only use this to contact you regarding your application')
    cy.get('#ViewModel\\.ContactChairEmail').should('be.enabled')

   // cy.get('.govuk-label').eq(5).contains('Chair\'s telephone number')
   // cy.get('#ViewModel\\.ContactChairTel')

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

    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
})

// eslint-disable-next-line @typescript-eslint/no-unused-vars
Cypress.Commands.add('fillHeadTeacherDetails', (headName:string,headEmail:string,headTel:string):void => {
    headName = 'Paul Lockwood'
    headEmail = 'paul.lockwood@education.gov.uk'
  //  headTel = '01752 404930'
    
    cy.get('#ViewModel\\.ContactHeadName').type(headName)
    cy.get('#ViewModel\\.ContactHeadEmail').type(headEmail)
  //  cy.get('#ViewModel\\.ContactHeadTel').type(headTel)
})

// eslint-disable-next-line @typescript-eslint/no-unused-vars
Cypress.Commands.add('fillChairDetails', (chairName:string,chairEmail:string,chairTel:string):void => {
    chairName = 'Dan Good'
    chairEmail = 'dan.good@education.gov.uk'
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    chairTel = '01752 404000'
    cy.get('#ViewModel\\.ContactChairName').type(chairName)
    cy.get('#ViewModel\\.ContactChairEmail').type(chairEmail)
 //   cy.get('#ViewModel\\.ContactChairTel').type(chairTel)
})

Cypress.Commands.add('fillApproverDetails', (approverName:string,approverEmail:string):void => {
    approverName = 'James Stewart'
    approverEmail = 'james.stewart@aol.com'
    
    cy.get('#ApproverContactName').type(approverName)
    cy.get('#ApproverContactEmail').type(approverEmail)
})

Cypress.Commands.add('aboutTheConversionMainContactsCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('a[class="govuk-link"]').eq(1).contains('Change your answers')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains('Paul Lockwood')

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains('paul.lockwood@education.gov.uk')

  //  cy.get('.govuk-body').eq(4).contains('Headteacher\'s telephone number')
  //  cy.get('.govuk-body').eq(5).contains('01752404930')

    cy.get('.govuk-body').eq(4).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(5).contains('Dan Good')

    cy.get('.govuk-body').eq(6).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(7).contains('dan.good@education.gov.uk')

  //  cy.get('.govuk-body').eq(8).contains('Chair\'s telephone number')
  //  cy.get('.govuk-body').eq(9).contains('01752404000')

    cy.get('.govuk-body').eq(8).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(9).contains('The chair of the governing body')

    cy.get('.govuk-body').eq(10).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(11).contains('James Stewart')

    cy.get('.govuk-body').eq(12).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(13).contains('james.stewart@aol.com')

    

   // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('.govuk-body').eq(14).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(15).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('.govuk-body').eq(16).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(17).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(1).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')
    cy.get('.govuk-body').eq(18).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(2).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')
})

// OK now we want to click on Start section for Date of Conversion
Cypress.Commands.add('selectDateForConversionStartSection', () => {
    cy.contains('Start section').eq(0).click()
})

Cypress.Commands.add('conversionTargetDateElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Conversion target date')
    cy.get('.govuk-body-l').contains('Conversion usually takes around 6 months. It may take longer if the school is part of a private finance initiative (PFI) contract.')
    cy.get('.govuk-heading-s').contains('Do you want the conversion to happen on a particular date?')
    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for="selectoptionYes"]').contains('Yes')
    cy.get('#selectoptionNo').should('not.be.checked')
    cy.get('label[for="selectoptionNo"]').contains('No')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')

})

Cypress.Commands.add('reasonsForJoiningElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Reasons for joining')

    cy.get('.govuk-label').contains('Why does the school want to join this trust in particular?')

    cy.get('#ApplicationJoinTrustReason').should('be.visible')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')

})

Cypress.Commands.add('reasonsForJoiningInput', (reasonsForJoining:string):void => {
    reasonsForJoining = 'Why does the school want to join this trust in particular? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'
    cy.get('#ApplicationJoinTrustReason').type(reasonsForJoining)
})

Cypress.Commands.add('submitReasonsForJoining', ():void => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('changingTheNameOfTheSchoolElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('h1').contains('Changing the name of the school')
    cy.get('#role-hint').contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for="selectoptionYes"]').contains('Yes')
    cy.get('#selectoptionNo').should('not.be.checked')
    cy.get('label[for="selectoptionNo"]').contains('No')
    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')

})

Cypress.Commands.add('aboutTheConversionCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('a[class="govuk-link"]').eq(1).contains('Change your answers')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains('Paul Lockwood')

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains('paul.lockwood@education.gov.uk')

   // cy.get('.govuk-body').eq(4).contains('Headteacher\'s telephone number')
   // cy.get('.govuk-body').eq(5).contains('01752404930')

    cy.get('.govuk-body').eq(4).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(5).contains('Dan Good')

    cy.get('.govuk-body').eq(6).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(7).contains('dan.good@education.gov.uk')

   // cy.get('.govuk-body').eq(10).contains('Chair\'s telephone number')
   // cy.get('.govuk-body').eq(11).contains('01752404000')

    cy.get('.govuk-body').eq(8).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(9).contains('The chair of the governing body')

    cy.get('.govuk-body').eq(10).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(11).contains('James Stewart')

    cy.get('.govuk-body').eq(12).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(13).contains('james.stewart@aol.com')


    

   // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('a[class="govuk-link"]').eq(2).contains('Change your answers')

    cy.get('.govuk-body').eq(14).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(15).contains('No')

   // cy.get('hr').eq(9).should('be.visible')

    

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('a[class="govuk-link"]').eq(3).contains('Change your answers')
    cy.get('.govuk-body').eq(16).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(17).contains('Why does the school want to join this trust in particular? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.')

   // cy.get('hr').eq(10).should('be.visible')



    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')

    cy.get('a[class="govuk-link"]').eq(4).contains('Change your answers')

    cy.get('.govuk-body').eq(18).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(19).contains('No')

   // cy.get('hr').eq(11).should('be.visible')


    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')
})


Cypress.Commands.add('yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')


})

Cypress.Commands.add('additionalDetailsSummaryNotStartedElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Further information')

    cy.get('.govuk-heading-m').contains('Additional details')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('What will the school bring to the trust they are joining?')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('b').eq(1).contains('Have Ofsted recently inspected the school but not published the report yet?')
    cy.get('p').eq(4).contains('You have not added any information')

    cy.get('b').eq(2).contains('Are there any safeguarding investigations ongoing at the school?')
    cy.get('p').eq(6).contains('You have not added any information')

    cy.get('b').eq(3).contains('Is the school part of a local authority reorganisation?')
    cy.get('p').eq(8).contains('You have not added any information')

    cy.get('b').eq(4).contains('Is the school part of any local authority closure plans?')
    cy.get('p').eq(10).contains('You have not added any information')


    cy.get('b').eq(5).contains('Is your school linked to a diocese?')
    cy.get('p').eq(12).contains('You have not added any information')

    cy.get('b').eq(6).contains('Is the school part of a federation?')
    cy.get('p').eq(14).contains('You have not added any information')

    cy.get('b').eq(7).contains('Is the school supported by a foundation, trust or other body (e.g. parish council) that appoints foundation governors?')
    cy.get('p').eq(16).contains('You have not added any information')

    cy.get('b').eq(8).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')
    cy.get('p').eq(18).contains('You have not added any information')

    cy.get('b').eq(9).contains('Provide a list of your main feeder schools')
    cy.get('p').eq(20).contains('You have not added any information')

    cy.get('b').eq(10).contains('The school\'s Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school\'s consent to converting and joining the trust.')
    cy.get('p').eq(22).contains('You have not added any information')

    cy.get('b').eq(11).contains('Has an equalities impact assessment been carried out and considered by the governing body?')
    cy.get('p').eq(24).contains('You have not added any information')

    cy.get('b').eq(12).contains('Do you want to add any further information?')
    cy.get('p').eq(26).contains('You have not added any information')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})

Cypress.Commands.add('additionalDetailsDetailsNotStartedElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Additional details')

    cy.get('label[for="TrustBenefitDetails"]').contains('What will the school bring to the trust they are joining?')
    cy.get('#trust-benefit-hint').contains('Describe the contribution they will make')

    cy.get('#TrustBenefitDetails').should('be.enabled')

    cy.get('legend').eq(0).contains('Have Ofsted recently inspected the school but not published the report yet?')

    cy.get('#ofstedInspectedOptionYes').should('not.be.checked')
    cy.get('label[for="ofstedInspectedOptionYes"]').contains('Yes')

    cy.get('#ofstedInspectedOptionNo').should('not.be.checked')
    cy.get('label[for="ofstedInspectedOptionNo"]').contains('No')

    cy.get('legend').eq(1).contains('Are there any safeguarding investigations ongoing at the school')

    cy.get('#safeguardingOptionYes').should('not.be.checked')
    cy.get('label[for="safeguardingOptionYes"]').contains('Yes')

    cy.get('#safeguardingOptionNo').should('not.be.checked')
    cy.get('label[for="safeguardingOptionNo"]').contains('No')

    cy.get('legend').eq(2).contains('Is the school part of a local authority reorganisation?')

    cy.get('#localAuthorityOptionYes').should('not.be.checked')
    cy.get('label[for="localAuthorityOptionYes"]').contains('Yes')

    cy.get('#localAuthorityOptionNo').should('not.be.checked')
    cy.get('label[for="localAuthorityOptionNo"]').contains('No')


    cy.get('legend').eq(3).contains('Is the school part of any local authority closure plans?')

    cy.get('#localAuthorityClosurePlanOptionYes').should('not.be.checked')
    cy.get('label[for="localAuthorityClosurePlanOptionYes"]').contains('Yes')

    cy.get('#localAuthorityClosurePlanOptionNo').should('not.be.checked')
    cy.get('label[for="localAuthorityClosurePlanOptionNo"]').contains('No')


    cy.get('legend').eq(4).contains('Is the school linked to a diocese?')

    cy.get('#dioceseOptionYes').should('not.be.checked')
    cy.get('label[for="dioceseOptionYes"]').contains('Yes')

    cy.get('#dioceseOptionNo').should('not.be.checked')
    cy.get('label[for="dioceseOptionNo"]').contains('No')


    cy.get('legend').eq(6).contains('Is the school part of a federation')
    cy.get('#federation-hint').contains('A federation is a group of maintained schools under one governing body (The School Governance (Federations) (England) Regulations 2012)')
    cy.get('a[href="https://www.legislation.gov.uk/uksi/2012/1035/contents/made"]').contains('(The School Governance (Federations) (England) Regulations 2012)')

    cy.get('#federationOptionYes').should('not.be.checked')
    cy.get('label[for="federationOptionYes"]').contains('Yes')

    cy.get('#federationOptionNo').should('not.be.checked')
    cy.get('label[for="federationOptionNo"]').contains('No')

    cy.get('legend').eq(7).contains('Is the school supported by a foundation, trust or other body(e.g. parish council) that appoints foundation governors?')

    cy.get('#supportedByFoundationTrustOrBodyOptionYes').should('not.be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionYes"]').contains('Yes')

    cy.get('#supportedByFoundationTrustOrBodyOptionNo').should('not.be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionNo"]').contains('No')


    cy.get('legend').eq(9).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')

    cy.get('#exemptionFromSACREOptionYes').should('not.be.checked')
    cy.get('label[for="exemptionFromSACREOptionYes"]').contains('Yes')

    cy.get('#exemptionFromSACREOptionNo').should('not.be.checked')
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

    cy.get('#equalitiesImpactAssessmentOptionNo').should('not.be.checked')
    cy.get('label[for="equalitiesImpactAssessmentOptionNo"]').contains('No')


    cy.get('legend').eq(13).contains('Do you want to add any further information?')

    cy.get('#furtherInformationOptionYes').should('not.be.checked')
    cy.get('label[for="furtherInformationOptionYes"]').contains('Yes')

    cy.get('#furtherInformationOptionNo').should('not.be.checked')
    cy.get('label[for="furtherInformationOptionNo"]').contains('No')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')


})

Cypress.Commands.add('fillSchoolContribution', (schoolContribution:string):void => {
    schoolContribution = 'What will the school bring to the trust they are joining? Describe the contribution they will make Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore '
    cy.get('#TrustBenefitDetails').type(schoolContribution)
})

Cypress.Commands.add('dioceseSectionElementsVisible', ():void => {
    cy.get('#dioceseOptionYes').should('be.checked')
    cy.get('label[for="dioceseOptionYes"]').contains('Yes')
    
    // TEST DIOCESE YES SECTION IS VISIBLE
    cy.get('#dioceseOption-yes').should('be.visible')

    cy.get('label[for="DioceseName"]').contains('Name of diocese')

    cy.get('#DioceseName').should('be.visible').should('be.enabled')
    cy.get('#dioceseOption-yes > :nth-child(1) > :nth-child(4)').contains('Upload a letter from the diocese giving permission for the school to convert.')
    cy.get('label[for="dioceseFileUpload"]').contains('Upload a file')
    cy.get(':nth-child(5) > .govuk-hint').contains('The application cannot be submitted without this. You can carry on the application and come back to this later.')
    cy.get('#dioceseOption-yes > :nth-child(1) > .govuk-fieldset__legend').contains('Uploaded files')
    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')
    cy.get('#dioceseOptionNo').should('not.be.checked')
    cy.get('label[for="dioceseOptionNo"]').contains('No')
})

Cypress.Commands.add('dioceseFileUpload', ():void => {
  const filepath = '../fixtures/fifty-k.docx'
  cy.get('#dioceseFileUpload').attachFile(filepath)
})

Cypress.Commands.add('selectYesSchoolSupportedByTrustOrFoundation', ():void => {
    cy.get('#supportedByFoundationTrustOrBodyOptionYes').click()
})

Cypress.Commands.add('schoolSupportedByElementsVisible', ():void => {
    cy.get('#supportedByFoundationTrustOrBodyOptionYes').should('be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionYes"]').contains('Yes')
    cy.get('label[for="FoundationTrustOrBodyName"]').contains('Name of this body')
    cy.get('#FoundationTrustOrBodyName').should('be.visible').should('be.enabled')
    cy.get('#supportedByFoundationTrustOrBodyOption-yes > :nth-child(1) > :nth-child(5)').contains('Please upload their letter of consent')
   // cy.get('#FoundationConsentFiles > .govuk-label').contains('Upload a file')
   cy.get('#supportedByFoundationTrustOrBodyOption-yes > :nth-child(1) > .govuk-fieldset__legend').contains('Uploaded files')
    cy.get('hr').eq(3).should('be.visible')
    cy.get('#supportedByFoundationTrustOrBodyOption-yes > :nth-child(1) > :nth-child(9)').contains('No file uploaded')
    cy.get('hr').eq(4).should('be.visible')
    cy.get('#supportedByFoundationTrustOrBodyOptionNo').should('not.be.checked')
    cy.get('label[for="supportedByFoundationTrustOrBodyOptionNo"]').contains('No')


})

Cypress.Commands.add('uploadSchoolSupportedByTrustOrBody', ():void => {
    const filepath = '../fixtures/fifty-k.pptx'
    cy.get('#foundationConsentFileUpload').attachFile(filepath)
})

Cypress.Commands.add('inputListOfFeederSchools', (feederSchools:string):void => {
    feederSchools = 'Please provide a list of your main feeder schools We recognise you may have many feeder schools, therefore please just detail the top 5 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do '
    cy.get('#MainFeederSchools').type(feederSchools)
})

Cypress.Commands.add('uploadSchoolLetterOfConsent', ():void => {
    const filepath = '../fixtures/fiftyk.pdf'
    cy.get('#resolutionConsentFileUpload').attachFile(filepath)
})

Cypress.Commands.add('additionalDetailsSummaryCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Further information')

    cy.get('.govuk-heading-m').contains('Additional details')

    cy.get('.govuk-link').should('be.visible').contains('Change your answers')

    cy.get('b').eq(0).contains('What will the school bring to the trust they are joining?')
    cy.get('p').eq(2).contains('What will the school bring to the trust they are joining? Describe the contribution they will make Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore ')

    cy.get('b').eq(1).contains('Have Ofsted recently inspected the school but not published the report yet?')
    cy.get('p').eq(4).contains('No')

    cy.get('b').eq(2).contains('Are there any safeguarding investigations ongoing at the school?')
    cy.get('p').eq(6).contains('No')

    cy.get('b').eq(3).contains('Is the school part of a local authority reorganisation?')
    cy.get('p').eq(8).contains('No')

    cy.get('b').eq(4).contains('Is the school part of any local authority closure plans?')
    cy.get('p').eq(10).contains('No')

    cy.get('b').eq(5).contains('Is your school linked to a diocese?')
    cy.get('p').eq(12).contains('Yes')

    cy.get('b').eq(6).contains('Is the school part of a federation?')
    cy.get('p').eq(14).contains('No')

    cy.get('b').eq(7).contains('Is the school supported by a foundation, trust or other body (e.g. parish council) that appoints foundation governors?')
    cy.get('p').eq(16).contains('Yes')

    cy.get('b').eq(8).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')
    cy.get('p').eq(18).contains('No')

    cy.get('b').eq(9).contains('Provide a list of your main feeder schools')
    cy.get('p').eq(20).contains('Please provide a list of your main feeder schools We recognise you may have many feeder schools, therefore please just detail the top 5 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do ')

    cy.get('b').eq(10).contains('The school\'s Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school\'s consent to converting and joining the trust.')
    cy.get('p').eq(22).contains('fiftyk.pdf')

    cy.get('b').eq(11).contains('Has an equalities impact assessment been carried out and considered by the governing body?')
    cy.get('p').eq(24).contains('No')

    cy.get('b').eq(12).contains('Do you want to add any further information?')
    cy.get('p').eq(26).contains('No')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')
})

Cypress.Commands.add('yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('financeSummaryNotStartedElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Finances')

    cy.get('.govuk-heading-m').eq(0).contains('Previous financial year')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).should('be.visible').contains('Start section')
   
    //cy.get('hr').eq(0).should('be.visible')
    
    cy.get('b').eq(0).contains('End of previous financial year end date?')
    cy.get('.govuk-body').eq(1).contains('You have not added any information')
    //cy.get('hr').eq(1).should('be.visible')

    cy.get('b').eq(1).contains('Revenue carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(3).contains('You have not added any information')
   // cy.get('hr').eq(2).should('be.visible')

    cy.get('b').eq(2).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(5).contains('You have not added any information')
    //cy.get('hr').eq(3).should('be.visible')

    cy.get('b').eq(3).contains('Capital carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(7).contains('You have not added any information')
   // cy.get('hr').eq(4).should('be.visible')

    cy.get('b').eq(4).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(9).contains('You have not added any information')
   // cy.get('hr').eq(5).should('be.visible')

    cy.get('.govuk-heading-m').eq(1).contains('Current financial year')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(1).should('be.visible').contains('Start section')

   // cy.get('hr').eq(6).should('be.visible')

    cy.get('b').eq(5).contains('End of current financial year end date?')
    cy.get('.govuk-body').eq(11).contains('You have not added any information')
    //cy.get('hr').eq(7).should('be.visible')

    cy.get('b').eq(6).contains('Revenue carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(13).contains('You have not added any information')
   // cy.get('hr').eq(8).should('be.visible')

    cy.get('b').eq(7).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(15).contains('You have not added any information')
    //cy.get('hr').eq(9).should('be.visible')

    cy.get('b').eq(8).contains('Capital carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(17).contains('You have not added any information')
   // cy.get('hr').eq(10).should('be.visible')

    cy.get('b').eq(9).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')
    //cy.get('hr').eq(11).should('be.visible')

    cy.get('.govuk-heading-m').eq(2).contains('Next financial year')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(2).should('be.visible').contains('Start section')

   // cy.get('hr').eq(12).should('be.visible')

    cy.get('b').eq(10).contains('End of next financial year end date?')
    cy.get('.govuk-body').eq(21).contains('You have not added any information')
   // cy.get('hr').eq(13).should('be.visible')

    cy.get('b').eq(11).contains('Revenue carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(23).contains('You have not added any information')
  //  cy.get('hr').eq(14).should('be.visible')

    cy.get('b').eq(12).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(25).contains('You have not added any information')
  //  cy.get('hr').eq(15).should('be.visible')

    cy.get('b').eq(13).contains('Capital carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(27).contains('You have not added any information')
  //  cy.get('hr').eq(16).should('be.visible')

    cy.get('b').eq(14).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(29).contains('You have not added any information')
   // cy.get('hr').eq(17).should('be.visible')

    cy.get('.govuk-heading-m').eq(3).contains('Loans')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(3).should('be.visible').contains('Start section')

  //  cy.get('hr').eq(18).should('be.visible')

    cy.get('b').eq(15).contains('Are there any existing loans?')
    cy.get('.govuk-body').eq(31).contains('You have not added any information')
   // cy.get('hr').eq(19).should('be.visible')

    cy.get('.govuk-heading-m').eq(4).contains('Leases')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(4).should('be.visible').contains('Start section')

   // cy.get('hr').eq(20).should('be.visible')

    cy.get('b').eq(16).contains('Are there any existing leases?')
    cy.get('.govuk-body').eq(33).contains('You have not added any information')
   // cy.get('hr').eq(21).should('be.visible')

    cy.get('.govuk-heading-m').eq(5).contains('Financial investigations')
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(5).should('be.visible').contains('Start section')

  //  cy.get('hr').eq(22).should('be.visible')

    cy.get('b').eq(17).contains('Finance ongoing investigations?')
    cy.get('.govuk-body').eq(35).contains('You have not added any information')
  //  cy.get('hr').eq(23).should('be.visible')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})


Cypress.Commands.add('previousFinancialYrElementsVisible', ():void => {
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

Cypress.Commands.add('currentFinancialYrElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Finances (Step 2 of 6)')
    cy.get('.govuk-heading-l').contains('Current financial year')

    cy.get('p').eq(1).contains('Converting schools should normally be in surplus or have a balanced budget')

    cy.get('p').eq(2).contains('We may let the school carry forward a deficit, but only if they have a plan to balance their budget in a reasonable time and provide a forecast showing how they\'ll do this in 2 to 3 years')


    cy.get('legend').contains('End of current financial year')
    cy.get('#pfy-end-date-hint').contains('For example, 01 09 2022')

    cy.get('label[for="sip_cfyenddate-day"]').contains('Day')
    cy.get('#sip_cfyenddate-day').should('be.visible').should('be.enabled')

    cy.get('label[for="sip_cfyenddate-month"]').contains('Month')
    cy.get('#sip_cfyenddate-month').should('be.visible').should('be.enabled')

    cy.get('label[for="sip_cfyenddate-year"]').contains('Year')
    cy.get('#sip_cfyenddate-year').should('be.visible').should('be.enabled')

    cy.get('label[for="Revenue"]').contains('Forecasted revenue carry forward at end of the current financial year (31 March)')

    cy.get('#Revenue').should('be.visible').should('be.enabled')

    cy.get('#revenueTypeSurplus').should('not.be.checked')
    cy.get('label[for="revenueTypeSurplus"]').contains('Surplus')

    cy.get('#revenueTypeDeficit').should('not.be.checked')
    cy.get('label[for="revenueTypeDeficit"]').contains('Deficit')

    cy.get('label[for="CapitalCarryForward"]').contains('Forecasted capital carry forward at end of the current financial year (31 March)')

    cy.get('#CapitalCarryForward').should('be.visible').should('be.enabled')

    cy.get('#capitalTypeSurplus').should('not.be.checked')
    cy.get('label[for="capitalTypeSurplus"]').contains('Surplus')

    cy.get('#capitalTypeDeficit').should('not.be.checked')
    cy.get('label[for="capitalTypeDeficit"]').contains('Deficit')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
    
})

Cypress.Commands.add('verifyCurrentRevenueCarryForwardDeficitSelectedSectionDisplays', ():void => {
    cy.get('#revenueTypeDeficit').should('be.checked')
    
    cy.get('label[for="CFYRevenueCarryForwardExplained"]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(1).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#CFYRevenueCarryForwardExplained').should('be.visible').should('be.enabled')

    cy.get('.govuk-label').eq(6).contains('You can upload the school’s recovery plan.')

    cy.get('.govuk-hint').eq(2).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')
    
    cy.get('label[for="schoolCfyRevenueFileUpload"]').contains('Upload a file')

    cy.get('legend').eq(1).contains('Uploaded files')
    
    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').eq(9).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')

})



Cypress.Commands.add('uploadFileForCurrentRevenueCarryForwardDeficit', ():void => {
    const filepath = '../fixtures/fifty-k.docx'
    cy.get('#schoolCfyRevenueFileUpload').attachFile(filepath)
})

Cypress.Commands.add('selectCurrentCapitalCarryForwardDeficit', ():void => {
    cy.get('#capitalTypeDeficit').click()

})

Cypress.Commands.add('selectNextCapitalCarryForwardDeficit', ():void => {
    cy.get('#capitalRevenueTypeDeficit').click()

})

Cypress.Commands.add('verifyCurrentCapitalCarryForwardDeficitSelectedSectionDisplays', ():void => {
    cy.get('#capitalTypeDeficit').should('be.checked')

    cy.get('label[for="CFYCapitalCarryForwardExplained"]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(3).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#CFYCapitalCarryForwardExplained').should('be.visible').should('be.enabled')

    cy.get('.govuk-label').eq(13).contains('You can upload the school’s recovery plan.')

    cy.get('.govuk-hint').eq(4).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')
    
    cy.get('label[for="schoolCfyRevenueFileUpload"]').contains('Upload a file')

    cy.get('legend').eq(2).contains('Uploaded files')
    
    cy.get('hr').eq(2).should('be.visible')
    cy.get('.govuk-label').eq(16).contains('No file uploaded')
    cy.get('hr').eq(3).should('be.visible')
})

Cypress.Commands.add('verifyNextCapitalCarryForwardDeficitSelectedSectionDisplays', ():void => {
    cy.get('#capitalRevenueTypeDeficit').should('be.checked')

    cy.get('label[for="PFYCapitalCarryForwardExplained"]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(3).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#PFYCapitalCarryForwardExplained').should('be.visible').should('be.enabled')

    cy.get('.govuk-label').eq(13).contains('You can upload the school’s recovery plan.')

    cy.get('.govuk-hint').eq(4).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')
    
    cy.get('label[for="schoolNfyRevenueFileUpload"]').contains('Upload a file')

    cy.get('legend').eq(2).contains('Uploaded files')
    
    cy.get('hr').eq(2).should('be.visible')
    cy.get('.govuk-label').eq(16).contains('No file uploaded')
    cy.get('hr').eq(3).should('be.visible')
})



Cypress.Commands.add('uploadFileForCurrentCapitalCarryForwardDeficit', ():void => {
    const filepath = '../fixtures/fiftyk.pdf'
    cy.get('#schoolCfyCapitalFileUpload').attachFile(filepath)
})

Cypress.Commands.add('submitCurrentFinancialYr', ():void => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('selectNextFinancialYrStartSection', ():void => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(2).click()

})

Cypress.Commands.add('nextFinancialYrElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Finances (Step 3 of 6)')
    cy.get('.govuk-heading-l').contains('Next financial year')
    cy.get('legend').contains('End of next financial year')
    cy.get('#pfy-end-date-hint').contains('For example, 01 09 2022')

    cy.get('label[for="sip_nfyenddate-day"]').contains('Day')
    cy.get('#sip_nfyenddate-day').should('be.visible').should('be.enabled')

    cy.get('label[for="sip_nfyenddate-month"]').contains('Month')
    cy.get('#sip_nfyenddate-month').should('be.visible').should('be.enabled')

    cy.get('label[for="sip_nfyenddate-year"]').contains('Year')
    cy.get('#sip_nfyenddate-year').should('be.visible').should('be.enabled')

    cy.get('label[for="Revenue"]').contains('Forecasted revenue carry forward at end of the next financial year (31 March)')

    cy.get('#Revenue').should('be.visible').should('be.enabled')

    cy.get('#revenueTypeSurplus').should('not.be.checked')
    cy.get('label[for="revenueTypeSurplus"]').contains('Surplus')

    cy.get('#revenueTypeDeficit').should('not.be.checked')
    cy.get('label[for="revenueTypeDeficit"]').contains('Deficit')

    cy.get('label[for="CapitalCarryForward"]').contains('Forecasted capital carry forward at end of the next financial year (31 March)')

    cy.get('#CapitalCarryForward').should('be.visible').should('be.enabled')

    cy.get('#capitalRevenueTypeSurplus').should('not.be.checked')
    cy.get('label[for="capitalRevenueTypeSurplus"]').contains('Surplus')

    cy.get('#capitalRevenueTypeDeficit').should('not.be.checked')
    cy.get('label[for="capitalRevenueTypeDeficit"]').contains('Deficit')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue')
    
})

Cypress.Commands.add('inputNextFinancialYrDate', ():void => {
    cy.get('#sip_nfyenddate-day').type('31')
    cy.get('#sip_nfyenddate-month').type('03')
    cy.get('#sip_nfyenddate-year').type('2024')

})

Cypress.Commands.add('inputNextFinancialYrRevenueCarryForward', ():void => {
    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('199999.99')
    
})

Cypress.Commands.add('verifyNextRevenueCarryForwardDeficitSelectedSectionDisplays', ():void => {
    cy.get('#revenueTypeDeficit').should('be.checked')
    
    cy.get('label[for="NFYRevenueStatusExplained"]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(1).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#NFYRevenueStatusExplained').should('be.visible').should('be.enabled')

    cy.get('.govuk-label').eq(6).contains('You can upload the school’s recovery plan.')

    cy.get('.govuk-hint').eq(2).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')
    
    cy.get('label[for="schoolNfyRevenueFileUpload"]').contains('Upload a file')

    cy.get('legend').eq(1).contains('Uploaded files')
    
    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').eq(9).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')

})

Cypress.Commands.add('inputReasonsForNextRevenueCarryForwardDeficit', (reasonsRevenueCarryForwardDeficit:string):void => {
    reasonsRevenueCarryForwardDeficit = 'C) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('#NFYRevenueStatusExplained').type(reasonsRevenueCarryForwardDeficit)
})

Cypress.Commands.add('uploadFileForNextRevenueCarryForwardDeficit', ():void => {
    const filepath = '../fixtures/fiftyk.pdf'
    cy.get('#schoolNfyRevenueFileUpload').attachFile(filepath)
})

Cypress.Commands.add('inputNextFinancialYrCapitalCarryForward', ():void => {
    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('199998.98')
    
})

Cypress.Commands.add('inputReasonsForNextCapitalCarryForwardDeficit', ():void => {
    cy.get('#PFYCapitalCarryForwardExplained').type('D) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author')
})

Cypress.Commands.add('uploadFileForNextCapitalCarryForwardDeficit', ():void => {
    const filepath = '../fixtures/fiftyk.pdf'
    cy.get('#schoolNfyCapitalFileUpload').attachFile(filepath)
})

Cypress.Commands.add('submitNextFinancialYr', ():void => {
    cy.get('input[type="submit"]').click()
})



Cypress.Commands.add('selectLoansStartSection', ():void => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(3).click()

})

Cypress.Commands.add('loansSummaryElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Finances (Step 4 of 6)')
    cy.get('.govuk-heading-l').contains('Loans')

    cy.get('legend').contains('Are there any existing loans?')

    cy.get('#anyLoansOptionYes').should('not.be.checked')
    cy.get('label[for="anyLoansOptionYes"]').contains('Yes')

    cy.get('#anyLoansOptionNo').should('not.be.checked')
    cy.get('label[for="anyLoansOptionNo"]').contains('No')

    cy.get('input[type="submit"]').should('be.visible').contains('Continue')
})

Cypress.Commands.add('submitLoansSummary', ():void => {
    cy.get('input[type="submit"]').click()
})


Cypress.Commands.add('selectLeasesStartSection', ():void => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(4).click()

})

Cypress.Commands.add('leasesSummaryElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Finances (Step 5 of 6)')
    cy.get('.govuk-heading-l').contains('Leases')

    cy.get('legend').contains('Are there any existing leases?')

    cy.get('#anyLeasesOptionYes').should('not.be.checked')
    cy.get('label[for="anyLeasesOptionYes"]').contains('Yes')

    cy.get('#anyLeasesOptionNo').should('not.be.checked')
    cy.get('label[for="anyLeasesOptionNo"]').contains('No')

    cy.get('input[type="submit"]').should('be.visible').contains('Continue')
})

Cypress.Commands.add('submitLeasesSummary', ():void => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('selectFinancialInvestigationsStartSection', ():void => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(5).click()

})

Cypress.Commands.add('financialInvestigationsElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Finances (Step 6 of 6)')
    cy.get('.govuk-heading-l').contains('Financial investigations')

    cy.get('legend').contains('Are there any financial investigations ongoing at the school?')

    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for="selectoptionYes"]').contains('Yes')

    cy.get('#selectoptionNo').should('not.be.checked')
    cy.get('label[for="selectoptionNo"]').contains('No')

    cy.get('input[type="submit"]')


})

Cypress.Commands.add('submitFinancialInvestigations', ():void => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('financeSummaryCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Finances')

    cy.get('.govuk-heading-m').eq(0).contains('Previous financial year')
    cy.get('.govuk-link').eq(1).contains('Change your answers')
    
    cy.get('b').eq(0).contains('End of previous financial year end date?')
    cy.get('.govuk-body').eq(1).contains('31/03/2022')

    cy.get('b').eq(1).contains('Revenue carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(3).contains('4999.99')

    cy.get('b').eq(2).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(5).contains('Surplus')

    cy.get('b').eq(3).contains('Capital carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(7).contains('4998.98')

    cy.get('b').eq(4).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(9).contains('Surplus')

    cy.get('.govuk-heading-m').eq(1).contains('Current financial year')
    cy.get('.govuk-link').eq(2).contains('Change your answers')

    cy.get('b').eq(5).contains('End of current financial year end date?')
    cy.get('.govuk-body').eq(11).contains('31/03/2023')

    cy.get('b').eq(6).contains('Revenue carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(13).contains('99999.99')
    cy.get('hr').eq(8).should('be.visible')

    cy.get('b').eq(7).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(15).contains('Deficit')

    cy.get('b').contains('Capital carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(17).contains('A) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author')

    cy.get('b').contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(21).contains('Deficit')

    cy.get('.govuk-heading-m').eq(2).contains('Next financial year')
    cy.get('.govuk-link').eq(3).contains('Change your answers')

    cy.get('b').contains('End of next financial year end date?')
    cy.get('.govuk-body').eq(25).contains('31/03/2024')

    cy.get('b').contains('Revenue carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(27).contains('199999.99')

    cy.get('b').contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(29).contains('Deficit')

    cy.get('b').contains('Capital carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(33).contains('199998.98')

    cy.get('b').contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(35).contains('Deficit')

    cy.get('.govuk-heading-m').eq(3).contains('Loans')
    cy.get('.govuk-link').eq(4).contains('Change your answers')

    cy.get('b').contains('Are there any existing loans?')
    cy.get('.govuk-body').eq(39).contains('No')

    cy.get('.govuk-heading-m').eq(4).contains('Leases')
    cy.get('.govuk-link').eq(5).contains('Change your answers')

    cy.get('b').contains('Are there any existing leases?')
    cy.get('.govuk-body').eq(41).contains('No')

    cy.get('.govuk-heading-m').eq(5).contains('Financial investigations')
    cy.get('.govuk-link').eq(6).contains('Change your answers')

    cy.get('b').contains('Finance ongoing investigations?')
    cy.get('.govuk-body').eq(43).contains('No')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})

Cypress.Commands.add('submitFinanceSummary', ():void => {
    cy.get('.govuk-button').click()
})


Cypress.Commands.add('financeCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('selectFuturePupilNumbers', ():void => {
    cy.contains('Future pupil numbers').click()
})

Cypress.Commands.add('futurePupilNumbersSummaryElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Future pupil numbers')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('Projected pupil numbers on roll in the year the academy opens (year 1)')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('b').eq(1).contains('Projected pupil numbers on roll in the following year after the academy has opened (year 2)')
    cy.get('p').eq(4).contains('You have not added any information')

    cy.get('b').eq(2).contains('Projected pupil numbers on roll in the following year (year 3)')
    cy.get('p').eq(6).contains('You have not added any information')

    cy.get('b').eq(3).contains('What do you base these projected numbers on?')
    cy.get('p').eq(8).contains('You have not added any information')

    cy.get('b').eq(4).contains('What is the school\'s published admissions number (PAN)?')
    cy.get('p').eq(10).contains('You have not added any information')


    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application')
})

Cypress.Commands.add('selectFuturePupilNumbersStartSection', ():void => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()
})

Cypress.Commands.add('futurePupilNumbersDetailsElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Future pupil numbers')

    cy.get('label[for="ProjectedPupilNumbersYear1"]').contains('Projected pupil numbers on roll in the year the academy opens (year 1)')
    cy.get('#ProjectedPupilNumbersYear1').should('be.enabled')

    cy.get('label[for="ProjectedPupilNumbersYear2"]').contains('Projected pupil numbers on roll in the following year after the academy has opened (year 2)')
    cy.get('#ProjectedPupilNumbersYear2').should('be.enabled')

    cy.get('label[for="ProjectedPupilNumbersYear3"]').contains('Projected pupil numbers on roll in the following year (year 3)')
    cy.get('#ProjectedPupilNumbersYear3').should('be.enabled')

    cy.get('label[for="SchoolCapacityAssumptions"]').contains('What do you base these projected numbers on?')
    cy.get('#SchoolCapacityAssumptions').should('be.enabled')

    cy.get('label[for="SchoolCapacityPublishedAdmissionsNumber"]').contains('What is the school\'s published admissions number (PAN)?')
    cy.get('#SchoolCapacityPublishedAdmissionsNumber').should('be.enabled')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')


})

Cypress.Commands.add('fillFuturePupilNumbersDetails', ():void => {
    cy.get('#ProjectedPupilNumbersYear1').type('999')
    cy.get('#ProjectedPupilNumbersYear2').type('1499')
    cy.get('#ProjectedPupilNumbersYear3').type('1999')
    cy.get('#SchoolCapacityAssumptions').type('School Capacity Assumptions')
    cy.get('#SchoolCapacityPublishedAdmissionsNumber').type('999')

})

Cypress.Commands.add('submitFuturePupilNumbersDetails', ():void => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('futurePupilNumbersSummaryCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Future pupil numbers')

    cy.get('.govuk-link').should('be.visible').contains('Change your answers')

    cy.get('b').eq(0).contains('Projected pupil numbers on roll in the year the academy opens (year 1)')
    cy.get('p').eq(2).contains('999')

    cy.get('b').eq(1).contains('Projected pupil numbers on roll in the following year after the academy has opened (year 2)')
    cy.get('p').eq(4).contains('1499')

    cy.get('b').eq(2).contains('Projected pupil numbers on roll in the following year (year 3)')
    cy.get('p').eq(6).contains('1999')

    cy.get('b').eq(3).contains('What do you base these projected numbers on?')
    cy.get('p').eq(8).contains('School Capacity Assumptions')

    cy.get('b').eq(4).contains('What is the school\'s published admissions number (PAN)?')
    cy.get('p').eq(10).contains('999')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application')
})

Cypress.Commands.add('submitFuturePupilNumbersSummary', ():void => {
    cy.get('.govuk-button').click()
})

Cypress.Commands.add('futurePupilNumbersCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('selectLandAndBuildings', ():void => {
    cy.contains('Land and buildings').click()
})

Cypress.Commands.add('landAndBuildingsSummaryElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Land and buildings')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('b').eq(1).contains('Are there any current or planned building works?')
    cy.get('p').eq(4).contains('You have not added any information')

    cy.get('b').eq(2).contains('Are there any shared facilities on site?')
    cy.get('p').eq(6).contains('You have not added any information')

    cy.get('b').eq(3).contains('Has the school had any grants from Sport England, the Big Lottery Fund, or the Football Federation?')
    cy.get('p').eq(8).contains('You have not added any information')

    cy.get('b').eq(4).contains('Is the school part of a Private Finance Initiative (PFI) scheme?')
    cy.get('p').eq(10).contains('You have not added any information')

    cy.get('b').eq(5).contains('Is the school part of the Priority School Building Programme?')
    cy.get('p').eq(12).contains('You have not added any information')

    cy.get('b').eq(6).contains('Is the school part of the Building Schools for the Future Programme?')
    cy.get('p').eq(14).contains('You have not added any information')

})

Cypress.Commands.add('selectLandAndBuildingsStartSection', ():void => {
    cy.contains('Start section').click()
})

Cypress.Commands.add('landAndBuildingsDetailsElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Land and buildings')

    cy.get('label[for="SchoolBuildLandOwnerExplained"]').contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')
    cy.get('#SchoolBuildLandOwnerExplained').should('be.enabled')

    cy.get('legend').eq(0).contains('Are there any current or planned building works?')
   
    cy.get('#buildingWorksOptionYes').should('not.be.checked')
    cy.get('label[for="buildingWorksOptionYes"]').contains('Yes')

    cy.get('#buildingWorksOptionNo').should('not.be.checked')
    cy.get('label[for="buildingWorksOptionNo"]').contains('No')

    cy.get('legend').eq(1).contains('Are there any shared facilities on site?')

    cy.get('span[class="govuk-hint govuk-body govuk-!-margin-bottom-5"]').contains('For example, a nursery, children’s centre, swimming pool, leisure centre, caretaker’s house, community library or SEN unit')

    cy.get('#sharedFacilitiesOptionYes').should('not.be.checked')
    cy.get('label[for="sharedFacilitiesOptionYes"]').contains('Yes')

    cy.get('#sharedFacilitiesOptionNo').should('not.be.checked')
    cy.get('label[for="sharedFacilitiesOptionNo"]').contains('No')

    cy.get('legend').eq(2).contains('Has the school had any grants from Sport England, the Big Lottery Fund, or the Football Federation?')

    cy.get('#landGrantsOptionYes').should('not.be.checked')
    cy.get('label[for="landGrantsOptionYes"]').contains('Yes')

    cy.get('#landGrantsOptionNo').should('not.be.checked')
    cy.get('label[for="landGrantsOptionNo"]').contains('No')

    cy.get('legend').eq(3).contains('Is the school part of a Private Finance Initiative (PFI) scheme?')

    cy.get('#pfiSchemeOptionYes').should('not.be.checked')
    cy.get('label[for="pfiSchemeOptionYes"]').contains('Yes')

    cy.get('#pfiSchemeOptionNo').should('not.be.checked')
    cy.get('label[for="pfiSchemeOptionNo"]').contains('No')

    cy.get('legend').eq(4).contains('Is the school part of the Priority School Building Programme?')

    cy.get('#SchoolBuildLandPriorityBuildingProgrammeYes').should('not.be.checked')
    cy.get('label[for="SchoolBuildLandPriorityBuildingProgrammeYes"]').contains('Yes')

    cy.get('#SchoolBuildLandPriorityBuildingProgrammeNo').should('not.be.checked')
    cy.get('label[for="SchoolBuildLandPriorityBuildingProgrammeNo"]').contains('No')

    cy.get('legend').eq(5).contains('Is the school part of the Building Schools for the Future Programme?')

    cy.get('#SchoolBuildLandFutureProgrammeYes').should('not.be.checked')
    cy.get('label[for="SchoolBuildLandFutureProgrammeYes"]').contains('Yes')

    cy.get('#SchoolBuildLandFutureProgrammeNo').should('not.be.checked')
    cy.get('label[for="SchoolBuildLandFutureProgrammeNo"]').contains('No')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')

})

Cypress.Commands.add('fillLandAndBuildingsDetails', (landOwnerExplained:string):void => {
landOwnerExplained = 'As far as you\'re aware, who owns or holds the school\'s buildings and land?'
    cy.get('#SchoolBuildLandOwnerExplained').type(landOwnerExplained)
})

Cypress.Commands.add('submitLandAndBuildingsDetails', ():void => {
    cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('landAndBuildingsSummaryCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Land and buildings')

    cy.get('.govuk-link').contains('Change your answers')


    cy.get('b').eq(0).contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')
    cy.get('p').eq(2).contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')

    cy.get('b').eq(1).contains('Are there any current or planned building works?')
    cy.get('p').eq(4).contains('No')

    cy.get('b').eq(2).contains('Are there any shared facilities on site?')
    cy.get('p').eq(6).contains('No')

    cy.get('b').eq(3).contains('Has the school had any grants from Sport England, the Big Lottery Fund, or the Football Federation?')
    cy.get('p').eq(8).contains('No')

    cy.get('b').eq(4).contains('Is the school part of a Private Finance Initiative (PFI) scheme?')
    cy.get('p').eq(10).contains('No')

    cy.get('b').eq(5).contains('Is the school part of the Priority School Building Programme?')
    cy.get('p').eq(12).contains('No')

    cy.get('b').eq(6).contains('Is the school part of the Building Schools for the Future Programme?')
    cy.get('p').eq(14).contains('No')
})

Cypress.Commands.add('submitLandAndBuildingsSummary', ():void => {
cy.get('.govuk-button').click()
})

Cypress.Commands.add('landAndBuildingsCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('selectConsultation', ():void => {
    cy.contains('Consultation').click()
})

Cypress.Commands.add('consultationSummaryElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Consultation')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('Has the governing body consulted the relevant stakeholders?')
    cy.get('p').eq(2).contains('You have not added any information')


    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})

Cypress.Commands.add('selectConsultationStartSection', ():void => {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()
})

Cypress.Commands.add('consultationDetailsElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Consultation')

    cy.get('.govuk-body').eq(0).contains('Schools must consult any stakeholders relevant to the conversion')

    cy.get('#role-hint').contains('Has the governing body consulted the relevant stakeholders?')

    cy.get('#consultationStakeholdersOptionYes').should('not.be.checked')
    cy.get('label[for="consultationStakeholdersOptionYes"]').contains('Yes')

    cy.get('#consultationStakeholdersOptionNo').should('not.be.checked')
    cy.get('label[for="consultationStakeholdersOptionNo"]').contains('No')

    cy.get('label[for="SchoolConsultationStakeholdersConsult"]').contains('When does the governing body plan to consult?')
    cy.get('#SchoolConsultationStakeholdersConsult').should('be.enabled')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')


})

Cypress.Commands.add('fillConsultationDetails', (consultationDetails:string):void => {
    consultationDetails = 'When does the governing body plan to consult?'
    cy.get('#SchoolConsultationStakeholdersConsult').type(consultationDetails)
})

Cypress.Commands.add('submitConsultationDetails', ():void => {
cy.get('input[type="submit"]').click()
})

Cypress.Commands.add('consultationSummaryCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Consultation')

    cy.get('.govuk-link').contains('Change your answers')

    cy.get('b').eq(0).contains('Has the governing body consulted the relevant stakeholders?')
    cy.get('p').eq(2).contains('No')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')
})

Cypress.Commands.add('submitConsultationSummary', ():void => {
    cy.get('.govuk-button').click()
})

Cypress.Commands.add('consultationCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Not Started')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('selectPreopeningSupportGrant', ():void => {
    cy.contains('Pre-opening support grant').click()
})

Cypress.Commands.add('preopeningSupportGrantSummaryElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Pre-opening support grant')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

   // cy.get('hr').eq(0).should('be.visible')

    cy.get('b').eq(0).contains('Do you want these funds paid to the school or the trust the school is joining?')
    cy.get('p').eq(2).contains('You have not added any information')

  //  cy.get('hr').eq(1).should('be.visible')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})



Cypress.Commands.add('preopeningSupportGrantDetailsElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-heading-l').contains('Academy pre-opening support grant')

    cy.get('.govuk-body').eq(0).contains('If your application is successful you will be issued with an academy order. Once issued, the Department for Education will pay the pre-opening support grant into a nominated bank account.')


    cy.get('legend').contains('Do you want these funds paid to the school or the trust the school is joining?')

    cy.get('#pay-toSchool').should('not.be.checked')
    cy.get('label[for="pay-toSchool"]').contains('To the school')

    cy.get('#pay-toTrust').should('not.be.checked')
    cy.get('label[for="pay-toTrust"]').contains('To the trust')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return to overview')

})

Cypress.Commands.add('verifyToTheSchoolPreopeningSupportGrantDetailsSectionDisplays', ():void => {
    cy.get('#pay-toSchool').should('be.checked')
    cy.get('.govuk-body').eq(1).should('be.visible').contains('Go to provide information about your banking payments to DfE to add the school’s bank details.')
    cy.get('#funds-paid-to-school-hint').should('be.visible').contains('Your application can be submitted without completing this action now, however please provide your bank details either before or shortly after submission of your application.')
})

Cypress.Commands.add('preopeningSupportGrantSummaryCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Pre-opening support grant')

    cy.get('.govuk-link').contains('Change your answers')

  //  cy.get('hr').eq(0).should('be.visible')

    cy.get('b').eq(0).contains('Do you want these funds paid to the school or the trust the school is joining?')
    cy.get('p').eq(2).contains('To the school')

  //  cy.get('hr').eq(1).should('be.visible')

    cy.get('.govuk-button').should('be.visible').contains('Back')

})

Cypress.Commands.add('preopeningSupportGrantCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('.govuk-body.govuk-radios__conditional').contains('Your answers will be saved after each question. Once all sections are complete, you will be able to submit the application.')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Not Started')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

})

Cypress.Commands.add('declarationSummaryElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Declaration')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('I agree with all of these statements, and believe that the facts stated in this application are true')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})

Cypress.Commands.add('declarationElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Declaration')

    cy.get('.govuk-inset-text').contains('This section must be completed by the chair of the school\'s governing body. You can invite the chair to contribute if this is not you.')

    cy.get('p').eq(2).contains('As the chair of the governing body of the applying school, I confirm that the governing body agrees with these statements:')

    cy.get('li').eq(0).contains('The governing body has passed a resolution that the school should become an academy.')
    cy.get('li').eq(1).contains('The school will complete a consultation with relevant stakeholders (such as parents, staff, the local communities and others), and consider their equality needs before they sign the funding agreement.')
    cy.get('li').eq(2).contains('The school agrees to the terms set out in the academy pre-opening support grant certificate.')
    cy.get('li').eq(3).contains('The school agrees to provide any further information that the Department for Education needs to assess this application.')
    cy.get('li').eq(4).contains('That if any information in this application is false or misleading, this application may be rejected or the academy order may be revoked if it has already been awarded.')

    cy.get('.govuk-fieldset__heading').contains('I confirm that:')

    cy.get('#SchoolDeclarationTeacherChair').should('not.be.checked')
    cy.get('label[for="SchoolDeclarationTeacherChair"]').contains('I am the chair of governors of the applying school')

    cy.get('#SchoolDeclarationBodyAgree').should('not.be.checked')
    cy.get('label[for="SchoolDeclarationBodyAgree"]').contains('The information in this application is true to the best of my knowledge')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and return')
})

Cypress.Commands.add('selectAgreements', ():void => {
    cy.get('#SchoolDeclarationTeacherChair').click()
    cy.get('#SchoolDeclarationBodyAgree').click()
})

Cypress.Commands.add('verifyAgreementsSelected', ():void => {
    cy.get('#SchoolDeclarationTeacherChair').should('be.checked')
    cy.get('#SchoolDeclarationBodyAgree').should('be.checked')
})

Cypress.Commands.add('declarationSummaryCompleteElementsVisible', ():void => {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Declaration')

    cy.get('.govuk-link').contains('Change your answers')

    cy.get('b').eq(0).contains('I agree with all of these statements, and believe that the facts stated in this application are true')
    cy.get('p').eq(2).contains('Yes')

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

})

Cypress.Commands.add('declarationCompleteElementsVisible', ():void => {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('p').contains('Application reference:')
    cy.get('.govuk-heading-l').contains('Join a multi-academy trust')
    cy.get('h2').contains('The school applying to convert')
    cy.get('p').eq(3).contains('Plymstock School')
    cy.get(`a[href="/school/application-select-school?appId=${globalApplicationId}"]`).contains('Change')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('About the conversion')
    cy.get('.govuk-grid-column-one-third').eq(0).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Further information')
    cy.get('.govuk-grid-column-one-third').eq(1).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Finances')
    cy.get('.govuk-grid-column-one-third').eq(2).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Future pupil numbers')
    cy.get('.govuk-grid-column-one-third').eq(3).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Land and buildings')
    cy.get('.govuk-grid-column-one-third').eq(4).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Consultation')
    cy.get('.govuk-grid-column-one-third').eq(5).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Pre-opening support grant')
    cy.get('.govuk-grid-column-one-third').eq(6).contains('Completed')
    cy.get('div[class="govuk-grid-row"]').eq(2).contains('Declaration')
    cy.get('.govuk-grid-column-one-third').eq(7).contains('Completed')

    cy.get('h2').eq(1).contains('The trust the school will join')
    //cy.get('.govuk-button.govuk-button--secondary').should('be.visible').contains('Add a trust')
    cy.get('h3').contains('PLYMOUTH CAST')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${globalApplicationId}"]`).contains('Change')
    cy.get(`a[href="/trust/join-amat/application-school-join-amat-trust-summary?appId=${globalApplicationId}"]`).contains('Trust details')
    cy.get('strong[class="govuk-tag app-task-list__tag"]').contains('Completed')

    cy.get('h2[class="govuk-heading-l"]').contains('Contributors')
    cy.get('p').eq(4).contains('You can invite or remove contributors to this form. If you are not the chair of the school\'s governing body, you must add them so that they can submit this application.')

    // CHECK FOR SUBMIT APPLICATION BUTTON
    cy.contains('Submit application').should('be.visible').contains('Submit application')

})

Cypress.Commands.add('applicationSubmittedSuccessfullyElementsVisible', ():void => {
    cy.get('.govuk-panel__title').contains('Your application has been submitted')

    cy.get('.govuk-panel__body').contains('Your reference number is')

    cy.get('strong').contains(`A2B_${globalApplicationId}`)

    cy.get('.govuk-heading-m').eq(0).contains('Completed application')

    cy.get('.govuk-heading-m').eq(1).contains('What happens next')

    cy.get('.govuk-body-m').eq(0).contains('It takes us 2 to 6 weeks to assess your application and grant your academy order, if you\'re successful.')

    cy.get('.govuk-body-m').eq(1).contains('Your project lead will contact you if they need to check anything.')

    cy.get('.govuk-body-m').eq(2).contains('These are the main steps in the conversion process:')

    cy.get('.sip-application-status--row-content').eq(0).contains('Application submission')

    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(1).contains('Regional schools commissioner makes a decision with advice from the Headteacher Board')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(2).contains('Academy order is issued')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(3).contains('School’s solicitor submits a land questionnaire, including site plan')

    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(4).contains('School’s solicitor submits draft funding agreement (and memorandum and articles of association for new trusts)')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(5).contains('School’s solicitor confirms that the commercial transfer agreement (CTA) and land arrangements are agreed')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(6).contains('School signs and submits the funding agreement')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(7).contains('School’s solicitor confirms TUPE and stakeholder consultation are complete')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(8).contains('School submits the academy bank details to ESFA')

    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(9).contains('Academy Opens')

    cy.get('p').eq(4).contains('If have any queries about the progress of your application, please contact the Department for Education.')

    cy.get('p').eq(5).contains('If you have any questions or comments about this service, e-mail regionalservices.rg@education.gov.uk')

    cy.get('p').eq(6).contains('As we continue to develop this service we would value your feedback on your experience. Please complete our short survey. The survey takes around 10 minutes to complete')

    cy.get('h2').eq(1).contains('If your application is successful')

    cy.contains('Prepare for conversion').should('be.visible')

    cy.get('hr').should('be.visible')
})



