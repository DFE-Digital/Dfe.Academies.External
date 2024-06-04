import BasePage from ../basePage
export default class A2BForgottenYourPasswordVerifyCode extends BasePage {

    static forgotPasswordVerifyCodeElementsVisible() {
        cy.forgotPasswordVerifyCodeElementsVisible()
    }

}