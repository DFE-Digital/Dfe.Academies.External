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
