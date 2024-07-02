class TrustOpeningDateDetails {
  public enterOpeningDateDetails(trustOpeningYear: string, approverName: string, approverEmail: string): this {
    cy.get('#sip_formtrustopeningdate-day').click()
    cy.get('#sip_formtrustopeningdate-day').type('1')

    cy.get('#sip_formtrustopeningdate-month').click()
    cy.get('#sip_formtrustopeningdate-month').type('9')

    cy.get('#sip_formtrustopeningdate-year').click()
    cy.get('#sip_formtrustopeningdate-year').type(trustOpeningYear)

    if (approverName.includes('.')) {
      approverName = approverName.replace('.', '')
    }
    cy.get('#TrustApproverName').click()
    cy.get('#TrustApproverName').type(approverName)

    cy.get('#TrustApproverEmail').click()
    cy.get('#TrustApproverEmail').type(approverEmail)

    cy.get('input[type=submit]').should('be.visible').contains('Save and continue').click()

    return this
  }
}

const trustOpeningDateDetails = new TrustOpeningDateDetails()

export default trustOpeningDateDetails
