import BasePage from "../BasePage"
export default class A2BForgottenYourPasswordVerifyCode extends BasePage {

    static forgotPasswordVerifyCodeElementsVisible() {
        cy.forgotPasswordVerifyCodeElementsVisible()
    }

}