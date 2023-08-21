import BasePage from "../BasePage"

export default class A2BInviteContributor extends BasePage {
    static fillDetailsAndSubmit()
    {
        cy.fillDetailsAndSubmit()
    }

    static verifySuccessBannerAndContributorList()
    {
        cy.verifySuccessBannerAndContributorList()
    }

    static selectRemoveContributorLink()
    {
        cy.contains('Remove contributor').click()
    }

    static verifyContributorRemovedAndSuccessRemoved()
    {
        cy.verifyContributorRemovedAndSuccessRemoved()
    }
}


