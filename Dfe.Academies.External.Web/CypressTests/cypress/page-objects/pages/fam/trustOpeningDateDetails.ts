class TrustOpeningDateDetails {
  public selectDayAndInput(): this {
    cy.get('#sip_formtrustopeningdate-day').click()
    cy.get('#sip_formtrustopeningdate-day').type('1')

    return this
  }

  public selectMonthAndInput(): this {
    cy.get('#sip_formtrustopeningdate-month').click()
    cy.get('#sip_formtrustopeningdate-month').type('9')

    return this
  }

  public selectYearAndInput(trustOpeningDateYear: string): this {
    cy.get('#sip_formtrustopeningdate-year').click()
    cy.get('#sip_formtrustopeningdate-year').type(trustOpeningDateYear)

    return this
  }

  public FAMTrustOpeningDateInputApproverDetailsAndSubmit(approverName: string, approverEmail: string): this {
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
