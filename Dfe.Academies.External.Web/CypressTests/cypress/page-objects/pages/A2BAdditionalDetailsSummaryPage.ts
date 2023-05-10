import BasePage from "../BasePage"
export default class A2BAdditionalDetailsSummaryPage extends BasePage {
    static additionalDetailsSummaryNotStartedElementsVisible()
    {
        cy.additionalDetailsSummaryNotStartedElementsVisible()
    }

    static selectAdditionalDetailsStartSection()
    {
        cy.selectAdditionalDetailsStartSection()
    }

    static additionalDetailsSummaryCompleteElementsVisible()
    {
        cy.additionalDetailsSummaryCompleteElementsVisible()
    }

    static submitAdditionalDetailsSummary()
    {
        cy.submitAdditionalDetailsSummary()
    }

}