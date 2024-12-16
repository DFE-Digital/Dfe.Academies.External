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

  public warningIcon(): this {
    cy.get('span[class="govuk-warning-text__icon"]')

    return this
  }

  public warningText(): this {
    cy.get('strong[class="govuk-warning-text__text"]')

    return this
  }
}

const preopeningSupportGrantDetails = new PreopeningSupportGrantDetails()

export default preopeningSupportGrantDetails
