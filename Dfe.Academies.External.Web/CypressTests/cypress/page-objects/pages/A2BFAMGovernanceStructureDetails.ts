import BasePage from "../BasePage"
export default class A2BFAMGovernanceStructureDetails extends BasePage {
   
    static uploadFileAndSubmit()
    {
        const filepath = '../fixtures/fifty-k.docx'
        cy.get('#governanceDetailsFileUpload').attachFile(filepath)
        cy.get('input[type="submit"]').should('be.visible').contains('Save and continue').click()
    }
}