import BasePage from ../basePage
export default class A2BFAMKeyPeopleSummary extends BasePage {
    static selectAddKeyPerson()
    {
        cy.contains('Add a key person').click()
    }

    static FAMKeyPeopleSummaryCompleteElementsVisibleAndSubmit()
    {
        cy.FAMKeyPeopleSummaryCompleteElementsVisibleAndSubmit()
    }


}