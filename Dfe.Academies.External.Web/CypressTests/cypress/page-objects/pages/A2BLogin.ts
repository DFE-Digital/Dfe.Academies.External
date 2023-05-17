import BasePage from "../BasePage"
export default class A2BLogin extends BasePage {
    
    static loginWithWrongUsername(username, password)
    {
        cy.loginWithWrongUsername(username, password)
    }

    static loginWithNoPassword(username, password)
    {
        cy.loginWithNoPassword(username, password)
    }

    static loginWithNoUsername(username, password)
    {
        cy.loginWithNoUsername(username, password)
    }

    static loginWithNoUsernameOrPassword(username, password)
    {
        cy.loginWithNoPassword(username, password)
        cy.loginWithNoUsername(username, password)
    }
  

    static loginErrorVisibleWithNoPassword() {
        cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
        cy.get('span[id="validation-password"]').contains('Please enter your password')
    })
    }

    static loginErrorVisibleWithNoUsername() {
        cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
        cy.get('span[id="validation-username"]').contains('Please enter your email')
    })

    }

    static loginWithWrongPassword(username, password)
    {
        cy.loginWithWrongPassword(username, password)
    }

    static loginErrorVisibleWithWrongPassword() {
        cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
        cy.get('a[href="#username"]').contains('Sorry, we did not recognise your sign-in details, please try again.')
    })
    }

    static loginErrorVisibleWithWrongUsername() {
        cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
        cy.get('a[href="#username"]').contains('Sorry, we did not recognise your sign-in details, please try again.')
    })
    }

    static sqlInjectionAndInvalidUsername(username, password)
    {
        cy.sqlInjectionAndInvalidUsername(username, password)
    }


    static loginErrorVisibleWithSqlInjectionAttemptAndInvalidUsername() {
        cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
            cy.get('span[class="govuk-error-message"]').contains('Please enter a valid email address')
        })
    }

    static sqlInjectionAndInvalidUsername(username, password)
    {
        cy.sqlInjectionAndInvalidUsername(username, password)
    }


    static loginErrorVisibleWithSqlInjectionAttemptAndInvalidUsername() {
        cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
            cy.get('span[class="govuk-error-message"]').contains('Please enter a valid email address')
        })
    }

    static crossSiteScriptAndInvalidUsername(username, password)
    {
        cy.crossSiteScriptAndInvalidUsername(username, password)
    }


    static loginErrorVisibleWithCrossSiteScriptAndInvalidUsername() {
        cy.origin('https://test-interactions.signin.education.gov.uk//7fbd2f4e-8296-4211-a7e4-a38df63d3ff5/usernamepassword', () => {
            cy.get('span[class="govuk-error-message"]').contains('Please enter a valid email address')
        })
    }
    
    static login(username, password)
    {
        cy.login(username, password)
        cy.wait(7000)
    }

    static loginToUnauthApplication(username, password)
    {
        cy.loginToUnauthApplication(username, password)
    }

    static createAccount()
    {
        cy.createAccount()
    }

    static forgotPassword()
    {
        cy.forgotPassword()
    }


}