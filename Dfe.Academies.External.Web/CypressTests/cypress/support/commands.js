import DataGenerator from "../fixtures/data-generator"

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
    cy.get('#password').type(password)
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
    cy.get('#password').type(password)
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
    cy.get('#password').type(password)
    cy.contains('Sign in').click()
})
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
    cy.get('.govuk-grid-column-full').contains('Enter your email address')
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