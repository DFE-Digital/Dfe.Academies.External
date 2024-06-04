import BasePage from ../basePage
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