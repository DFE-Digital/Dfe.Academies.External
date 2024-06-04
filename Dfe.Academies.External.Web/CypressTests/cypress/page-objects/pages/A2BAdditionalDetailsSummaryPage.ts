import BasePage from ../basePage
export default class A2BAdditionalDetailsSummaryPage extends BasePage {
    static additionalDetailsSummaryNotStartedElementsVisible()
    {
        cy.additionalDetailsSummaryNotStartedElementsVisible()
    }

    static selectAdditionalDetailsStartSection()
    {
        cy.get('a[class=govuk-button govuk-button--secondary]').click()
    }

    static additionalDetailsSummaryCompleteElementsVisible()
    {
        cy.additionalDetailsSummaryCompleteElementsVisible()
    }

    static submitAdditionalDetailsSummary()
    {
        cy.get('.govuk-button').click()
    }

}