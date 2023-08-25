import BasePage from "../BasePage"
export default class A2BFAMTrustOpeningDateSummary extends BasePage {
    static selectStartSection()
    {
        cy.contains('Start section').click()
    }

    static FAMOpeningDateSummaryCompleteElementsVisibleAndSubmit()
    {
        cy.FAMOpeningDateSummaryCompleteElementsVisibleAndSubmit()
           
    }
}