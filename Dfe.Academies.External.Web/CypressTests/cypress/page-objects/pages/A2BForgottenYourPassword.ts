import BasePage from ../basePage
export default class A2BForgottenYourPassword extends BasePage {

    static forgotPasswordElementsVisible() {
        cy.forgotPasswordElementsVisible()
    }

    static forgotPasswordEmptyEmailSubmitted() {
        cy.forgotPasswordEmptyEmailSubmitted()
    }

    static forgotPasswordInvalidEmailSubmitted() {
        cy.forgotPasswordInvalidEmailSubmitted()
    }

    static forgotPasswordA2BUserEmailSubmitted(username) {
        cy.forgotPasswordA2BUserEmailSubmitted(username)
    }

}