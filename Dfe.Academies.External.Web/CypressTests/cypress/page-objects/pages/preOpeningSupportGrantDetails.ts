class PreopeningSupportGrantDetails {
  public enterPreopeningSupportGrantDetails(): this {
    cy.get('#pay-toSchool').click()
    cy.get('#pay-toSchool').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }

  // FAM-Specific
  public confirmPreopeningSupportGrantDetails(): this {
    cy.get('#ConfirmSchoolPay').click()
    cy.get('#ConfirmSchoolPay').should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }

}

const preopeningSupportGrantDetails = new PreopeningSupportGrantDetails()

export default preopeningSupportGrantDetails
