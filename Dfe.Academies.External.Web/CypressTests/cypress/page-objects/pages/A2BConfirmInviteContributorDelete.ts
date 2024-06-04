import BasePage from ../basePage
export default class A2BConfirmInviteContributorDelete extends BasePage {

    static confirmRemoveContributor()
    {
        cy.get('.govuk-button').eq(0).click()
    }

}