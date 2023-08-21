import BasePage from "../BasePage"
export default class A2BConfirmInviteContributorDelete extends BasePage {

    static confirmRemoveContributor()
    {
        cy.get('.govuk-button').eq(0).click()
    }

}