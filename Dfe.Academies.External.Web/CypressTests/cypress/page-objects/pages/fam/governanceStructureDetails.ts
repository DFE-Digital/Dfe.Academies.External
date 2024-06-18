class GovernanceStructureDetails {
  public uploadFileAndSubmit(): this {
    const filepath = '../fixtures/fifty-k.docx'
    cy.get('#governanceDetailsFileUpload').attachFile(filepath)
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue').click()

    return this
  }
}

const governanceStructureDetails = new GovernanceStructureDetails()

export default governanceStructureDetails
